using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.ViewModels
{
    public class WeekViewModel
    {
        public IEnumerable<Day> Days { get; set; }
        public IEnumerable<TaskItem> Items { get; set; }
        public IEnumerable<Person> People { get; set; }
    }
}
