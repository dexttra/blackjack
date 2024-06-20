using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Db.Models
{
    public class PlayerStats
    {
        public int Id { get; set; }

        public int Wins { get; set; } // Победы

        public int Losses { get; set; } // Поражения

        public int TotalGames { get; set; } // Общее количество игр

        public int Blackjacks { get; set; } // Количество блэкджеков
    }
}
