using System;
using TaskDelegatingWebApp.Models;
namespace TaskDelegatingWebApp.ViewModels
{
    public class WeekVIewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Day> Days { get; set; }
        public IEnumerable<TaskItem> Tasks { get; set; }
        public IEnumerable<Person> People { get; set; }


        public WeekVIewModel()
        {
            Days = new List<Day>();
            Tasks = new List<TaskItem>();
            People = new List<Person>();
        }



        public WeekVIewModel(Week week, IEnumerable<TaskItem> taskItems, IEnumerable<Person> people)
        {
            Id = week.Id;
            Name = week.WeekName;
            Days = week.Days;
            Tasks = taskItems;
            People = people;
        }
    }
}

