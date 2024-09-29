using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAn_ThucTapAlta.Services
{
    public class WorkGroupService : IWorkGroupService
    {
        public readonly ApplicationDBContext _context;

        public WorkGroupService (ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<WorkGroup> GetWorkGroupByIdAsync(int workGroupId)
        {
            return await _context.WorkGroups.FindAsync(workGroupId);
        }

        public async Task<IEnumerable<WorkGroup>> GetAllWorkGroupsAsync()
        {
            return await _context.WorkGroups.ToListAsync();
        }

        public async Task<WorkGroup> CreateWorkGroupAsync(WorkGroup workGroup)
        {
            _context.WorkGroups.Add(workGroup);
            await _context.SaveChangesAsync();
            return workGroup;
        }

        public async Task<WorkGroup> UpdateWorkGroupAsync(WorkGroup workGroup)
        {
            _context.Entry(workGroup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return workGroup;
        }
    }
}
