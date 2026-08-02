using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Models;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}