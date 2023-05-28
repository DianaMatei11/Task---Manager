using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for ChangePathWindow.xaml
    /// </summary>
    public partial class ChangePathWindow : Window
    {
        ChangePathVM vm;
        public ChangePathWindow(ChangePathVM vm)
        {
            InitializeComponent();
            this.vm = vm;
        }

        private void ToDoListsTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Get the selected to-do list item from the tree view
            var selectedToDoList = e.NewValue as ToDoList;

            // Set the selected to-do list item in the view model
            vm.SelectedToDoList = selectedToDoList;
        }
    }
}
