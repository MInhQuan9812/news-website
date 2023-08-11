using Microsoft.AspNetCore.Mvc;
using news24h.Models;
using news24h.Repository;

namespace news24h.ViewComponents
{
    [ViewComponent(Name = "MostCountPost")]
    public class MostCountPost : ViewComponent
    {

        private readonly News24hContext _context;
        private Worker _worker;


        public MostCountPost(News24hContext context)
        {
            _context = context;
            _worker = new Worker(_context);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View(_worker.postRepository.AllPost().Take(5).OrderByDescending(d => d.ViewCount).ToList());
        }

    }
}
