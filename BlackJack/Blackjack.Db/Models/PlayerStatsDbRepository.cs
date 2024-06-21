using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Db.Models
{
    public class PlayerStatsDbRepository : IPlayerStatsRepository
    {
        private readonly DatabaseContext context;

        public PlayerStatsDbRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddPlayerStats(PlayerStats playerStats)
        {
            context.PlayersStats.Add(playerStats);
            context.SaveChanges();
        }
        public void AddBlackjack(int id)
        {
            var stats = GetPlayerStats(id);
            if (stats == null)
            {
                AddPlayerStats(new PlayerStats
                {
                    Wins = 1,
                    TotalGames = 1,
                    Blackjacks = 1,
                    Losses = 0
                });
            }
            else
            {
                stats.Blackjacks += 1;
                stats.Wins += 1;
                stats.TotalGames += 1;
            }
            context.SaveChanges();

        }

        public void AddLosses(int id)
        {
            var stats = GetPlayerStats(id);
            if (stats == null)
            {
                AddPlayerStats(new PlayerStats
                {
                    Wins = 0,
                    TotalGames = 1,
                    Blackjacks = 0,
                    Losses = 1
                });
            }
            else
            {
                stats.Losses += 1;
                stats.TotalGames += 1;
            }
            context.SaveChanges();
        }

       

        public void AddWins(int id)
        {
            var stats = GetPlayerStats(id);
            if (stats == null)
            {
                AddPlayerStats(new PlayerStats
                {
                    Wins = 1,
                    TotalGames = 1,
                    Blackjacks = 0,
                    Losses = 0
                });
            }
            else
            {
                stats.Wins += 1;
                stats.TotalGames += 1;
            }
            context.SaveChanges();

        }

        public PlayerStats GetPlayerStats(int id)
        {
           return context.PlayersStats.FirstOrDefault(p => p.Id == id);
        }
    }
}
