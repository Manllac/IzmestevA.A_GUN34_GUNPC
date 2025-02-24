using System;

namespace CasinoGame
{
    public class Casino : IGame
    {
        private Player _player;
        private ISaveLoadService<Player> _saveLoadService;

        public Casino(ISaveLoadService<Player> saveLoadService)
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
                Console.WriteLine("Choose a game:");
                Console.WriteLine("1. Game 1 (Blackjack)");
                Console.WriteLine("2. Game 2 (Dice Game)");

                string choice = Console.ReadLine();

                if (choice == "1" || choice == "2")
                {
                    int bet = GetPlayerBet();
                    if (bet <= 0)
                    {
                        Console.WriteLine("Invalid bet. Bet must be greater than 0. Please try again.");
                        continue;
                    }

                    try
                    {
                        if (choice == "1")
                            PlayBlackjack(bet);
                        else
                            PlayDiceGame(bet);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose again.");
                }

                Console.WriteLine("Do you want to play again? (y/n)");
                if (Console.ReadLine().ToLower() != "y") gameInProgress = false;
            }

            SaveProfile();
            Console.WriteLine("Goodbye!");
        }

        private int GetPlayerBet()
        {
            Console.WriteLine($"Enter your bet (max: {_player.Bank}):");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int bet) && bet > 0 && bet <= _player.Bank)
            {
                return bet;
            }
            return -1; 
        }

        private void PlayBlackjack(int bet)
        {
            var game = new Blackjack(_player, bet);
            game.OnWin += () => Console.WriteLine("You win!");
            game.OnLoose += () => Console.WriteLine("You lose!");
            game.OnDraw += () => Console.WriteLine("It's a draw!");
            game.PlayGame();
        }

        private void PlayDiceGame(int bet)
        {
            var game = new DiceGame(_player, bet);
            game.OnWin += () => Console.WriteLine("You win!");
            game.OnLoose += () => Console.WriteLine("You lose!");
            game.OnDraw += () => Console.WriteLine("It's a draw!");
            game.PlayGame();
        }

        private string LoadProfile()
        {
            Player profile = _saveLoadService.LoadData("playerProfile");
            Console.WriteLine($"Raw profile loaded: {Newtonsoft.Json.JsonConvert.SerializeObject(profile)}");
            if (profile == null)
            {
                Console.WriteLine("Enter your name:");
                string profileName = Console.ReadLine();
                return profileName;
            }
            _player = profile;
            Console.WriteLine($"Loaded profile: Name = {_player.Name}, Bank = {_player.Bank}");
            return _player.Name;
        }

        private void SaveProfile()
        {
            Console.WriteLine($"Saving profile before write: Name = {_player.Name}, Bank = {_player.Bank}");
            _saveLoadService.SaveData(_player, "playerProfile");
            Console.WriteLine($"Profile saved successfully.");
        }
    }
}