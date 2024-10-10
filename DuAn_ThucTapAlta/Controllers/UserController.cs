using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.DTO.Users;
using DuAn_ThucTapAlta.Mappers;
using DuAn_ThucTapAlta.Models;
using DuAn_ThucTapAlta.Services;
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
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _userService.GetAllUsersAsync();

            var userDto = users.Select(s => s.ToUserDTO()).ToList();

            return Ok(userDto);
            //var users = _userService.GetAllUsersAsync.ToList();
            //return Ok(users);
        }

        // Tạo mới User
        [HttpPost]
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

            //if (!_userService.ValidateEmailDomain)
            //{
            //    return BadRequest("Email phải thuộc miền @vietjetair.com.");
            //}

            // Chuyển DTO thành Model để lưu vào database
            //var user = new User
            //{
            //    Email = userDto.Email,
            //    PassWord = userDto.PassWord
            //};

            //var createdUser = await _userService.CreateUserAsync(user);

            //return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequestDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (!_userService.ValidateEmailDomain(userDto.Email))
            //{
            //    return BadRequest("Email phải thuộc miền @vietjetair.com.");
            //}

            var userModel = await _userService.UpdateUserAsync(id, updateDto);

            if (userModel == null)
            {
                return NotFound("Người dùng không tồn tại.");
            }

            return Ok(userModel.ToUserDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var isDeleted = await _userService.DeleteUserAsync(id);

            if (!isDeleted)
            {
                return NotFound("Người dùng không tồn tại.");
            }

            return NoContent();
        }
    }
}
