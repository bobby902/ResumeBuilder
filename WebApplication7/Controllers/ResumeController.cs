using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{

    //    public class ResumeController : Controller
    //    {
    //        private readonly AppDbContext _context;
    //        public ResumeController()
    //        {
    //            _context = new();

    //        }


    //        // GET: Resume/Create
    //        public IActionResult Create()
    //        {
    //            return View();
    //        }
    //        private HashSet<Experience> Experiences;
    //        private HashSet<Education> Educations;
    //        private HashSet<Project> Projects;
    //        // POST: Resume/Create
    //        [HttpPost]
    //        [ValidateAntiForgeryToken]
    //        public IActionResult Create(Resume resume)
    //        {
    //            // Assuming you have user authentication and a way to get the logged-in user
    //            var user = _context.Users.SingleOrDefault(u => u.Email == User.Identity.Name); // Get logged-in user

    //            if (user != null)
    //            {
    //                resume.UserId = user.Id; // Associate resume with logged-in user
    //                _context.Resumes.Add(resume);
    //                _context.SaveChanges();
    //                return RedirectToAction("Index", "Resume");
    //            }
    //            else
    //            {
    //                return Unauthorized(); // Return unauthorized if user is not logged in
    //            }
    //        }

    //        // GET: Resume/Index
    //        public IActionResult Index()
    //        {
    //            // Get all resumes for the logged-in user
    //            var user = _context.Users.SingleOrDefault(u => u.Email == User.Identity.Name);
    //            if (user != null)
    //            {
    //                // var resumes = _context.Resumes.Where(r => r.UserId == user.Id).ToList()
    //                var resumes = _context.Resumes
    //                    .Where(r => r.UserId == user.Id)
    //                    .Include(r => r.Experiences)
    //                    .Include(r => r.Educations)
    //                    .Include(r => r.Projects)
    //                    .ToList();

    //                ;
    //                return View(resumes);
    //            }
    //            else
    //            {
    //                return Unauthorized();
    //            }




    //            [HttpPost]
    //            public IActionResult AddExperience(int resumeId, Experience experience)
    //            {
    //                var resume = _context.Resumes.Include(r => r.Experiences)
    //                                              .FirstOrDefault(r => r.Id == resumeId);
    //                if (resume != null)
    //                {
    //                    resume.AddExperience(experience);
    //                    _context.SaveChanges();
    //                    return RedirectToAction("ViewResume", new { id = resumeId });
    //                }
    //                return NotFound();
    //            }

    //            [HttpPost]
    //            public IActionResult AddEducation(int resumeId, Education education)
    //            {
    //                var resume = _context.Resumes.Include(r => r.Educations)
    //                                              .FirstOrDefault(r => r.Id == resumeId);
    //                if (resume != null)
    //                {
    //                    resume.AddEducation(education);
    //                    _context.SaveChanges();
    //                    return RedirectToAction("ViewResume", new { id = resumeId });
    //                }
    //                return NotFound();
    //            }

    //            [HttpPost]
    //            public IActionResult AddProject(int resumeId, Project project)
    //            {
    //                var resume = _context.Resumes.Include(r => r.Projects)
    //                                              .FirstOrDefault(r => r.Id == resumeId);
    //                if (resume != null)
    //                {
    //                    resume.AddProject(project);
    //                    _context.SaveChanges();
    //                    return RedirectToAction("ViewResume", new { id = resumeId });
    //                }
    //                return NotFound();
    //            }





    //        }

    //    }
    //}
    public class ResumeController : Controller
    {
        private readonly AppDbContext _context;

        public ResumeController()
        {
            _context = new();  // Initialize the context properly.
        }

        // GET: Resume/Create
        public IActionResult Create()
        {
            // Pass a new instance of the Resume model to the view
            var resume = new Resume();

            // Retrieve the logged-in user's information if needed
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Unauthorized();  // Redirect to login if user is not logged in
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();  // Handle case if user is not found
            }

            resume.User = user;  // Assign user to the resume

            return View(resume);  // Pass the resume model to the view
            
        }

        // Other actions...
    

    private HashSet<Experience> Experiences;
    private HashSet<Education> Educations;
    private HashSet<Project> Projects;

        // POST: Resume/Create
        //[HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public IActionResult Create(Resume resume)
        //    {
        //        var user = _context.Users.SingleOrDefault(u => u.Email == User.Identity.Name);


        //           resume.UserId= user.Id;
        //            _context.Resumes.Add(resume);
        //            _context.SaveChanges();
        //            return RedirectToAction("Index", "Resume");



        //    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Resume resume)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    // Handle the unauthenticated user case (e.g., redirect to login or return an error)
            //    return RedirectToAction("Login", "Account"); // Or an appropriate action
            //}

            var user = _context.Users.SingleOrDefault(u => u.Email == User.Identity.Name);

            if (user == null)
            {
                // Handle the case where the user was not found in the database
                ModelState.AddModelError("", "User not found.");
                return View(resume); // Return the view so the user can correct any issues
            }

            // If the user is valid, proceed with saving the resume
            resume.UserId = user.Id;
            _context.Resumes.Add(resume);
            _context.SaveChanges();

            return RedirectToAction("Index", "Resume");
        }


        // GET: Resume/Index
        public IActionResult Index()
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == User.Identity.Name);
            if (user != null)
            {
                var resumes = _context.Resumes
                    .Where(r => r.UserId == user.Id)
                    .Include(r => r.Experiences)
                    .Include(r => r.Educations)
                    .Include(r => r.Projects)
                    .ToList();

                return View(resumes);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public IActionResult AddExperience(int resumeId, Experience experience)
        {
            var resume = _context.Resumes.Include(r => r.Experiences)
                                          .FirstOrDefault(r => r.Id == resumeId);
            if (resume != null)
            {
                resume.AddExperience(experience);
                _context.SaveChanges();
                return RedirectToAction("ViewResume", new { id = resumeId });
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddEducation(int resumeId, Education education)
        {
            var resume = _context.Resumes.Include(r => r.Educations)
                                          .FirstOrDefault(r => r.Id == resumeId);
            if (resume != null)
            {
                resume.AddEducation(education);
                _context.SaveChanges();
                return RedirectToAction("ViewResume", new { id = resumeId });
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddProject(int resumeId, Project project)
        {
            var resume = _context.Resumes.Include(r => r.Projects)
                                          .FirstOrDefault(r => r.Id == resumeId);
            if (resume != null)
            {
                resume.AddProject(project);
                _context.SaveChanges();
                return RedirectToAction("ViewResume", new { id = resumeId });
            }
            return NotFound();
        }
    }
}



