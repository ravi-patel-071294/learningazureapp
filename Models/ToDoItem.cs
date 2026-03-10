using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class ToDoItem
    {
        // Primary key for the To Do item
        [Key]
        public int Id { get; set; }

        // Title of the To Do item (max 200 characters)
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        // Detailed description of the To Do item
        [Required]
        public string Description { get; set; }

        // Indicates whether the To Do item is completed
        [Display(Name = "Is Done?")]
        public bool IsDone { get; set; }

        // Date and time when the To Do item created
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
