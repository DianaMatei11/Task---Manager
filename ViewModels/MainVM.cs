using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Serialization;
using TaskManager.Commands;
using TaskManager.Models;
using TaskManager.Services;
using TaskManager.Views;
using Task = TaskManager.Models.Task;
using Type = TaskManager.Models.Type;

namespace TaskManager.ViewModels
{
    public class MainWindowViewModel : BaseVM
    {
        public MainWindowViewModel() 
        { 
            RootsList = new ObservableCollection<ToDoList>();
        }
        public MainWindowViewModel(string dataBaseName)
        {
            this.DatabaseName = dataBaseName;
            string directoryPath = @"..\..\..\Assets\Databases";
            string filePath = Path.Combine(directoryPath, DatabaseName);
            bool isEmpty = new FileInfo(filePath).Length == 0;
            if(isEmpty)
            {
                RootsList = new ObservableCollection<ToDoList>();
                return;
            }
            else
            {
                this.RootsList = DataSerialization.Deserialize<ObservableCollection<ToDoList>>(dataBaseName);
            }
        }
        public MainWindowViewModel(string dataBaseName, ObservableCollection<ToDoList> roots)
        {
            this.DatabaseName = dataBaseName;
            this.RootsList = roots;
        }
        private ICommand newTaskCommand;
        public ICommand NewTaskCommand
        {
            get
            {
                if (newTaskCommand == null)
                {
                    newTaskCommand = new RelayCommand(NewTask);
                }
                return newTaskCommand;
            }
        }
        public void NewTask()
        {
            AddTaskWindow atw = new AddTaskWindow();
            if (SelectedToDoList == null)
            {
                MessageBox.Show("Please select a list first");
                return;
            }
            AddTaskViewModel atvm = new AddTaskViewModel(SelectedToDoList);
            atw.DataContext = atvm;
            App.Current.MainWindow = atw;
            atw.ShowDialog();
            ShowStatisticsPanel();
        }

        private string _databaseName;
        public string DatabaseName
        {
            get
            {
                return _databaseName;
            }
            set
            {
                _databaseName = value;
                OnPropertyChanged(nameof(DatabaseName));
            }
        }

        private ICommand openDatabaseCommand;
        public ICommand OpenDatabaseCommand
        {
            get
            {
                if (openDatabaseCommand == null)
                {
                    openDatabaseCommand = new RelayCommand(OpenDatabase);
                }
                return openDatabaseCommand;
            }
        }
        public void OpenDatabase()
        {
            OpenDatabaseWindow odw = new OpenDatabaseWindow();
            OpenDatabaseViewModel odvm = new OpenDatabaseViewModel(this);
            odw.DataContext = odvm;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = odw;
            odw.Show();
        }


        private ICommand newDatabaseCommand;
        public ICommand NewDatabaseCommand
        {
            get
            {
                if (newDatabaseCommand == null)
                {
                    newDatabaseCommand = new RelayCommand(NewDatabase);
                }
                return newDatabaseCommand;
            }
        }
        public void NewDatabase()
        {
            NewDatabaseWindow ndw = new NewDatabaseWindow();
            App.Current.MainWindow.Close();
            NewDatabaseVM ndvm = new NewDatabaseVM();
            ndw.DataContext = ndvm;
            App.Current.MainWindow = ndw;
            ndw.Show();
        }

