using Microsoft.AspNetCore.Mvc;

namespace news24h.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
