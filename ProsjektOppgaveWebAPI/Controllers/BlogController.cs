using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProsjektOppgaveWebAPI.Services.BlogServices;
using ProsjektOppgaveWebAPI.Services.BlogServices.Models;

namespace ProsjektOppgaveWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }
    
    [HttpGet]
    [Route("Blogs")]
    public async Task<IActionResult> GetAllBlogs()
    {
        var blogs = await _blogService.GetAll();
        return Ok(JsonConvert.SerializeObject(blogs, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetBlog(long id)
    {
        var response = await _blogService.GetById(id);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }
        return Ok(response.Value);
    }
    
    [HttpPost]
    [Route ("[action]")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateBlogHttpPostModel model)
    {
        var response = await _blogService.Create(model);
        
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }
        return Ok(new
        {
            success = true
        });
    }
    
    [HttpPost]
    [Route ("[action]")]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateBlogHttpPostModel vm)
    {
        var response = await _blogService.Update(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }
        
        return Ok(new { success = true});
    }
    
    [HttpPost]
    [Route ("[action]")]
    [Authorize]
    public async Task<IActionResult> Delete([FromBody] DeleteBlogHttpPostModel vm)
    {
        var response = await _blogService.Delete(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }
        
        return Ok(new { success = true });
    }
    
    [HttpGet]
    [Route ("Blogs/{userId}")]
    public async Task<IActionResult> GetBlogsByUser(long userId)
    {
        var blogs = await _blogService.GetAllByUserId(userId);
        return Ok(blogs);
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Search(string searchString)
    {
        var blogs = await _blogService.Search(searchString);
        return Ok(JsonConvert.SerializeObject(blogs, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Find(string title)
    {
        var blogs = await _blogService.GetByName(title);
        return Ok(JsonConvert.SerializeObject(blogs, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }
}
