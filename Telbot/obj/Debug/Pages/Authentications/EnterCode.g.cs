﻿#pragma checksum "..\..\..\..\Pages\Authentications\EnterCode.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EADEEE32D934F8E1D53129507BA5B683E3A38DA4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Telbot.Pages.Authentications {
    
    
    /// <summary>
    /// EnterCode
    /// </summary>
    public partial class EnterCode : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PulseBox;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox inp_code;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_countdown;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_second;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_send_code_again;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_confirm_code;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_edit_number;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AutoMemberBot;component/pages/authentications/entercode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PulseBox = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.inp_code = ((System.Windows.Controls.TextBox)(target));
            
            #line 79 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
            this.inp_code.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.inp_code_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 79 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
            this.inp_code.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.inp_code_Pasting));
            
            #line default
            #line hidden
            return;
            case 3:
            this.txt_countdown = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txt_second = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txt_send_code_again = ((System.Windows.Controls.TextBlock)(target));
            
            #line 81 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
            this.txt_send_code_again.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.txt_send_code_again_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_confirm_code = ((System.Windows.Controls.Button)(target));
            
            #line 83 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
            this.btn_confirm_code.Click += new System.Windows.RoutedEventHandler(this.btn_confirm_code_click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_edit_number = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\..\Pages\Authentications\EnterCode.xaml"
            this.btn_edit_number.Click += new System.Windows.RoutedEventHandler(this.btn_edit_number_click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

