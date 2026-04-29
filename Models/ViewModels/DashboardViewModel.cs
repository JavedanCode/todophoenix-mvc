using TodoPhoenix.Models;

namespace TodoPhoenix.Models.ViewModels
{
    public class DashboardViewModel
    {
        public List<Project> Projects { get; set; } = new();
        public List<TaskItem> Tasks { get; set; } = new();

        public int? SelectedProjectId { get; set; }
        public string CurrentFilter { get; set; } = "All";
    }
}
