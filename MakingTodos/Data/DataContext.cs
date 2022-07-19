using Microsoft.EntityFrameworkCore;

namespace MakingTodos.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Notes> Notess { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
