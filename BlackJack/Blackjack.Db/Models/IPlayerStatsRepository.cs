using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Db.Models
{
    public interface IPlayerStatsRepository
    {
        PlayerStats GetPlayerStats();
        void UpdatePlayerStats(PlayerStats stats);
    }
}
