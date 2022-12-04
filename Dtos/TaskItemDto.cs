using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.Dtos
{
    public class TaskItemDto
    {
        public int TaskItemId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TimeOfDay { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int DayId { get; set; }
        public Day Day { get; set; }

        
    }
}
