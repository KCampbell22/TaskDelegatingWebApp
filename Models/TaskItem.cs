using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace TaskDelegatingWebApp.Models
{
    
    public class TaskItem
    {
        public int TaskItemId { get; set; }
        public int DayId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public TimeOfDay? TimeOfDay { get; set; }

        public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public ICollection<Person> Persons { get; set; }

        public Day Day { get; set; }

       

    }
}