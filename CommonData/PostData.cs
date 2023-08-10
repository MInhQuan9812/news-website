using Microsoft.AspNetCore.Mvc.Rendering;
using news24h.Models;
using news24h.Repository;

namespace news24h.CommonData
{
    public static class PostData
    {
        private static News24hContext _context;

        public static List<SelectListItem> getTagList(News24hContext context)
        {
            _context = context;
            Worker _worker=new Worker(_context);
            List<SelectListItem> lstTag =_worker.topicRepository.AllTopic()
                .Select(m =>
                new SelectListItem
                {
                    Text = m.TopicName,
                    Value = m.TopicId.ToString(),
                }
                ).ToList();
            return lstTag;
        }


    }  

}

