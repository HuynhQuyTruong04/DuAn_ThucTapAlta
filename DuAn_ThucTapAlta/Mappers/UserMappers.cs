using DuAn_ThucTapAlta.DTO.Users;
using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Mappers
{
    public static class UserMappers
    {
        public static UserDTO ToUserDTO(this User userModel)
        {
            return new UserDTO
            {
                UserId = userModel.UserId,
                Email = userModel.Email,
                PassWord = userModel.PassWord,
                GroupId = userModel.GroupId
            };
        }

        public static User ToUserFromCreateDTO(this CreateUserRequestDTO userDto)
        {
            return new User
            {
                UserId = userDto.UserId,
                Email = userDto.Email,
                PassWord = userDto.PassWord,
                GroupId = userDto.GroupId
            };
        }
    }
}
