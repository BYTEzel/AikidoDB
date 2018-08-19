using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure.Dialogs;
using System;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AikidoTrainingDatabase.Infrastructure.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewCategory : Page
    {
        public ICollection categoryCollection;
        private IApplication application;
        private IGui gui;

        public ViewCategory()
        {
            InitializeComponent();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ViewParameter parameter = new ViewParameter(ViewParameter.Action.MainMenuShow, gui, application);
            gui.NavigateTo(Views.Main, parameter);
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Object[] param = e.Parameter as Object[];
            if (param != null)
            {
                ViewParameter parameter = new ViewParameter(param);
                gui = parameter.GetGui();
                application = parameter.GetApplication();

                // Since we do not need the parameter any more, overwrite them
                param = parameter.GetParameter();
                switch (parameter.GetAction())
                {
                    case ViewParameter.Action.CategoryShow:
                        categoryCollection = param[0] as ICollection;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            base.OnNavigatedTo(e);
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            application.CreateCategory();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            application.ShowCategorySingle(ListViewCategory.SelectedItem as Category);
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            // Get the element on which the button was pressed
            Category selectedCategory = (e.OriginalSource as Button).DataContext as Category;
            DisplayDeleteDataDialog(selectedCategory);
        }

        private async void DisplayDeleteDataDialog(ICategory category)
        {
            ContentDialogResult result = await (new DeleteDialog()).GetDialog().ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                application.DeleteCategory(category);
            }
        }


        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            application.EditCategory((e.OriginalSource as Button).DataContext as Category);
        }
    }
}
