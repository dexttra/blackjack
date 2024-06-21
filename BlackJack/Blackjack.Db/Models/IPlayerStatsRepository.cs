using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Db.Models
{
    public interface IPlayerStatsRepository
    {
        void AddPlayerStats(PlayerStats playerStats);
        PlayerStats GetPlayerStats(int id);
        void AddWins(int id);
        void AddLosses(int id);
        void AddBlackjack(int id);
        
    }
}
