using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Services.JwtServices;
using ProsjektOppgaveWebAPI.Services.Response;
using ProsjektOppgaveWebAPI.Services.UserServices.Models;

namespace ProsjektOppgaveWebAPI.Services.UserServices;

public class UserService: IUserService
{
    
    private readonly IGenericRepository<UserEntity> _genericRepository;
    
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher<UserEntity> _passwordHasher;

    public UserService(IJwtService jwtService, 
        IPasswordHasher<UserEntity> passwordHasher,
        IGenericRepository<UserEntity> genericRepository)
    {
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
        _genericRepository = genericRepository;
    }
    
    public async Task<ResponseService<string>> Create(CreateUserHttpPostModel vm)
    {
        UserEntity user = await _genericRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Username == vm.Username);
        if (user != null)
        {
            return ResponseService<string>.Error(Errors.USER_ALREADY_EXISTS);
        }
        
        user = new UserEntity()
        {
            Username = vm.Username,
            HashedPassword = _passwordHasher.HashPassword(null, vm.Password)
        };

        try
        {
            await _genericRepository.Create(user);
        }
        catch (Exception e)
        {
            return ResponseService<string>.Error(e.Message);
        }
        
        return await _jwtService.GenerateToken(user);
    }

    public async Task<ResponseService<string>> SignIn(SignInHttpPostModel vm)
    {
        UserEntity userEntity = await _genericRepository.GetAll()
            .FirstOrDefaultAsync(user => user.Username == vm.Username);
        if (userEntity == null)
        {
            return ResponseService<string>.Error(Errors.USER_NOT_FOUND);
        }
        var verifiedResult = _passwordHasher.VerifyHashedPassword(null, userEntity.HashedPassword, vm.Password);
        if (verifiedResult == PasswordVerificationResult.Failed)
        {
            return ResponseService<string>.Error(Errors.USER_NOT_FOUND);
        }
        return await _jwtService.GenerateToken(userEntity);
    }
}