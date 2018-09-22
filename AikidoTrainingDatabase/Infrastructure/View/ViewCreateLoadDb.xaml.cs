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
        private IApplication application;
        private IGui gui;

        private string pathDbDefault;

        private string fileDb;
        private string pathDb;
        
        public string PathDb
        {
            get => pathDb + "\\" + fileDb;
            set
            {
                if (value != pathDb)
                {
                    pathDb = value;
                    Bindings.Update();
                }
            }
        }

        public ViewCreateLoadDb()
        {
            InitializeComponent();            
            DataContextChanged += (s, e) => Bindings.Update();
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
                picker.FileTypeFilter.Add("."+fileDb.Split('.')[1]);

                Windows.Storage.StorageFolder folder = await picker.PickSingleFolderAsync();
                if (folder != null)
                {
                    // Application now has read/write access to all contents in the picked folder
                    // (including other sub-folder contents)
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                    // Check, if a database is already present
                    if (await folder.TryGetItemAsync(fileDb) != null)
                    {
                        ContentDialog dialogOverwrite = new ContentDialog();
                        dialogOverwrite.Title = "Unable to create a database here";
                        dialogOverwrite.Content = "A database is already present here. Would you like to overwrite it?";
                        dialogOverwrite.CloseButtonText = "Yes";
                        dialogOverwrite.PrimaryButtonText = "No";

                        ContentDialogResult result = await dialogOverwrite.ShowAsync();
                        // Only assign the result if the user decides this way, otherwise reset the default.
                        PathDb = (result == ContentDialogResult.Primary) ? "" : folder.Path;
                    }
                }
            }
            finally
            {
                EnableUi();
                ButtonLoadDatabase.Focus(FocusState.Programmatic);
            }
        }

        private async void ButtonSearchDatabase_Click(object sender, RoutedEventArgs e)
        {
            DisableUi();

            try
            {
                var picker = new Windows.Storage.Pickers.FolderPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add("*");

                var folder = await picker.PickSingleFolderAsync();
                if (folder != null)
                {
                    // Application now has read/write access to all contents in the picked folder
                    // (including other sub-folder contents)
                    Windows.Storage.AccessCache.StorageApplicationPermissions.
                        FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                    PathDb = folder.Path;
                }
            }
            finally
            {
                EnableUi();
                ButtonLoadDatabase.Focus(FocusState.Programmatic);
            }
        }

        private async void ButtonLoadDatabase_Click(object sender, RoutedEventArgs e)
        {
            string path = PathDb; // TextBoxDatabasePath.Text;

            var errorDialog = new ContentDialog();
            errorDialog.CloseButtonText = "OK";

            bool pathOk = false;

            // Check the path
            if (CheckPath(path))
            {
                // Check, if the database exists or a new one should be created
                if (await CheckPathExist(path))
                {
                    try
                    {
                        await Task.Run(() => application.ReadDatabase(path));
                        pathOk = true;
                    }
                    catch (Exception exception)
                    {
                        errorDialog.Content = exception.Message;
                        await errorDialog.ShowAsync();
                    }
                }
                else
                {
                    try
                    {
                        await Task.Run(() => application.CreateDatabase(path));
                        pathOk = true;
                    }
                    catch (Exception exception)
                    {
                        errorDialog.Content = exception.Message;
                        await errorDialog.ShowAsync();
                    }
                }
                if (pathOk)
                {
                    // Navigate to the next view
                    ViewParameter viewParameter = new ViewParameter(ViewParameter.Action.MainMenuShow, gui, application);
                    gui.NavigateTo(Views.Main, viewParameter);
                }
            }
            else
            {
                errorDialog.Title = "Invalid path";
                // In case the path is incorrect, give a message to the user
                errorDialog.Content = "Unable to create a database here :(\n\nPlease create a new database in an empty location or select an existing one.";
                // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
                // Show the message dialog
                await errorDialog.ShowAsync();
            }
        }


        private bool CheckPath(string path)
        {
            return ((PathDb != pathDbDefault)&&(path.IndexOfAny(Path.GetInvalidPathChars()) <= 0) && (path != null) && (path != string.Empty));
        }

        private Task<bool> CheckPathExist(string path)
        {
            return Task.Run(() => File.Exists(path));
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

                fileDb = application.GetDatabasePathExtension();
                pathDbDefault = PathDb;   // Store the default path for later checking - if the user interrupts certain operations, the (invalid) default path will be reset. We use this string to check it.
            }
            base.OnNavigatedTo(e);
        }
    }
}
