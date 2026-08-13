// https://stackoverflow.com/questions/3232013/get-and-post-to-same-controller-action-in-asp-net-mvc
// https://medium.com/@ravitejherwatta/controllers-and-actions-in-asp-net-core-mvc-82f7f2fbdc8e
// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-10.0
// https://stackoverflow.com/questions/19250017/prevent-access-to-page-based-on-authentication


using System.Security.Claims;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.DTOs;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Controllers;


public class AccessController : Controller
{
    private readonly AuthService _authService;
    public AccessController(AuthService authService)
    {
        _authService = authService;
    }
    // GET
    public IActionResult Login()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Staff");
        }
        return View();
    }
    // POST Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        // Test to see if username and password match
        
        var isLoggedIn = await _authService.Login(loginDto);
        if (isLoggedIn)
        {
            return RedirectToAction("Index", "Staff");
        }
        return View(loginDto);
        
        // Remember to validate and add errors
        
        
        
    }
    
    // POST
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}