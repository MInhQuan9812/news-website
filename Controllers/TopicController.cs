using Microsoft.AspNetCore.Mvc;
using news24h.Models;
using news24h.Repository;

namespace news24h.Controllers
{
    public class TopicController : Controller
    {

        private readonly News24hContext _context;
        private Worker _worker;

        public TopicController(News24hContext context)
        {
            _context = context;
            _worker = new Worker(_context);
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult TopicPartial()
        //{
        //    var list = _worker.topicRepository.AllTopic();
        //    ViewData["TopicPartial"] = list;
        //    return View(list);
        //}
    }
}
