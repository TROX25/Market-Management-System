using System.ComponentModel.DataAnnotations;

namespace Build_Market_Management_System.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
