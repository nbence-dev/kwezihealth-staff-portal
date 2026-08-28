// Deliverable 3: Controllers, Authentication & Views — administrator Login (GET/POST) and Logout (POST) with cookie-based session/authentication state.
// https://stackoverflow.com/questions/3232013/get-and-post-to-same-controller-action-in-asp-net-mvc
// https://medium.com/@ravitejherwatta/controllers-and-actions-in-asp-net-core-mvc-82f7f2fbdc8e
// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-10.0
// https://stackoverflow.com/questions/19250017/prevent-access-to-page-based-on-authentication
// https://www.youtube.com/watch?v=PUX3PzyBofg&t=1


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
    [HttpGet]
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
        if (!ModelState.IsValid)
        {
            return View(loginDto);
        }
        
        var isLoggedIn = await _authService.Login(loginDto);

        if (isLoggedIn)
        {
            TempData["StatusMessage"] = "Logged in successfully.";
            return RedirectToAction("Index", "Staff");
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password");
        return View(loginDto);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        TempData["StatusMessage"] = "You have been logged out.";
        return RedirectToAction("Login");
    }
}