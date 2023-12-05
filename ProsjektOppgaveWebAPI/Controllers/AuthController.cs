using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Services.UserServices;
using ProsjektOppgaveWebAPI.Services.UserServices.Models;

namespace ProsjektOppgaveWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService  userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SignUp([FromBody] CreateUserHttpPostModel vm)
    {
       var response = await _userService.Create(vm);

       if (response.IsError)
       {
           return BadRequest(new
           {
               responseMessage = response.ErrorMessage
           });
       }
       return Ok(new
       {
           token = response.Value
       });
    }
    
    [HttpGet]
    [Route ("/CheckToken")]
    [Authorize]
    public async Task<IActionResult> CheckToken()
    {
        return Ok(new
        {
            success = true
        });
    }
    
    [HttpPost]
    [Route("/SignIn")]
    public async Task<IActionResult> SignIn([FromBody] SignInHttpPostModel vm)
    {
        var response = await _userService.SignIn(vm);

        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }
        return Ok(new
        {
            token = response.Value
        });
    }

}
