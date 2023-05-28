using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TaskManager.Commands;
using TaskManager.Models;
using Constants = TaskManager.Models.Constants;
using Task = TaskManager.Models.Task;

namespace TaskManager.ViewModels
{
    public class EditTaskVM : BaseVM
    {
        private Constants constants = new Constants();
        public EditTaskVM(Task selectedTask)
        {
            Categories = constants.Categories;
            SelectedTask = selectedTask;
            NameTextBox = SelectedTask.Name;
            DescriptionTextBox = SelectedTask.Description;
            SelectedPriority = SelectedTask.Priority.ToString();
            SelectedCategory = SelectedTask.Category;
            SelectedDate = SelectedTask.Deadline;
        }

        private Task _selectedTask;
        public Task SelectedTask
        {
            get { return _selectedTask; }
            set { _selectedTask = value; OnPropertyChanged(nameof(SelectedTask)); }
        }

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


        ObservableCollection<string> _priorities;
        public ObservableCollection<string> Priorities
        {
            get
            {
                if (_priorities == null)
                {
                    _priorities = new ObservableCollection<string>();
                    _priorities.Add("Low");
                    _priorities.Add("Medium");
                    _priorities.Add("High");
                }
                return _priorities;
            }
        }
        private string selectedPriority;
        public string SelectedPriority
        {
            get
            {
                return selectedPriority;
            }
            set
            {
                selectedPriority = value;
                OnPropertyChanged(nameof(SelectedPriority));
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

        private ICommand editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new RelayCommand(Edit);
                }
                return editCommand;
            }
        }
        public void Edit()
        {
            SelectedTask.Name = NameTextBox;
            SelectedTask.Category = SelectedCategory;
            SelectedTask.Description = DescriptionTextBox;
            Priority priority = (Priority)Enum.Parse(typeof(Priority), selectedPriority);
            SelectedTask.Priority = priority;
            SelectedTask.Deadline = SelectedDate;
            App.Current.MainWindow.Close();
        }
    }
}
