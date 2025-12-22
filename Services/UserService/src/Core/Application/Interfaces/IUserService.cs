using Application.Dtos.Requests;
using Application.Dtos.Responses;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<Guid> CreateUserAsync(CreateUserRequestDto createUserRequestDto, CancellationToken cancellationToken = default);
    public Task<LoginUserResponse> RefreshUserAsnc(RefreshTokenRequestDto refreshTokenRequestDto, CancellationToken cancellationToken = default);
    public Task<LoginUserResponse> LoginUserAsync(LoginUserRequestDto loginUserRequestDto, CancellationToken cancellationToken = default);
}
