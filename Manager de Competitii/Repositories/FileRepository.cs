using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Manager_de_Competitii.Repositories
{
    public class FileRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _options;

        public FileRepository(string fileName)
        {
            var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            _filePath = Path.Combine(dataDir, fileName);
            _options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
        }

        private async Task<List<T>> ReadFromFileAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }
            var json = await File.ReadAllTextAsync(_filePath);
            if (string.IsNullOrWhiteSpace(json)) return new List<T>();
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
        }

        public async Task SaveChangesAsync(List<T> entities)
        {
            var json = JsonSerializer.Serialize(entities, _options);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await ReadFromFileAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var all = await ReadFromFileAsync();
            return all.FirstOrDefault(x => x.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            var all = await ReadFromFileAsync();
            if (entity.Id == 0)
            {
                entity.Id = all.Any() ? all.Max(x => x.Id) + 1 : 1;
            }
            all.Add(entity);
            await SaveChangesAsync(all);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            var all = await ReadFromFileAsync();
            var index = all.FindIndex(x => x.Id == id);
            if (index != -1)
            {
                all[index] = entity;
                await SaveChangesAsync(all);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var all = await ReadFromFileAsync();
            var index = all.FindIndex(x => x.Id == id);
            if (index != -1)
            {
                all.RemoveAt(index);
                await SaveChangesAsync(all);
            }
        }
    }
}
