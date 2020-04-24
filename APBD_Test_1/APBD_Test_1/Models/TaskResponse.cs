using System;

namespace APBD_Test_1.Models
{
    public class TaskResponse
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string ProjectName { get; set; }

        public TaskResponse()
        {
        }

        public TaskResponse(string name, string type, string description, DateTime deadline, string projectName)
        {
            Name = name;
            Type = type;
            Description = description;
            Deadline = deadline;
            ProjectName = projectName;
        }
    }
}