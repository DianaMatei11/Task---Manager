using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TaskManager.ViewModels;


namespace TaskManager.Models
{
    [Serializable]
    
    public class Roots : BaseVM
    {
        private static ObservableCollection<ToDoList> _rootLists;
        [XmlElement("ToDoList")]
        public static ObservableCollection<ToDoList> RootLists
        {
            get
            {
                return _rootLists;
            }
            set
            {
                _rootLists = value;
                //OnPropertyChanged("RootLists");
            }
        }


        public Roots()
        {
            //RootLists = new ObservableCollection<ToDoList>
            //{
            //    new ToDoList
            //    {
            //        Name = "Grocery List",
            //        Type = Type.Root,
            //        ImageSource = "\\Assets\\Icons\\2.png",
            //        SubLists = new ObservableCollection<ToDoList>
            //        {
            //            new ToDoList
            //            {
            //                ImageSource = "\\Assets\\Icons\\1.png",
            //                Name = "Produce",
            //                Type = Type.SubTDL,
            //                Tasks = new ObservableCollection<Task>
            //                {
            //                    new Models.Task
            //                    {
            //                        Name = "Buy apples",
            //                        Category = Category.Home,
            //                        Priority = Priority.High,
            //                        Status = Status.Created,
            //                        Description = "Get at least 10 apples",
            //                        Deadline = DateTime.Now
            //                    },
            //                    new Models.Task
            //                    {
            //                        Name = "Buy bananas",
            //                        Category = Category.Home,
            //                        Priority = Priority.Low,
            //                        Status = Status.Created
            //                    }
            //                }
            //            },
            //            new ToDoList
            //            {
            //                ImageSource = "\\Assets\\Icons\\4.png",
            //                Name = "Meat",
            //                Type = Type.SubTDL,
            //                Tasks = new ObservableCollection<Task>
            //                {
            //                    new Models.Task
            //                    {
            //                        Name = "Buy chicken",
            //                        Category = Category.Home,
            //                        Priority = Priority.Medium,
            //                        Status = Status.InProgress
            //                    },
            //                    new Models.Task
            //                    {
            //                        Name = "Buy beef",
            //                        Category = Category.Home,
            //                        Priority = Priority.High,
            //                        Status = Status.Done
            //                    }
            //                }
            //            }
            //        },
            //        Tasks = new ObservableCollection<Task>
            //                {
            //                    new Models.Task
            //                    {
            //                        Name = "Buy apples",
            //                        Category = Category.Home,
            //                        Priority = Priority.High,
            //                        Status = Status.Created,
            //                        Description = "Get at least 10 apples"
            //                    },
            //                    new Models.Task
            //                    {
            //                        Name = "Buy bananas",
            //                        Category = Category.Home,
            //                        Priority = Priority.Low,
            //                        Status = Status.Created
            //                    }
            //                }
            //    },
            //    new ToDoList
            //    {
            //        Name = "Work List",
            //        Type = Type.Root,
            //        ImageSource = "\\Assets\\Icons\\5.png",
            //        SubLists = new ObservableCollection<ToDoList>
            //        {
            //            new ToDoList
            //            {
            //                Name = "Projects",
            //                Type = Type.SubTDL,
            //                ImageSource = "\\Assets\\Icons\\5.png",
            //                Tasks = new ObservableCollection<Task>
            //                {
            //                    new Models.Task
            //                    {
            //                        Name = "Finish project A",
            //                        Category = Category.Work,
            //                        Priority = Priority.High,
            //                        Status = Status.InProgress,
            //                        Description = "Finish project A by the end of the week"
            //                    },
            //                    new Models.Task
            //                    {
            //                        Name = "Start project B",
            //                        Category = Category.Work,
            //                        Priority = Priority.Medium,
            //                        Status = Status.Created,
            //                        Description = "Meet with team to discuss project B"
            //                    }
            //                }
            //            },
            //            new ToDoList
            //            {
            //                ImageSource = "\\Assets\\Icons\\7.png",
            //                Name = "Meetings",
            //                Type = Type.SubTDL,
            //                Tasks = new ObservableCollection<Task>
            //                {
            //                    new Models.Task
            //                    {
            //                        Name = "Meet with manager",
            //                        Category = Category.Work,
            //                        Priority = Priority.High,
            //                        Status = Status.Created,
            //                        Description = "Discuss project A progress with manager"
            //                    },
            //                    new Models.Task
            //                    {
            //                        Name = "Meet with team",
            //                        Category = Category.Work,
            //                        Priority = Priority.Medium,
            //                        Status = Status.Created,
            //                        Description = "Discuss project B details with team"
            //                    }
            //                }
            //            }
            //        }
            //    }
            //};
            RootLists = new ObservableCollection<ToDoList>
            {
                new ToDoList { Name = "Groceries", Type = Type.Root, ImageSource = "1.png" },
                new ToDoList { Name = "Home projects", Type = Type.Root, ImageSource = "2.png" },
                new ToDoList { Name = "Work tasks", Type = Type.Root, ImageSource = "3.png" }
            };

        }
    }
}
