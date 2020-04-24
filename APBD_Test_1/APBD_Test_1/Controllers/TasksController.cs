using System.Data.SqlClient;
using APBD_Test_1.Models;
using APBD_Test_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Test_1.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {

        private readonly ITasksDb _tasksDb;

        public TasksController(ITasksDb tasksDb)
        {
            _tasksDb = tasksDb;
        }

        
        [HttpGet("{id}")]
        public IActionResult GetTeamMemberWithTasks(int id)
        {
            if (!_tasksDb.TeamMemberExists(id))
            {
                return BadRequest("Incorrect Id");
            }
            try
            {
                TaskResponse response = _tasksDb.GetTeamMemberWithTasks(id);
                return Ok(response);
            }
            catch (SqlException e)
            {
                return NotFound(e + "Error trying to fetch data from db");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProjectWithTasks(int id)
        {
            if (!_tasksDb.ProjectExists(id))
            {
                return BadRequest("Incorrect Id");
            }
            try
            {
                _tasksDb.DeleteProject(id);
                return Ok("Project with id "+ id + " safely deleted");
            }
            catch (SqlException e)
            {
                return NotFound(e + " Error trying to fetch data from db");
            }
        }
    }
}