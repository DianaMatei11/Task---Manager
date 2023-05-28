using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mwvm;

        public MainWindow()
        {
            InitializeComponent();
            mwvm = new MainWindowViewModel();
            this.DataContext = mwvm;
        }
        public MainWindow(string name, ObservableCollection<ToDoList> list)
        {
            InitializeComponent();
            mwvm = new MainWindowViewModel(name, list);
            this.DataContext = mwvm;
        }

        public MainWindow(MainWindowViewModel mwvm)
        {
            this.mwvm = mwvm;
        }

        private void ToDoListsTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Get the selected to-do list item from the tree view
            var selectedToDoList = e.NewValue as ToDoList;

            // Set the selected to-do list item in the view model
            mwvm.SelectedToDoList = selectedToDoList;
        }

        
    }
}
