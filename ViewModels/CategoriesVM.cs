using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Models;
using TaskManager.Commands;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace TaskManager.ViewModels
{
    class CategoriesVM : BaseVM
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

        public CategoriesVM()
        {
            Categories = constants.Categories;

        }

        private string inputCategory;
        public string InputCategory
        {
            get { return inputCategory; }
            set
            {
                inputCategory = value;
                OnPropertyChanged(nameof(InputCategory));
            }
        }

        private ICommand createCommand;
        public ICommand CreateCommand
        {
            get
            {
                if (createCommand == null)
                {
                    createCommand = new RelayCommand(Create);
                }
                return createCommand;
            }
        }
        public void Create()
        {

            string filePath = @"..\..\..\Assets\categories.txt";
            if (InputCategory != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    Categories.Add(InputCategory);
                    writer.WriteLine(InputCategory);
                }
            }
            else
            {
                MessageBox.Show("Please write a category!");
            }

        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(Delete);
                }
                return deleteCommand;
            }
            set { deleteCommand = value; }
        }

        private string selectedCategory;
        public string SelectedCategory
        {
            get { return selectedCategory; }
            set { selectedCategory = value; OnPropertyChanged("SelectedCategory"); }
        }

        public void Delete()
        {
            if (SelectedCategory != null && Categories.Contains(SelectedCategory))
            {
                Categories.Remove(SelectedCategory);
                File.WriteAllLines(@"..\..\..\Assets\categories.txt", Categories);
                
            }
        }



    }
}
