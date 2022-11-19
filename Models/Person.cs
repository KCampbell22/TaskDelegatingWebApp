namespace TaskDelegatingWebApp.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set;}
        public bool Thursday { get; set;}
        public bool Friday { get; set;}
        public bool Saturday { get; set;}
        public bool Sunday { get; set;}

        public ICollection<TaskAssignment> TaskAssignments { get; set;}

        public Person()
        {
            TaskAssignments = new HashSet<TaskAssignment>();
        }
    }
}
