using Microsoft.AspNetCore.Mvc;
using news24h.Models;
using news24h.Repository;
using System.Diagnostics;

namespace news24h.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly News24hContext _context;
        private Worker _worker;
        public HomeController(ILogger<HomeController> logger,News24hContext context)
        {
            _logger = logger;
            _context = context;
            _worker = new Worker(_context);
        }
        

        public IActionResult Index(string topic,string title)
        {
            
            if (topic != null)
            {
                return View(_context.Posts.Where(d=>d.PostTopic.Contains(topic)).ToList());
            }
            else if(title != null)
            {
                return View(_context.Posts.Where(d => d.PostTitle.Contains(title)).ToList());
            }
            return View(_worker.postRepository.AllPost());
        }


        public IActionResult DetailPost(int id)
        {
            Post post =_worker.postRepository.FindById(id);
            return View(post);

        }
        public ActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}