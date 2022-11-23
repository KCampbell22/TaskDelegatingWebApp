using System.ComponentModel;
using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.ViewModels
{
#nullable disable
    public class PersonViewModel
    {
        [DisplayName("Name")]
        public string PersonName { get; set; }


        public ICollection<TaskItem> AssignedTasks { get; set; }

    }
}
