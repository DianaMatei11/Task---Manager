using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Commands;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class EditTDLVM : BaseVM
    {
        private Constants constants = new Constants();
        private ObservableCollection<string> images = new ObservableCollection<string>();

        private ToDoList _selectedTDL;
        public ToDoList SelectedTDL
        {
            get { return _selectedTDL; }
            set { _selectedTDL = value; OnPropertyChanged(nameof(SelectedTDL)); }
        }

        private string _nameTextBox;
        public string NameTextBox
        {
            get { return _nameTextBox; }
            set { _nameTextBox = value; OnPropertyChanged(nameof(NameTextBox)); }
        }

        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; OnPropertyChanged(nameof(ImageSource)); }
        }
        public EditTDLVM(ToDoList selectedTDL)
        {
            images = constants.Icons;
            SelectedTDL = selectedTDL;
            NameTextBox = SelectedTDL.Name;
            ImageSource = SelectedTDL.ImageSource;
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

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(EditMethod);
                }
                return _editCommand;
            }
        }
        public void EditMethod()
        {
            SelectedTDL.Name = NameTextBox;
            SelectedTDL.ImageSource = ImageSource;
            App.Current.MainWindow.Close();
        }
    }
}
