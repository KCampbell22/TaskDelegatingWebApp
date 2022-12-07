using NuGet.Packaging;
using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.Dtos
{
    public class DaysDto
    {
        public int DayId { get; set; }
        public string DayName { get; set; }

        public int WeekId { get; set; }
        public Week Week { get; set; }

        
        public ICollection<TaskItem> TaskItems { get; set; }




        public void SortTasksByTimeOfDay()
        {
            if (TaskItems == null || !TaskItems.Any())
            {
                throw new InvalidOperationException("The TaskItems collection is null or empty.");
            }

            var tasks = TaskItems.ToArray();

            for (int i = 1; i < tasks.Length; i++)
            {
                var key = tasks[i];
                int j = i - 1;

                while (j >= 0 && tasks[j].TimeOfDay > key.TimeOfDay)
                {
                    tasks[j + 1] = tasks[j];
                    j--;
                }

                tasks[j + 1] = key;
            }

            // Clear the TaskItems collection and add the sorted items back to it.
            TaskItems.Clear();
            foreach (var task in tasks)
            {
                TaskItems.Add(task);
            }
        }
    }


    

}
