using System;
using System.Data.SqlClient;
using APBD_Test_1.Models;

namespace APBD_Test_1.Mappers
{
    public class TaskResponseMapper
    {
        public static TaskResponse MapToTaskResponse(SqlDataReader dataReader)
        {
            return new TaskResponse
            {
                Name = dataReader["Name"].ToString(),
                Type = dataReader["Name"].ToString(),
                Description = dataReader["Description"].ToString(),
                Deadline = Convert.ToDateTime(dataReader["Deadline"].ToString()),
                ProjectName = dataReader["Name"].ToString()
            };
        }
    }
}