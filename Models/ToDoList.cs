using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels;

namespace TaskManager.Models
{
    
    public enum Type
    {
        Root,
        SubTDL
    }
    [Serializable]
    public class ToDoList : BaseVM
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }

        private ObservableCollection<Task> _tasks;
        public ObservableCollection<Task> Tasks
        {
            get { return _tasks; }
            set
            {
                if (_tasks != value)
                {
                    _tasks = value;
                    OnPropertyChanged(nameof(Tasks));
                }
            }
        }
        private ObservableCollection<ToDoList> _subLists = new ObservableCollection<ToDoList>();
        public ObservableCollection<ToDoList> SubLists
        {
            get { return _subLists; }
            set { _subLists = value; }
        }

        Type type;
        public Type Type { get { return type; } set { type = value; } }

        public ToDoList()
        {
            Tasks = new ObservableCollection<Task>();
            SubLists = new ObservableCollection<ToDoList>();
        }
    }
}
