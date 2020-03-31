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
using System.Windows.Shapes;
using Telbot.telegram;
using TeleSharp.TL;

namespace Telbot.Dialogs
{
    /// <summary>
    /// Interaction logic for FailedDialog.xaml
    /// </summary>
    public partial class AddToChannelDialog : Window
    {

        private TLVector<TLInputPhoneContact> contacts;
        private TLChannel channel;
        public AddToChannelDialog(TLVector<TLInputPhoneContact> contacts, TLChannel channel)
        {
            
            InitializeComponent();

            this.contacts = contacts;
            this.channel = channel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_head.Content = "در حال انجام عملیات..." + "       زمان تقریبی عملیات = " + contacts.Count + " دقیقه ";
            txt_message.Text = "";
            btn_close.Visibility = Visibility.Collapsed;

            startAdd();
        }

        private void startAdd()
        {
            Contact_telegram tel = new Contact_telegram();
            tel.addNumberToChannel(on_task_finished_handler, on_contact_added_handler, contacts, channel);
        }

        private void on_task_finished_handler(object sender, EventArgs e)
        {
            TelegramResponse res = (TelegramResponse)sender;
            lbl_head.Content = "عملیات تمام شد";
            txt_message.Text = res.message;
            btn_close.Content = "بستن";
            btn_close.Visibility = Visibility.Visible;
        }

        private void on_contact_added_handler(object sender, EventArgs e)
        {
            TelegramResponse res = (TelegramResponse)sender;
            txt_message.Text = res.message;
        }




        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_close_dialog_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       


      
 
    }
}
