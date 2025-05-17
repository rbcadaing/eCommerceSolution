using AutoMapper;
using eCommerce.Core.Dto;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;
using System.Net.Http.Headers;

namespace eCommerce.Core.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDTO> GetUserByUserID(Guid userId)
    {
       var user = await _userRepository.GetUserByUserID(userId);
        return _mapper.Map<UserDTO>(user);  
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

        if (user == null) { return null; }

        return _mapper.Map<AuthenticationResponse>(user) with
        {
            Success = true,
            Token = "token"
        }; ;
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {

        var user = _mapper.Map<ApplicationUser>(registerRequest);

        var resiteredUser = await _userRepository.AddUser(user);
        if (registerRequest == null) { return null; }

        return _mapper.Map<AuthenticationResponse>(resiteredUser) with
        {
            Success = true,
            Token = "token"
        };


    }
}
