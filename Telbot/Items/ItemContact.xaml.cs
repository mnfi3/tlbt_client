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
using TeleSharp.TL;

namespace Telbot.Items
{
    /// <summary>
    /// Interaction logic for ItemContact.xaml
    /// </summary>
    public partial class ItemContact : UserControl
    {
        TLUser user;
        public ItemContact(TLUser user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txt_fullname.Text = user.FirstName + " " + user.LastName;
            txt_phone.Content = user.Phone;
            txt_user_name.Content = user.Username;
        }
    }
}
