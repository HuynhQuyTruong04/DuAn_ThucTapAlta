using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.DTO.WorkGroup;
using DuAn_ThucTapAlta.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DuAn_ThucTapAlta.Services
{
    public class WorkGroupService : IWorkGroupService
    {
        private readonly ApplicationDBContext _context;

        public WorkGroupService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<WorkGroup> GetWorkGroupByIdAsync(int id)
        {
            return await _context.WorkGroups.FirstOrDefaultAsync(s => s.GroupId == id);
        }

        public async Task<IEnumerable<WorkGroup>> GetAllWorkGroupsAsync()
        {
            return await _context.WorkGroups.ToListAsync();
        }

        public async Task<WorkGroup> CreateWorkGroupAsync(WorkGroup workGroup)
        {
            await _context.WorkGroups.AddAsync(workGroup);
            await _context.SaveChangesAsync();
            return workGroup;
        }

        public async Task<WorkGroup> UpdateWorkGroupAsync(int id, UpdateWorkGroupRequestDTO updateDto)
        {
            var existingWorkGroup = await _context.WorkGroups.FirstOrDefaultAsync(x => x.GroupId == id);

            if (existingWorkGroup == null)
            {
                return null;
            }

            existingWorkGroup.GroupName = updateDto.GroupName;
            existingWorkGroup.Member = updateDto.Member;
            existingWorkGroup.CreateDate = updateDto.CreateDate;
            existingWorkGroup.CreatedBy =  updateDto.CreatedBy;

            await _context.SaveChangesAsync();
            return existingWorkGroup;
        }

        public async Task<bool> DeleteWorkGroupAsync(int id)
        {
            var workGroup = await _context.WorkGroups.FirstOrDefaultAsync(x => x.GroupId == id);
            if (workGroup == null)
            {
                return false;
            }
            _context.WorkGroups.Remove(workGroup);
            await _context.SaveChangesAsync();
            return true;
        }

        //kiem tra email dung dinh dang @vietjetair.com
        public bool ValidateEmailDomain(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@vietjetair\.com$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
