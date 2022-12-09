namespace TaskDelegatingWebApp.Models
{
    public class Week
    {
        public int Id { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public string WeekName { get; set; }

        public ICollection<Day> Days { get; set; }

       

        public IEnumerator<Day> GetEnumerator()
        {
            foreach (var day in Days)
            {
                yield return day;
            }
        }




    }
}
