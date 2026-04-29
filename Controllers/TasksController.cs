using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoPhoenix.Models;

namespace TodoPhoenix.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TasksController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Show tasks for a project
        public async Task<IActionResult> Index(int projectId)
        {
            var user = await _userManager.GetUserAsync(User);

            var project = await _context
                .Projects.Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == user.Id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        // GET: Create task
        public async Task<IActionResult> Create(int projectId)
        {
            var user = await _userManager.GetUserAsync(User);

            var project = await _context.Projects.FirstOrDefaultAsync(p =>
                p.Id == projectId && p.UserId == user.Id
            );

            if (project == null)
                return NotFound();

            var task = new TaskItem { ProjectId = projectId };
            return View(task);
        }

        // POST: Create task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            var user = await _userManager.GetUserAsync(User);

            var project = await _context.Projects.FirstOrDefaultAsync(p =>
                p.Id == task.ProjectId && p.UserId == user.Id
            );

            if (project == null)
                return Unauthorized();

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(task);
            }

            if (task.DueDate.HasValue)
            {
                task.DueDate = DateTime.SpecifyKind(task.DueDate.Value, DateTimeKind.Utc);
            }

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }

        // Toggle complete
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var task = await _context
                .Tasks.Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && t.Project.UserId == user.Id);

            if (task == null)
                return NotFound();

            task.IsCompleted = !task.IsCompleted;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }
    }
}
