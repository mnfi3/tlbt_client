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
            api_hash.Text = G.ApiHash;
            api_id.Text = G.ApiId;
            
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

        }
    }
}
