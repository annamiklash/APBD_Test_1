using System;
using System.Collections.Generic;

namespace APBD_Test_1.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string TaskType { get; set; }

        public Task()
        {
        }

        public Task(int id, string name, string description, DateTime deadline, string taskType)
        {
            Id = id;
            Name = name;
            Description = description;
            Deadline = deadline;
            TaskType = taskType;
        }
    }
}