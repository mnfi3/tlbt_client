using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telbot.storage;
using Telbot.system;

namespace Telbot.Dialogs
{
    /// <summary>
    /// Interaction logic for EnterConfiguration.xaml
    /// </summary>
    public partial class EnterConfiguration : Window
    {
        
        public EnterConfiguration()
        {
            InitializeComponent();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_api_id.Text = G.telegram.api_id.ToString();
            txt_api_hash.Text = G.telegram.api_hash;
        }


        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var link = (Hyperlink)sender;
            var uri = link.NavigateUri.ToString();
            Process.Start(uri);
            e.Handled = true;
        }
        
        public void btn_set_configuration(object sender, RoutedEventArgs e)
        {
            if (checkEntry())
            {
                G.telegram.api_id = int.Parse(txt_api_id.Text);
                G.telegram.api_hash = txt_api_hash.Text;
                Telegram_pref pref = new Telegram_pref();
                pref.saveTelegram(G.telegram);
                G.telegram = pref.getTelegram();
                SuccessfullDialog _dialog = new SuccessfullDialog("مقادیر با موفقیت ذخیره شد");
                _dialog.ShowDialog();
            }
        }

        private bool checkEntry()
        {
            if (txt_api_hash.Text.Length < 3 || txt_api_id.Text.Length < 3)
            {
                FailedDialog _dialog = new FailedDialog("لطفا مقادیر را با دقت وارد کنید");
                _dialog.ShowDialog();
                return false;
            }
            return true;
        }
        
    }
}
