﻿using System;
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

namespace Telbot.Pages.Authentications
{
    /// <summary>
    /// Interaction logic for EnterTwoStepVerificationPassword.xaml
    /// </summary>
    public partial class EnterTwoStepVerificationPassword : Page
    {
        public EnterTwoStepVerificationPassword()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
            
        }
    }
}
