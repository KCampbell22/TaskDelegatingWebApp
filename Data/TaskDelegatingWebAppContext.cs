using Microsoft.EntityFrameworkCore;
using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.Data
{
    public class TaskDelegatingWebAppContext : DbContext
    {
        public TaskDelegatingWebAppContext(DbContextOptions<TaskDelegatingWebAppContext> options) : base(options) 
        {
        }

        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Day> Days { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(c => new { c.DayId, c.PersonId });
            });
            
        }
    }
}
