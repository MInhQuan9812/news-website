
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using news24h.Models;
using news24h.Repository;
using System.Security.Claims;
using System.Text.Json;

namespace news24h.Controllers
{
    public class UserController : Controller
    {
        private readonly News24hContext _context;
        public const string SessionKeyName = "Username";
        public const string SessionKeyPass = "Pass";
        private List<Post>? _savePost=new List<Post>();
        private Worker _worker;

        public UserController(News24hContext context)
        {
            _context = context;
            _worker = new Worker(_context);
        }

        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<ActionResult> Login([Bind(include: "Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var account = _context.Users.FirstOrDefault(x => x.Username.Equals(user.Username) && x.Password.Equals(CommonData.CommonFunction.GetHashString(user.Password)));
                if (account != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.Username),
                        new Claim(ClaimTypes.Role, account.Role),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties { };

                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                     authProperties);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Profile()
        {
            string username = User.Identity.Name;
            User user = _context.Users.Where(x => x.Username == username).FirstOrDefault();
            return View(user);
        }

        //public IActionResult`

        [HttpPost]
        public async Task<ActionResult> SignUp([Bind(include: "Username, Password, Fullname")] User user)
        {
            if (ModelState.IsValid)
            {
                var account = _context
                    .Users
                    .FirstOrDefault(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password));

                if (account != null)
                {
                    throw new Exception("This account is already created.");
                }

                User nUser = new User
                {
                    Username = user.Username,
                    Fullname = user.Fullname,
                    Password = CommonData.CommonFunction.GetHashString(user.Password),
                    Role = user.Role
                };

                account = _worker.userRepository.AddUser(nUser);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.Username),
                        new Claim(ClaimTypes.Role, account.Role),
                    };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties { };

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                 authProperties);
                _worker.userRepository.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult WatchedNews()
        {
            return View();
        }


        public ActionResult SavedNews(int id)
        {
            var value = HttpContext.Session.Get<List<Post>>("saves");
            var post = _context.Posts.SingleOrDefault(s => s.Id == id);
            if (value != null)
            {
                //list.Add_Post_Save(post);
                _savePost.Add(post);
                HttpContext.Session.Set<List<Post>>("saves",_savePost);
            }
            
            //GetSavedNews().Add_Post_Save(post);

            HttpContext.Session.Set<List<Post>>("saves", _savePost);
            //HttpContext.Session.Set<Saves>("saves");
            //var valueP = HttpContext.Session.Get<List<Post>>("saves");
            return RedirectToAction($"{id}", "bai-viet");
        }

        public IActionResult ShowSavedNews()
        {
                       
            var valueP = HttpContext.Session.Get<List<Post>>("saves");
            return View(valueP);
        }

        //public Saves GetSavedNews()
        //{
        //    Saves post = HttpContext.Session.Get<Saves>("saves");
        //    if(post == null)
        //    {
        //        post = new Saves();
        //        HttpContext.Session.Set("saves", post);
        //    }
        //    // chua co session luu du lieu
        //    return post;
        //}
    }
}
