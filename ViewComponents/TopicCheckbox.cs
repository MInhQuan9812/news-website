using Microsoft.AspNetCore.Mvc;
using news24h.Models;
using news24h.Repository;

namespace news24h.ViewComponents
{
    [ViewComponent(Name = "TopicCheckbox")]
    public class TopicCheckbox:ViewComponent
    {
        private readonly News24hContext _context;
        private Worker _worker;


        public TopicCheckbox(News24hContext context)
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
