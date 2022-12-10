
using System;
namespace TaskDelegatingWebApp.Models
{
    public class Day
    {
        public int DayId { get; set; }
        public string DayName { get; set; }
        public int WeekId { get; set; }

        public Week Week { get; set; }
        public ICollection<TaskItem> TaskItems { get; set; }
        public ICollection<Person> AssignedPeople { get; set; }

        public void SortPeople()
        {
            // Create a new empty LinkedList
            LinkedList<Person> sortedPeople = new LinkedList<Person>();

            // Iterate over each person in the AssignedPeople list
            foreach (var person in AssignedPeople)
            {
                // If the sorted list is empty, just add the first person to it
                if (sortedPeople.Count == 0)
                {
                    sortedPeople.AddFirst(person);
                }
                else
                {
                    // Otherwise, find the correct position to insert the person in the sorted list
                    // by iterating over the sorted list and comparing the person's name to the names
                    // of the people already in the list
                    LinkedListNode<Person> currentNode = sortedPeople.First;
                    while (currentNode != null && string.Compare(currentNode.Value.PersonName, person.PersonName) < 0)
                    {
                        currentNode = currentNode.Next;
                    }

                    // Once we've found the correct position, use the AddAfter method to insert the
                    // person into the list
                    if (currentNode == null)
                    {
                        // If the current node is null, that means we've reached the end of the list,
                        // so we just add the person to the end of the list
                        sortedPeople.AddLast(person);
                    }
                    else
                    {
                        // Otherwise, insert the person after the current node
                        sortedPeople.AddAfter(currentNode, person);
                    }
                }
            }

            // Finally, assign the sorted list of people to the AssignedPeople property
            AssignedPeople = sortedPeople;
        }
    }
}

