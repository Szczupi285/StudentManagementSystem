﻿#pragma checksum "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C2B923FF487A79F197C659B96117C67FEC24B694"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace StudentManagementSystem.MVVM.ViewModel {
    
    
    /// <summary>
    /// ProfessorPanelView
    /// </summary>
    public partial class ProfessorPanelView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Minimalize;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Fullscreen;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Close;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Schedule;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Grades;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Courses;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StudentManagementSystem;component/mvvm/view/professorpanelview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Minimalize = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml"
            this.Minimalize.Click += new System.Windows.RoutedEventHandler(this.Minimalize_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Fullscreen = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml"
            this.Fullscreen.Click += new System.Windows.RoutedEventHandler(this.Fullscreen_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Close = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\..\MVVM\View\ProfessorPanelView.xaml"
            this.Close.Click += new System.Windows.RoutedEventHandler(this.Close_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Schedule = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.Grades = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.Courses = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

