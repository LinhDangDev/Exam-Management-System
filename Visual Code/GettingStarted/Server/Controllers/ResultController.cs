using Microsoft.AspNetCore.Mvc;

namespace GettingStarted.Server.Controllers;

public class ResultController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}