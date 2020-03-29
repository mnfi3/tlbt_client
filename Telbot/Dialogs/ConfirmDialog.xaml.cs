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
    public partial class ConfirmDialog : Window
    {

        public ConfirmDialog(string message)
        {
            
            InitializeComponent();
            txt_message.Text = message;

        }
        

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
 
    }
}
