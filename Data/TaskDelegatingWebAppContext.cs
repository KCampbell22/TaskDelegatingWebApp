using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.Data
{
    public class TaskDelegatingWebAppContext : DbContext
    {
        public TaskDelegatingWebAppContext (DbContextOptions<TaskDelegatingWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<TaskDelegatingWebApp.Models.Day> Day { get; set; } = default!;

        public DbSet<TaskDelegatingWebApp.Models.Person> Person { get; set; }

        public DbSet<TaskDelegatingWebApp.Models.TaskItem> TaskItem { get; set; }

        public DbSet<TaskDelegatingWebApp.Models.Week> Week { get; set; }
    }
}
