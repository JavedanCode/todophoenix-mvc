using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TodoPhoenix.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidateNever]
        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        [Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidateNever]
        public IdentityUser User { get; set; }

        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
