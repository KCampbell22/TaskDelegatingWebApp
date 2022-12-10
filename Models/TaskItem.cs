using System;
namespace TaskDelegatingWebApp.Models
{
    public class TaskItem
    {
        public int TaskItemId { get; set; }
        public string TaskName { get; set; }
        public int PersonId { get; set; }
        public int DayId { get; set; }

        public Person AssignedPerson { get; set; }
        public Day AssignedDay { get; set; }
    }
}

