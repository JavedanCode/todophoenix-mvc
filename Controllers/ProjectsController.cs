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
        public async Task<IActionResult> Create(Project project)
        {
            Console.WriteLine("POST HIT");

            var user = await _userManager.GetUserAsync(User);

            project.UserId = user.Id;

            _context.Add(project);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard");
        }

        // DELETE: Project
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var project = await _context
                .Projects.Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == user.Id);

            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
