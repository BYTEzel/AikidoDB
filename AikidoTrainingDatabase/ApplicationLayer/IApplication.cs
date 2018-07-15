﻿using AikidoTrainingDatabase.Domain;
using System.Collections.ObjectModel;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    public interface IApplication
    {
        void CreateDatabase(string PathDb);
        void CreateCategory();
        void CreateExcercise();
        void CreateTraining();
        void CreateCategoryCallback(ICategory category);
        void CreateExcerciseCallback(IExcercise excercise);
        void CreateTrainingCallback(ITraining training);
        void ShowCategories();
        void ShowExcercises();
        void ShowTrainings();
        void ShowTrainingSingle(ITraining training);
        bool VerifyCategory(ICategory category);
        bool VerifyExcercise(IExcercise excercise);
        bool VerifyTraining(ITraining training);
        void EditCategory(ICategory categoryToEdit);
        void EditExcercise(IExcercise excerciseToEdit);
        void EditTraining(ITraining trainingToEdit);
        void EditCategoryCallback(ICategory categoryEdited);
        void EditExcerciseCallback(IExcercise excerciseEdited);
        void EditTrainingCallback(ITraining trainingEdited);
        void DeleteCategory(ICategory category);
        void DeleteExcercise(IExcercise excercise);
        void DeleteTraining(ITraining training);
        string GetDatabasePathExtension();
        ObservableCollection<Category> GetCategories();
        ObservableCollection<Excercise> GetExcercises();
        ObservableCollection<Training> GetTrainings();
        void WriteDatabase();
        void WriteDatabase(string PathDb);
        void ReadDatabase(string PathDb);
    }
}
