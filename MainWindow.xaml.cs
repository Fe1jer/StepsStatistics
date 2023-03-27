using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestTask.Models;
using TestTask.Sevices;
using TestTask.ViewModels;

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel(new JsonDialogService(), new JsonFileService());
        }

        private void BooksDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            for (int i = 0; i < BooksDataGrid.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)BooksDataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null)
                {
                    row.Background = Brushes.White;
                    User selectedUser = (User)BooksDataGrid.SelectedItem;
                    var user = (User)BooksDataGrid.Items[i];
                    if (selectedUser != user)
                    {
                        if (selectedUser.AvgSteps * 1.2f < user.MaxSteps)
                        {
                            row.Background = Brushes.LightGreen;
                        }
                        else if (selectedUser.AvgSteps * 0.8f > user.MinSteps)
                        {
                            row.Background = Brushes.LightPink;
                        }
                    }
                }
            }
        }
    }
}
