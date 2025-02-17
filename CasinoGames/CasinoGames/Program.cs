using System;

namespace CasinoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var saveLoadService = new FileSystemSaveLoadService("profile.txt");
            var casino = new Casino(saveLoadService);
            casino.StartGame();
        }
    }
}
