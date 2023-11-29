using Compare.Models;

namespace Compare.Data
{
    public interface IUserRepository
    {
        HashSet<User> GetUsers();
        void SaveUsers(HashSet<User> users);
        Task SaveUsersAsync(List<User> users);
        Task<List<User>> GetUsersInBatchAsync(List<string> userUniq);

        bool SaveChanges();
    }
}