using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.Dtos
{
    public class DaysDto
    {
        public int DayId { get; set; }
        public string DayName { get; set; }

        public int WeekId { get; set; }
        public Week Week { get; set; }

        public ICollection<Person> People { get; set; }
        
        public ICollection<TaskItem> TaskItems { get; set; }
    }
}
