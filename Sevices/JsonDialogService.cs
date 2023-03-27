using Microsoft.Win32;
using System.Windows;
using TestTask.Interfaces;

namespace TestTask.Sevices
{
    /// <summary>
    /// Класс диалоговых окон для работы с json файлами
    /// </summary>
    /// <inheritdoc cref="IDialogService"/>
    internal class JsonDialogService : IDialogService
    {
        /// <summary>
        /// <see cref="IDialogService.FilePath"/>
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// <see cref="IDialogService.OpenFileDialog()"/>
        /// </summary>
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Json file (*.json)|*.json"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        /// <summary>
        /// <see cref="IDialogService.SaveFileDialog()"/>
        /// </summary>
        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Json file (*.json)|*.json"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        /// <summary>
        /// <see cref="IDialogService.ShowMessage(string)"/>
        /// </summary>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}