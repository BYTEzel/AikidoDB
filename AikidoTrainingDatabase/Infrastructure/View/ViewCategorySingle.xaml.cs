using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure.Dialogs;
using System;
using System.Threading.Tasks;
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
        public bool Editable;

        public ICategory category;
        ICategory categoryTmp;   // as internal storage for the editing
        IApplication application;
        IGui gui;
        ViewParameter parameter;

        public ViewCategorySingle()
        {
            InitializeComponent();
            category = new Category(string.Empty, string.Empty);
            categoryTmp = new Category(string.Empty, string.Empty);
            Editable = false;
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
                    case ViewParameter.Action.CategoryShow:
                        category = param[0] as ICategory;
                        Editable = false;
                        break;
                    case ViewParameter.Action.CategoryCreate:
                        Editable = true;
                        break;
                    case ViewParameter.Action.CategoryEdit:
                        category = param[0] as ICategory;
                        categoryTmp = new Category(category.Name, category.Description);
                        Editable = false;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            base.OnNavigatedTo(e);
        }

        private async void ButtonBack_Click(object sender, RoutedEventArgs e)
        {

            if (application.VerifyCategory(category))
            {
                if (parameter.GetAction() == ViewParameter.Action.CategoryCreate)
                {
                    application.CreateCategoryCallback(category);
                    await Task.Run(() => application.WriteDatabase());
                }
                else if ((parameter.GetAction() == ViewParameter.Action.CategoryEdit) || (parameter.GetAction() == ViewParameter.Action.CategoryShow))
                {
                    application.EditCategoryCallback(category);
                    await Task.Run(() => application.WriteDatabase());
                }
            }
            else
            {
                // Show an error message
                var dialog = new ContentDialog();
                dialog.Content = "Category cannot be created, the data is incomplete :(";
                dialog.CloseButtonText = "Ok";
                var result = await dialog.ShowAsync();
            }
        }

        private void SwitchEdit_Toggled(object sender, RoutedEventArgs e)
        {
            // Nothing to do here
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = await(new DeleteDialog().GetDialog().ShowAsync());

            if (result == ContentDialogResult.Primary)
            {
                application.DeleteCategory(category);
                application.ShowCategories();
            }
        }
    }
}
