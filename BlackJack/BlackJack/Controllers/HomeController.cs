using Microsoft.AspNetCore.Mvc;
using BlackJack.Models;

namespace BlackJack.Controllers
{
    public class HomeController : Controller
    {
        private IGame game { get; set; }
        public HomeController(IGame g) => game = g;

        public IActionResult Game() => View(game);
        public IActionResult Index() => View();

        [HttpPost("Deal")]
        public IActionResult Deal()
        {
            var result = game.Deal();

            if (result == Models.Game.Result.Shuffling)
            {
                TempData["message"] = "Shuffling. Press Deal to continue.";
                TempData["background"] = "info";
            }
            else if (result == Models.Game.Result.PlayerBlackJack)
            {
                TempData["message"] = "Blackjack! You win!";
                TempData["background"] = "success";
            }
            else if (result == Models.Game.Result.DealerBlackJack)
            {
                TempData["message"] = "Dang! Dealer got a Blackjack! You lose.";
                TempData["background"] = "danger";
            }
            else if (result == Models.Game.Result.DoubleBlackJack)
            {
                TempData["message"] = "Push";
                TempData["background"] = "info";
            }

            return RedirectToAction("Game");

        }

        [HttpPost("Hit")]
        public IActionResult Hit()
        {
            var result = game.Hit();

            if (result == Models.Game.Result.Shuffling)
            {
                TempData["message"] = "Shuffling. Press Hit to continue.";
                TempData["background"] = "info";
            }
            else if (result == Models.Game.Result.PlayerBust)
            {
                TempData["message"] = "BUST! You lose.";
                TempData["background"] = "danger";
            }

            return RedirectToAction("Game");
        }

        [HttpPost("Stand")]
        public IActionResult Stand()
        {
            var result = game.Stand();

            if (result == Models.Game.Result.Shuffling)
            {
                TempData["message"] = "Shuffling. Press Hit to continue.";
                TempData["background"] = "info";
            }
            else if (result == Models.Game.Result.Continue)
            {
                TempData["message"] = "Dealer needs another card. Hit Stand to continue.";
                TempData["background"] = "info";
            }
            else if (result == Models.Game.Result.DealerBust)
            {
                TempData["message"] = "Dealer BUST! You win!";
                TempData["background"] = "success";
            }
            else if (result == Models.Game.Result.DealerWin)
            {
                TempData["message"] = "You lose.";
                TempData["background"] = "danger";
            }
            else if (result == Models.Game.Result.PlayerWin)
            {
                TempData["message"] = "You win!";
                TempData["background"] = "success";
            }
            else if (result == Models.Game.Result.Push)
            {
                TempData["message"] = "PUSH";
                TempData["background"] = "info";
            }

            return RedirectToAction("Game");

        }
    }
}
