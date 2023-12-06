using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.PostServices;

public interface IPostService
{
    Task<ResponseService<long>> Create(CreatePostHttpPostModel vm);
    
    Task<ResponseService> Delete(DeletePostHttpPostModel vm);
    
    Task<ResponseService> Update(UpdatePostHttpPostModel vm);
    
    Task<ICollection<PostEntity>> GetAll();
    
    Task<ICollection<PostEntity>> GetByBlogId(long id);
    
    Task<ICollection<PostEntity>> FindByTitle(string title);
    
    Task<ICollection<PostEntity>> SearchByTitle(string searchTitle);
    
    Task<ICollection<PostEntity>> SearchByContent(string searchContent);
}