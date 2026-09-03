using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Controllers;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApp.Tests;

public class UnitTest1
{
    [Fact]
    public void Login_WhenNotAuthenticated_ReturnsAViewResult()
    {
        var mockAuthService = new Mock<AuthService>(null!, null!);

        var controller = new AccessController(mockAuthService.Object)
        {
            // The GET action reads HttpContext.User, so the controller needs a context to run against.
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        var result = controller.Login();

        Assert.IsType<ViewResult>(result);
    }
}
