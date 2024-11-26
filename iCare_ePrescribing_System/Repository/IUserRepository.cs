using iCare_ePrescribing_System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace iCare_ePrescribing_System.Repository
{
    public interface IUserRepository
    {
        Task<List<StaffMembers>> GetUsers();
        Task<StaffMembers> GetUser(string id);
        Task<StaffMembers> UpdateUser(StaffMembers User);
        Task<bool> DeleteUser(StaffMembers user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StaffMembers> GetUser(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<List<StaffMembers>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<StaffMembers> UpdateUser(StaffMembers User)
        {
            _dbContext.Update(User);
            await _dbContext.SaveChangesAsync();
            return User;
        }
        public async Task<bool> DeleteUser(StaffMembers user)
        {
            if (user == null)
            {
                // Return false if user is null
                return false;
            }

            try
            {
                _dbContext.Users.Remove(user); // Assuming Users is the DbSet for StaffMembers
                await _dbContext.SaveChangesAsync();
                return true; // Deletion was successful
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                // Optionally: you could return false here if you want to indicate failure
                return false;
            }
        }


    }
}