        private ICommand _archiveDatabaseCommand;
        public ICommand ArchiveDatabaseCommand
        {
            get
            {
                if (_archiveDatabaseCommand == null)
                {
                    _archiveDatabaseCommand = new RelayCommand(ArchiveDatabase);
                }
                return _archiveDatabaseCommand;
            }
        }
        public void ArchiveDatabase()
        {
            string directoryPath = @"..\..\..\Assets\Databases";
            if (DatabaseName == null)
            {
                DatabaseName = Microsoft.VisualBasic.Interaction.InputBox("Enter a name for your database", "Database name:", "");
                if (string.IsNullOrEmpty(DatabaseName)) { return; }
            }
            string filePath = Path.Combine(directoryPath, DatabaseName);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            using (StreamWriter fileStream = File.CreateText(filePath))
            {
                fileStream.Dispose();
                DataSerialization.Serialize(roots, filePath);
            }
            MessageBox.Show("Your database has been saved in the file with success!");
        }


        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(Exit);
                }
                return exitCommand;
            }
        }
        public void Exit()
        {
            App.Current.Shutdown();
        }


        private ICommand categoriesCommand;
        public ICommand CategoriesCommand
        {
            get
            {
                if (categoriesCommand == null)
                {
                    categoriesCommand = new RelayCommand(Categories);
                }
                return categoriesCommand;
            }
        }
        public void Categories()
        {
            CategoriesWindow cw = new CategoriesWindow();
            CategoriesVM cvm = new CategoriesVM();
            cw.DataContext = cvm;
            App.Current.MainWindow = cw;
            cw.Show();
        }

        private ICommand addRootTDLCommand;
        public ICommand AddRootTDLCommand
        {
            get
            {
                if (addRootTDLCommand == null)
                {
                    addRootTDLCommand = new RelayCommand(AddRootTDL);
                }
                return addRootTDLCommand;
            }
        }

        public void AddRootTDL()
        {
            AddRootTDLWindow artw = new AddRootTDLWindow();
            AddRootTDLVM artvm = new AddRootTDLVM(this);
            artw.DataContext = artvm;
            App.Current.MainWindow = artw;
            artw.Show();
        }

        private ICommand addSubTDLCommand;
        public ICommand AddSubTDLCommand
        {
            get
            {
                if (addSubTDLCommand == null)
                {
                    addSubTDLCommand = new RelayCommand(AddSubTDL);
                }
                return addSubTDLCommand;
            }
        }

        public void AddSubTDL()
        {
            if (SelectedToDoList != null)
            {
                AddSubTDLWindow astw = new AddSubTDLWindow();
                AddSubTDLVM astvm = new AddSubTDLVM(SelectedToDoList, this);
                astw.DataContext = astvm;
                App.Current.MainWindow = astw;
                astw.Show();
            }
            else
            {
                MessageBox.Show("Please select a ToDoList.");
            }
        }

        private ICommand editTDLCommand;
        public ICommand EditTDLCommand
        {
            get
            {
                if (editTDLCommand == null)
                {
                    editTDLCommand = new RelayCommand(EditTDL);
                }
                return editTDLCommand;
            }
        }

        public void EditTDL()
        {
            if (SelectedToDoList == null)
            {
                MessageBox.Show("Please select a To Do List.");
                return;
            }
            EditTDLWindow etw = new EditTDLWindow();
            EditTDLVM etvm = new EditTDLVM(SelectedToDoList);
            etw.DataContext = etvm;
            App.Current.MainWindow = etw;
            etw.ShowDialog();
        }

        private ICommand deleteTDLCommand;
        public ICommand DeleteTDLCommand
        {
            get
            {
                if (deleteTDLCommand == null)
                {
                    deleteTDLCommand = new RelayCommand(DeleteTDL);
                }
                return deleteTDLCommand;
            }
        }
        public void DeleteTDL()
        {
            if (SelectedToDoList == null)
            {
                MessageBox.Show("Please select a To Do List.");
                return;
            }
            if (RootsList.Contains(SelectedToDoList))
            {
                RootsList.Remove(SelectedToDoList);
            }
            else
            {
                ToDoList parent = Constants.FindParent(RootsList, SelectedToDoList);

                if (parent != null)
                {
                    parent.SubLists.Remove(SelectedToDoList);
                    ShowStatisticsPanel();
                }
            }
        }

        private ICommand _moveUpTDLCommand;
        public ICommand MoveUpTDLCommand
        {
            get
            {
                if (_moveUpTDLCommand == null)
                {
                    _moveUpTDLCommand = new RelayCommand(MoveUpTDL);
                }
                return _moveUpTDLCommand;
            }
        }
        public void MoveUpTDL()
        {
            if (SelectedToDoList == null) { return; }
            int currentIndex;
            ToDoList parentList = Constants.FindParent(RootsList, SelectedToDoList);
            if (parentList == null)
            {
                currentIndex = RootsList.IndexOf(SelectedToDoList);

                if (RootsList.Count == 1 || currentIndex == 0)
                {
                    MessageBox.Show("The selected to do list cannot be moved up!");
                    return;
                }
                RootsList.Move(currentIndex, currentIndex - 1);
                SelectedToDoList = RootsList[currentIndex - 1];
            }
            else
            {
                currentIndex = parentList.SubLists.IndexOf(SelectedToDoList);

                if (parentList.SubLists.Count == 1 || currentIndex == 0)
                {
                    MessageBox.Show("The selected to do list cannot be moved up!");
                    return;
                }
                parentList.SubLists.Move(currentIndex, currentIndex - 1);
                SelectedToDoList = parentList.SubLists[currentIndex - 1];
            }
        }

        private ICommand _moveDownTDLCommand;
        public ICommand MoveDownTDLCommand
        {
            get
            {
                if (_moveDownTDLCommand == null)
                {
                    _moveDownTDLCommand = new RelayCommand(MoveDownTDL);
                }
                return _moveDownTDLCommand;
            }
        }
        private void MoveDownTDL()
        {
            if (SelectedToDoList == null) { return; }
            int currentIndex;
            ToDoList parentList = Constants.FindParent(RootsList, SelectedToDoList);

            if (parentList == null)
            {
                currentIndex = RootsList.IndexOf(SelectedToDoList);

                if (RootsList.Count == 1 || currentIndex == RootsList.Count - 1)
                {
                    MessageBox.Show("The selected to do list cannot be moved down!");
                    return;
                }
                RootsList.Move(currentIndex, currentIndex + 1);
                SelectedToDoList = RootsList[currentIndex + 1];
            }
            else
            {
                currentIndex = parentList.SubLists.IndexOf(SelectedToDoList);

                if (parentList.SubLists.Count == 1 || currentIndex == parentList.SubLists.Count - 1)
                {
                    MessageBox.Show("The selected to do list cannot be moved down!");
                    return;
                }
                parentList.SubLists.Move(currentIndex, currentIndex + 1);
                SelectedToDoList = parentList.SubLists[currentIndex + 1];
            }

        }

        private ICommand changePathCommand;
        public ICommand ChangePathCommand
        {
            get
            {
                if (changePathCommand == null)
                {
                    changePathCommand = new RelayCommand(ChangePath);
                }
                return changePathCommand;
            }
        }

        public void ChangePath()
        {
            ChangePathVM cpvm = new ChangePathVM(RootsList, SelectedToDoList);
            ChangePathWindow cpw = new ChangePathWindow(cpvm);
            cpw.DataContext = cpvm;
            App.Current.MainWindow = cpw;
            cpw.Show();

        }

        private ICommand editTaskCommand;
        public ICommand EditTaskCommand
        {
            get
            {
                if (editTaskCommand == null)
                {
                    editTaskCommand = new RelayCommand(EditTask);
                }
                return editTaskCommand;
            }
        }

        public void EditTask()
        {
            EditTaskWindow etw = new EditTaskWindow();
            EditTaskVM etvm = new EditTaskVM(SelectedTask);
            etw.DataContext = etvm;
            App.Current.MainWindow = etw;
            etw.ShowDialog();
            ShowStatisticsPanel();
        }

        private ICommand _moveUpTaskCommand;
        public ICommand MoveUpTaskCommand
        {
            get
            {
                if (_moveUpTaskCommand == null)
                {
                    _moveUpTaskCommand = new RelayCommand(MoveUpTask);
                }
                return _moveUpTaskCommand;
            }
        }

        public void MoveUpTask()
        {
            if (SelectedTask == null) { return; }
            int currentIndex;

            if (SelectedToDoList != null)
            {
                currentIndex = SelectedToDoList.Tasks.IndexOf(SelectedTask);
                if (SelectedToDoList.Tasks.Count == 1 || currentIndex == 0)
                {
                    MessageBox.Show("The selected task cannot be moved up!");
                    return;
                }
                SelectedToDoList.Tasks.Move(currentIndex, currentIndex - 1);
                SelectedTask = SelectedToDoList.Tasks[currentIndex - 1];
            }
            else
            {
                MessageBox.Show("Please selected a list first!");
            }
        }

        private ICommand _moveDownTaskCommand;
        public ICommand MoveDownTaskCommand
        {
            get
            {
                if (_moveDownTaskCommand == null)
                {
                    _moveDownTaskCommand = new RelayCommand(MoveDownTask);
                }
                return _moveDownTaskCommand;
            }
        }

        private void MoveDownTask()
        {

            if (SelectedTask == null) { return; }
            int currentIndex;

            if (SelectedToDoList != null)
            {
                currentIndex = SelectedToDoList.Tasks.IndexOf(SelectedTask);
                if (SelectedToDoList.Tasks.Count == 1 || currentIndex == SelectedToDoList.Tasks.Count - 1)
                {
                    MessageBox.Show("The selected task cannot be moved down!");
                    return;
                }
                SelectedToDoList.Tasks.Move(currentIndex, currentIndex + 1);
                SelectedTask = SelectedToDoList.Tasks[currentIndex + 1];
            }
            else
            {
                MessageBox.Show("Please selected a list first!");
            }

        }

        private ICommand findTaskCommand;
        public ICommand FindTaskCommand
        {
            get
            {
                if (findTaskCommand == null)
                {
                    findTaskCommand = new RelayCommand(FindTask);
                }
                return findTaskCommand;
            }
        }

        public void FindTask()
        {
            FindTaskWindow ftw = new FindTaskWindow();
            FindTaskVM ftvm = new FindTaskVM(RootsList);
            ftw.DataContext = ftvm;
            App.Current.MainWindow = ftw;
            ftw.Show();

        }

        private ICommand aboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (aboutCommand == null)
                {
                    aboutCommand = new RelayCommand(About);
                }
                return aboutCommand;
            }
        }
        public void About()
        {
            MessageBox.Show("Nume: Matei Diana\nGrupa: 10LF312\n diana.matei@student.unitbv.ro");
        }

        private Task _selectedTask;
        public Task SelectedTask
        {
            get
            {
                return _selectedTask;
                isFound = false;
            }
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    OnPropertyChanged(nameof(SelectedTask));
                    isFound = false;
                }
            }
        }

        public ICommand _setDoneCommand => new RelayCommand(SetDoneCommand);

        private void SetDoneCommand()
        {
            if (SelectedTask != null)
            {
                SelectedTask.Status = TaskManager.Models.Status.Done;
                SelectedTask.DateFinish = DateTime.Now;
                ShowStatisticsPanel();
            }
            else
            {
                MessageBox.Show("Please select a task.");
            }

        }

        private ICommand deleteTaskCommand;
        public ICommand DeleteTaskCommand
        {
            get
            {
                if (deleteTaskCommand == null)
                {
                    deleteTaskCommand = new RelayCommand(DeleteTask);
                }
                return deleteTaskCommand;
            }
        }

        private void DeleteTask()
        {
            if (SelectedTask != null && SelectedToDoList.Tasks.Count != 0)
            {
                RemoveTaskFromToDoList(SelectedToDoList.Tasks, SelectedTask);
                ShowStatisticsPanel();
            }
            else if (SelectedToDoList == null)
            {
                MessageBox.Show("please select a to do list");
            }
            else if (SelectedToDoList.Tasks == null)
            {
                MessageBox.Show("please select the to do list where the task is located");
            }
            else if (SelectedTask == null)
            {
                MessageBox.Show("please select a task");
            }
            else if (SelectedToDoList.Tasks.Count == 0)
            {
                MessageBox.Show("the to do list is empty, please select a valid to do list");
            }
        }

        bool isFound = false;
        private void RemoveTaskFromToDoList(ObservableCollection<Task> tasks, Task taskToRemove)
        {

            foreach (Task t in tasks)
            {
                if (isFound)
                {
                    return;
                }
                if (t.Equals(taskToRemove))
                {
                    tasks.Remove(taskToRemove);
                    isFound = true;
                    return;
                }
            }
        }


        private string statisticsPanel;
        public string StatisticsPanel
        {
            get
            {
                List<Task> allTasks = GetAllTasks(RootsList);

                int tasksDueToday = allTasks.Count(t => t.Deadline == DateTime.Today && t.Status != Models.Status.Done);

                int tasksDueTomorrow = allTasks.Count(t => t.Deadline == DateTime.Today.AddDays(1) && t.Status != Models.Status.Done);

                int lateTasks = allTasks.Count(t => t.Status == Models.Status.Done && t.DateFinish > t.Deadline);

                int onTimeTasks = allTasks.Count(t => t.Status == Models.Status.Done && t.DateFinish <= t.Deadline);

                statisticsPanel = $"Tasks due today: {tasksDueToday}\nTasks due tomorrow: {tasksDueTomorrow}\nLate tasks: {lateTasks}\nTasks completed on time: {onTimeTasks}\nTotal tasks: {allTasks.Count}";

                return statisticsPanel;
            }
            set
            {
                statisticsPanel = value;
                OnPropertyChanged(nameof(StatisticsPanel));
            }
        }
        private List<Task> GetAllTasks(ObservableCollection<ToDoList> lists)
        {
            List<Task> allTasks = new List<Task>();
            foreach (ToDoList list in lists)
            {
                allTasks.AddRange(list.Tasks);
                if (list.SubLists.Count > 0)
                {
                    allTasks.AddRange(GetAllTasks(list.SubLists));
                }
            }
            return allTasks;
        }

        public void ShowStatisticsPanel()
        {
            List<Task> allTasks = GetAllTasks(RootsList);

            int tasksDueToday = allTasks.Count(t => t.Deadline == DateTime.Today && t.Status != Models.Status.Done);

            int tasksDueTomorrow = allTasks.Count(t => t.Deadline == DateTime.Today.AddDays(1) && t.Status != Models.Status.Done);

            int lateTasks = allTasks.Count(t => t.Status == Models.Status.Done && t.DateFinish > t.Deadline);

            int onTimeTasks = allTasks.Count(t => t.Status == Models.Status.Done && t.DateFinish <= t.Deadline);

            string statisticsInfo = $"Tasks due today: {tasksDueToday}\nTasks due tomorrow: {tasksDueTomorrow}\nLate tasks: {lateTasks}\nTasks completed on time: {onTimeTasks}\nTotal tasks: {allTasks.Count}";

            StatisticsPanel = statisticsInfo;
        }

        private ICommand _filterCommand;
        public ICommand FilterCommand
        {
            get
            {
                if (_filterCommand == null)
                {
                    _filterCommand = new RelayCommand(Filter);
                }
                return _filterCommand;
            }
        }

        private void Filter()
        {
            FilterWindow w = new FilterWindow();
            FilterVM vm = new FilterVM(GetAllTasks(RootsList));
            w.DataContext = vm;
            App.Current.MainWindow = w;
            w.Show();

        }

        ObservableCollection<ToDoList> roots = new Constants().RootLists;


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

        private ObservableCollection<Task> selectedToDoListTasks = new ObservableCollection<Task>();
        public ObservableCollection<Task> SelectedToDoListTasks
        {
            get
            {
                return selectedToDoListTasks;

                isFound = false;
            }
            set
            {
                if (selectedToDoListTasks != value)
                {
                    selectedToDoListTasks = value;
                    OnPropertyChanged(nameof(SelectedToDoListTasks));
                    isFound = false;
                }
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

        
    }
}