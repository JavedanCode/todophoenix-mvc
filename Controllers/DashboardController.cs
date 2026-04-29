using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoPhoenix.Models;
using TodoPhoenix.Models.ViewModels;

namespace TodoPhoenix.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? projectId, string filter = "All")
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            // Load projects
            var projects = await _context.Projects.Where(p => p.UserId == user.Id).ToListAsync();

            // Load tasks
            var tasksQuery = _context
                .Tasks.Include(t => t.Project)
                .Where(t => t.Project.UserId == user.Id);

            if (projectId.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.ProjectId == projectId.Value);
            }
            else if (filter == "Today")
            {
                var today = DateTime.UtcNow.Date;
                tasksQuery = tasksQuery.Where(t =>
                    t.DueDate.HasValue && t.DueDate.Value.Date == today
                );
            }
            else if (filter == "Completed")
            {
                tasksQuery = tasksQuery.Where(t => t.IsCompleted);
            }

            var tasks = await tasksQuery.ToListAsync();

            var vm = new DashboardViewModel
            {
                Projects = projects,
                Tasks = tasks,
                SelectedProjectId = projectId,
                CurrentFilter = filter,
            };

            return View(vm);
        }
    }
}
