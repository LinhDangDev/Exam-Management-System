using Microsoft.AspNetCore.Mvc;

namespace GettingStarted.Server.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
