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
    /// Interaction logic for ItemGroup.xaml
    /// </summary>
    public partial class ItemGroup : UserControl
    {
        public string title;
        public string username;
        public TLChannel channel; 
        public ItemGroup(TLChannel channel)
        {
            InitializeComponent();
            this.channel = channel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txt_title.Text = channel.Title;
            if (channel.Username != null)
                lbl_username.Content = "@" + channel.Username;
            else
                lbl_username.Content = "";
        }

    }
}
