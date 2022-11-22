namespace TaskDelegatingWebApp.Models
{
#nullable disable

    public class Week
    {
        public int Id { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }

        public ICollection<Day> Days { get; set; }

        
    }
}
