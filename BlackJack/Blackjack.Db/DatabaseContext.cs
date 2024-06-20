using Blackjack.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Blackjack.Db
{
    public class DatabaseContext : DbContext 
    {
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
