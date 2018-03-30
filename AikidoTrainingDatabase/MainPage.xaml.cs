using Windows.UI.Xaml.Controls;
using AikidoTrainingDatabase.Infrastructure;
using AikidoTrainingDatabase.ApplicationLayer;
using System.Collections;
using AikidoTrainingDatabase.Infrastructure.View;
using AikidoTrainingDatabase.Domain;
using System;
using Windows.UI.Xaml;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AikidoTrainingDatabase
{
    /// <summary>
    /// Controls the views and navigation, does not contain any GUI elements itself.
    /// </summary>
    public sealed partial class MainPage : Page, IGui
    {
        private IApplication application;                

        public MainPage()
        {
            InitializeComponent();
            application = new ApplicationLayer.Application(this);

            // Add handler for storing the database on closing the program
            Windows.UI.Xaml.Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);         
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ShowCreateLoadDatabase();

            /*
            await application.Init();
            // Navigate to the menu
            ViewParameter parameter = new ViewParameter(ViewParameter.Action.MainMenuShow, this, application);
            NavigateTo(Views.Main, parameter);
            */
        }


        public void ShowCreateLoadDatabase()
        {
            ViewParameter par = new ViewParameter(ViewParameter.Action.Empty, this, application);
            NavigateTo(Views.CreateLoadDatabase, par);
        }

        public void ShowCategoryPage(ICollection categoryCollection)
        {
            ViewParameter par = new ViewParameter(ViewParameter.Action.CategoryShow, this, application);
            par.AddParameter(categoryCollection);
            NavigateTo(Views.Category, par);
        }


        public void ShowExcercisePage(ICollection exerciseCollection)
        {
            ViewParameter par = new ViewParameter(ViewParameter.Action.ExcerciseShow, this, application);
            par.AddParameter(exerciseCollection);
            NavigateTo(Views.Excercise, par);
        }

        public void ShowMainMenu()
        {
            ViewParameter parameter = new ViewParameter(ViewParameter.Action.Empty, this, application);
            NavigateTo(Views.Main);
        }
        
        public void RequestCategory()
        {
            ViewParameter parameter = new ViewParameter(ViewParameter.Action.CategoryCreate, this, application);
            parameter.AddParameter(application);
            NavigateTo(Views.CategorySingle, parameter);
        }


        public void ShowEditCategory(ICategory category, int index)
        {
            ViewParameter parameter = new ViewParameter(ViewParameter.Action.CategoryEdit, this, application);
            parameter.AddParameter(category);
            parameter.AddParameter(index);
            NavigateTo(Views.CategorySingle, parameter);
        }

        public void NavigateTo(Views view)
        {
            NavigateTo(view, null);
        }
        
        public void NavigateTo(Views view, ViewParameter parameter)
        {
            object[] paramObj;
            if (parameter != null)
            {
                paramObj = parameter.GetParameterComplete();
                switch (view)
                {
                    case Views.Category:
                        Frame.Navigate(typeof(ViewCategory), paramObj);
                        break;
                    case Views.CategorySingle:
                        Frame.Navigate(typeof(ViewCategorySingle), paramObj);
                        break;
                    case Views.Excercise:
                        Frame.Navigate(typeof(ViewExcercise), paramObj);
                        break;
                    case Views.Main:
                        Frame.Navigate(typeof(Menu), paramObj);
                        break;
                    case Views.CreateLoadDatabase:
                        Frame.Navigate(typeof(ViewCreateLoadDb), paramObj);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            else
            {
                switch (view)
                {
                    case Views.Category:
                        Frame.Navigate(typeof(ViewCategory));
                        break;
                    case Views.CategorySingle:
                        Frame.Navigate(typeof(ViewCategorySingle));
                        break;
                    case Views.Excercise:
                        Frame.Navigate(typeof(ViewExcercise));
                        break;
                    case Views.Main:
                        Frame.Navigate(typeof(Menu));
                        break;
                    case Views.CreateLoadDatabase:
                        Frame.Navigate(typeof(ViewCreateLoadDb));
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }            
        }

        /// <summary>
        /// Write all the data before closing the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_Suspending(Object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            // The writing in this case is threaded, so no await needed
            //await application.WriteDatabase();
        }

       
    }
}
