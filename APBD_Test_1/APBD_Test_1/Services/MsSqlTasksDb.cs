using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APBD_Test_1.Mappers;
using APBD_Test_1.Models;

namespace APBD_Test_1.Services
{
    public class MsSqlTasksDb : ITasksDb
    {
        private const string CONNECTION_DATA_STRING =
            "Data Source=db-mssql;Initial Catalog=s18458;Integrated Security=True";

        private const string GET_TEAM_MEMBER_WITH_TASKS_BY_ID =
            "select t.Name as task_name, tt.Name as type_name, t.Description, t.Deadline, p.Name as project_name, t.IdTask" +
            " from dbo.TeamMember tm" +
            " JOIN dbo.Task t on tm.IdTeamMember = t.IdAssignedTo" +
            " JOIN dbo.TaskType tt on t.IdTaskType = tt.IdTaskType" +
            " JOIN dbo.Project p on t.IdProject = p.IdProject" +
            " where tm.IdTeamMember = @teamMemberId ORDER BY t.Deadline;";

        private const string GET_MEMBER_INFO =
            "Select tm.IdTeamMember, tm.FirstName, tm.LastName, tm.Email, p.Name from dbo.TeamMember tm" +
            " JOIN dbo.Task t on tm.IdTeamMember = t.IdAssignedTo" +
            " JOIN dbo.Project p ON t.IdProject = p.IdProject" +
            " where IdTeamMember =  @idmember";
        
        

        private const string TEAM_MEMBER_EXISTS =
            "select COUNT(*) as id_exists from dbo.TeamMember where IdTeamMember = @teamMemberId ";
        
        private const string PROJECT_EXISTS = 
            "select COUNT(*) as id_exists from dbo.Project where IdProject = @projectId ";

        private const string GET_PROJECT_TASKS =
            "select t.IdTask from dbo.Task t JOIN dbo.Project p on t.IdProject = p.IdProject where p.IdProject = @IdProject;";

        private const string DELETE_TASKS = "Delete from dbo.Task where IdProject = @id";

        private const string DELETE_PROJECT =
            "Delete from dbo.Project where IdProject = @projectId";
        
        
        
        
        public TaskResponse GetTeamMemberWithTasks(int teamMemberId)
        {
            try
            {
                TeamMember teamMember = GetMemberInfo(teamMemberId);
                List<Task> taskList = new List<Task>();
                Task task = null;

                using (SqlConnection sqlConnection = new SqlConnection(CONNECTION_DATA_STRING))
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = GET_TEAM_MEMBER_WITH_TASKS_BY_ID;

                    sqlCommand.Parameters.Add("@teamMemberId", SqlDbType.Int, 6);
                    sqlCommand.Parameters["@teamMemberId"].Value = teamMemberId;

                    sqlConnection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        task = TaskMapper.MapToTask(dataReader);
                        taskList.Add(task);
                    }

                    dataReader.Close();
                    
                    return  new TaskResponse(teamMember, taskList);
                }
            }

            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        private TeamMember GetMemberInfo(int teamMemberId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(CONNECTION_DATA_STRING))
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = GET_MEMBER_INFO;

                    sqlCommand.Parameters.Add("@idmember", SqlDbType.Int, 6);
                    sqlCommand.Parameters["@idmember"].Value = teamMemberId;
                
                    sqlConnection.Open();


                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        return TeamMemberMapper.MapToTeamMember(reader);
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public void DeleteProject(int projectId)
        {
            try
            {
                List<int> projectTaskIds = new List<int>();
                
                using (SqlConnection sqlConnection = new SqlConnection(CONNECTION_DATA_STRING))
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = GET_PROJECT_TASKS;

                    sqlCommand.Parameters.Add("@IdProject", SqlDbType.Int, 10);
                    sqlCommand.Parameters["@IdProject"].Value = projectId;
                    sqlCommand.Connection = sqlConnection;

                    sqlConnection.Open();
                    var transaction = sqlConnection.BeginTransaction();
                    sqlCommand.Transaction = transaction;

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        int taskId = Convert.ToInt32(dataReader["IdTask"].ToString());
                        projectTaskIds.Add(taskId);
                    }

                    dataReader.Close();

                    if (projectTaskIds.Count > 0)
                    {
                        sqlCommand.CommandText = DELETE_TASKS;

                        sqlCommand.Parameters.Add("@id", System.Data.SqlDbType.Int, 10);
                        sqlCommand.Parameters["@id"].Value = projectId;
                        
                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlCommand.CommandText = DELETE_PROJECT;

                    sqlCommand.Parameters.Add("@projectId", System.Data.SqlDbType.Int, 10);
                    sqlCommand.Parameters["@projectId"].Value = projectId;

                    sqlCommand.ExecuteNonQuery();
                    
                    transaction.Commit();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
               
            }
        }

        public bool ProjectExists(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(CONNECTION_DATA_STRING))
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = PROJECT_EXISTS;
                
                sqlCommand.Parameters.Add("@projectId", SqlDbType.Int, 10);
                sqlCommand.Parameters["@projectId"].Value = id;
                
                sqlConnection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader(); 
                while (dataReader.Read())
                {
                    var exists = dataReader["id_exists"].ToString();
                    if (exists == "1")
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool TeamMemberExists(int index)
        {
            using (SqlConnection sqlConnection = new SqlConnection(CONNECTION_DATA_STRING))
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = TEAM_MEMBER_EXISTS;
                
                sqlCommand.Parameters.Add("@teamMemberId", SqlDbType.Int, 10);
                sqlCommand.Parameters["@teamMemberId"].Value = index;
                
                sqlConnection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader(); 
                while (dataReader.Read())
                {
                    var exists = dataReader["id_exists"].ToString();
                    if (exists == "1")
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}