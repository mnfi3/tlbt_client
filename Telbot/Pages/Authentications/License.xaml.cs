using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
using Telbot.helper;
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

            checkForUpdate();

            checkLicense();
        }

        private void btn_check_again_Click(object sender, RoutedEventArgs e)
        {
            checkLicense();
        }



        private void checkForUpdate()
        {
            var webRequest = WebRequest.Create(@"" + Urls.UPDATE_INFO_URL);

            string str_update = Config.VERSION;
            try
            {
                using (var response = webRequest.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    str_update = reader.ReadToEnd();
                }

            }
            catch (Exception e1) {
                Log.e("check for update failed.error=" + e1.ToString(), "License", "checkForUpdate");
            }



            //application is latest version
            if (str_update.Contains(Config.VERSION)) return;


            ConfirmDialog _dialog = new ConfirmDialog("نسخه ی جدید برنامه منتشر شده است.آیا میخواهید آپدیت کنید؟");
            if (_dialog.ShowDialog() == false) return;

            //backup database
            DB_helper.backup();
            string updater_path = AppDomain.CurrentDomain.BaseDirectory + "updater.exe";
            Process updater_app = new Process();
            updater_app.StartInfo.FileName = updater_path;
            try
            {
                updater_app.Start();
                System.Windows.Application.Current.Shutdown();
            }
            catch (Exception ex) 
            {
                Log.e("updater run failed.error=" + ex.ToString(), "License", "checkForUpdate");
            }


            return;
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
                Mouse.OverrideCursor = previousCursor;
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
                Log.i("server auth was successfull from server", "License", "auth_checked");

                //check time token
                DynamicTokenManager manager = new DynamicTokenManager();
                //time token is ok
                if (manager.checkTokenValidity(app.time_token))
                {
                    
                    //previousCursor = Mouse.OverrideCursor;
                    //Mouse.OverrideCursor = Cursors.Wait;

                    Log.i("app auth was successfull", "License", "auth_checked");


                    this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterNumber.xaml", UriKind.Relative));

                    //MainWindow mainWindow = new MainWindow();
                    //mainWindow.Show();
                    //Window.GetWindow(this).Close();


                    //checkTelegramSession();
                }
                //time token not ok
                else
                {
                    FailedDialog _dialog = new FailedDialog("لطفا تاریخ و ساعت سیستم خود را برای بررسی لایسنس به صورت دقیق تنظیم کنید ");
                    _dialog.ShowDialog();
                    btn_check_again.Visibility = Visibility;
                    Log.e("time token check failed", "License", "auth_checked");
                }

            }
            //server response not ok
            else if (res.status == 0)
            {
                FailedDialog _dialog = new FailedDialog(res.message);
                _dialog.ShowDialog();
                this.NavigationService.Navigate(new Uri("/Pages/authentications/Login.xaml", UriKind.Relative));
            }
            //not server connection
            else
            {
                FailedDialog _dialog = new FailedDialog(res.message);
                _dialog.ShowDialog();
                btn_check_again.Visibility = Visibility;

            }
        }


        //private void checkTelegramSession()
        //{
        //    int a = G.telegram.is_logged_in;
        //    if (File.Exists("session.dat") && G.telegram.is_logged_in == 1)
        //    {
        //        txt_message.Text = "در حال بررسی ارتباط با سرور تلگرام ...";
        //        checkTelegramAuth();
        //    }
        //    else
        //    {
        //        Mouse.OverrideCursor = previousCursor;
        //        this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterNumber.xaml", UriKind.Relative));
        //        Log.w("telegram sesseion file not exist", "License", "checkTelegramSession");
        //    }
        //}



        //private void checkTelegramAuth()
        //{
        //    Auth_telegram auth = new Auth_telegram();
        //    auth.isUserAuthorized(on_telegram_auth_checked);
        //}

        //private void on_telegram_auth_checked(object sender, EventArgs e)
        //{
        //    Mouse.OverrideCursor = previousCursor;

        //    TelegramResponse res = (TelegramResponse)sender;
        //    if (res.status == 1)
        //    {

        //        //save telegram login flag
        //        Telegram_pref pref = new Telegram_pref();
        //        G.telegram.is_logged_in = 1;
        //        pref.saveTelegram(G.telegram);

        //        MainWindow mainWindow = new MainWindow();
        //        mainWindow.Show();
        //        Window.GetWindow(this).Close();

        //        Log.i("telegram auth was successfull", "License", "on_telegram_auth_checked");

               

        //    }
        //    else
        //    {

        //        FailedDialog _dialog = new FailedDialog("اتصال به سرور تلگرام با مشکل مواجه شد");
        //        _dialog.ShowDialog();

        //        Log.e("telegram auth failed", "License", "on_telegram_auth_checked");

        //        this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterNumber.xaml", UriKind.Relative));
        //    }
        //}

        

        
    }
}
