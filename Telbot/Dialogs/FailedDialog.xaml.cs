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

namespace Telbot.Dialogs
{
    /// <summary>
    /// Interaction logic for FailedDialog.xaml
    /// </summary>
    public partial class FailedDialog : Window
    {
      
        public FailedDialog(string message)
        {
            
            InitializeComponent();
            txt_message.Text = message;

        }
        private void btn_close_dialog(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
 
    }
}
