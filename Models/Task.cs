using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels;

namespace TaskManager.Models
{
    
    public enum Status
    {
        Created,
        InProgress,
        Done
    }
    public enum Priority
    {
        High,
        Medium,
        Low
    }
    
    [Serializable]
    public class Task : BaseVM
    {
        string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        
        string description;
        public string Description { get { return description; } set { description = value; OnPropertyChanged(nameof(Description)); } }

        Status status;
        public Status Status { get { return status; } set { status = value; OnPropertyChanged("Status"); } }

        Priority priority;
        public Priority Priority { get { return priority; } set { priority = value; OnPropertyChanged("Priority"); } }

        string category;
        public string Category { get { return category; } set { category = value; OnPropertyChanged(nameof(Category)); } }

        DateTime deadline;
        public DateTime Deadline { get { return deadline; } set { deadline = value; OnPropertyChanged(nameof(Deadline)); } }

        DateTime dateFinish;
        public DateTime DateFinish { get { return dateFinish; } set { dateFinish = value; OnPropertyChanged(nameof(DateFinish)); } }

        private bool isDone;
        public bool IsDone { get { return isDone; } set { isDone = value; OnPropertyChanged("IsDone"); } }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

    }
}
