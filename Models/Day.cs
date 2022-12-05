using System.ComponentModel.DataAnnotations;
using TaskDelegatingWebApp.Controllers;
#nullable disable

namespace TaskDelegatingWebApp.Models
{
    public class Day
    {
        public int DayId { get; set; }
        public string DayName { get; set; }


        public ICollection<TaskItem> TaskItems { get; set; }
        public ICollection<Person> People { get; set; }

        public int WeekId { get; set; }
        public Week Week { get; set; }



        
    }
}
