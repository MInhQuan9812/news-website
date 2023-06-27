
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news24h.Models;
using news24h.Repository;
using System.Security.Claims;

namespace news24h.Controllers
{
    public class UserController : Controller
    {
        private readonly News24hContext _context;
        public const string SessionKeyName = "Username";
        public const string SessionKeyPass = "Pass";
        private Worker _worker;
        public UserController(News24hContext context)
        {
            _context = context;
            _worker = new Worker(_context);
        }


        [Authorize(Roles = "admin")]
        public ActionResult UserManager()
        {
            return View(_context.Users.ToList());
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

        public IActionResult Info()
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
    }
}
