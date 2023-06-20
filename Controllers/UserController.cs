
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news24h.Models;
using System.Security.Claims;

namespace news24h.Controllers
{
    public class UserController : Controller
    {
        private readonly News24hContext _context;
        public const string SessionKeyName = "Username";
        public const string SessionKeyPass = "Pass";

        public UserController(News24hContext context)
        {
            _context = context;
        }


        [Authorize(Roles ="admin")]
        public ActionResult UserManager()
        {
            return View(_context.Users.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login([Bind(include: "Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var account = _context.Users.FirstOrDefault(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password));
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
                     return RedirectToAction("UserManager", "User");
                }
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include : "Id,Username,Role,Password,Fullname")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
