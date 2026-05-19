using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TodoPhoenix.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        [Required]
        [RegularExpression("Low|Medium|High")]
        public string? Priority { get; set; } = "Low";

        public bool IsCompleted { get; set; } = false;

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        [Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidateNever]
        public Project? Project { get; set; }
    }
}
