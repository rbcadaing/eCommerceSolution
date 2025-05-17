using eCommerce.Core.Dto;

namespace eCommerce.Core.ServiceContracts;

public interface IUserService
{
    Task<AuthenticationResponse?> Login(LoginRequest loginRequest);
    Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);
    Task<UserDTO> GetUserByUserID(Guid userId);
}
