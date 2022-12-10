namespace TaskDelegatingWebApp.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }

        public ICollection<Day> Days {get; set;}
        public ICollection<TaskItem> Tasks { get; set; }


    }
}