using System.ComponentModel.DataAnnotations.Schema;

namespace TaskDelegatingWebApp.Models
{
    public class TaskAssignment
    {
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person? Person { get; set; }
        public int TaskItemId { get; set; }

        public virtual TaskItem? TaskItem { get; set; }


    }
}