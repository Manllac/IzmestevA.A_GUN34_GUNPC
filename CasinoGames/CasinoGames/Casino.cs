using System;

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

            while (gameInProgress)
            {
                Console.WriteLine($"Your current bank: {_player.Bank}");
                Console.WriteLine("Choose a game:\n1. Blackjack\n2. Dice game");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    var game = new Blackjack(_player, 100);
                    game.OnWin += () => Console.WriteLine("You win!");
                    game.OnLoose += () => Console.WriteLine("You lose!");
                    game.OnDraw += () => Console.WriteLine("It's a draw!");
                    game.PlayGame();
                }
                else if (choice == "2")
                {
                    var game = new DiceGame(_player, 100, 2, 1, 6);
                    game.OnWin += () => Console.WriteLine("You win!");
                    game.OnLoose += () => Console.WriteLine("You lose!");
                    game.OnDraw += () => Console.WriteLine("It's a draw!");
                    game.PlayGame();
                }

                Console.WriteLine("Do you want to play again? (y/n)");
                if (Console.ReadLine().ToLower() != "y") break;
            }

            SaveProfile();
            Console.WriteLine("Goodbye!");
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
            _saveLoadService.SaveData(_player.Name, "playerProfile");
        }
    }
}
