using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure.ExtendedClasses;
using System;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Excercise> excerciseCollection;
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
                        excerciseCollection = param[0] as ObservableCollection<Excercise>;
                        CastExcerciseCollection(excerciseCollection);
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
    }
}
