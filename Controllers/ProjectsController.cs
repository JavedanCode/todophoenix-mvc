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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var projects = await _context.Projects.Where(p => p.UserId == user.Id).ToListAsync();

            return View(projects);
        }

        // GET: Projects/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            project.Name = project.Name?.Trim() ?? "";

            if (!ModelState.IsValid)
                return PartialView(project);

            var exists = await _context.Projects.AnyAsync(p =>
                p.UserId == user.Id && p.Name.ToLower() == project.Name.ToLower()
            );

            if (exists)
            {
                ModelState.AddModelError("Name", "You already have a project with this name.");
                return PartialView(project);
            }

            project.UserId = user.Id;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // DELETE: Project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var project = await _context
                .Projects.Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == user.Id);

            if (project == null)
                return NotFound();

            _context.Tasks.RemoveRange(project.Tasks);

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
