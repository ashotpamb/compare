using System.Reflection.Emit;
using Compare.Models;
using Microsoft.EntityFrameworkCore;

namespace Compare.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}