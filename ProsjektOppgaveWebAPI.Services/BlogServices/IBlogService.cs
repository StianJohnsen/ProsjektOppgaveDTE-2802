

using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.BlogServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.BlogServices;

public interface IBlogService
{
    Task<ResponseService<long>> Create(CreateBlogHttpPostModel vm);
    
    Task<ResponseService> Delete(DeleteBlogHttpPostModel vm);

    Task<ResponseService> Update(UpdateBlogHttpPostModel vm);
    
    Task<ICollection<BlogEntity>> GetAll();

    Task<ResponseService<BlogEntity>> GetById(long id);
    
    Task<ICollection<BlogEntity>> GetAllByUserId(long userId);
    
    Task<ICollection<BlogEntity>> GetByName(string name);
    
    Task<ICollection<BlogEntity>> GetByIsOpen(bool isOpen);

    Task<ICollection<BlogEntity>> Search(string searchQuery);

}