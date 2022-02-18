using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace D8.Models
{
    public class TaskCreateModel
    {
        [Required, MaxLength(100)]
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}