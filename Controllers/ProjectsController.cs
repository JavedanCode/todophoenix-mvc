using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoPhoenix.Models;

namespace TodoPhoenix.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProjectsController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var projects = await _context.Projects.Where(p => p.UserId == user.Id).ToListAsync();

            return View(projects);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Projects/Create
        public async Task<IActionResult> Create(Project project)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            project.UserId = user.Id;

            if (!ModelState.IsValid)
            {
                return View(project);
            }

            _context.Add(project);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
