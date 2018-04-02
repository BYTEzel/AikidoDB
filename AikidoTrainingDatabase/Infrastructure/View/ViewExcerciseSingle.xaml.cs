using AikidoTrainingDatabase.ApplicationLayer;
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
    public sealed partial class ViewExcerciseSingle : Page
    {
        public IExcercise excercise;
        IExcercise excerciseTmp;   // as internal storage for the editing
        IApplication application;
        IGui gui;
        ViewParameter parameter;

        int index; // Index keeps track of the position of the current dataset (in case it is edited)


        public ViewExcerciseSingle()
        {
            this.InitializeComponent();
            excercise = new Excercise();
            excerciseTmp = new Excercise();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (parameter.GetAction() == ViewParameter.Action.ExcerciseEdit)
            {
                // Reset the Excercise
                application.EditExcercise(excerciseTmp, index);
            }
            application.ShowExcercises();
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
                    case ViewParameter.Action.ExcerciseCreate:
                        break;
                    case ViewParameter.Action.ExcerciseEdit:
                        excercise = param[0] as IExcercise;
                        excerciseTmp = new Excercise(excercise.Name, excercise.Description, excercise.Categories, excercise.Images);
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
