//
// ********************************************************************************************************
// ********************************************************************************************************
// ***                                                                                                  ***
// *** Code Copyright © 2020, Will `Willow' Osborn.                                                     ***
// ***                                                                                                  ***
// *** This code is provided 'AS IS, NO WARRANTY' and is intended for no specific use or person.        ***
// *** In fact, the code herein is so confuggled, it should not be used by ANYONE EVER and ANYTHING     ***
// *** that happens as a result of its use is COMPLETELY and UTTERLY YOUR FAULT.  :p                    ***
// ***                                                                                                  ***
// *** You have my permission to extract, copy, modify, steal, borrow, beg, fold, spindle, mutilate or  ***
// *** otherwise abuse the code herein PROVIDED YOU LEAVE ME OUT OF IT! You Acknowledge and Accept      ***
// *** FULL and SOLE responsibility and culpability for ANYTHING you do with or around it.              ***
// ***                                                                                                  ***
// ********************************************************************************************************
// ********************************************************************************************************
//

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WillPower
{
    /// <summary>
    /// Interaction logic for UniForm.xaml
    /// </summary>
    public sealed partial class UniForm : Window
    {
        /// <summary>
        /// The mood of the window.
        /// </summary>
        public MessageBoxImage Mood
        {
            get
            {
                return pvMood;
            }
            set
            {
                pvMood = value;
                this.ColorFadeBottom = value.GetMood();
            }
        }
        private MessageBoxImage pvMood = MessageBoxImage.None;
        /// <summary>
        /// The topmost background (fade from)
        /// </summary>
        public GradientStop ColorFadeTop
        {
            get
            {
                return Application.Current.Invoke(delegate
                { return this.FadeFrom; });
            }
            set
            {
                Application.Current.Invoke(delegate
                { this.FadeFrom = value; });
            }
        }
        /// <summary>
        /// The bottommost background (fade to)
        /// </summary>
        public GradientStop ColorFadeBottom
        {
            get
            {
                return Application.Current.Invoke(delegate
                { return this.FadeTo; });
            }
            set
            {
                Application.Current.Invoke(delegate
                { this.FadeTo = value; });
            }
        }
        /// <summary>
        /// The title label, completely accessible and modifiable.
        /// </summary>
        public Label TitleLabel
        {
            get
            {
                return Application.Current.Invoke(delegate
                { return this.Caption; });
            }
            set
            {
                Application.Current.Invoke(delegate
                { this.Caption = value; });
            }
        }
        /// <summary>
        /// The form title.
        /// </summary>
        public new string Title
        {
            get
            {
                return Application.Current.Invoke(delegate
                { return this.Caption.Content?.ToString(); });
            }
            set
            {
                Application.Current.Invoke(delegate
                { this.Caption.Content = value; });
            }
        }
        /// <summary>
        /// the Control Box Minimize Button.
        /// </summary>
        public Button MinimizeButton
        {
            get
            {
                return this.MinWindow;
            }
            set
            {
                this.MinWindow = value;
            }
        }
        /// <summary>
        /// the Control Box Maximize Button.
        /// </summary>
        public Button MaximizeButton
        {
            get
            {
                return this.MaxWindow;
            }
            set
            {
                this.MaxWindow = value;
                if (this.MaxWindow.Visibility == Visibility.Hidden)
                {
                    this.MinWindow.Margin = new Thickness(0, 1, 23, 0);
                }
                else
                {
                    this.MinWindow.Margin = new Thickness(0, 1, 45, 0);
                }
            }
        }
        /// <summary>
        /// the Control Box Close Button.
        /// </summary>
        public Button CloseButton
        {
            get
            {
                return this.CloseWindow;
            }
            set
            {
                this.CloseWindow = value;
            }
        }
        /// <summary>
        /// The Content portion of the Window. Can be any UIElement, 
        /// such as another Window or a Control.
        /// </summary>
        public UIElement FormContent
        {
            get
            {
                if (this.SurfaceGrid.Children.Count < 1)
                {
                    return null;
                }
                return this.SurfaceGrid.Children[0];
            }
            set
            {
                if (this.SurfaceGrid.Children.Count > 0)
                {
                    this.SurfaceGrid.Children.Clear();
                }
                this.SurfaceGrid.Children.Add(value);
            }
        }

        /// <summary>
        /// Creates a new Instance of UniForm.
        /// </summary>
        public UniForm()
        {
            InitializeComponent();
            this.CloseWindow.ToolTip = UI.Dialogs.Properties.Resources.Close;
            this.MaxWindow.ToolTip = UI.Dialogs.Properties.Resources.Maximize;
            this.MinWindow.ToolTip = UI.Dialogs.Properties.Resources.Minimize;
        }
        /// <summary>
        /// Creates a new Instance of UniForm.
        /// </summary>
        /// <param name="style">System.Windows.WindowStyle.</param>
        public UniForm(WindowStyle style) : this()
        {
            switch (style)
            {
                case WindowStyle.ToolWindow:
                    this.MaxWindow.Visibility = Visibility.Hidden;
                    this.MinWindow.Visibility = Visibility.Hidden;
                    break;
                case WindowStyle.None:
                    this.MaxWindow.Visibility = Visibility.Hidden;
                    this.MinWindow.Visibility = Visibility.Hidden;
                    this.CloseWindow.Visibility = Visibility.Hidden;
                    break;
            }
        }
        /// <summary>
        /// Creates a new Instance of UniForm.
        /// </summary>
        /// <param name="childForm">System.Windows.UIElement; The Content portion of the Window.</param>
        /// <param name="style">System.Windows.WindowStyle.</param>
        public UniForm(UIElement childForm, WindowStyle style) : this(style)
        {
            this.FormContent = childForm;
        }
        /// <summary>
        /// Creates a new Instance of UniForm.
        /// </summary>
        /// <param name="childForm">System.Windows.UIElement; The Content portion of the Window.</param>
        public UniForm(UIElement childForm) : this()
        {
            this.FormContent = childForm;
        }

        /// <summary>
        /// Closes this Window safely.
        /// </summary>
        public void CloseFromChild()
        {
            Application.Current.DisposeOf(this);
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.DisposeOf(this);
        }

        private void MinWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Invoke(delegate
            {
                this.WindowState = WindowState.Minimized;
            });
        }

        private void MaxWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Invoke(delegate
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }
            });
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Application.Current.Invoke(delegate
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.MaxWindow.Content = "L";
                    this.MaxWindow.ToolTip = UI.Dialogs.Properties.Resources.RestoreDown;
                }
                else
                {
                    this.MaxWindow.Content = "^";
                    this.MaxWindow.ToolTip = UI.Dialogs.Properties.Resources.Maximize;
                }
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (typeof(Window) == this.FormContent?.GetType())
            {
                Application.Current.DisposeOf(this.FormContent as Window);
            }
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
