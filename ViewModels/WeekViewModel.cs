using TaskDelegatingWebApp.Models;
using TaskDelegatingWebApp.Dtos;

namespace TaskDelegatingWebApp.ViewModels
{
    public class WeekViewModel
    {
        public IEnumerable<Day> Days { get; set; }
        public IEnumerable<TaskItem> Tasks { get; set; }
        public IEnumerable<Person> People { get; set; }
        public IEnumerable<Week> Week { get; set; }
       
    }
}
