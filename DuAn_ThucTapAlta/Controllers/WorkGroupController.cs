using DuAn_ThucTapAlta.Models;
using DuAn_ThucTapAlta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_ThucTapAlta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkGroupController : ControllerBase
    {
        private readonly IWorkGroupService _workGroupService;

        public WorkGroupController(IWorkGroupService workGroupService)
        {
            _workGroupService = workGroupService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkGroup(int id)
        {
            var workGroup = await _workGroupService.GetWorkGroupByIdAsync(id);

            if (workGroup == null)
            {
                return NotFound("Nhóm không tồn tại! ");
            }

            return Ok(workGroup);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkGroups()
        {
            var workGroups = await _workGroupService.GetAllWorkGroupsAsync();
            return Ok(workGroups);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkGroup([FromBody] WorkGroup workGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (workGroup == null)
            {
                return BadRequest("Nhóm không hợp lệ!");
            }

            var createWorkGroup = await _workGroupService.CreateWorkGroupAsync(workGroup);

            return CreatedAtAction(nameof(GetWorkGroup), new { id = createWorkGroup.GroupId }, createWorkGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkGroup(int id, [FromBody] WorkGroup workGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workGroup.GroupId)
            {
                return BadRequest("ID không hợp lệ!");
            }

            var updateWorkGroup = await _workGroupService.UpdateWorkGroupAsync(workGroup);

            if (updateWorkGroup == null)
            {
                return NotFound("Nhóm không tồn tại!");
            }

            return Ok(updateWorkGroup);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkGroup(int id)
        {
            var isDelete = await _workGroupService.DeleteWorkGroupAsync(id);

            if (!isDelete)
            {
                return NotFound("Nhóm không tồn tại!");
            }

            return NoContent();
        }
    }
}
