﻿using System.Collections.ObjectModel;
using AikidoTrainingDatabase.Domain;
using Windows.UI.Xaml.Media.Imaging;

namespace AikidoTrainingDatabase.Infrastructure.ExtendedClasses
{
    public class ExcerciseDisplay : IExcercise
    {
        private ObservableCollection<Category> categories;
        private ObservableCollection<BitmapImage> images;
        private string name;
        private string description;
        private string categoriesDisplay;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public ObservableCollection<BitmapImage> Images { get => images; set => images = value; }
        public ObservableCollection<Category> Categories { get => categories; set => categories = value; }
        public string CategoriesDisplay
        {
            get
            {
                string s = string.Empty;
                foreach(Category c in categories)
                {
                    s += c.Name;
                    // For the last entry do not add the "," in between the names
                    if (c != categories[categories.Count-1])
                    {
                        s += ", ";
                    }
                }
                return s;
            }
        }

        public ExcerciseDisplay(IExcercise excercise)
        {
            Name = excercise.Name;
            Description = excercise.Description;
            Categories = excercise.Categories;
            Images = excercise.Images;
        }
    }
}