using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoPhoenix.Models;
using TodoPhoenix.Models.ViewModels;

namespace TodoPhoenix.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;

        public ProfileController(UserManager<IdentityUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var tasks = await _context
                .Tasks.Include(t => t.Project)
                .Where(t => t.Project != null && t.Project.UserId == user.Id)
                .ToListAsync();

            var vm = new ProfileViewModel
            {
                Email = user.Email ?? "Unknown",

                TotalProjects = await _context.Projects.CountAsync(p => p.UserId == user.Id),

                TotalTasks = tasks.Count,

                CompletedTasks = tasks.Count(t => t.IsCompleted),

                PendingTasks = tasks.Count(t => !t.IsCompleted),

                TasksDueToday = tasks.Count(t =>
                    t.DueDate.HasValue && t.DueDate.Value.Date == DateTime.UtcNow.Date
                ),

                HighPriorityTasks = tasks.Count(t =>
                    t.Priority != null && t.Priority.ToLower() == "high"
                ),
            };

            return View(vm);
        }
    }
}
