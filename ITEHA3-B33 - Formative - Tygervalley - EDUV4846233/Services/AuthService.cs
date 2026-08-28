// Deliverable 3: Controllers, Authentication & Views — authentication logic (credential check, claims/cookie sign-in) used by AccessController.
using System.Security.Claims;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.DTOs;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Services;

public class AuthService
{
    private readonly SystemAdminRepository _systemAdminRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(SystemAdminRepository systemAdminRepository, IHttpContextAccessor httpContextAccessor)
    {
        _systemAdminRepository = systemAdminRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> Login(LoginDto loginDto)
    {
        // Search user by username and get the object back
        var systemAdmin = await _systemAdminRepository.GetAdminByUsername(loginDto.Username);

        if (systemAdmin == null)
        {
            return false;
        }

        if (systemAdmin.Password != loginDto.Password)
        {
            return false;
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginDto.Username),
            new Claim(ClaimTypes.Role, "Admin"),

        };
        
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
        };
        
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        return true;
    }
    
    public async Task Logout()
    {
        
    }
}