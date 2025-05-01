using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interface;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTask createTask)
        {
            var result = await _taskService.Create(createTask);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, UpdateStatusTask updateStatusTask)
        {
            updateStatusTask.Id = id;
            var result = await _taskService.UpdateStatus(updateStatusTask);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _taskService.GetById(id);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasksViewModel = _taskService.GetAll();
            return Ok(tasksViewModel);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _taskService.Delete(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return NoContent();
        }
    }
}
