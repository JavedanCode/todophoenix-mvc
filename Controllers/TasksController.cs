using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoPhoenix.Models;

namespace TodoPhoenix.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // Show tasks for a project
        public async Task<IActionResult> Index(int projectId)
        {
            var project = await _context
                .Projects.Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
                return NotFound();

            return View(project);
        }

        // GET: Create task
        public IActionResult Create(int projectId)
        {
            var task = new TaskItem { ProjectId = projectId };
            return View(task);
        }

        // POST: Create task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(task);
            }

            // ✅ FIX HERE
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
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            task.IsCompleted = !task.IsCompleted;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }
    }
}
