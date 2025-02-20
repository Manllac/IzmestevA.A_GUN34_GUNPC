using System;
using System.Collections.Generic;

namespace CasinoGame
{
    public class Casino : IGame
    {
        private Player _player;
        private ISaveLoadService<string> _saveLoadService;

        public Casino(ISaveLoadService<string> saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void StartGame()
        {
            Console.WriteLine("Welcome to the Casino!");
            string profileName = LoadProfile();
            _player = new Player(profileName, 1000);
            bool gameInProgress = true;

            var games = new Dictionary<string, Action>
            {
                { "1", () => PlayBlackjack() },
                { "2", () => PlayDiceGame() }
            };

            while (gameInProgress)
            {
                Console.WriteLine($"Your current bank: {_player.Bank}");
                Console.WriteLine("Choose a game:");
                foreach (var game in games)
                {
                    Console.WriteLine($"{game.Key}. Game {game.Key}");
                }

                string choice = Console.ReadLine();

                if (games.ContainsKey(choice))
                {
                    games[choice]();  
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose again.");
                }

                Console.WriteLine("Do you want to play again? (y/n)");
                if (Console.ReadLine().ToLower() != "y") break;
            }

            SaveProfile();
            Console.WriteLine("Goodbye!");
        }

        private void PlayBlackjack()
        {
            var game = new Blackjack(_player, 100);
            game.OnWin += () => Console.WriteLine("You win!");
            game.OnLoose += () => Console.WriteLine("You lose!");
            game.OnDraw += () => Console.WriteLine("It's a draw!");
            game.PlayGame();
        }

        private void PlayDiceGame()
        {
            var game = new DiceGame(_player, 100);
            game.OnWin += () => Console.WriteLine("You win!");
            game.OnLoose += () => Console.WriteLine("You lose!");
            game.OnDraw += () => Console.WriteLine("It's a draw!");
            game.PlayGame();
        }

        private string LoadProfile()
        {
            string profileName = _saveLoadService.LoadData("playerProfile");
            if (string.IsNullOrEmpty(profileName))
            {
                Console.WriteLine("Enter your name:");
                profileName = Console.ReadLine();
                _saveLoadService.SaveData(profileName, "playerProfile");
            }
            return profileName;
        }

        private void SaveProfile()
        {
            string data = $"{_player.Name}:{_player.Bank}";
            _saveLoadService.SaveData(data, "playerProfile");
        }
    }
}
