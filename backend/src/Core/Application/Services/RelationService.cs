using System.Security.Cryptography.X509Certificates;
using Application.Abstractions;
using Application.Exceptions;
using Application.Interfaces;
using Application.Repositories;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Application.Services
{
    public class RelationService(ILogger<RelationService> logger, IGenericRepository<User, Guid> UserRepo, IGenericRepository<UserRelation, Guid> UserRelationRepo, IGenericRepository<Profile, Guid> ProfileRepo, IUserRelationRepository userRelationRepository, IUnitOfWork unitOfWork) : IRelationService
    {
        public async Task ApproveFriendRequest(Guid ReciverId, Guid SenderId, CancellationToken cancellationToken)
        {

            try
            {
                var receiverUser = await UserRepo.GetById(ReciverId)
                    ?? throw new NotFoundException("User.NotFound", ReciverId);

                var senderUser = await UserRepo.GetById(SenderId)
                    ?? throw new NotFoundException("User.NotFound", SenderId);

                await unitOfWork.BeginTransactionAsync(cancellationToken);

                var userRelationFriend = await userRelationRepository
                    .GetByFromIdAndToIdAndRelationForUpdate(SenderId, ReciverId, RelationType.Friend)
                    ?? throw new NotFoundException("User.Friend.NotFound", SenderId, ReciverId);

                if (userRelationFriend.Status != StatusType.Pending)
                    throw new ConflictException("User.Friend.Accept.Conflict", ReciverId, SenderId);

                userRelationFriend.Status = StatusType.Active;

                await unitOfWork.SaveChangesAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (LocalizedHttpException ex)
            {
                logger.LogError(ex, "rolling back dueto flow exception");
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw; // rethrow to propagate
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Unexpected error while accepting friend request. SenderId={SenderId}, ReceiverId={ReciverId}",
                    SenderId, ReciverId);

                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw new ApplicationException("An unexpected error occurred while accepting the friend request.", ex);
            }

        }

        public async Task SendFriendRequest(Guid FromId, Guid ToId, CancellationToken cancellationToken)
        {
            var FromUser = await UserRepo.GetById(FromId) ?? throw new NotFoundException("User.NotFound", FromId);
            var ToUser = await UserRepo.GetById(ToId) ?? throw new NotFoundException("User.NotFound", ToId);
            var ToUserProfile = await ProfileRepo.GetById(ToId) ?? throw new NotFoundException("User.Profile.NotFound", ToId);
            var userRelationStatus = await GetStrangerRelationStatus(FromUser, ToUser);
            await CheckIfUserIsEligibleToFriend(ToUserProfile, userRelationStatus, FromId, ToId);
            await CreateFriendRequest(ToUserProfile, userRelationStatus, FromId, ToId);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        private async Task CheckIfUserIsEligibleToFriend(Profile ToUserProfile, StrangerRelationStatus userRelationStatus, Guid FromId, Guid ToId)
        {

            // check if user is eligible to send a request 
            if (!((userRelationStatus & ToUserProfile.CanRequestFriend) > 0))
                throw new ConflictException("User.Friend.Policy.Conflict", userRelationStatus);

            //  user has a pending request from the reciver
            var userRelationFriendRecive = await userRelationRepository.GetByFromIdAndToIdAndRelation(ToId, FromId, RelationType.Friend);
            if (userRelationFriendRecive != null)
                throw new ConflictException("User.Friend.Request.Conflict", ToId, FromId);

            // does the user have a pending friend request ? 
            var userRelationFriend = await userRelationRepository.GetByFromIdAndToIdAndRelation(FromId, ToId, RelationType.Friend);
            if (userRelationFriend != null)
                throw new ConflictException("User.Friend.Request.Conflict", FromId, ToId);

            // check if the sender is blocked by the reciver
            var userRelationBlockRecive = await userRelationRepository.GetByFromIdAndToIdAndRelation(ToId, FromId, RelationType.Block);
            if (userRelationBlockRecive != null)
                throw new ConflictException("User.Friend.Request.Conflict", ToId, FromId);

            // check if the reciver blocked the sender
            var userRelationBlock = await userRelationRepository.GetByFromIdAndToIdAndRelation(FromId, ToId, RelationType.Block);
            if (userRelationBlock != null)
                throw new ConflictException("User.Friend.Request.Conflict", FromId, ToId);
        }

        private async Task CreateFollowRequest(Guid FromId, Guid ToId)
        {
            var userReilation = UserRelation.Create(FromId, ToId, RelationType.Follow, StatusType.Active);
            UserRelationRepo.Add(userReilation);
        }

        private async Task CreateFriendRequest(Profile ToUserProfile, StrangerRelationStatus userRelationStatus, Guid FromId, Guid ToId)
        {
            if ((userRelationStatus & ToUserProfile.AutoAcceptFriend) > 0)
            {
                var userRelation = UserRelation.Create(FromId, ToId, RelationType.Friend, StatusType.Active);
                UserRelationRepo.Add(userRelation);
            }
            else
            {
                var userRelation = UserRelation.Create(FromId, ToId, RelationType.Friend, StatusType.Pending);
                UserRelationRepo.Add(userRelation);
            }
        }

        private async Task<StrangerRelationStatus> GetStrangerRelationStatus(User FromUser, User ToUser)
        {
            StrangerRelationStatus strangerRelationStatus = StrangerRelationStatus.Anonymos;
            bool isFriendOfFriend = await userRelationRepository.CountMutualFriends(FromUser.Id, ToUser.Id) > 0;
            if (isFriendOfFriend)
                strangerRelationStatus |= StrangerRelationStatus.FriendOfFriend;
            return strangerRelationStatus;
        }
    }
}