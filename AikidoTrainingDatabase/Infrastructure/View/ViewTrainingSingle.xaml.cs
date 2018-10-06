using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ViewTrainingSingle : Page
    {
        public bool Editable;
        public ITraining training;
        private ITraining trainingTmp;
        private ViewParameter parameter;
        private IGui gui;
        private IApplication application;
        public ObservableCollection<Excercise> excercisesAll;

        private List<Button> listButton;


        public ViewTrainingSingle()
        {
            this.InitializeComponent();
            Editable = false;
            // Add all interactive elements to a list for easier en-/disabling
            listButton = new List<Button>();
            listButton.Add(ButtonBack);
            listButton.Add(ButtonDelete);
            listButton.Add(ButtonExcerciseAdd);
            listButton.Add(ButtonExcerciseDelete);
            listButton.Add(ButtonExport);
        }

        private async void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (application.VerifyTraining(training))
            {
                if (parameter.GetAction() == ViewParameter.Action.TrainingCreate)
                {
                    application.CreateTrainingCallback(training);
                    await Task.Run(() => application.WriteDatabase());
                }
                else if ((parameter.GetAction() == ViewParameter.Action.TrainingEdit) || (parameter.GetAction() == ViewParameter.Action.TrainingShow))
                {
                    application.EditTrainingCallback(training);
                    await Task.Run(() => application.WriteDatabase());
                }
            }
            else
            {
                // Show an error message
                var dialog = new ContentDialog();
                dialog.Content = "Training cannot be created, the data is incomplete :(";
                dialog.CloseButtonText = "Ok";
                var result = await dialog.ShowAsync();
            }
        }

        private void SwitchEdit_Toggled(object sender, RoutedEventArgs e)
        {
            // Nothing to to do here
        }


        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = await (new DeleteDialog().GetDialog().ShowAsync());

            if (result == ContentDialogResult.Primary)
            {
                application.DeleteTraining(training);
                application.ShowTrainings();
            }
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
                    case ViewParameter.Action.TrainingCreate:
                        excercisesAll = param[0] as ObservableCollection<Excercise>;
                        training = new Training();
                        Editable = true;
                        break;
                    case ViewParameter.Action.TrainingShow:
                        training = param[0] as ITraining;
                        excercisesAll = param[1] as ObservableCollection<Excercise>;
                        Editable = false;
                        break;
                    case ViewParameter.Action.TrainingEdit:
                        training = param[0] as ITraining;
                        excercisesAll = param[1] as ObservableCollection<Excercise>;
                        trainingTmp = new Training(training.ID, training.Name, training.Description, training.Excercises);
                        Editable = true;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            base.OnNavigatedTo(e);
        }

        private void ButtonExcerciseAdd_Click(object sender, RoutedEventArgs e)
        {
            // Event gets handled in the flyout -> ListViewFlyoutExcercisesAll_SelectionChanged
        }

        private async void ButtonExcerciseDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListExcercises.SelectedIndex >= 0)
            {
                application.DeleteExcerciseOfTraining(training, ListExcercises.SelectedIndex);
            }
            else
            {
                // Show an error message
                var dialog = new ContentDialog();
                dialog.Content = "Please select a excercise before deleting it ;)";
                dialog.CloseButtonText = "Ok";
                var result = await dialog.ShowAsync();
            }
        }
        
        private void ListViewFlyoutExcercisesAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            application.AddExcerciseToTraining(training, ListViewFlyoutExcercisesAll.SelectedItem as Excercise);
        }

        private async void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            DisableUi();

            try
            {
                var picker = new Windows.Storage.Pickers.FolderPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add(".html");

                Windows.Storage.StorageFolder folder = await picker.PickSingleFolderAsync();
                if (folder != null)
                {
                    // Application now has read/write access to all contents in the picked folder
                    // (including other sub-folder contents)
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                    await application.ExportTrainingAsync(training, training.ID + "_" + training.Name + ".html", folder);
                }
            }
            finally
            {
                EnableUi();
            }
        }

        private void EnableUi()
        {
            foreach(Button b in listButton)
            {
                b.IsEnabled = true;
            }
        }

        private void DisableUi()
        {
            foreach (Button b in listButton)
            {
                b.IsEnabled = false;
            }
        }
    }
}
