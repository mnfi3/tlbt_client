using System;
using System.Collections.Generic;
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
using Telbot.Dialogs;
using Telbot.storage;
using Telbot.system;
using Telbot.telegram;

namespace Telbot.Pages.Authentications
{
    /// <summary>
    /// Interaction logic for EnterTwoStepVerificationPassword.xaml
    /// </summary>
    public partial class EnterTwoStepVerificationPassword : Page
    {

        private Cursor previousCursor;
        public EnterTwoStepVerificationPassword()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_password.Password.Length < 1)
            {
                FailedDialog _dialog = new FailedDialog("لطفا رمز عبور را وارد کنید");
                _dialog.ShowDialog();
                return;
            }
            previousCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;

            verifyPassword();
        }

        private void verifyPassword()
        {
            Auth_telegram auth = new Auth_telegram();
            auth.verifyTwoStepPassword(on_password_verified, txt_password.Password);
        }

        private void on_password_verified(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = previousCursor;

            TelegramResponse res = (TelegramResponse) sender;
            if (res.status == 1)
            {
                previousCursor = Mouse.OverrideCursor;
                Mouse.OverrideCursor = Cursors.Wait;

                checkTelegramAuth();
            }
            else
            {
                FailedDialog _dialog = new FailedDialog(res.message);
                _dialog.ShowDialog();
            }
        }



        private void checkTelegramAuth()
        {
            Auth_telegram auth = new Auth_telegram();
            auth.isUserAuthorized(on_telegram_auth_checked);
        }

        private void on_telegram_auth_checked(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = previousCursor;

            TelegramResponse res = (TelegramResponse)sender;
            if (res.status == 1)
            {
                //save telegram login flag
                Telegram_pref pref = new Telegram_pref();
                G.telegram.is_logged_in = 1;
                pref.saveTelegram(G.telegram);


                SuccessfullDialog _dialog = new SuccessfullDialog(res.message);
                _dialog.ShowDialog();


                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this).Close();
            }
            else
            {
                FailedDialog _dialog = new FailedDialog(res.message);
                _dialog.ShowDialog();
                this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterNumber.xaml", UriKind.Relative));
            }
        }
    }
}
