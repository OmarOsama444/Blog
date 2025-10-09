using Application.Dtos.Responses;

namespace Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<string> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default);
    Task<LoginUserResponse> RefreshUserAsync(string token, CancellationToken cancellationToken = default);
    Task<LoginUserResponse> LoginUserAsync(string email, string password, CancellationToken cancellationToken = default);
}
