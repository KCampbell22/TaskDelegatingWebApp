using System;
using System.Configuration;
using TaskDelegatingWebApp.Models;
namespace TaskDelegatingWebApp.ViewModels
{
#nullable disable

    public class PersonViewModel
    {

        public IEnumerable<TaskItem> TaskItems { get; set; }
        public IEnumerable<TaskAssignment> TaskAssignments { get; set; }
        public IEnumerable<Person>People { get; set; }
        public IEnumerable<Day> Days { get; set; }
        

    }
}

