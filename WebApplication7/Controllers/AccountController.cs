using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace WebApplication7.Controllers
{
    public class AccountController : Controller
    {
       
            private readonly AppDbContext _context;

            public AccountController()
            {
                _context = new();

            }



            //public AccountController(appDbcontext context)
            //{
            //    _context = context;
            //}
            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]

            public IActionResult Login(LoginVM login)
            {

                if (!ModelState.IsValid)
                {
                    return View(login);
                }


                User? user = _context.Users.SingleOrDefault(e => e.Email == login.Email && e.Password == login.Password);

                if (user == null)
                {

                    ViewBag.Error = "Invalid Email or Password";
                    return View();
                }


                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetInt32("UserId", user.Id);


                return RedirectToAction("Details", "User", new { id = user.Id });
            }

            public IActionResult Logout()
            {

                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
        }

    }

