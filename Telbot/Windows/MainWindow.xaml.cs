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
using Telbot.storage;
using System.IO;
using Telbot.api;

namespace Telbot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string file_path = "";
        TLChannel selected_channel = null;
        MessageDialog _dialog_add = new MessageDialog("");
            
        public List<ItemNumber> tempListOfItem = new List<ItemNumber>();

        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //change loggin flag
            //save telegram login flag
            Telegram_pref pref = new Telegram_pref();
            G.telegram.is_logged_in = 1;
            pref.saveTelegram(G.telegram);
            G.telegram = pref.getTelegram();

            //loadContacts();
            loadChannels();
            //loadNumbers();
            txt_mobile.Header = G.telegram.mobile.Replace("+", "");
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
            prg_numbers.Visibility = Visibility.Visible;
            Mobile_db db = new Mobile_db();
            List<Mobile_model> mobiles = db.searchMobiles(search, from, to, first_name, last_name);
            setMobiles(mobiles);
        }


        //---------------------------------------------contacts---------------------------------------------------
        private void loadContacts()
        {
            prg_contact.Visibility = Visibility.Visible;
            Contact_telegram contact = new Contact_telegram();
            contact.getContacts(on_contacts_received);
        }

        private void on_contacts_received(object sender, EventArgs e)
        {
            prg_contact.Visibility = Visibility.Collapsed;
            TelegramResponse res = (TelegramResponse)sender;
            List<TLUser> users = (List<TLUser>)res.data;
            setContacts(users);

            //loadChannels();
        }

        private async void setContacts(List<TLUser> users)
        {
            prg_contact.Visibility = Visibility.Visible;
            lst_contacts.Items.Clear();
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
            prg_contact.Visibility = Visibility.Collapsed;
        }

        //---------------------------------------------channels---------------------------------------------------
        private  void loadChannels()
        {
            prg_channel.Visibility = Visibility.Visible;
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
            lst_groups.Items.Clear();
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
                contact = new TLInputPhoneContact() { FirstName = item.mobile.first_name, LastName = item.mobile.last_name, Phone = item.mobile.number, ClientId=0 };
                contacts.Add(contact);
            }

            if (contacts.Count == 0)
            {
                FailedDialog _dialog = new FailedDialog("هیچ شماره ای انتخاب نشده است");
                _dialog.ShowDialog();
                return;
            }

            if (selected_channel == null)
            {
                FailedDialog _dialog = new FailedDialog("هیچ کانال یا گروهی انتخاب نشده است");
                _dialog.ShowDialog();
                return;
            }




            ConfirmDialog _d = new ConfirmDialog("آیا مطمئن هستید؟");
            if(_d.ShowDialog() == false){
                return;
            }

            //_dialog_add = new MessageDialog("در حال انجام عملیات ...");
            //_dialog_add.Show();
           

            //Contact_telegram tel = new Contact_telegram();
            //tel.addNumberToChannel(on_contacts_added, contacts, selected_channel);

            AddToChannelDialog _add_dialog = new AddToChannelDialog(contacts, selected_channel);
            _add_dialog.ShowDialog();
            
        }

        private void on_contacts_added(object sender, EventArgs e) 
        {
            _dialog_add.Close();

            TelegramResponse res = (TelegramResponse)sender;
            List<TLUser> added_users = (List<TLUser>)res.data;
            if (res.status == 1)
            {
                SuccessfullDialog _d2 = new SuccessfullDialog(res.message);
                _d2.ShowDialog();
            }
            else
            {
                FailedDialog _d = new FailedDialog(res.message);
                _d.ShowDialog();
            }
        }



        private void btn_add_to_database_Click(object sender, RoutedEventArgs e)
        {
            if (file_path.Length < 2)
            {
                FailedDialog _dialog = new FailedDialog("هیچ فایلی انتخاب نشده است");
                _dialog.ShowDialog();
                return;
            }

            readFromExcel();     
        }
         
        
        void readFromExcel()
        {
            AddMobileDialog _dialog = new AddMobileDialog(file_path);
            _dialog.Show();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            searchNumbers(inp_number.Text, inp_number_from.Text, inp_number_to.Text, inp_name.Text, inp_lastname.Text);
        }

        private void btn_refresh_channels_Click(object sender, RoutedEventArgs e)
        {
            loadChannels();
        }

        private void btn_refresh_contacts_Click(object sender, RoutedEventArgs e)
        {
            loadContacts();
        }


        //------------------------------------remove mobiles------------------------------------------------
        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            List<Mobile_model> mobiles = new List<Mobile_model>();
            foreach (var item in lst_numbers.Items.OfType<ItemNumber>())
            {
                if (!(bool)item.chk_num.IsChecked) continue;

                mobiles.Add(item.mobile);
            }

            if (mobiles.Count == 0)
            {
                FailedDialog _dialog = new FailedDialog("هیچ شماره ای انتخاب نشده است");
                _dialog.ShowDialog();
                return;
            }

            ConfirmDialog _d = new ConfirmDialog("آیا مطمئن هستید؟");
            if (_d.ShowDialog() == false)
            {
                return;
            }

            prg_numbers.Visibility = Visibility.Visible;
            Mobile_db db = new Mobile_db();
            db.removeMobiles(mobiles);

            foreach (var item in lst_numbers.Items.OfType<ItemNumber>())
            {
                foreach (Mobile_model m in mobiles)
                {
                    if (item.mobile.number == m.number)
                    {
                        item.Visibility = Visibility.Collapsed;
                    }
                }
            }
            prg_numbers.Visibility = Visibility.Collapsed;
            SuccessfullDialog _dialog2 = new SuccessfullDialog("عملیات با موفقیت انجام شد");
            _dialog2.ShowDialog();
        }


        //------------------------------------app logout------------------------------------------------
        private void txt_app_logout_Click(object sender, RoutedEventArgs e)
        {
            ConfirmDialog _dialog = new ConfirmDialog("آیا مطمئن هستید؟");
            if (_dialog.ShowDialog() == false) return;

            App_service service = new App_service();
            service.logout(app_logout_completed);
           
        }

        private void app_logout_completed(object sender, EventArgs e)
        {
            Response res = sender as Response;
            if (res.status == 1)
            {
                App_pref pref = new App_pref();
                pref.saveApp(new App_model());
                G.app = pref.getApp();

                SuccessfullDialog _dialog = new SuccessfullDialog(res.message);
                _dialog.ShowDialog();

                AuthenticationWindow _window = new AuthenticationWindow();
                _window.Show();
                this.Close();
            }
            else
            {
                FailedDialog _dialog = new FailedDialog(res.message);
                _dialog.ShowDialog();
            }
        }


        //------------------------------------telegram logout------------------------------------------------
        private void txt_telegram_logout_Click(object sender, RoutedEventArgs e)
        {
            ConfirmDialog _dialog = new ConfirmDialog("آیا مطمئن هستید؟");
            if (_dialog.ShowDialog() == false) return;

            Auth_telegram auth = new Auth_telegram();
            auth.logout();

            AuthenticationWindow _window = new AuthenticationWindow();
            _window.Show();
            this.Close();
        }

        //------------------------------------close app------------------------------------------------
        private void txt_exit_Click(object sender, RoutedEventArgs e)
        {
            ConfirmDialog _dialog = new ConfirmDialog("آیا مطمئن هستید؟");
            if (_dialog.ShowDialog() == false) return;

            System.Windows.Application.Current.Shutdown();
        }

        private void txt_about_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Config.SITE_URL);
        }




        


      


    }
}
