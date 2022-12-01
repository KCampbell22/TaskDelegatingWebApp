using Microsoft.AspNetCore.Mvc;

namespace TaskDelegatingWebApp.Controllers
{
    public class Schedule : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
