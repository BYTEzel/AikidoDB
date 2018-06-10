using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure.Dialogs;
using AikidoTrainingDatabase.Infrastructure.ExtendedClasses;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AikidoTrainingDatabase.Infrastructure.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewExcercise : Page
    {
        public ObservableCollection<ExcerciseDisplay> excerciseCollectionDisplay;
        private IApplication application;
        private IGui gui;

        public ViewExcercise()
        {
            this.InitializeComponent();
            excerciseCollectionDisplay = new ObservableCollection<ExcerciseDisplay>();
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
                    case ViewParameter.Action.ExcerciseShow:
                        CastExcerciseCollection(param[0] as ObservableCollection<Excercise>);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            base.OnNavigatedTo(e);
        }

        private void CastExcerciseCollection(ObservableCollection<Excercise> observableCollection)
        {
            excerciseCollectionDisplay.Clear();
            foreach (Excercise e in observableCollection)
            {
                excerciseCollectionDisplay.Add(new ExcerciseDisplay(e));
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ViewParameter parameter = new ViewParameter(ViewParameter.Action.MainMenuShow, gui, application);
            gui.NavigateTo(Views.Main, parameter);
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            application.CreateExcercise();
        }

        private void ListViewExcercise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                ListViewItem lvi = (sender as ListView).ContainerFromItem(item) as ListViewItem;
                lvi.ContentTemplate = (DataTemplate)this.Resources["Detail"];
            }
            //Remove DataTemplate for unselected items
            foreach (var item in e.RemovedItems)
            {
                ListViewItem lvi = (sender as ListView).ContainerFromItem(item) as ListViewItem;
                lvi.ContentTemplate = (DataTemplate)this.Resources["Normal"];
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ExcerciseDisplay excerciseDisplayTmp = (e.OriginalSource as Button).DataContext as ExcerciseDisplay;
            application.EditExcercise(excerciseDisplayTmp.GetExcercise());
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            // Get the element on which the button was pressed
            Button button = e.OriginalSource as Button;
            ExcerciseDisplay selectedExcercise = (e.OriginalSource as Button).DataContext as ExcerciseDisplay;
            DisplayDeleteDataDialog(selectedExcercise);
        }

        private async void DisplayDeleteDataDialog(IExcercise excercise)
        {
            ContentDialogResult result = await (new DeleteDialog()).GetDialog().ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                application.DeleteExcercise(excercise);
                // Manually refresh the displayed exercises
                CastExcerciseCollection(application.GetExcercises());
            }
        }
    }
}
