using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telbot.db;
using Telbot.Dialogs;
using Telbot.Items;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Telbot.model;
using Telbot.system;
using Telbot.telegram;
using TeleSharp.TL;
using Newtonsoft.Json;
using TeleSharp.TL.Contacts;

namespace Telbot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string file_path = "";
        TLChannel selected_channel = null;
            
        public List<ItemNumber> tempListOfItem = new List<ItemNumber>();

        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadContacts();
            //loadChannels();
            //loadNumbers();
        }



        //---------------------------------------------mobiles---------------------------------------------------
        private void loadMobiles()
        {
            Mobile_db db = new Mobile_db();
            List<Mobile_model> mobiles = db.getMobiles();
            setMobiles(mobiles);
        }

        private async void setMobiles(List<Mobile_model> mobiles)
        {
            prg_numbers.Visibility = Visibility.Visible;
            lst_numbers.Items.Clear();
            ItemNumber _item;

            
                int i = 0;
                foreach (Mobile_model mobile in mobiles)
                {
                    _item = new ItemNumber(++i, mobile);
                    lst_numbers.Items.Add(_item);
                    await Task.Delay(1);
                }
            prg_numbers.Visibility = Visibility.Collapsed;

        }

        private void searchNumbers(string search, string from, string to, string first_name, string last_name)
        {
            Mobile_db db = new Mobile_db();
            List<Mobile_model> mobiles = db.searchMobiles(search, from, to, first_name, last_name);
            setMobiles(mobiles);
        }


        //---------------------------------------------contacts---------------------------------------------------
        private void loadContacts()
        {
            Contact_telegram contact = new Contact_telegram();
            contact.getContacts(on_contacts_received);
        }

        private void on_contacts_received(object sender, EventArgs e)
        {
            prg_contact.Visibility = Visibility.Collapsed;
            TelegramResponse res = (TelegramResponse)sender;
            List<TLUser> users = (List<TLUser>)res.data;
            setContacts(users);

            loadChannels();
        }

        private async void setContacts(List<TLUser> users)
        {
            Random random = new Random();
            ItemContact _item = null;
            foreach (TLUser user in users)
            {
                byte r = (byte)random.Next(20, 220);
                byte g = (byte)random.Next(20, 220);
                byte b = (byte)random.Next(20, 220);

                _item = new ItemContact(user);
                _item.Background = new SolidColorBrush(Color.FromArgb(110, r, g, b));
                lst_contacts.Items.Add(_item);
                await Task.Delay(1);
            }
            //MessageBox.Show("setted");


        }

        //---------------------------------------------channels---------------------------------------------------
        private  void loadChannels()
        {
            Channel_telegram channel = new Channel_telegram();
            channel.getChannels(on_channels_received);
        }

        private void on_channels_received(object sender, EventArgs e)
        {
            prg_channel.Visibility = Visibility.Collapsed;
            TelegramResponse res = (TelegramResponse)sender;
            List<TLChannel> channels = (List<TLChannel>)res.data;
            setChannels(channels);
        }

        private async void setChannels(List<TLChannel> channels)
        {
            prg_channel.Visibility = Visibility.Visible;
            Random random = new Random();
            ItemGroup _item = null;
            foreach (TLChannel channel in channels)
            {
                byte r = (byte)random.Next(20, 220);
                byte g = (byte)random.Next(20, 220);
                byte b = (byte)random.Next(20, 220);

                _item = new ItemGroup(channel);

                _item.Background = new SolidColorBrush(Color.FromArgb(110, r, g, b));
                lst_groups.Items.Add(_item);
                await Task.Delay(1);
            }

            prg_channel.Visibility = Visibility.Collapsed;
        }

       
        




        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void inp_number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }



        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xls)|*.xlsx|All files (*.*)|*.*";
            openFileDialog.Title = "Browse your number list file";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                lbl_filepath.Content = openFileDialog.FileName;
                file_path = openFileDialog.FileName;
            }

        }

        private void chk_all_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in lst_numbers.Items.OfType<ItemNumber>())
            {
                item.updateCheckBox(chk_all.IsChecked ?? false);
            }
        }
        public void updateCheckBox(object sender, EventArgs e)
        {
            chk_all.IsChecked = false;
        }

        private void lst_groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem == null) return;

            ItemGroup item = (ItemGroup)(sender as ListView).SelectedItem;     
            if (item != null)
            {
                selected_channel = item.channel;
                btn_add_member_to_group.Content = " افزودن لیست موجود به گروه " + item.txt_title.Text ; 
            }
        }



        //---------------------------------------------add to group---------------------------------------------------

        private void btn_add_member_to_group_Click(object sender, RoutedEventArgs e)
        {
            TLVector<TLInputPhoneContact> contacts = new TLVector<TLInputPhoneContact>();
            TLInputPhoneContact contact;
            foreach (var item in lst_numbers.Items.OfType<ItemNumber>())
            {
                if (!(bool)item.chk_num.IsChecked) continue;

                string first_name = (item.mobile.first_name.Length > 0) ? item.mobile.first_name : item.mobile.number;
                contact = new TLInputPhoneContact() { FirstName=item.mobile.first_name, LastName = item.mobile.last_name, Phone = item.mobile.number};
                contacts.Add(contact);
            }

            if(contacts.Count == 0)
            {
                FailedDialog _dialog = new FailedDialog("هیچ شماره ای انتخاب نشده است");
                _dialog.Show();
                return;
            }

            if (selected_channel == null)
            {
                FailedDialog _dialog = new FailedDialog("هیچ کانال یا گروهی انتخاب نشده است");
                _dialog.Show();
                return;
            }


            Contact_telegram tel = new Contact_telegram();
            tel.addNumberToChannel(on_contacts_added, contacts, selected_channel);
            
        }

        private void on_contacts_added(object sender, EventArgs e) 
        {
            TelegramResponse res = (TelegramResponse)sender;
            List<TLUser> added_users = (List<TLUser>)res.data;
            MessageBox.Show("تعداد افراد افزوده شده به چت = " + added_users.Count.ToString());
        }



        private void btn_add_to_database_Click(object sender, RoutedEventArgs e)
        {
            if (file_path.Length < 2)
            {
                FailedDialog _dialog = new FailedDialog("هیچ فایلی انتهاب نشده است");
                _dialog.ShowDialog();
                return;
            }

            readFromExcel();     
        }
         
        
        void readFromExcel()
        {
            ProgressDialog _dialog = new ProgressDialog(file_path);
            _dialog.Show();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            searchNumbers(inp_number.Text, inp_number_from.Text, inp_number_to.Text, inp_name.Text, inp_lastname.Text);
        }




        


      


    }
}
