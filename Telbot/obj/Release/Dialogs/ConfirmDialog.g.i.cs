﻿#pragma checksum "..\..\..\Dialogs\ConfirmDialog.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5E3BE21459B80B6D342965739A762A0B5A0B1876"
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


namespace Telbot.Dialogs {
    
    
    /// <summary>
    /// ConfirmDialog
    /// </summary>
    public partial class ConfirmDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\Dialogs\ConfirmDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_message;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Dialogs\ConfirmDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_cancel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Dialogs\ConfirmDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_ok;
        
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
            System.Uri resourceLocater = new System.Uri("/Telbot;component/dialogs/confirmdialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Dialogs\ConfirmDialog.xaml"
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
            
            #line 10 "..\..\..\Dialogs\ConfirmDialog.xaml"
            ((System.Windows.Controls.DockPanel)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.DockPanel_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txt_message = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.btn_cancel = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Dialogs\ConfirmDialog.xaml"
            this.btn_cancel.Click += new System.Windows.RoutedEventHandler(this.btn_cancel_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_ok = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Dialogs\ConfirmDialog.xaml"
            this.btn_ok.Click += new System.Windows.RoutedEventHandler(this.btn_ok_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

