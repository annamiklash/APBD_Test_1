using System.Collections.Generic;

namespace APBD_Test_1.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Task> tasks { get; set; }

        public TeamMember()
        {
        }

        public TeamMember(int id, string firstName, string lastName, string email, List<Task> tasks)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            this.tasks = tasks;
        }
    }
}