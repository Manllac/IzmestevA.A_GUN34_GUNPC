using System;
using System.IO;
using Newtonsoft.Json;

namespace CasinoGame
{
    public class FileSystemSaveLoadService<T> : ISaveLoadService<T>
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

        public void SaveData(T data, string identifier)
        {
            string filePath = Path.Combine(_path, $"{identifier}.txt");
            try
            {
                string serializedData = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, serializedData);
                Console.WriteLine($"Data saved to {filePath}: {serializedData}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
                throw;
            }
        }

        public T LoadData(string identifier)
        {
            string filePath = Path.Combine(_path, $"{identifier}.txt");
            if (File.Exists(filePath))
            {
                try
                {
                    string jsonData = File.ReadAllText(filePath);
                    Console.WriteLine($"Data loaded from {filePath}: {jsonData}");
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing data: {ex.Message}");
                    return default(T);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error loading data: {ex.Message}");
                    return default(T);
                }
            }
            Console.WriteLine($"File {filePath} does not exist, returning default value.");
            return default(T);
        }
    }
}