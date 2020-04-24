using System;
using System.Collections.Generic;

namespace APBD_Test_1.Models
{
    public class TaskResponse
    {
        public TeamMember TeamMember { get; set; }
        public List<Task> Tasks { get; set; }

        public TaskResponse()
        {
        }

        public TaskResponse(TeamMember teamMember, List<Task> tasks)
        {
            TeamMember = teamMember;
            Tasks = tasks;
        }
    }
}