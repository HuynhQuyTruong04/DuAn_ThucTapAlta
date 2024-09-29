using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Services
{
    public interface IWorkGroupService
    {
        Task<WorkGroup> GetWorkGroupByIdAsync(int workGroupId);
        Task<IEnumerable<WorkGroup>> GetAllWorkGroupsAsync();
        Task<WorkGroup> CreateWorkGroupAsync(WorkGroup workGroup);
        Task<WorkGroup> UpdateWorkGroupAsync(WorkGroup workGroup);
    }
}
