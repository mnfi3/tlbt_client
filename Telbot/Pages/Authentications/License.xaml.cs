using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Telbot.api;
using Telbot.Dialogs;
using Telbot.license;
using Telbot.model;
using Telbot.storage;
using Telbot.system;
using Telbot.telegram;

namespace Telbot.Pages.Authentications
{
    /// <summary>
    /// Interaction logic for LicensePage.xaml
    /// </summary>
    public partial class License : Page
    {

        private Cursor previousCursor;

        public License()
        {
            InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            checkLicense();
        }

        private void btn_check_again_Click(object sender, RoutedEventArgs e)
        {
            checkLicense();
        }


        private void checkLicense()
        {
            previousCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;

            btn_check_again.Visibility = Visibility.Collapsed;

            //local sesseion is set
            if (G.app.is_logged_in == 1)
            {
                App_service service = new App_service();
                service.authCheck(auth_checked);
            }
            //local session not set
            else
            {
                this.NavigationService.Navigate(new Uri("/Pages/Authentications/Login.xaml", UriKind.Relative));
            }
        }


        private void auth_checked(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = previousCursor;

            Response res = (Response)sender;
            App_model app = new App_model();
            //server response is ok
            if (res.status == 1)
            {
                JObject data = res.data;
                app = App_model.parse(data);
                app.is_logged_in = 1;
                App_pref pref = new App_pref();
                pref.saveApp(app);
                G.app = pref.getApp();

                //check time token
                DynamicTokenManager manager = new DynamicTokenManager();
                //time token is ok
                if (manager.checkTokenValidity(app.time_token))
                {
                    //checkTelegramAuth();
                    previousCursor = Mouse.OverrideCursor;
                    Mouse.OverrideCursor = Cursors.Wait;

                    checkTelegramSession();
                }
                //time token not ok
                else
                {
                    FailedDialog _dialog = new FailedDialog("لطفا تاریخ و ساعت سیستم خود را برای بررسی لایسنس به صورت دقیق تنظیم کنید ");
                    _dialog.ShowDialog();
                    btn_check_again.Visibility = Visibility;
                }

            }
            //server response not ok
            else if (res.status == 0)
            {
                this.NavigationService.Navigate(new Uri("/Pages/authentications/Login.xaml", UriKind.Relative));
            }
            //not server connection
            else
            {
                FailedDialog _dialog = new FailedDialog(res.full_response);
                _dialog.ShowDialog();
                btn_check_again.Visibility = Visibility;

            }
        }


        private void checkTelegramSession()
        {
            if (G.telegram.is_session_exist == 1)
            {
                txt_message.Text = "در حال بررسی ارتباط با سرور تلگرام ...";
                checkTelegramAuth();
            }
            else
            {
                Mouse.OverrideCursor = previousCursor;
                this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterNumber.xaml", UriKind.Relative));
            }
        }



        private void checkTelegramAuth()
        {
            Auth_telegram auth = new Auth_telegram();
            auth.isUserAuthorized(on_telegram_auth_checked);
        }

        private void on_telegram_auth_checked(object sender, EventArgs e)
        {
           

            TelegramResponse res = (TelegramResponse)sender;
            if (res.status == 1)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this).Close();

                Mouse.OverrideCursor = previousCursor;

            }
            else
            {
                Mouse.OverrideCursor = previousCursor;
                FailedDialog _dialog = new FailedDialog("اتصال به سرور تلگرام با مشکل مواجه شد");
                _dialog.ShowDialog();
                this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterNumber.xaml", UriKind.Relative));
            }
        }

        

        
    }
}
