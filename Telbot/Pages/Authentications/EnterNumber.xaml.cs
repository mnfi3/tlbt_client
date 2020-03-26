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
        }



        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void getCountryCodes()
        {
            Dictionary<string, string> codes =  CountryCode_helper.getCodes();
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
            if (string.IsNullOrEmpty(G.ApiHash) || string.IsNullOrEmpty(G.ApiId))
            {
                MessageBox.Show("api_id و یا api_hash وارد نشده است");
            }
            else
            {
                previousCursor = Mouse.OverrideCursor;
                Mouse.OverrideCursor = Cursors.Wait;
                sleepProcess(goToEnterNumberPage);
            }
            


        }
        private void goToEnterNumberPage(object o, EventArgs e)
        {
            Mouse.OverrideCursor = previousCursor;
            this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterCode.xaml", UriKind.Relative));
            //SuccessfullDialog success = new SuccessfullDialog("عملیات با خطا مواجه شد.");
            //success.ShowDialog();
            //FailedDialog failed = new FailedDialog("عملیات با خطا مواجه شد.");
            //failed.ShowDialog();

        }
        private void sleepProcess(EventHandler handler)
        {
            DispatcherTimer countdown = null;
            TimeSpan time;
            time = TimeSpan.FromSeconds(1);
            countdown = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {

                if (time == TimeSpan.Zero)
                {
                    handler(new object(), new EventArgs());
                }
                time = time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            countdown.Start();
        }
        private void btn_open_Enter_configuration(object sender, RoutedEventArgs e)
        {
            EnterConfiguration mainWindow = new EnterConfiguration();
            mainWindow.ShowDialog();


        }
    }
}
