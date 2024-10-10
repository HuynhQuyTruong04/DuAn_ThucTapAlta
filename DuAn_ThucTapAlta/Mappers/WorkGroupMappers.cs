using DuAn_ThucTapAlta.DTO.WorkGroup;
using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Mappers
{
    public static class WorkGroupMappers
    {
        public static WorkGroupDTO ToWorkGroupDTO(this WorkGroup workGroupModel)
        {
            return new WorkGroupDTO
            {
                GroupId = workGroupModel.GroupId,
                GroupName = workGroupModel.GroupName,
                Member = workGroupModel.Member,
                CreateDate = workGroupModel.CreateDate,
                CreatedBy = workGroupModel.CreatedBy
            };
        }

        public static WorkGroup ToWorkGroupFromCreateDTO(this CreateWorkGroupRequestDTO workGroupDto)
        {
            return new WorkGroup
            {
                GroupId = workGroupDto.GroupId,
                GroupName = workGroupDto.GroupName,
                Member = workGroupDto.Member,
                CreateDate = workGroupDto.CreateDate,
                CreatedBy = workGroupDto.CreatedBy
            };
        }
    }
}
