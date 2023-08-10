using Microsoft.AspNetCore.Mvc.Rendering;
using news24h.Models;

namespace news24h.ViewModel
{
    public class createPostViewModel
    {
        public string? PostTitle { get; set; }

        public string? PostContent { get; set; }

        //public string? PostTopic { get; set; } = "Kinh tế";

        public List<SelectListItem> PostTopic { get; set; }

        public IFormFile? ImageUrl
        {
            get; set;
        }
    }
}
