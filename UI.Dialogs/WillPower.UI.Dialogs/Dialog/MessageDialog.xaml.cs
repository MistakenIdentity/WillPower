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
    /// Interaction logic for MessageDialog.xaml
    /// </summary>
    public sealed partial class MessageDialog : Window
    {

        /// <summary>
        /// Threadsafe execution of Dialog.
        /// </summary>
        /// <param name="Message">string: The caption message to display to the User</param>
        /// <param name="Buttons">System.Windows.MessageBoxButton: Optional. The type of MessageBox to display. Default is OK</param>
        /// <param name="Mood">System.Windows.MessageBoxImage: Optional. Sets the *mood* of the dialog. Default is Information/></param>
        /// <param name="Title">string: Optional. Title Bar Content</param>
        /// <returns>MessageBoxResult: The selected Result or None</returns>
        /// <exception cref="System.NullReferenceException">Can occur when either the Application or its Dispatcher object is Null</exception>
        public static MessageBoxResult Invoke(string Message, MessageBoxButton Buttons = MessageBoxButton.OK,
            MessageBoxImage Mood = MessageBoxImage.Information, string Title = null)
        {
            MessageBoxResult result = MessageBoxResult.None;
            Application.Current.Invoke(delegate
            {
                MessageDialog dlg = new MessageDialog(Message, Buttons, Mood, Title);
                if (dlg.ShowDialog() == true)
                {
                    result = dlg.Result;
                }
            });
            return result;
        }

        /// <summary>
        /// Shows a default Information box.
        /// </summary>
        /// <param name="Message">string: The caption message to display to the User</param>
        /// <param name="Title">string: Optional. Title Bar Content</param>
        public static void ShowInfo(string Message, string Title = null)
        {
            Invoke(Message, MessageBoxButton.OK, MessageBoxImage.Information, Title);
        }

        /// <summary>
        /// Shows a default Information box.
        /// </summary>
        /// <param name="Message">string: The caption message to display to the User</param>
        /// <param name="Title">string: Optional. Title Bar Content</param>
        public static void ShowWarning(string Message, string Title = null)
        {
            Invoke(Message, MessageBoxButton.OK, MessageBoxImage.Warning, Title);
        }

        /// <summary>
        /// Shows a default Information box.
        /// </summary>
        /// <param name="Message">string: The caption message to display to the User</param>
        /// <param name="Title">string: Optional. Title Bar Content</param>
        public static void ShowAlert(string Message, string Title = null)
        {
            Invoke(Message, MessageBoxButton.OK, MessageBoxImage.Error, Title);
        }

        /// <summary>
        /// Shows a default Information box.
        /// </summary>
        /// <param name="Message">string: The caption message to display to the User</param>
        /// <param name="Buttons">System.Windows.MessageBoxButton: Optional. The type of MessageBox to display. Default is OK</param>
        /// <param name="Title">string: Optional. Title Bar Content</param>
        /// <returns>MessageBoxResult: The selected Result or None</returns>
        /// <exception cref="System.NullReferenceException">Can occur when either the Application or its Dispatcher object is Null</exception>
        public static MessageBoxResult ShowQuestion(string Message, MessageBoxButton Buttons = MessageBoxButton.YesNo, string Title = null)
        {
            return Invoke(Message, Buttons, MessageBoxImage.Question, Title);
        }

        /// <summary>
        /// The selected Result or None.
        /// </summary>
        public MessageBoxResult Result { get; private set; }
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
        /// The message area TextBlock, completely accessible and modifiable.
        /// </summary>
        public TextBlock MessageBlock
        {
            get
            {
                return Application.Current.Invoke(delegate
                { return this.Message; });
            }
            set
            {
                Application.Current.Invoke(delegate
                { this.Message = value; });
            }
        }

        private MessageDialog()
        {
            InitializeComponent();
            this.Result = MessageBoxResult.None;
            this.CloseWindow.ToolTip = UI.Dialogs.Properties.Resources.Close;
        }
        /// <summary>
        /// Instantiates a new MessageDialog.
        /// </summary>
        /// <param name="message">string: The caption message to display to the User</param>
        /// <param name="buttons">System.Windows.MessageBoxButton: Optional. The type of MessageBox to display. Default is OK</param>
        /// <param name="mood">System.Windows.MessageBoxImage: Optional. Sets the *mood of the dialog. Default is Information/></param>
        /// <param name="title">string: Optional. Title Bar Content</param>
        public MessageDialog(string message, MessageBoxButton buttons = MessageBoxButton.OK, 
            MessageBoxImage mood = MessageBoxImage.Information, string title = null) : this()
        {
            this.Caption.Content = title;
            this.Message.Text = message;
            this.Mood = mood;
            SetupButtons(buttons);
            UpdateLayout();
        }

        private void SetupButtons(MessageBoxButton buttons)
        {
            switch (buttons)
            {
                case MessageBoxButton.OK:
                    this.Button1.Hide();
                    this.Button3.Hide();
                    this.Button2.Content = UI.Dialogs.Properties.Resources.OK;
                    this.Button2.ToolTip = UI.Dialogs.Properties.Resources.OK;
                    break;
                case MessageBoxButton.OKCancel:
                    this.Button2.Hide();
                    this.Button1.Content = UI.Dialogs.Properties.Resources.OK;
                    this.Button3.Content = UI.Dialogs.Properties.Resources.Cancel;
                    this.Button1.ToolTip = UI.Dialogs.Properties.Resources.OK;
                    this.Button3.ToolTip = UI.Dialogs.Properties.Resources.Cancel;
                    break;
                case MessageBoxButton.YesNo:
                    this.Button2.Hide();
                    this.Button1.Content = UI.Dialogs.Properties.Resources.Yes;
                    this.Button3.Content = UI.Dialogs.Properties.Resources.No;
                    this.Button1.ToolTip = UI.Dialogs.Properties.Resources.Yes;
                    this.Button3.ToolTip = UI.Dialogs.Properties.Resources.No;
                    break;
                case MessageBoxButton.YesNoCancel:
                    this.Button1.Content = UI.Dialogs.Properties.Resources.Yes;
                    this.Button2.Content = UI.Dialogs.Properties.Resources.No;
                    this.Button3.Content = UI.Dialogs.Properties.Resources.Cancel;
                    this.Button1.ToolTip = UI.Dialogs.Properties.Resources.Yes;
                    this.Button2.ToolTip = UI.Dialogs.Properties.Resources.No;
                    this.Button3.ToolTip = UI.Dialogs.Properties.Resources.Cancel;
                    break;
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            string result = (sender as Button).Content.ToString();
            if (result == UI.Dialogs.Properties.Resources.Cancel)
            {
                this.Result = MessageBoxResult.Cancel;
            }
            else if (result == UI.Dialogs.Properties.Resources.No)
            {
                this.Result = MessageBoxResult.No;
            }
            else if (result == UI.Dialogs.Properties.Resources.OK)
            {
                this.Result = MessageBoxResult.OK;
            }
            else if (result == UI.Dialogs.Properties.Resources.Yes)
            {
                this.Result = MessageBoxResult.Yes;
            }
            this.DialogResult = true;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.DisposeOf(this);
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
