using Microsoft.AspNetCore.Mvc;
using BlackJack.Models;
using Blackjack.Db.Models;

namespace BlackJack.Controllers
{
    public class HomeController : Controller
    {
        private IGame game { get; set; }
        private readonly IPlayerStatsRepository playerStatsRepository;
        
        public HomeController(IGame g, IPlayerStatsRepository playerStatsRepository)
        {
            game = g;
            this.playerStatsRepository = playerStatsRepository;
        }

        public IActionResult Index() => View();
        public IActionResult Game() => View(game);

        [HttpPost]
        public IActionResult Deal()
        {
            var result = game.Deal();

            if (result == Models.Game.Result.Shuffling)
            {
                TempData["message"] = "Перемешивание. Нажмите Deal, чтобы продолжить.";
                TempData["background"] = "info";
            }
            else if (result == Models.Game.Result.PlayerBlackJack)
            {
                TempData["message"] = "Blackjack! Вы выиграли!";
                TempData["background"] = "success";
                playerStatsRepository.AddBlackjack(Constants.Player);


            }
            else if (result == Models.Game.Result.DealerBlackJack)
            {
                TempData["message"] = "Увы! У дилера Blackjack! Вы проиграли.";
                TempData["background"] = "danger";
                playerStatsRepository.AddLosses(Constants.Player);
            }
            else if (result == Models.Game.Result.DoubleBlackJack)
            {
                TempData["message"] = "Push";
                TempData["background"] = "info";
            }

            return RedirectToAction("Game");
        }

        [HttpPost]
        public IActionResult Hit()
        {
            var result = game.Hit();

            if (result == Models.Game.Result.Shuffling)
            {
                TempData["message"] = "Перемешивание. Нажмите Hit, чтобы продолжить.";
                TempData["background"] = "info";
            }
            else if (result == Models.Game.Result.PlayerBust)
            {
                TempData["message"] = "Перебор! Вы проиграли.";
                TempData["background"] = "danger";
                playerStatsRepository.AddLosses(Constants.Player);
            }

            return RedirectToAction("Game");
        }

        [HttpPost]
        public IActionResult Stand()
        {
            var result = game.Stand();

            if (result == Models.Game.Result.Shuffling)
            {
                TempData["message"] = "Перемешивание. Нажмите Hit, чтобы продолжить.";
                TempData["background"] = "info";
            }
            else if (result == Models.Game.Result.Continue)
            {
                TempData["message"] = "Дилеру нужна еще одна карта. Нажмите Stand, чтобы продолжить.";
                TempData["background"] = "info";
            }
            else if (result == Models.Game.Result.DealerBust)
            {
                TempData["message"] = "Перебор у дилера! Вы выиграли!";
                TempData["background"] = "success";
                playerStatsRepository.AddWins(Constants.Player);
            }
            else if (result == Models.Game.Result.DealerWin)
            {
                TempData["message"] = "Вы проиграли.";
                TempData["background"] = "danger";
                playerStatsRepository.AddLosses(Constants.Player);
            }
            else if (result == Models.Game.Result.PlayerWin)
            {
                TempData["message"] = "Вы выиграли!";
                TempData["background"] = "success";
                playerStatsRepository.AddWins(Constants.Player);
            }
            else if (result == Models.Game.Result.Push)
            {
                TempData["message"] = "Push";
                TempData["background"] = "info";
            }

            return RedirectToAction("Game");
        }

        public IActionResult NewGame()
        {
            game = new Game(new HttpContextAccessor());
            game.Deal();
            return RedirectToAction("Game");
        }

        public IActionResult Profile()
        {
            var stats = playerStatsRepository.GetPlayerStats(Constants.Player);
			return View(stats);
        }
    }
}