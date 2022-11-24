namespace TaskDelegatingWebApp.Models
{
#nullable disable

    public class Week
    {
        public LinkedList<Day> DaysOfTheWeek { get; set; }
        public List<TaskItem> TaskQueue { get; set; }
        public List<Person> ListOfPeople { get; set; }
        

        public Week()
        {
            DaysOfTheWeek = new LinkedList<Day>();
            TaskQueue = new List<TaskItem>();
            ListOfPeople = new List<Person>();
            
            
        }

    }
}
