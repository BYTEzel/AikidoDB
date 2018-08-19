using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure.Dialogs;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AikidoTrainingDatabase.Infrastructure.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewExcerciseSingle : Page
    {
        public bool Editable;
        
        public IExcercise excercise;
        public ObservableCollection<Category> categoriesList;
        IExcercise excerciseTmp;   // as internal storage for the editing
        IApplication application;
        IGui gui;
        ViewParameter parameter;

        public ViewExcerciseSingle()
        {
            this.InitializeComponent();
            excercise = new Excercise();
            excerciseTmp = new Excercise();
            categoriesList = new ObservableCollection<Category>();
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
                    case ViewParameter.Action.ExcerciseShow:
                        excercise = param[0] as IExcercise;
                        categoriesList = param[1] as ObservableCollection<Category>;
                        break;
                    case ViewParameter.Action.ExcerciseCreate:
                        categoriesList = param[0] as ObservableCollection<Category>;
                        break;
                    case ViewParameter.Action.ExcerciseEdit:
                        excercise = param[0] as IExcercise;
                        excerciseTmp = new Excercise(excercise.ID, excercise.Name, excercise.Description, excercise.Categories);
                        categoriesList = param[1] as ObservableCollection<Category>;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            base.OnNavigatedTo(e);
        }

        private void ListViewCategories_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);            
        }

        private void FlyoutSelection_Closed(object sender, object e)
        {
            excercise.Categories.Clear();
            foreach (Category cat in ListViewCategoriesAll.SelectedItems)
            {
                excercise.Categories.Add(cat);
            }
        }

        private async void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (application.VerifyExcercise(excercise))
            {
                if (parameter.GetAction() == ViewParameter.Action.ExcerciseCreate)
                {
                    application.CreateExcerciseCallback(excercise);
                }
                else if ((parameter.GetAction() == ViewParameter.Action.ExcerciseEdit) || (parameter.GetAction() == ViewParameter.Action.ExcerciseShow))
                {
                    application.EditExcerciseCallback(excercise);
                }
            }
            else
            {
                // Show an error message
                var dialog = new ContentDialog();
                dialog.Content = "Excercise cannot be created, the data is incomplete :(";
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
                application.DeleteExcercise(excercise);
                application.ShowExcercises();
            }
        }
    }
}
