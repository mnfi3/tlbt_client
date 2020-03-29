using Telbot.Dialogs;
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
using Telbot.system;
using Telbot.helper;
using Telbot.telegram;
using Telbot.storage;

namespace Telbot.Pages.Authentications
{
    /// <summary>
    /// Interaction logic for EnterNumber.xaml
    /// </summary>
    public partial class EnterNumber : Page
    {
        private Cursor previousCursor;
        public EnterNumber()
        {
            InitializeComponent();
            getCountryCodes();

            setMobile();
        }


        private void setMobile(){
             //txt_mobile.Text = G.telegram.mobile;
            string full_number = G.telegram.mobile;
            if (full_number.Length == 0) return;
            int lenght = full_number.Length;
            string mobile = full_number.Substring(lenght - 10, 10);
            string code = full_number.Substring(0, lenght - 10);
            txt_mobile.Text = mobile;
            foreach (ComboBoxItem item in country_code.Items)
            {
                if (item.Content.ToString().Contains(code))
                {
                    item.IsSelected = true;
                }
            }
        }

        private void getCountryCodes()
        {
            Dictionary<string, string> codes = CountryCode_helper.getCodes();
            for (int i = 0; i < codes.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = "+" + codes.ElementAt(i).Value + "\t\t\t" + codes.ElementAt(i).Key;
                item.Tag = codes.ElementAt(i).Value;
                country_code.Items.Add(item);
                if (codes.ElementAt(i).Value == "98")
                {
                    item.IsSelected = true;
                }

            }

        }


        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
      
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
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



        private void btn_send_code(object sender, RoutedEventArgs e)
        {
            if (G.telegram.api_hash.Length < 3 || G.telegram.api_id == 0)
            {
                FailedDialog _dialog = new FailedDialog("لطفا api_id و api_hash را وارد کنید");
                _dialog.ShowDialog();
                return;
            }
            if(txt_mobile.Text.Length < 8)
            {
                FailedDialog _dialog = new FailedDialog("لطفا شماره موبایل خود را با دقت وارد کنید");
                _dialog.ShowDialog();
                return;
            }

            
            //send code
            sendCode();
            
        }

        private  void sendCode()
        {
            previousCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;

            string mobile = getContryCodeFromCobmo() + txt_mobile.Text;
            G.telegram.mobile = mobile;
            new Telegram_pref().saveTelegram(G.telegram);
            

            Auth_telegram auth = new Auth_telegram();
            auth.sendVerificationCode(on_verification_code_sent);
        }

        private void on_verification_code_sent(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = previousCursor;
            TelegramResponse res = (TelegramResponse)sender;
            if (res.status == 0)
            {
                FailedDialog _dialog = new FailedDialog(res.message);
                _dialog.ShowDialog();
                return;
            }

            G.telegram.hash = (string) res.data;
            new Telegram_pref().saveTelegram(G.telegram);
            this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterCode.xaml", UriKind.Relative));
        }

       
        private void btn_open_Enter_configuration(object sender, RoutedEventArgs e)
        {
            EnterConfiguration mainWindow = new EnterConfiguration();
            mainWindow.ShowDialog();
        }

        private string getContryCodeFromCobmo()
        {
            string code = ((ComboBoxItem)country_code.SelectedItem).Tag.ToString(); ;
            return "+" + code;
        }





        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            checkTelegramAuth();
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
