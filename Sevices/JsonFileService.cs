using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestTask.Interfaces;
using TestTask.Models;

namespace TestTask.Sevices
{
    /// <summary>
    /// Класс для работы с json файлами
    /// </summary>
    /// <inheritdoc cref="IFileService"/>
    public class JsonFileService : IFileService
    {
        /// <summary>
        /// Чтение из json файлов
        /// <see cref="IFileService.Open(List{string})"/>
        /// </summary>
        public List<User> Open(List<string> fileNames)
        {
            List<User> users = new List<User>();
            List<List<TestItem>> testDate = new List<List<TestItem>>();
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());

            // Извлечение данных из файлов
            foreach (string fileName in fileNames)
            {
                using (StreamReader sw = new StreamReader(fileName))
                using (JsonReader writer = new JsonTextReader(sw))
                {
                    testDate.Add(serializer.Deserialize<List<TestItem>>(writer));
                }
            }
            List<string> userNames = new List<string>();
            // Получение всех пользователей
            foreach (var a in testDate)
            {
                userNames.AddRange(a.Select(p => p.User));
                userNames = userNames.Distinct().ToList();
            }
            // Получение статистики по пользователям
            foreach (string userName in userNames)
            {
                int idDay = 1;
                // Получение статистики по пользователю
                List<Statistic> statistics = testDate.Select(list => list.FirstOrDefault(it => it.User == userName)).ToList().Where(s => s != null)
                    .Select(it => new Statistic() { Day = idDay++, Rank = it.Rank, Status = it.Status, Steps = it.Steps }).ToList();

                User user = new User(userName, statistics);
                users.Add(user);
            }

            return users;
        }

        /// <summary>
        /// Сохранение в json файл
        /// <see cref="IFileService.Save(string, List{User})"/>
        /// </summary>
        public void Save(string filename, List<User> usersList)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(filename))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, usersList);
            }
        }

        /// <summary>
        /// Сохранение в json файл
        /// <see cref="IFileService.Save(string, User)"/>
        /// </summary>
        public void Save(string filename, User user)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(filename))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, user);
            }
        }
    }
}
