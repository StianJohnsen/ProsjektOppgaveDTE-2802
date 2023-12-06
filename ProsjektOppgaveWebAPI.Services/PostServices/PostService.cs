using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.PostServices;

public class PostService: IPostService
{
    private readonly IGenericRepository<PostEntity> _postRepository;

    public PostService(IGenericRepository<PostEntity> postRepository)
    {
        _postRepository = postRepository;
    }
    
    public async Task<ResponseService<long>> Create(CreatePostHttpPostModel vm)
    {
        PostEntity postEntity = await _postRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Title == vm.Title 
                                      && x.BlogFk == vm.BlogId 
                                      && !x.DeletedAt.HasValue);
        if (postEntity != null)
        {
            return ResponseService<long>.Error(Errors.POST_ALREADY_EXISTS_ERROR);
        }
        postEntity = new PostEntity()
        {
            Title = vm.Title,
            Content = vm.Content,
            BlogFk = vm.BlogId,
            CreatedAt = DateTime.Now
        };

        try
        {
            await _postRepository.Create(postEntity);
        }
        catch (Exception e)
        {
            return ResponseService<long>.Error(Errors.CANT_CREATE_POST_ERROR);
        }

        return ResponseService<long>.Ok(postEntity.Id);

    }

    public async Task<ResponseService> Delete(DeletePostHttpPostModel vm)
    {
        PostEntity postEntity = await _postRepository.GetById(vm.Id);
        if (postEntity == null)
        {
            return ResponseService.Error(Errors.POST_NOT_FOUND_ERROR);
        }
        
        postEntity.DeletedAt = DateTime.Now;
        return await Update(postEntity);
    }

    public async Task<ResponseService> Update(UpdatePostHttpPostModel vm)
    {
       PostEntity post = await _postRepository.GetById(vm.Id);
       if (post == null)
       {
           return ResponseService.Error(Errors.POST_NOT_FOUND_ERROR);
       }
       post.Title = vm.Title;
       post.Content = vm.Content;
    
        try 
        {
            await _postRepository.Update(post);
        }
        catch (Exception e)
        {
            return ResponseService.Error(Errors.CANT_UPDATE_POST_ERROR);
        }

        return ResponseService.Ok();

    }

    public async Task<ICollection<PostEntity>> GetAll()
    {
        return await _postRepository.GetAll()
            .Where(x => !x.DeletedAt.HasValue)
            .ToListAsync();
    }

    public Task<ICollection<PostEntity>> GetByBlogId(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<PostEntity>> FindByTitle(string title)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<PostEntity>> SearchByTitle(string searchTitle)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<PostEntity>> SearchByContent(string searchContent)
    {
        throw new NotImplementedException();
    }

    private async Task<ResponseService> Update(PostEntity postEntity)
    {
        try
        {
            await _postRepository.Update(postEntity);
        }
        catch (Exception e)
        {
            return ResponseService.Error(Errors.CANT_UPDATE_POST_ERROR);
        }
        return ResponseService.Ok();
    }
}