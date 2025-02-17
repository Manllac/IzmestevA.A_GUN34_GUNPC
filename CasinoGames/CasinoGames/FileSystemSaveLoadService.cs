using System.IO;

namespace CasinoGame
{
    public class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private string _path;

        public FileSystemSaveLoadService(string path)
        {
            _path = path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void SaveData(string data, string identifier)
        {
            string filePath = Path.Combine(_path, $"{identifier}.txt");
            File.WriteAllText(filePath, data);
        }

        public string LoadData(string identifier)
        {
            string filePath = Path.Combine(_path, $"{identifier}.txt");
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return string.Empty;
        }
    }
}
