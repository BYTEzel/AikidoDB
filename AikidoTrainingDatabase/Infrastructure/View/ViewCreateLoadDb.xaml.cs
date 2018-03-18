using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AikidoTrainingDatabase.ApplicationLayer;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AikidoTrainingDatabase.Infrastructure.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewCreateLoadDb : Page
    {
        private Button[] ButtonsUi;
        private string dbPathExtension;

        public ViewCreateLoadDb()
        {
            this.InitializeComponent();
            ButtonsUi = new Button[] { ButtonMenuNew, ButtonSearchDatabase, ButtonLoadDatabase };
            dbPathExtension = (new ApplicationLayer.Application()).GetDatabasePathExtension();
        }

        private async void ButtonNewDatabase_Click(object sender, RoutedEventArgs e)
        {
            DisableUi();

            try
            {
                var picker = new Windows.Storage.Pickers.FolderPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add("*");

                Windows.Storage.StorageFolder folder = await picker.PickSingleFolderAsync();
                if (folder != null)
                {
                    // Application now has read/write access to all contents in the picked folder
                    // (including other sub-folder contents)
                    Windows.Storage.AccessCache.StorageApplicationPermissions.
                        FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                    TextBoxDatabasePath.Text = folder.Path + "\\" + dbPathExtension;
                    // Set the cursor at the position where the name could be typed in.
                    TextBoxDatabasePath.SelectionStart = folder.Path.Length+1;
                    TextBoxDatabasePath.SelectionLength = 0;
                }
            }
            finally
            {
                EnableUi();
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
                picker.FileTypeFilter.Add(dbPathExtension);

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
    }
}
