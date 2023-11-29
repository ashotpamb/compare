using Compare.Models;
using Microsoft.EntityFrameworkCore;

namespace Compare.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public HashSet<User> GetUsers()
        {
            return _dataContext.Users.ToHashSet();
        }

        public async Task<List<User>> GetUsersInBatchAsync(List<string> userUniq)
        {
            return await _dataContext.Users.Where(u => userUniq.Contains(u.Email)).ToListAsync();
        }

        public bool SaveChanges()
        {
            return _dataContext.SaveChanges() >= 0;
        }

        public void SaveUsers(HashSet<User> users)
        {
            _dataContext.Users.AddRange(users);
        }

        public async Task SaveUsersAsync(List<User> users)
        {
            await _dataContext.AddRangeAsync(users);
            await _dataContext.SaveChangesAsync();
        }
    }
}