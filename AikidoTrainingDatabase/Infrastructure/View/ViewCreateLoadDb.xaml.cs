using AikidoTrainingDatabase.ApplicationLayer;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
        private IApplication application;
        private IGui gui;

        public ViewCreateLoadDb()
        {
            this.InitializeComponent();
            ButtonsUi = new Button[] { ButtonMenuNew, ButtonSearchDatabase, ButtonLoadDatabase };            
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

        private async void ButtonLoadDatabase_Click(object sender, RoutedEventArgs e)
        {
            string path = TextBoxDatabasePath.Text;

            // Check the path
            if (checkPath(path))
            {
                // Check, if the database exists or a new one should be created
                if (checkPathExist(path))
                {
                    await application.ReadDatabase(path);
                }
                else
                {
                    await Task.Run(() => application.CreateDatabase(path));
                }
                // Navigate to the next view
                ViewParameter viewParameter = new ViewParameter(ViewParameter.Action.MainMenuShow, gui, application);
                gui.NavigateTo(Views.Main, viewParameter);
            }
            else
            {
                // In case the path is incorrect, give a message to the user
                var messageDialog = new MessageDialog("Invalid path");
                // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
                messageDialog.Commands.Add(new UICommand("OK"));
                // Set the command that will be invoked by default
                messageDialog.DefaultCommandIndex = 0;
                // Show the message dialog
                await messageDialog.ShowAsync();
            }
        }


        private bool checkPath(string path)
        {
            return ((path.IndexOfAny(Path.GetInvalidPathChars()) <= 0) && (path != null) && (path != string.Empty));
        }

        private bool checkPathExist(string path)
        {
            return File.Exists(path);
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Object[] param = e.Parameter as Object[];
            if (param != null)
            {
                ViewParameter parameter = new ViewParameter(param);
                application = parameter.GetApplication();
                gui = parameter.GetGui();

                dbPathExtension = application.GetDatabasePathExtension();
            }
            base.OnNavigatedTo(e);
        }
    }
}
