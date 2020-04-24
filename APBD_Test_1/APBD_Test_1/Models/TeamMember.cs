using System.Collections.Generic;

namespace APBD_Test_1.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string ProjectName { get; set; }
        

        public TeamMember()
        {
        }

        public TeamMember(int id, string firstName, string lastName, string email, string ProjectName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ProjectName = ProjectName;

        }
    }
}