using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.DTO.Users;
using DuAn_ThucTapAlta.Mappers;
using DuAn_ThucTapAlta.Models;
using DuAn_ThucTapAlta.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DuAn_ThucTapAlta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IUserService _userService;

        public UserController(IUserService userService, ApplicationDBContext context)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound("Không tìm thấy User!");
            }

            return Ok(user.ToUserDTO());
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _userService.GetAllUsersAsync();

            var userDto = users.Select(s => s.ToUserDTO()).ToList();

            return Ok(userDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userDto == null)
            {
                return BadRequest("Người dùng không hợp lệ!");
            }
    
            var userModel = userDto.ToUserFromCreateDTO();

            await _userService.CreateUserAsync(userModel);

            return CreatedAtAction(nameof(GetUser), new { id = userModel.UserId }, userModel.ToUserDTO());
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequestDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = await _userService.UpdateUserAsync(id, updateDto);

            if (userModel == null)
            {
                return NotFound("Người dùng không tồn tại.");
            }

            return Ok(userModel.ToUserDTO());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]    
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound("Người dùng không tồn tại.");
            }

            var result = await _userService.DeactivateUserAsync(id);

            if (!result)
            {
                return NotFound("Không thể vô hiệu hóa người dùng!");
            }

            return Ok("Người dùng đã được vô hiệu hóa");
        }

        [HttpPut("{id}/activate")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var result = await _userService.ActivateUserAsync(id);

            if (!result)
            {
                return NotFound("Người dùng không tồn tại hoặc đã đang hoạt động.");
            }

            return Ok("Người dùng đã được kích hoạt.");
        }

        [HttpGet("inactive")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetInactiveUsers()
        {
            var inactiveUsers = await _userService.GetInactiveUsersAsync();

            if (!inactiveUsers.Any())
            {
                return NotFound("Không có người dùng nào bị vô hiệu hóa.");
            }

            var userDtos = inactiveUsers.Select(u => u.ToUserDTO()).ToList();
            return Ok(userDtos);
        }
    }
}
