using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TaskManager.ViewModels;

namespace TaskManager.Models
{
    public class Constants : BaseVM
    {
        public ObservableCollection<string> Icons { get; set; }
        private string[] _images;

        public ObservableCollection<string> Categories { get; set; }

        //private Roots _rootLists;
        //public Roots R
        //{
        //    get
        //    {
        //        return _rootLists;
        //    }
        //    set
        //    {
        //        _rootLists = value;
        //        OnPropertyChanged("RootLists");
        //    }
        //}

        private ObservableCollection<ToDoList> _rootLists;
        public ObservableCollection<ToDoList> RootLists
        {
            get
            {
                return _rootLists;
            }
            set
            {
                _rootLists = value;
                OnPropertyChanged("RootLists");
            }
        }
        public Constants()
        {
            Icons = new ObservableCollection<string>();
            _images = Directory.GetFiles(@"..\..\..\Assets\Icons\", "*.png");
            foreach (string path in _images)
            {
                Icons.Add(path);
            }

            Categories = new ObservableCollection<string>();
            string filePath = @"..\..\..\Assets\categories.txt";
            foreach (string line in File.ReadAllLines(filePath))
            {
                Categories.Add(line);
            }
            RootLists = new ObservableCollection<ToDoList>();
        }

        public static ToDoList FindParent(ObservableCollection<ToDoList> searchList, ToDoList toDoList)
        {
            foreach (ToDoList item in searchList)
            {
                if (item.SubLists.Contains(toDoList))
                {
                    return item;
                }

                ToDoList parent = FindParent(item.SubLists, toDoList);
                if (parent != null)
                {
                    return parent;
                }
            }

            return null;
        }
    }
}
