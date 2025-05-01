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
            try
            {
                var result = await _taskService.Create(createTask);

                return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro ao criar a tarefa.", Details = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, UpdateStatusTask updateStatusTask)
        {
            try
            {

                updateStatusTask.Id = id;
                var result = await _taskService.UpdateStatus(updateStatusTask);
                if (!result.IsSuccess)
                {
                    return NotFound(result);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro ao criar a tarefa.", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _taskService.GetById(id);
                if (!result.IsSuccess)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro ao criar a tarefa.", Details = ex.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasksViewModel = _taskService.GetAll();
                return Ok(tasksViewModel);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro ao criar a tarefa.", Details = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _taskService.Delete(id);
                if (!result.IsSuccess)
                {
                    return NotFound(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro ao criar a tarefa.", Details = ex.Message });
            }

        }
    }
}
