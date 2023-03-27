namespace TestTask.Interfaces
{
    /// <summary>
    /// Интерфейс диалоговых окон для работы с файлами
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Получает или задаёт путь выбранного файла 
        /// в диалоговом окне
        /// </summary>
        /// <value> Путь выбранного файла в диалоговом окне</value>
        string FilePath { get; set; }

        /// <summary>
        /// Метод вывода сообщения
        /// </summary>
        /// <param name="message">Сообщение которое надо вывести</param>
        void ShowMessage(string message);

        /// <summary>
        /// Метод выбора файла для загрузки из диалогового окна
        /// </summary>
        /// <returns> 
        /// Успешная или неуспешная работа
        /// с диалоговым окном
        /// </returns>
        bool OpenFileDialog();

        /// <summary>
        /// Метод выбора файла для сохранения из диалогового окна
        /// </summary>
        /// <returns> 
        /// Успешная или неуспешная работа
        /// с диалоговым окном
        /// </returns>
        bool SaveFileDialog();
    }
}
