using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace TaskDelegatingWebApp.Models
{
    public enum TimeOfDay
    {
        Morning, Afternoon, Night
    }
    public class TaskItem
    {
        public int TaskItemId { get; set; }
        public int DayId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public TimeOfDay? TaskToD { get; set; }

        public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public ICollection<Person> Persons { get; set; }
        public Day Day { get; set; }

        public TaskItem()
        {
            TaskAssignments = new HashSet<TaskAssignment>();
            Persons = new HashSet<Person>();
        }


    }
}