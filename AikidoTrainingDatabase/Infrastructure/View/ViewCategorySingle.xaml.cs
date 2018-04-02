using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AikidoTrainingDatabase.Infrastructure.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewCategorySingle : Page
    {
        public ICategory category;
        ICategory categoryTmp;   // as internal storage for the editing
        IApplication application;
        IGui gui;
        ViewParameter parameter;

        int index; // Index keeps track of the position of the current dataset (in case it is edited)

        public ViewCategorySingle()
        {
            InitializeComponent();
            category = new Category(string.Empty, string.Empty);
            categoryTmp = new Category(string.Empty, string.Empty);
        }

        private async void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if(application.VerifyCategory(category))
            {
                if (parameter.GetAction() == ViewParameter.Action.CategoryCreate)
                {
                    application.CreateCategoryCallback(category);
                }
                else if (parameter.GetAction() == ViewParameter.Action.CategoryEdit)
                {
                    application.EditCategoryCallback(category, index);
                }
            }
            else
            {
                // Show an error message
                var dialog = new ContentDialog();
                dialog.Content = "Category cannot be created, the data is incomplete :(";
                dialog.CloseButtonText = "OK";
                var result = await dialog.ShowAsync();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (parameter.GetAction() == ViewParameter.Action.CategoryEdit)
            {
                // Reset the category
                application.EditCategoryCallback(categoryTmp, index);
            }
            application.ShowCategories();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Object[] param = e.Parameter as Object[];
            if (param != null)
            {
                parameter = new ViewParameter(param);
                gui = parameter.GetGui();
                application = parameter.GetApplication();

                // Since we do not need the parameter any more, overwrite them
                param = parameter.GetParameter();
                switch (parameter.GetAction())
                {
                    case ViewParameter.Action.CategoryCreate:
                        break;
                    case ViewParameter.Action.CategoryEdit:
                        category = param[0] as ICategory;
                        categoryTmp = new Category(category.Name, category.Description);
                        index = (int)param[1];
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            base.OnNavigatedTo(e);
        }
    }
}
