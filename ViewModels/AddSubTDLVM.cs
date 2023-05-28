using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Commands;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class AddSubTDLVM : BaseVM
    {
        private readonly MainWindowViewModel mainViewModel;
        private ToDoList parentTDL;
        private Constants constants = new Constants();
        private ObservableCollection<string> images = new ObservableCollection<string>();
        public AddSubTDLVM(ToDoList parentTDL, MainWindowViewModel mainViewModel)
        {
            this.parentTDL = parentTDL;
            this.mainViewModel = mainViewModel;
            images = constants.Icons;
            ImageSource = images[0];
        }

        private string imageSource;
        public string ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        private ICommand nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new RelayCommand(NextMethod);
                }
                return nextCommand;
            }
        }

        public void NextMethod()
        {
            int index = images.IndexOf(ImageSource);
            if (index < images.Count - 1)
            {
                ImageSource = images[++index];
            }
        }

        private ICommand prevCommand;
        public ICommand PrevCommand
        {
            get
            {
                if (prevCommand == null)
                {
                    prevCommand = new RelayCommand(PrevMethod);
                }
                return prevCommand;
            }
        }

        public void PrevMethod()
        {
            int index = images.IndexOf(ImageSource);
            if (index > 0)
            {
                ImageSource = images[--index];
            }
        }

        private ICommand createCommand;
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

        private void Create()
        {
            if (NameTextBox != null && !DoesToDoListExist(NameTextBox))
            {
                var newList = new ToDoList
                {
                    Name = NameTextBox,
                    Type = TaskManager.Models.Type.SubTDL,
                    ImageSource = ImageSource,
                    SubLists = new ObservableCollection<ToDoList>()
                };
                parentTDL.SubLists.Add(newList);
                MessageBox.Show("Your sub To Do List has been created with success!");
                App.Current.MainWindow.Close();

            }
            else if (NameTextBox == null)
            {
                MessageBox.Show("Insert a name for TDL!");
            }
            else
            {
                MessageBox.Show("A TDL with this name already exists!");
            }
        }
        public bool DoesToDoListExist(string name)
        {
            return mainViewModel.RootsList.Any(tdl => tdl.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private string nameTextBox;
        public string NameTextBox
        {
            get; set;
        }
    }
}
