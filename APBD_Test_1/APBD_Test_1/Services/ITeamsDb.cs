using System.Collections.Generic;
using APBD_Test_1.Models;

namespace APBD_Test_1.Services
{
    public interface ITeamsDb
    {
        TaskResponse GetTeamMemberWithTasks(int teamMemberId);
        bool TeamMemberExists(int id);
        void DeleteProject(int projectId);
        bool ProjectExists(int id);
    }
}