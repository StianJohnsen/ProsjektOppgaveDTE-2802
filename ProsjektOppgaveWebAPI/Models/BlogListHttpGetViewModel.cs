using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.Models;

public class BlogListHttpGetViewModel
{
    public BlogListHttpGetViewModel()
    {
        Blogs = new List<BlogEntity>();
        Users = new List<UserEntity>();
    }

    public ICollection<BlogEntity> Blogs { get; set; }
    public ICollection<UserEntity> Users { get; set; }
}