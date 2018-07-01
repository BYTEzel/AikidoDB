using AikidoTrainingDatabase.ApplicationLayer;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AikidoTrainingDatabase.Infrastructure.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu : Page
    {
        IApplication application;

        public Menu()
        {
            this.InitializeComponent();
        }

        private void ButtonTraining_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            application.ShowTrainings();
        }

        private void ButtonExcercise_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            application.ShowExcercises();
        }

        private void ButtonCategory_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            application.ShowCategories();
        }

        private async void ButtonSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await Task.Run(() => application.WriteDatabase());

            // Give a message to the user
            var messageDialog = new ContentDialog();
            messageDialog.Title = "Database has been stored.";
            messageDialog.CloseButtonText = "Thanks";
            await messageDialog.ShowAsync();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Object[] param = e.Parameter as Object[];
            if (param != null)
            {
                ViewParameter parameter = new ViewParameter(param);
                application = parameter.GetApplication();

                // Since we do not need the parameter any more, overwrite them
                param = parameter.GetParameter();
                switch (parameter.GetAction())
                {
                    case ViewParameter.Action.MainMenuShow:
                        // Nothing more to do here
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            base.OnNavigatedTo(e);
        }
    }
}
