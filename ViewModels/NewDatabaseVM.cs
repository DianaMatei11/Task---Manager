using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Commands;
using TaskManager.Views;
using TaskManager.ViewModels;
using TaskManager.Models;
using System.Collections.ObjectModel;

namespace TaskManager.ViewModels
{
    class NewDatabaseVM : BaseVM
    {
        private string? databaseName;
        public string DatabaseName
        {
            get => databaseName;
            set
            {
                databaseName = value;
                OnPropertyChanged("DatabaseName");
            }
        }

        private ICommand? createCommand;
        public ICommand CreateCommand
        {
            get
            {
                if (createCommand == null)
                {
                    createCommand = new RelayCommand(Create);
                }
                return createCommand;
            }
        }

        public void Create()
        {
            string directoryPath = @"..\..\..\Assets\Databases";
            string filePath = Path.Combine(directoryPath, DatabaseName);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            using (StreamWriter fileStream = File.CreateText(filePath))
            {
                fileStream.Dispose();
                MessageBox.Show("Your database has been created with success!");
                MainWindow w = new MainWindow(DatabaseName, new ObservableCollection<ToDoList>());
                MainWindowViewModel vm = new MainWindowViewModel(DatabaseName);
                w.DataContext = vm;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = w;
                w.Show();

            }

        }
    }
}
