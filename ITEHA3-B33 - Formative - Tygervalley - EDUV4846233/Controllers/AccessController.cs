// https://stackoverflow.com/questions/3232013/get-and-post-to-same-controller-action-in-asp-net-mvc
// https://medium.com/@ravitejherwatta/controllers-and-actions-in-asp-net-core-mvc-82f7f2fbdc8e
using Microsoft.AspNetCore.Mvc;


namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Controllers;

public class AccessController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }
    // POST Login
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        return RedirectToAction("Index", "Staff");
    }
    
    // POST
    public IActionResult Logout()
    {
        return RedirectToAction("Login");
    }
}