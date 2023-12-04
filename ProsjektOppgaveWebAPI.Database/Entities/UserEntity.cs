using System.Collections.ObjectModel;

namespace ProsjektOppgaveWebAPI.Database.Entities;

public class UserEntity
{
    public UserEntity()
    {
        Comments = new Collection<CommentEntity>();
        Blogs = new Collection<BlogEntity>();
    }
    
    public long Id { get; set; }
    public string Username { get; set; }
    public string HashedPassword { get; set; }

    public ICollection<CommentEntity> Comments { get; set; }
    public ICollection<BlogEntity> Blogs { get; set; }
    public ICollection<PostUsersEntity> Posts { get; set; }
    public ICollection<TagEntity> Tags { get; set; }
}