using System.ComponentModel.DataAnnotations;

namespace TaskDelegatingWebApp.Models
{
    public class Day
    {
        public int DayId { get; set; }
        public string? DayName { get; set; }


        public int WeekID { get; set; }
        public Week? Week { get; set; }
        public ICollection<TaskAssignment> TaskAssignments { get; set; }

        public Day()
        {
            TaskAssignments = new HashSet<TaskAssignment>();
        }
    }
}
