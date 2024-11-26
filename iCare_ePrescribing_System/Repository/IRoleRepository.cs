using iCare_ePrescribing_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace iCare_ePrescribing_System.Repository
{
    public interface IRoleRepository
    {
        Task<List<IdentityRole>> GetRoles();
        Task<IdentityRole> GetRoleByNameAsync(string roleName);
    }
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<IdentityRole>> GetRoles()
        {
            return _dbContext.Roles.Where(s => s.Name != "Patient" && s.Name != "Receptionist").ToListAsync();
        }
        public async Task<IdentityRole> GetRoleByNameAsync(string roleName) // Implementation of new method
        {
            return await _dbContext.Roles.SingleOrDefaultAsync(r => r.Name == roleName);
        }

    }
}
