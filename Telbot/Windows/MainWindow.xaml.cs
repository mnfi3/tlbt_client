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

namespace Telbot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string file_path = "";
            
        public List<ItemNumber> tempListOfItem = new List<ItemNumber>();

        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        public MainWindow()
        {
            InitializeComponent();
            //tl.AuthUser();
            //tl.SendMessageTest();
            //tl.addMemmberTest();
            Random random = new Random();
            //for (int i = 0; i <= 20; i++)
            //{
            //    ItemContact lstItem = new ItemContact();
            //    byte r = (byte)random.Next(20, 220);
            //    byte g = (byte)random.Next(20, 220);
            //    byte b = (byte)random.Next(20, 220);

            //    lstItem.Background = new SolidColorBrush(Color.FromArgb(110, r, g, b));

            //    lst_contacts.Items.Add(lstItem);
            //}
            Random random2 = new Random();
            for (int i = 0; i <= 20; i++)
            {
                ItemGroup lstItem = new ItemGroup();
                byte r = (byte)random2.Next(20, 220);
                byte g = (byte)random2.Next(20, 220);
                byte b = (byte)random2.Next(20, 220);

                lstItem.Background = new SolidColorBrush(Color.FromArgb(110, r, g, b));

                lst_groups.Items.Add(lstItem);
            }

            //for (int i = 0; i <= 50; i++)
            //{
            //    ItemNumber itemNumber2 = new ItemNumber(updateCheckBox);
            //    lst_numbers.Items.Add(itemNumber2);
            //    tempListOfItem.Add(itemNumber2);
            //}

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadNumbers();
            loadContacts();
        }


        private async void loadNumbers()
        {
            Mobile_db db = new Mobile_db();
            List<Mobile_model> mobiles = db.getMobiles();
            ItemNumber _item;

            await this.Dispatcher.InvokeAsync((Action)(() =>
            {
                int i = 0;
                foreach (Mobile_model mobile in mobiles)
                {
                    _item = new ItemNumber(++i, mobile);
                    lst_numbers.Items.Add(_item);
                }
            }));
            
        }

        private async void loadContacts()
        {
            Contact_telegram contact = new Contact_telegram();
            await contact.getContacts(on_contacts_received);
        }

        private void on_contacts_received(object sender, EventArgs e)
        {
            TelegramResponse res = (TelegramResponse)sender;
            List<TLUser> users = (List<TLUser>)res.data;
            //ItemContact _item;

            //foreach (TLUser user in users)
            //{
            //    _item = new ItemContact(user);
            //    lst_contacts.Items.Add(_item);
            //}
            setContacts(users);
        }

        private async void setContacts(List<TLUser> users)
        {
            Random random = new Random();
            await this.Dispatcher.InvokeAsync((Action)(() =>
            {
                ItemContact _item = null;
                foreach (TLUser user in users)
                {
                    byte r = (byte)random.Next(20, 220);
                    byte g = (byte)random.Next(20, 220);
                    byte b = (byte)random.Next(20, 220);

                    _item = new ItemContact(user);
                    _item.Background = new SolidColorBrush(Color.FromArgb(110, r, g, b));
                    lst_contacts.Items.Add(_item);
                }
            }));
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
                btn_add_member_to_group.Content = " افزودن لیست موجود به گروه " + item.txt_title.Text ; 
            }
        }

        private void btn_add_member_to_group_Click(object sender, RoutedEventArgs e)
        {
            
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

        


      


    }
}
