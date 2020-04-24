using System;
using System.Data.SqlClient;
using APBD_Test_1.Models;

namespace APBD_Test_1.Mappers
{
    public class TeamMemberMapper
    {
        public static TeamMember MapToTeamMember(SqlDataReader reader)
        {
            return new TeamMember
            {
                Id = Convert.ToInt32(reader["IdTeamMember"].ToString()),
                LastName = reader["LastName"].ToString(),
                FirstName = reader["FirstName"].ToString(),
                Email = reader["Email"].ToString(),
                ProjectName = reader["Name"].ToString()
            };
        }
    }
}