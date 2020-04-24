using System;
using System.Data.SqlClient;
using APBD_Test_1.Models;

namespace APBD_Test_1.Mappers
{
    public class TaskMapper
    
    {
        public static Task MapToTask(SqlDataReader dataReader)
        {
            return new Task
            {
                Id = Convert.ToInt32(dataReader["IdTask"].ToString()),
                Name = dataReader["task_name"].ToString(),
                Type = dataReader["type_name"].ToString(),
                Description = dataReader["Description"].ToString(),
                Deadline = Convert.ToDateTime(dataReader["Deadline"].ToString()),
                ProjectName = dataReader["project_name"].ToString()
            };
        }
    }
}