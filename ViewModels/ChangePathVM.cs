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
    public class ChangePathVM : BaseVM
    {
        ObservableCollection<ToDoList> roots;
        public ObservableCollection<ToDoList> RootsList
        {
            get
            {
                return roots;
            }
            set
            {
                roots = value;
                OnPropertyChanged("RootsList");
            }
        }

        private ToDoList _selectedToDoList;
        public ToDoList SelectedToDoList
        {
            get { return _selectedToDoList; }
            set
            {
                _selectedToDoList = (ToDoList)value;
                OnPropertyChanged(nameof(SelectedToDoList));
            }
        }

        private ToDoList _toDoListToMove;
        public ToDoList ToDoListToMove
        {
            get { return _toDoListToMove; }
            set
            {
                _toDoListToMove = (ToDoList)value;
                OnPropertyChanged(nameof(ToDoListToMove));
            }
        }

        public ChangePathVM(ObservableCollection<ToDoList> roots, ToDoList ListToMove)
        {
            this.roots = roots;
            this.ToDoListToMove = ListToMove;
        }

        private ICommand _makeRootCommand;
        public ICommand MakeRootCommand
        {
            get
            {
                if (_makeRootCommand == null)
                {
                    _makeRootCommand = new RelayCommand(MakeRoot);
                }
                return _makeRootCommand;
            }
        }

        private void MakeRoot()
        {
            ToDoList parent = Constants.FindParent(RootsList, ToDoListToMove);

            if (parent != null)
            {
                parent.SubLists.Remove(ToDoListToMove);                
                RootsList.Add(ToDoListToMove);
                MessageBox.Show("Your list has been made a root with success!");
                App.Current.MainWindow.Close();
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(Save);
                }
                return _saveCommand;
            }
        }

        private void Save()
        {
            if(SelectedToDoList == null || ToDoListToMove == null)
            {
                MessageBox.Show("Select a to do list first!");
                return;
            }
            ToDoList parent = Constants.FindParent(RootsList, ToDoListToMove);
            if (parent != null)
            {
                parent.SubLists.Remove(ToDoListToMove);
            }
            else
            {
                RootsList.Remove(ToDoListToMove);
            }

            SelectedToDoList.SubLists.Add(ToDoListToMove);
            App.Current.MainWindow.Close();
        }
    }
}
