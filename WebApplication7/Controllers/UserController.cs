using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class UserController : Controller
    {
        
           



            private readonly AppDbContext _context;

            public UserController()
            {
                _context = new();
            }

            public IActionResult Index()
            {
                List<User> users = _context.Users.Include(u => u.Resumes).ToList();
                return View(users);
            }

            public IActionResult Details(int id)
            {
                User? user = _context.Users
                    .Include(u => u.Resumes)  // Include resumes
                    .SingleOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return Content("User Not Found");
                }

                return View(user);
            }

            public IActionResult AddForm()
            {
                return View();
            }

            [HttpPost]
            public IActionResult AddtoData(User user)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "User added successfully!";
                return RedirectToAction("Index");
            }

            public IActionResult EditForm(int id)
            {
                var user = _context.Users
                    .Include(u => u.Resumes)  // Include resumes for editing if needed
                    .FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }

            [HttpPost]
            public IActionResult EditForm(User user)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                TempData["Message"] = "User edited successfully!";
                return RedirectToAction("Index");
            }

            public IActionResult Delete(int id)
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound();
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }




