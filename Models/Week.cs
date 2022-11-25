﻿namespace TaskDelegatingWebApp.Models
{
    public class Week
    {
        public DateOnly WeekStart { get; set; }
        public DateOnly WeekEnd { get; set; }
        public ICollection<Day> Days { get; set; }
    }
}
