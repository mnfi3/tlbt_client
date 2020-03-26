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

namespace Telbot.Pages.Authentications
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class EnterCode : Page
    {
        DispatcherTimer countdown;
        TimeSpan time;
        public EnterCode()
        {
            InitializeComponent();
            time = TimeSpan.FromSeconds(5);
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
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
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
        private void btn_confirm_code_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Pages/authentications/EnterTwoStepVerificationPassword.xaml", UriKind.Relative));
        }
        private void btn_edit_number_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Pages/Authentications/EnterNumber.xaml", UriKind.Relative));
        }

        private void txt_send_code_again_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
