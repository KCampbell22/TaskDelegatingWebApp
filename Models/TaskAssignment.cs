using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace TaskDelegatingWebApp.Models
{
    public partial class TaskAssignment
    {
        [ForeignKey("")]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }

        public int DayId { get; set; }
        public Day Day { get; set; }

        

    }
}