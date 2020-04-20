using Telbot.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telbot.system;
using Telbot.model;
using Telbot.storage;
using Telbot.api;
using System.Threading;
using Telbot.telegram;

namespace Telbot.Pages.Authentications
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Login : Page
    {

        private Cursor previousCursor;


        public Login()
        {
            InitializeComponent();

        }






        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (checkEntry())
            {
                previousCursor = Mouse.OverrideCursor;
                Mouse.OverrideCursor = Cursors.Wait;

                App_service service = new App_service();
                service.login(txt_user_name.Text, txt_password.Password, login_completed);
            }
            //this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterNumber.xaml", UriKind.Relative));
        }





        private void login_completed(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = previousCursor;

            App_model app = (App_model) sender;
            if (app.id > 0)
            {
                App_pref pref = new App_pref();
                pref.saveApp(app);
                G.app = pref.getApp();

                //checkTelegramAuth();
                this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterNumber.xaml", UriKind.Relative));
            }
        }

        // private void checkTelegramAuth()
        //{
        //    Auth_telegram auth = new Auth_telegram();
        //    auth.isUserAuthorized(on_telegram_auth_checked);
        //}

        // private void on_telegram_auth_checked(object sender, EventArgs e)
        // {


        //     TelegramResponse res = (TelegramResponse)sender;
        //     if (res.status == 1)
        //     {
        //         MainWindow mainWindow = new MainWindow();
        //         mainWindow.Show();
        //         Window.GetWindow(this).Close();

        //         Mouse.OverrideCursor = previousCursor;

        //     }
        //     else
        //     {
        //         Mouse.OverrideCursor = previousCursor;
        //         FailedDialog _dialog = new FailedDialog("اتصال به سرور تلگرام با مشکل مواجه شد");
        //         _dialog.ShowDialog();
        //         this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterNumber.xaml", UriKind.Relative));
        //     }
        // }




        private bool checkEntry()
        {
            if (txt_user_name.Text.Length == 0 || txt_password.Password.Length == 0)
            {
                FailedDialog _dialog = new FailedDialog("لطفا نام کاربری و رمز عبور را با دقت وارد کنید");
                _dialog.ShowDialog();
                return false;
            }
            return true;
        }





        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var link = (Hyperlink)sender;
            var uri = Config.SITE_URL;
            Process.Start(new ProcessStartInfo(uri));
            e.Handled = true;
            this.NavigationService.Navigate(new Uri("/Pages/authentications/Login.xaml", UriKind.Relative));
           
        }
    }
}
