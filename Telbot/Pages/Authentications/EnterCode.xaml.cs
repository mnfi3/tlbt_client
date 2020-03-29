using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;
using Telbot.Dialogs;
using Telbot.system;
using Telbot.telegram;

namespace Telbot.Pages.Authentications
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class EnterCode : Page
    {
        DispatcherTimer countdown;
        TimeSpan time;
        private Cursor previousCursor;

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        public EnterCode()
        {
            InitializeComponent();

            time = TimeSpan.FromSeconds(120);
            countdown = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                txt_second.Text = time.ToString(@"mm\:ss");
                if (time == TimeSpan.Zero)
                {
                    countdown.Stop();
                    txt_countdown.Visibility = System.Windows.Visibility.Collapsed;
                    txt_send_code_again.Visibility = System.Windows.Visibility.Visible;
                    btn_confirm_code.Visibility = System.Windows.Visibility.Collapsed;
                    btn_edit_number.Visibility = System.Windows.Visibility.Visible;

                }
                time = time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            countdown.Start(); 




        }

        private void btn_confirm_code_click(object sender, RoutedEventArgs e)
        {
            if (inp_code.Text.Length < 1)
            {
                FailedDialog _dialog = new FailedDialog("کد تایید را وارد کنید");
                _dialog.ShowDialog();
                return;
            }

            previousCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;

            verifyCode();
        }

        private void  verifyCode(){
            Auth_telegram auth = new Auth_telegram();
            auth.verifyCode(on_code_verified, G.telegram.hash, inp_code.Text);
        }

        private void on_code_verified(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = previousCursor;

            TelegramResponse res = (TelegramResponse)sender;
            if (res.status == 1)
            {
                previousCursor = Mouse.OverrideCursor;
                Mouse.OverrideCursor = Cursors.Wait;

                checkTelegramAuth();
            }
            else if (res.status == 2)
            {
                this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterTwoStepVerificationPassword.xaml", UriKind.Relative));
            }
            else if (res.status == 0)
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





       
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void inp_code_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void inp_code_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }

        }
      
        private void btn_edit_number_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Pages/Authentications/EnterNumber.xaml", UriKind.Relative));
        }

        private void txt_send_code_again_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Pages/Authentications/EnterNumber.xaml", UriKind.Relative));
        }
    }
}
