using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Commands;
using TaskManager.Views;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    class OpenDatabaseViewModel : BaseVM
    {
        MainWindowViewModel vm;
        public OpenDatabaseViewModel(MainWindowViewModel vm) 
        {
            this.vm = vm;
            string path = @"..\..\..\Assets\Databases";
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                FileNames.Add(Path.GetFileName(file));
            }

        }

        private ObservableCollection<string> _fileNames = new ObservableCollection<string>();
        public ObservableCollection<string> FileNames
        {
            get { return _fileNames; }
            set { _fileNames = value; OnPropertyChanged("FileNames"); }
        }

        private string databasePath;
        public string DatabasePath
        {
            get
            {
                return databasePath;
            }
            set
            {
                databasePath = value;
            }
        }

        private ICommand openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (openCommand == null)
                {
                    openCommand = new RelayCommand(Open);
                }
                return openCommand;
            }
        }

        public void Open()
        {
            string directoryPath = @"..\..\..\Assets\Databases\";
            string filePath = Path.Combine(directoryPath, DatabasePath);
            if (File.Exists(filePath))
            {                
                ObservableCollection<ToDoList> rootLists = DataSerialization.Deserialize<ObservableCollection<ToDoList>>(filePath);

                MainWindow w = new MainWindow(DatabasePath, rootLists);
                App.Current.MainWindow.Close();
                w.Show();
                App.Current.MainWindow = w;
            }
            else
            {
                MessageBox.Show("The file was not founded!");
            }

        }
    }
}
