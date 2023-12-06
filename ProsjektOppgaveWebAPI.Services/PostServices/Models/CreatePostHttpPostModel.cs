namespace ProsjektOppgaveWebAPI.Services.PostServices.Models;

public class CreatePostHttpPostModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public long BlogId { get; set; }
    
}