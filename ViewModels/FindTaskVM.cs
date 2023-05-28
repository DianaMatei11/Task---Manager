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
using Task = TaskManager.Models.Task;

namespace TaskManager.ViewModels
{
    public class FindTaskVM : BaseVM
    {
        private bool _isNameVisible;
        public bool IsNameVisible => SelectedIndex == 0;

        private bool _isDeadlineVisible;
        public bool IsDeadlineVisible => SelectedIndex == 1;

        public FindTaskVM(ObservableCollection<ToDoList> roots)
        {
            RootsList = roots;
            FoundTasks = new ObservableCollection<Task>();
        }

        public ObservableCollection<ToDoList> RootsList { get; }

        private ObservableCollection<Task> _foundTasks;
        public ObservableCollection<Task> FoundTasks
        {
            get => _foundTasks;
            set
            {
                _foundTasks = value;
                OnPropertyChanged(nameof(FoundTasks));
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                OnPropertyChanged(nameof(IsNameVisible));
                OnPropertyChanged(nameof(IsDeadlineVisible));
            }
        }

        private string _nameText;
        public string NametextBox
        {
            get => _nameText;
            set
            {
                _nameText = value;
                OnPropertyChanged(nameof(NametextBox));
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private ICommand _findCommand;
        public ICommand FindCommand
        {
            get
            {
                if (_findCommand == null)
                {
                    _findCommand = new RelayCommand(Find);
                }
                return _findCommand;
            }
        }

        private void Find()
        {
            var results = new ObservableCollection<Task>();
            SearchTasksRecursive(RootsList, results, "");
            FoundTasks.Clear();
            if(results.Any())
            {
                FoundTasks = results;
            }
            else
            {
                MessageBox.Show("The task inserted was not found!");
            }
        }

        private void SearchTasksRecursive(ObservableCollection<ToDoList> lists, ObservableCollection<Task> results, string currentPath)
        {
            foreach (var list in lists)
            {
                var newPath = currentPath + " > " + list.Name;
                foreach (var task in list.Tasks)
                {
                    if (((IsNameVisible && !string.IsNullOrEmpty(NametextBox) && task.Name?.IndexOf(NametextBox, StringComparison.OrdinalIgnoreCase) >= 0) ||
                         (IsDeadlineVisible && SelectedDate != null && task.Deadline == SelectedDate)) &&
                         !results.Any(t => t == task))
                    {
                        results.Add(new Task
                        {
                            Name = task.Name,
                            Deadline = task.Deadline,
                            Location = newPath
                        });
                    }
                }

                if (list.SubLists != null)
                {
                    SearchTasksRecursive(list.SubLists, results, newPath);
                }
            }
        }

    }
}
