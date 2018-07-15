﻿using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
                        training = new Training();
                        Editable = true;
                        break;
                    case ViewParameter.Action.TrainingShow:
                        training = param[0] as ITraining;
                        Editable = false;
                        break;
                    case ViewParameter.Action.TrainingEdit:
                        training = param[0] as ITraining;
                        trainingTmp = new Training(training.ID, training.Name, training.Description, training.Excercises);
                        Editable = true;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            base.OnNavigatedTo(e);
        }
    }
}
