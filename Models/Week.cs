namespace TaskDelegatingWebApp.Models
{
    


    public class Week
    {
        public int Id { get; set; }
        public string WeekName { get; set; }

        public ICollection<Day> Days { get; set; }

        
    }
}