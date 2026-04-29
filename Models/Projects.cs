using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TodoPhoenix.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // 🔗 Relationship to User
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        // 🔗 One Project → Many Tasks
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}