using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AikidoTrainingDatabase.Infrastructure.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewCreateLoadDb : Page
    {
        private Button[] ButtonsUi;

        public ViewCreateLoadDb()
        {
            this.InitializeComponent();
            ButtonsUi = new Button[] { ButtonMenuNew, ButtonSearchDatabase, ButtonLoadDatabase };
        }

        private void ButtonNewDatabase_Click(object sender, RoutedEventArgs e)
        {
            DisableUi();
        }

        private void EnableUi()
        {
            SetUiElements(true);
        }

        private void DisableUi()
        {
            SetUiElements(false);
        }

        private void SetUiElements(bool active)
        {
            foreach (Button b in ButtonsUi)
            {
                b.IsEnabled = active;
            }
        }

        private async void ButtonSearchDatabase_Click(object sender, RoutedEventArgs e)
        {
            DisableUi();

            try
            {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add(".xml");

                Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
                if (file != null)
                {
                    // Application now has read/write access to the picked file
                    TextBoxDatabasePath.Text = file.Name;
                }
            }
            finally
            {
                EnableUi();
            }
        }

        private void ButtonLoadDatabase_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
