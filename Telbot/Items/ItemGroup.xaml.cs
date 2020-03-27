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

namespace Telbot.Items
{
    /// <summary>
    /// Interaction logic for ItemGroup.xaml
    /// </summary>
    public partial class ItemGroup : UserControl
    {
        public string title;
        public string username;
        public ItemGroup()
        {
            InitializeComponent();
            title = txt_title.Text;
            username = lbl_username.Content.ToString();
        }

    }
}
