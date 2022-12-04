using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.Dtos
{
    public class WeeksDto
    {
        public int Id { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }

        public ICollection<Day> Days { get; set; }
        public ICollection<Person> People { get; set; }
    }

}
