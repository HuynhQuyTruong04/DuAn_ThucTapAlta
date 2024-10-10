using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.DTO.WorkGroup;
using DuAn_ThucTapAlta.Mappers;
using DuAn_ThucTapAlta.Models;
using DuAn_ThucTapAlta.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DuAn_ThucTapAlta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkGroupController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IWorkGroupService _workGroupService;

        public WorkGroupController(IWorkGroupService workGroupService, ApplicationDBContext context)
        {
            _context = context;
            _workGroupService = workGroupService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkGroup(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workGroup = await _workGroupService.GetWorkGroupByIdAsync(id);

            if (workGroup == null)
            {
                return NotFound("Không tìm thấy Group!");
            }

            return Ok(workGroup.ToWorkGroupDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkGroups()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workGroups = await _workGroupService.GetAllWorkGroupsAsync();

            var workGroupDto = workGroups.Select(s => s.ToWorkGroupDTO()).ToList();

            return Ok(workGroupDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkGroup([FromBody] CreateWorkGroupRequestDTO workGroupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (workGroupDto == null)
            {
                return BadRequest("Group không hợp lệ!");
            }

            var workGroupModel = workGroupDto.ToWorkGroupFromCreateDTO();

            await _workGroupService.CreateWorkGroupAsync(workGroupModel);

            return CreatedAtAction(nameof(GetWorkGroup), new { id = workGroupModel.GroupId }, workGroupModel.ToWorkGroupDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkGroup(int id, [FromBody] UpdateWorkGroupRequestDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workGroupModel = await _workGroupService.UpdateWorkGroupAsync(id, updateDto);

            if (workGroupModel == null)
            {
                return NotFound("Group không tồn tại.");
            }

            return Ok(workGroupModel.ToWorkGroupDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkGroup(int id)
        {
            var isDeleted = await _workGroupService.DeleteWorkGroupAsync(id);

            if (!isDeleted)
            {
                return NotFound("Group không tồn tại.");
            }

            return NoContent();
        }
    }
}
