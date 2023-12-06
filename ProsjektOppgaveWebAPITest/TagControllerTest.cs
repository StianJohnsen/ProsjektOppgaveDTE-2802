using Microsoft.AspNetCore.Mvc;
using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services.TagServices;

namespace ProsjektOppgaveWebAPITest;

public class TagControllerTest
{
    public class TagControllerTests
    {
        private readonly Mock<ITagService> _serviceMock;
        private readonly TagController _controller;

        public TagControllerTests()
        {
            _serviceMock = new Mock<ITagService>();
            _controller = new TagController(_serviceMock.Object);
        }

        
        
        
        // POST
        [Fact]
        public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _controller.Create(new Tag());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsOk_WhenModelStateIsValid()
        {
            // Arrange
            var tag = new Tag();

            // Act
            var result = await _controller.Create(tag);

            // Assert
            Assert.IsType<OkResult>(result);
            _serviceMock.Verify(x => x.Save(tag), Times.Once);
        }
    }
}