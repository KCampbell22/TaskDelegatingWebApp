using System.ComponentModel.DataAnnotations;
#nullable disable

namespace TaskDelegatingWebApp.Models
{
    public class Day
    {
        public int DayId { get; set; }
        public string DayName { get; set; }

        public int WeekID { get; set; }
        public Week Week { get; set; }

        public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public ICollection<Person> People { get; set; }

        
    }
}
