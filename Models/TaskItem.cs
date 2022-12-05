using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace TaskDelegatingWebApp.Models
{

    public class TaskItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskItemId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public TimeOfDay TimeOfDay { get; set; }
        public int DayId { get; set; }
        public int PersonId { get; set; }


        public Day Day { get; set; }
        public Person Person { get; set; }
       
        



       

    }
}