using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using TaskManager.Models;
using Task = TaskManager.Models.Task;
using TaskManager.Services;
using TaskManager.Commands;

namespace TaskManager.ViewModels
{
    public class FilterVM : BaseVM
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

        private readonly ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        private string _selectedCategory;
        private int _selectedIndex;
        private bool _isCategoryVisible;
        public bool IsCategoryVisible => SelectedIndex == 0;


        public FilterVM(List<Task> tasks)
        {
            Categories = constants.Categories;
            this.tasks = new ObservableCollection<Task>(tasks);
            _isCategoryVisible = false;

            var collectionViewSource = new CollectionViewSource { Source = tasks };

            FoundTasksView = collectionViewSource.View;
        }

        public ICollectionView FoundTasks { get; }
        public ICollectionView FoundTasksView { get; set; }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                OnPropertyChanged(nameof(IsCategoryVisible));
            }
        }




        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private ICommand _filterTasksCommand;
        public ICommand FilterTasksCommand
        {
            get
            {
                if (_filterTasksCommand == null)
                {
                    _filterTasksCommand = new RelayCommand(FilterTasks);
                }

                return _filterTasksCommand;
            }
        }

        private bool FilterTasksByCategory(object obj)
        {
            if (!(obj is Task task))
            {
                return false;
            }

            return string.Compare(task.Category, SelectedCategory, StringComparison.OrdinalIgnoreCase) == 0;
        }

        private void FilterTasks()
        {
            switch (SelectedIndex)
            {
                case 0:
                    FoundTasksView.Filter = FilterTasksByCategory;
                    break;
                case 1:
                    FoundTasksView.Filter = task => ((Task)task).Status == Status.Done;
                    break;
                case 2:
                    FoundTasksView.Filter = task => ((Task)task).Deadline < ((Task)task).DateFinish && ((Task)task).Status != Status.Done;
                    break;
                case 3:
                    FoundTasksView.Filter = task => ((Task)task).Deadline < DateTime.Now && ((Task)task).Status != Status.Done;
                    break;
                case 4:
                    FoundTasksView.Filter = task => ((Task)task).Deadline > DateTime.Now && ((Task)task).Status != Status.Done;
                    break;
            }

            FoundTasksView.Refresh();
        }
    }
}
