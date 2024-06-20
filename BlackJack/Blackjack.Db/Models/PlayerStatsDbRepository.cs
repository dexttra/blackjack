using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Db.Models
{
    public class PlayerStatsDbRepository : IPlayerStatsRepository
    {
        private readonly DatabaseContext _context;

        public PlayerStatsDbRepository(DatabaseContext context)
        {
            _context = context;
        }

        public PlayerStats GetPlayerStats()
        {
            return _context.PlayerStats.FirstOrDefault();
        }

        public void UpdatePlayerStats(PlayerStats stats)
        {
            var existingStats = _context.PlayerStats.FirstOrDefault();

            if (existingStats == null)
            {
                _context.PlayerStats.Add(stats);
            }
            else
            {
                existingStats.Wins = stats.Wins;
                existingStats.Losses = stats.Losses;
                existingStats.TotalGames = stats.TotalGames;
                existingStats.Blackjacks = stats.Blackjacks;
            }

            _context.SaveChanges();
        }
    }
}
