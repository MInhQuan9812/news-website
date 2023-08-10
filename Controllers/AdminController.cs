using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news24h.CommonData;
using news24h.Models;
using news24h.Repository;
using news24h.ViewModel;

namespace news24h.Controllers
{
    public class AdminController : Controller
    {
        private readonly News24hContext _context;
        private Worker _worker;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(News24hContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _worker = new Worker(_context);
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var listTopic = _worker.topicRepository.AllTopic();
            return View(listTopic);
        }

        [Authorize(Roles = "admin")]
        public ActionResult createPost()
        {
            createPostViewModel model = new createPostViewModel
            {
                //PostTopic = "Đời sống"
                PostTopic = PostData.getTagList(_context)
            };
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> createPost(createPostViewModel model)
        {
            List<Topic> topicList=new List<Topic>();
            topicList.AddRange(model.PostTopic.Where(m => m.Selected)
                .Select(m => new Topic { TopicId = int.Parse(m.Value), TopicName = m.Text })
                );
            if (ModelState.IsValid)
            {
                string folder = "post/cover/";

                if (model.PostTitle == null)
                {
                    model.PostTitle = CommonFunction.GetTeaserFromContent(model.PostContent, 200);
                }


                if (model.ImageUrl != null)
                {
                    
                    folder += Guid.NewGuid().ToString()+"-"+ model.ImageUrl.FileName;
                    string serverFolder  = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await model.ImageUrl.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                User user=_worker.userRepository.FindByUsername(User.Identity.Name);
                Post post = new Post
                {
                    CreatorId = user.Id,
                    PostTitle = model.PostTitle,
                    PostTopic = _worker.topicRepository.FindById(topicList[0].TopicId).TopicName,
                    PostContent = model.PostContent,
                    ImageUrl = folder,
                    CreateAt = DateTime.Now
                };
                _worker.postRepository.AddPost(post);
                _worker.postRepository.SaveChanges();
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult UserManager()
        {
            return View(_context.Users.ToList());
        }
        [Authorize(Roles = "admin")]
        public ActionResult PostManager()
        {
            return View(_context.Posts.ToList());
        }

    }

}
