namespace ProsjektOppgaveWebAPI.Services.PostServices.Models;

public class UpdatePostHttpPostModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}