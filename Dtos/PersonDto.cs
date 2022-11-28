using System.ComponentModel.DataAnnotations;
using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.Dtos
{
    public class PersonDto
    {
       
        public int PersonId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public ICollection<TaskItem> TaskItems { get; set; }
    }
}
