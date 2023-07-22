using Microsoft.AspNetCore.Mvc;
using news24h.Models;
using news24h.Repository;

namespace news24h.ViewComponents
{
    [ViewComponent(Name = "TopicPartial")]
    public class TopicPartial:ViewComponent
    {
        private readonly News24hContext _context;
        private Worker _worker;


        public TopicPartial(News24hContext context)
        {
            _context= context;
            _worker=new Worker(_context);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = _worker.topicRepository.AllTopic();
            return View(list);
        }
    }
}
