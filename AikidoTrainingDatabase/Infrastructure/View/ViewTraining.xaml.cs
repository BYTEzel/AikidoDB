using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
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
    public sealed partial class ViewTraining : Page
    {
        public ObservableCollection<Training> trainingCollection;
        private IApplication application;
        private IGui gui;

        public ViewTraining()
        {
            this.InitializeComponent();
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
                    case ViewParameter.Action.TrainingShow:
                        trainingCollection = param[0] as ObservableCollection<Training>;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            base.OnNavigatedTo(e);
        }


        #region Button interaction
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ViewParameter parameter = new ViewParameter(ViewParameter.Action.MainMenuShow, gui, application);
            gui.NavigateTo(Views.Main, parameter);
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Training trainingTmp = (e.OriginalSource as Button).DataContext as Training;
            application.EditTraining(trainingTmp);
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Training trainingTmp = (e.OriginalSource as Button).DataContext as Training;
            application.DeleteTraining(trainingTmp);
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            application.CreateTraining();
        }

        private void ListViewTraining_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        #endregion
    }
}
