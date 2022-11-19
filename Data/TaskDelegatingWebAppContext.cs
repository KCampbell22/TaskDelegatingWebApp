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
        public DbSet<Week> Weeks { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<Day> Days { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<TaskAssignment>(entity =>
            {
                entity.HasKey(c => new { c.TaskItemId, c.PersonId });
                entity.ToTable("TaskAssignment");
            });
        }
    }
}
