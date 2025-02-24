using System;
using System.IO;

namespace CasinoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "playerProfile.txt");
            var saveLoadService = new FileSystemSaveLoadService<Player>(dataPath);
            var casino = new Casino(saveLoadService);
            casino.StartGame();
        }
    }
}