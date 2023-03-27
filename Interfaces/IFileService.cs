using System.Collections.Generic;
using TestTask.Models;

namespace TestTask.Interfaces
{
    /// <summary>
    /// Интерфейс для работы файлами
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Метод для чтения файлов и извлечения из него пользователей
        /// </summary>
        /// <param name="fileNames">Файлы</param>
        /// <returns>Все пользователи с их статистикой</returns>
        List<User> Open(List<string> fileNames);

        /// <summary>
        /// Метод для сохранения всех пользователей в файл
        /// </summary>
        /// <param name="filename">Файл для сохранения</param>
        /// <param name="usersList">Список пользователей, которых надо сохранить</param>
        void Save(string filename, List<User> usersList);

        /// <summary>
        /// Метод для сохранения пользователя в файл
        /// </summary>
        /// <param name="filename">Файл для сохранения</param>
        /// <param name="user">Пользователь, которого надо сохранить</param>
        void Save(string filename, User user);
    }
}
