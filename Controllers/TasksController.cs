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

        // GET: Create task
        [HttpGet]
        public async Task<IActionResult> Create(int projectId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var project = await _context.Projects.FirstOrDefaultAsync(p =>
                p.Id == projectId && p.UserId == user.Id
            );

            if (project == null)
                return NotFound();

            var task = new TaskItem { ProjectId = projectId, DueDate = DateTime.UtcNow.Date };
            return View(task);
        }

        // POST: Create task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var project = await _context.Projects.FirstOrDefaultAsync(p =>
                p.Id == task.ProjectId && p.UserId == user.Id
            );

            if (project == null)
                return Unauthorized();

            task.Title = task.Title?.Trim() ?? "";
            task.Description = task.Description?.Trim();

            if (task.DueDate.HasValue && task.DueDate.Value.Date < DateTime.UtcNow.Date)
            {
                ModelState.AddModelError("DueDate", "Due date cannot be in the past.");
                return PartialView(task);
            }

            if (!ModelState.IsValid)
                return PartialView(task);

            if (task.DueDate.HasValue)
            {
                task.DueDate = DateTime.SpecifyKind(task.DueDate.Value, DateTimeKind.Utc);
            }

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // Toggle complete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var task = await _context
                .Tasks.Include(t => t.Project)
                .FirstOrDefaultAsync(t =>
                    t.Id == id && t.Project != null && t.Project.UserId == user.Id
                );

            if (task == null)
                return NotFound();

            task.IsCompleted = !task.IsCompleted;
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // DELETE: Task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var task = await _context
                .Tasks.Include(t => t.Project)
                .FirstOrDefaultAsync(t =>
                    t.Id == id && t.Project != null && t.Project.UserId == user.Id
                );

            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // GET: Edit task
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var task = await _context
                .Tasks.Include(t => t.Project)
                .FirstOrDefaultAsync(t =>
                    t.Id == id && t.Project != null && t.Project.UserId == user.Id
                );

            if (task == null)
                return NotFound();

            return View(task);
        }

        // POST: Edit task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var existingTask = await _context
                .Tasks.Include(t => t.Project)
                .FirstOrDefaultAsync(t =>
                    t.Id == task.Id && t.Project != null && t.Project.UserId == user.Id
                );

            if (existingTask == null)
                return NotFound();

            task.Title = task.Title?.Trim() ?? "";
            task.Description = task.Description?.Trim();

            if (task.DueDate.HasValue && task.DueDate.Value.Date < DateTime.UtcNow.Date)
            {
                ModelState.AddModelError("DueDate", "Due date cannot be in the past.");
                return PartialView(task);
            }

            if (!ModelState.IsValid)
                return PartialView(task);

            // update fields
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Priority = task.Priority;

            if (task.DueDate.HasValue)
            {
                existingTask.DueDate = DateTime.SpecifyKind(task.DueDate.Value, DateTimeKind.Utc);
            }
            else
            {
                existingTask.DueDate = null;
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
