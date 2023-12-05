using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Services.BlogServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.BlogServices;

public class BlogService: IBlogService
{
    private readonly IGenericRepository<BlogEntity> _blogRepository;

    public BlogService(IGenericRepository<BlogEntity> repository)
    {
        _blogRepository = repository;
    }
    
    public async Task<ResponseService<long>> Create(CreateBlogHttpPostModel vm)
    {
        BlogEntity blogEntity = await _blogRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Title == vm.Title);
        if (blogEntity != null)
        {
            return ResponseService<long>.Error(Errors.BLOG_ALREADY_EXISTS_ERROR);
        }
        blogEntity = new BlogEntity()
        {
            Title = vm.Title,
            CreatedAt = DateTime.Now,
            UserFk = vm.UserId,
            IsOpen = vm.IsOpen
        };

        try
        {
            await _blogRepository.Create(blogEntity);
        }
        catch (Exception e)
        {
            return ResponseService<long>.Error(e.Message);
        }

        return ResponseService<long>.Ok(blogEntity.Id);
    }

    public async Task<ResponseService> Delete(DeleteBlogHttpPostModel vm)
    {
       BlogEntity blog = await _blogRepository.GetById(vm.Id);
       if (blog == null)
       {
           return ResponseService.Error(Errors.BLOG_NOT_FOUND_ERROR);
       }
       
       try
       { 
           await _blogRepository.Delete(blog);
       }
       catch (Exception e)
       { 
           return ResponseService.Error(e.Message);
       }
       return ResponseService.Ok();
    }

    public async Task<ResponseService> Update(UpdateBlogHttpPostModel vm)
    {
        BlogEntity comment = await _blogRepository.GetById(vm.Id);
        if (comment == null)
        {
            return ResponseService.Error(Errors.BLOG_NOT_FOUND_ERROR);
        }
        comment.Title = vm.Title;
        
        try
        {
            await _blogRepository.Update(comment);
        }
        catch (Exception e)
        {
            return ResponseService.Error(e.Message);
        }
        return ResponseService.Ok();
    }

    public async Task<ICollection<BlogEntity>> GetAll()
    {
        return await _blogRepository.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<ResponseService<BlogEntity>> GetById(long id)
    {
        BlogEntity blog = await _blogRepository.GetAll()
            .Include(x => x.Posts)
            .ThenInclude(x=>x.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (blog == null)
        {
            return ResponseService<BlogEntity>.Error(Errors.BLOG_NOT_FOUND_ERROR);
        }

        return ResponseService<BlogEntity>.Ok(blog);
    }

    public async Task<ICollection<BlogEntity>> GetAllByUserId(long userId)
    {
        var blogs = await _blogRepository.GetAll()
            .Where(b=>b.UserFk == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        
        return blogs;
    }

    public async Task<ICollection<BlogEntity>> GetByName(string name)
    {
       return await _blogRepository.GetAll()
            .Where(x => x.Title == name)
            .ToListAsync();
    }

    public async Task<ICollection<BlogEntity>> GetByIsOpen(bool isOpen)
    {
        return await _blogRepository.GetAll()
            .Where(x => x.IsOpen == isOpen)
            .ToListAsync();
    }

    public async Task<ICollection<BlogEntity>> Search(string searchQuery)
    {
        return await _blogRepository.GetAll()
            .Where(x => x.Title.Contains(searchQuery))
            .ToListAsync();
    }
}