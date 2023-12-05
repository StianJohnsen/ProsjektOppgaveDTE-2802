namespace ProsjektOppgaveWebAPI.Services.BlogServices.Models;

public class CreateBlogHttpPostModel
{
    public string Title { get; set; }
    public bool IsOpen { get; set; }
    public long UserId { get; set; }
}