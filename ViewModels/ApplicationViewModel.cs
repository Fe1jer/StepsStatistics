using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using TestTask.Interfaces;
using TestTask.Models;

namespace TestTask.ViewModels
{
    /// <summary>
    /// ModelView окна отображения пользователей и статистики
    /// </summary>
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        User selectedUser;

        private readonly IFileService fileService;
        private readonly IDialogService dialogService;

        public ObservableCollection<Series> StatisticsSeriesSelectedUser { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
            Users = new ObservableCollection<User>();
            StatisticsSeriesSelectedUser = new ObservableCollection<Series>();
            InitializationUsers();
        }

        /// <summary>
        /// Заполнение таблицы тестовыми значениями
        /// </summary>
        private void InitializationUsers()
        {
            List<string> paths = GetAllPathsWithTestData();
            var users = this.fileService.Open(paths);
            foreach (var p in users)
            {
                var User = p;
                Users.Add(User);
            }
        }

        /// <summary>
        /// Получение всех путей тестовых файлов
        /// </summary>
        /// <returns>Все пути файлов</returns>
        private List<string> GetAllPathsWithTestData()
        {
            List<string> paths = new List<string>();

            try
            {
                paths = new List<string>();
                IEnumerable<string> allfiles = Directory.EnumerateFiles("..\\..\\TestData");
                foreach (string filename in allfiles)
                {
                    paths.Add(filename);
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }

            return paths;
        }

        /// <summary>
        /// Установление значений статистики для отображения на MSChart
        /// </summary>
        /// <param name="statistics">Статистика для отображения</param>
        private void SetStatistic(List<Statistic> statistics)
        {
            StatisticsSeriesSelectedUser.Clear();
            Series ds = new Series
            {
                ChartType = SeriesChartType.Spline,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 6,
                BorderWidth = 3
            };
            int i = 1;
            DataPoint dataPoint;
            foreach (var statistic in statistics)
            {
                dataPoint = CreateStatisticDataPoint(ds, statistic);
                ds.Points.Add(dataPoint);
                i++;
            }
            StatisticsSeriesSelectedUser.Add(ds);
        }

        /// <summary>
        /// Создание точки статистики
        /// </summary>
        /// <param name="series">Series, на которой создаётся точка</param>
        /// <param name="statistic">Статистика для отображения</param>
        private DataPoint CreateStatisticDataPoint(Series series, Statistic statistic)
        {
            DataPoint dataPoint = new DataPoint(series);
            dataPoint.SetValueXY(statistic.Day, statistic.Steps);
            if (statistic.Steps == selectedUser.MaxSteps)
            {
                dataPoint.Color = System.Drawing.Color.Green;
                dataPoint.MarkerSize = 8;
            }
            else if (statistic.Steps == selectedUser.MinSteps)
            {
                dataPoint.Color = System.Drawing.Color.Red;
                dataPoint.MarkerSize = 8;
            }

            return dataPoint;
        }

        // команда сохранения файла
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (selectedUser == null)
                          {
                              dialogService.ShowMessage("Выберите пользователя");
                          }
                          else if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, selectedUser);
                              dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                SetStatistic(selectedUser.Statistics);
            }
        }
    }

    public class RelayCommand : ICommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
