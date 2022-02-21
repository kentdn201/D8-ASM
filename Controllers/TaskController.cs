using System.Linq;
using D8.Models;
using D8.Services;
using Microsoft.AspNetCore.Mvc;

namespace D8.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;

    private readonly ITaskService _taskService;

    public TaskController(ILogger<TaskController> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }

    [HttpGet]
    public IEnumerable<Models.Task> GetAll()
    {
        return _taskService.GetAll().AsEnumerable();
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetOne(Guid id)
    {
        var task = _taskService.GetOne(id);
        if (task == null) return NotFound();

        return new JsonResult(task);
    }

    [HttpPost]
    public Models.Task Add(TaskCreateModel model)
    {
        var task = new Models.Task
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Description = model.Description,
            Completed = false
        };
        return _taskService.Add(task);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult Edit(Guid id, TaskUpdateModel model)
    {
        var task = _taskService.GetOne(id);
        if (task == null) return NotFound();

        task.Title = model.Title;
        task.Description = model.Description;
        task.Completed = false;

        var result = _taskService.Edit(task);
        return new JsonResult(result);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_taskService.Exists(id)) return NotFound();

        _taskService.Remove(id);

        return Ok();
    }

    [HttpPost]
    [Route("mutiple")]
    public List<Models.Task> AddMultiple(List<TaskCreateModel> models)
    {
        var tasks = new List<Models.Task>();
        foreach (var model in models)
        {
            tasks.Add(new Models.Task
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                Completed = false
            });
        }
        return _taskService.Add(tasks);
    }

    [HttpDelete]
    [Route("delete-mutiple")]
    public IActionResult DeleteMutiple(List<Guid> ids)
    {
        _taskService.Remove(ids);
        return Ok();
    }
}