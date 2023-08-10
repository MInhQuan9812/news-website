using Microsoft.AspNetCore.Mvc;
using news24h.Models;
using news24h.Repository;
using System.Net;

namespace news24h.Controllers
{
    public class PostController : Controller
    {
        private readonly News24hContext _context;
        private readonly Worker _worker;

        public PostController(News24hContext context, Worker worker)
        {
            _context = context;
            _worker = new Worker(context);
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
