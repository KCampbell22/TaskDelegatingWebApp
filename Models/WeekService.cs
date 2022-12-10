using System;
namespace TaskDelegatingWebApp.Models
{
    public class WeekService
    {
        public WeekService()
        {
            // Initialize the list of default day names.
            DefaultDayNames = new List<string>
        {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"
        };
        }

        public List<string> DefaultDayNames { get; set; }

        public IEnumerable<Day> GenerateDefaultDays()
        {
            // Generate a list of seven Day objects, each with a default DayName value.
            return DefaultDayNames.Select((name, index) => new Day
            {
                DayId = index + 1,
                DayName = name
            });
        }
    }
}

