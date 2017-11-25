using Windows.UI.Xaml.Controls;

namespace AikidoTrainingDatabase.Infrastructure.Dialogs
{
    class DeleteDialog : ContentDialog
    {
        private ContentDialog deleteDialog;
        public DeleteDialog()
        {
            deleteDialog = new ContentDialog
            {
                Title = "Deleting dataset",
                Content = "Are you sure you want to continue?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };
        }

        public ContentDialog GetDialog()
        {
            return deleteDialog;
        }
    }
}
