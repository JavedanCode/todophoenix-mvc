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

        public async Task<IActionResult> Index(int? projectId, string filter = "all")
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            filter = filter?.ToLower() ?? "all";

            // Load projects
            var projects = await _context.Projects.Where(p => p.UserId == user.Id).ToListAsync();

            // Handle deleted project edge case
            if (projectId.HasValue && !projects.Any(p => p.Id == projectId.Value))
            {
                projectId = null;
                filter = "all";
            }

            // Base query
            var tasksQuery = _context.Tasks.Where(t => t.Project.UserId == user.Id);

            // Apply filters
            if (projectId.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.ProjectId == projectId.Value);
            }
            else
            {
                if (filter == "today")
                {
                    var today = DateTime.UtcNow.Date;
                    tasksQuery = tasksQuery.Where(t =>
                        t.DueDate.HasValue && t.DueDate.Value.Date == today
                    );
                }
                else if (filter == "completed")
                {
                    tasksQuery = tasksQuery.Where(t => t.IsCompleted);
                }
            }

            // Ordering
            tasksQuery = tasksQuery.OrderBy(t => t.IsCompleted).ThenBy(t => t.DueDate);

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
