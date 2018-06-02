using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure.View;
using System.Collections;

namespace AikidoTrainingDatabase.Infrastructure
{
    // Collect all the possible views to be selected for navigation in NavigateTo
    public enum Views { CreateLoadDatabase, Main, Category, CategorySingle, Excercise, ExcerciseSingle}

    public interface IGui
    {
        /// <summary>
        /// Shows the startup menu to load or create a new database.
        /// </summary>
        void ShowCreateLoadDatabase();
        /// <summary>
        /// Shows the main menu with the options to select the certain data
        /// types.
        /// </summary>
        void ShowMainMenu();
        /// <summary>
        /// Shows an overview of all the categories currently present.
        /// </summary>
        /// <param name="categoryCollection"></param>
        void ShowCategoryPage(ICollection categoryCollection);
        /// <summary>
        /// Get a single category.
        /// </summary>
        void ShowCreateCategory();
        /// <summary>
        /// Open a mask that allows to edit a certain category.
        /// </summary>
        /// <param name="category"></param>
        void ShowEditCategory(ICategory category);
        /// <summary>
        /// Shows an overview of all the excercises currently present.
        /// </summary>
        /// <param name="exerciseCollection"></param>
        void ShowExcercisePage(ICollection exerciseCollection);
        void ShowCreateExcercise();
        void ShowEditExcercise(IExcercise excercise);
        /// <summary>
        /// Navigate to a certain view without any parameter
        /// </summary>
        /// <param name="view"></param>
        void NavigateTo(Views view);
        /// <summary>
        /// Navigate to a certain view giving parameter to the new view.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="parameter"></param>
        void NavigateTo(Views view, ViewParameter parameter);
    }
}
