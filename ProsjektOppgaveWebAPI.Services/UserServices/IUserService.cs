using ProsjektOppgaveWebAPI.Services.Response;
using ProsjektOppgaveWebAPI.Services.UserServices.Models;

namespace ProsjektOppgaveWebAPI.Services.UserServices;

public interface IUserService
{
    Task<ResponseService<string>> Create(CreateUserHttpPostModel vm);
    
    Task<ResponseService<string>> SignIn(SignInHttpPostModel vm);
    
}