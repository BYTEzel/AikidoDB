using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure.Dialogs;
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
    public sealed partial class ViewTrainingSingle : Page
    {
        public bool Editable;
        public ITraining training;
        private ITraining trainingTmp;
        private ViewParameter parameter;
        private IGui gui;
        private IApplication application;
        public ObservableCollection<Excercise> excercisesAll;

        public ViewTrainingSingle()
        {
            this.InitializeComponent();
            Editable = false;
        }

        private async void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (application.VerifyTraining(training))
            {
                if (parameter.GetAction() == ViewParameter.Action.TrainingCreate)
                {
                    application.CreateTrainingCallback(training);
                }
                else if ((parameter.GetAction() == ViewParameter.Action.TrainingEdit) || (parameter.GetAction() == ViewParameter.Action.TrainingShow))
                {
                    application.EditTrainingCallback(training);
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

        }

        private void ButtonExcerciseDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
