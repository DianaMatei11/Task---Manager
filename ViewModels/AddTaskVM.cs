using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using TaskManager.Commands;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager.ViewModels
{
    class AddTaskViewModel : BaseVM
    {
        private Constants constants = new Constants();
        private ObservableCollection<string> categories = new ObservableCollection<string>();
        public ObservableCollection<string> Categories
        { 
            get { return categories; } 
            set 
            { 
                categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        private ToDoList toDoList;
        public AddTaskViewModel(ToDoList toDoList)
        {
            Categories = constants.Categories;
            this.toDoList = toDoList;
        }

        private string nameTextBox;
        public string NameTextBox
        {
            get { return nameTextBox; }
            set
            {
                nameTextBox = value;
                OnPropertyChanged(nameof(NameTextBox));
            }
        }

        private string descriptionTextBox;
        public string DescriptionTextBox
        {
            get { return descriptionTextBox; }
            set
            {
                descriptionTextBox = value;
                OnPropertyChanged(nameof(DescriptionTextBox));
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private TaskManager.Models.Priority selectedPriority;
        public TaskManager.Models.Priority SelectedPriority
        {
            get
            {
                return selectedPriority;
            }
            set
            {
                selectedPriority = value;
                OnPropertyChanged("SelectedPriority");
            }
        }

        private string selectedCategory;
        public string SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
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
        public void Create()
        {
            Task task = new Task()
            {
                Name = NameTextBox,
                Description = DescriptionTextBox,
                Category = SelectedCategory,
                Priority = SelectedPriority,
                Deadline = SelectedDate
            };

            toDoList.Tasks.Add(task);            
            MessageBox.Show("Your task has been created with success!");
            App.Current.MainWindow.Close();
        }
    }
}
