using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services.CommentServices;

namespace ProsjektOppgaveWebAPITest;

public class CommentControllerTest
{
    private readonly Mock<ICommentService> _serviceMock;
    private readonly CommentController _controller;

    public CommentControllerTest()
    {
        _serviceMock = new Mock<ICommentService>();
        _controller = new CommentController(_serviceMock.Object);
    }

    
    
    // GET
    [Fact]
    public async Task GetComments_ReturnsExpectedComments()
    {
        // Arrange
        const int postId = 1;
        var expectedComments = new List<Comment> { new Comment(), new Comment() };
        _serviceMock.Setup(service => service.GetCommentsForPost(postId)).ReturnsAsync(expectedComments);

        // Act
        var result = await _controller.GetComments(postId);

        // Assert
        Assert.Equal(expectedComments, result);
        _serviceMock.Verify(x => x.GetCommentsForPost(postId), Times.Once);
    }
    
    [Fact]
    public void GetComment_ReturnsExpectedComment()
    {
        // Arrange
        const int commentId = 1;
        var expectedComment = new Comment();
        _serviceMock.Setup(service => service.GetComment(commentId)).Returns(expectedComment);

        // Act
        var result = _controller.GetComment(commentId);

        // Assert
        Assert.Equal(expectedComment, result);
        _serviceMock.Verify(x => x.GetComment(commentId), Times.Once);
    }

    
    
    // POST
    
    
    
}