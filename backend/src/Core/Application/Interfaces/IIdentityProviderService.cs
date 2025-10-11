using Application.Abstractions.Identity;
using Application.Dtos.Responses;

namespace Application.Interfaces;

public interface IIdentityProviderService
{
    Task<string> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default);
    Task<LoginUserResponse> RefreshUserAsync(string token, CancellationToken cancellationToken = default);
    Task<LoginUserResponse> LoginUserAsync(string email, string password, CancellationToken cancellationToken = default);
}
