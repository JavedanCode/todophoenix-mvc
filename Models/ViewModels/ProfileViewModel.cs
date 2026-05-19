namespace TodoPhoenix.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string Email { get; set; } = string.Empty;

        public int TotalProjects { get; set; }

        public int TotalTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int PendingTasks { get; set; }

        public int TasksDueToday { get; set; }

        public int HighPriorityTasks { get; set; }
    }
}
