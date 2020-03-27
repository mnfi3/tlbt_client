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
using Telbot.model;

namespace Telbot.Items
{
    /// <summary>
    /// Interaction logic for ItemNumber.xaml
    /// </summary>
    public partial class ItemNumber : UserControl
    {

        private Mobile_model mobile;
        private int row;
        public ItemNumber(int row, Mobile_model mobile)
        {
            InitializeComponent();
            this.mobile = mobile;
            this.row = row;
        }
       
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txt_row.Text = row.ToString() ;
            txt_name.Text = mobile.first_name;
            txt_lastname.Text = mobile.last_name;
            txt_number.Text = mobile.number;
        }

        public void updateCheckBox(Boolean check)
        {
            chk_num.IsChecked = check;
        }

        
        
    }
}
