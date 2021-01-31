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

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WillPower
{
    /// <summary>
    /// Interaction logic for OptionsDialog.xaml
    /// </summary>
    public partial class OptionsDialog : Window
    {

        /// <summary>
        /// Threadsafe execution of Dialog.
        /// </summary>
        /// <param name="Message">string: The caption message to display to the User</param>
        /// <param name="Options">string[]: The List of Button Labels to be Presented to the User</param>
        /// <param name="Mood">System.Windows.MessageBoxImage enum: Used to derive the background color schema</param>
        /// <param name="Title">string: Optional. Title Bar Content</param>
        /// <returns>List(string): The selected Option Labels or Null</returns>
        /// <exception cref="System.NullReferenceException">Can occur when either the Application or its Dispatcher object is Null</exception>
        public static IList<string> Invoke(string Message, string[] Options, MessageBoxImage Mood = MessageBoxImage.None, string Title = null)
        {
            List<string> result = null;
            Application.Current.Invoke(delegate
            {
                OptionsDialog dlg = new OptionsDialog(Message, Options, Mood, Title);
                if (dlg.ShowDialog() == true)
                {
                    result = dlg.Result.ToList();
                }
            });
            return result;
        }

        /// <summary>
        /// The selected Button Label or Null.
        /// </summary>
        public IList<string> Result { get; private set; }
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

        private OptionsDialog()
        {
            InitializeComponent();
            this.CloseWindow.ToolTip = UI.Dialogs.Properties.Resources.Close;
            this.BtnOK.Content = UI.Dialogs.Properties.Resources.OK;
            this.BtnCancel.Content = UI.Dialogs.Properties.Resources.Cancel;
            this.BtnOK.ToolTip = UI.Dialogs.Properties.Resources.OK;
            this.BtnCancel.ToolTip = UI.Dialogs.Properties.Resources.Cancel;
            this.ChkSelectAll.ToolTip = UI.Dialogs.Properties.Resources.SelectAll;
            this.LblSelectAll.Content = UI.Dialogs.Properties.Resources.SelectAll;
            this.Result = null;
        }
        /// <summary>
        /// Instantiates a new OptionsDialog.
        /// </summary>
        /// <param name="message">string: The caption message to display to the User</param>
        /// <param name="options">string[]: The List of Option Labels to be Presented to the User</param>
        /// <param name="mood">System.Windows.MessageBoxImage enum: Used to derive the background color schema</param>
        /// <param name="title">string: Optional. Title Bar Content</param>
        public OptionsDialog(string message, string[] options, MessageBoxImage mood, string title = null) 
            : this()
        {
            const int ITEMWIDTH = 284;
            const int ITEMHEIGHT = 20;
            this.Caption.Content = title;
            this.Message.Text = message;
            foreach (string option in options)
            {
                if (!string.IsNullOrEmpty(option))
                {
                    InlineCheckBox icb = new InlineCheckBox(option)
                    {
                        Margin = new Thickness(4),
                        Width = ITEMWIDTH,
                        Height = ITEMHEIGHT,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    this.Options.Items.Add(icb);
                }
            }
            this.Mood = mood;
            UpdateLayout();
        }
        /// <summary>
        /// Instantiates a new CustomButtonsDialog.
        /// </summary>
        /// <param name="message">string: The caption message to display to the User</param>
        /// <param name="buttonLabels">string[]: The List of Button Labels to be Presented to the User</param>
        /// <param name="title">string: Optional. Title Bar Content</param>
        public OptionsDialog(string message, string[] buttonLabels, string title = null)
            : this(message, buttonLabels, MessageBoxImage.None, title)
        { }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.DisposeOf(this);
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            List<string> ret = new List<string>();
            foreach (var cntrl in this.Options.Items)
            {
                if (typeof(InlineCheckBox) == cntrl.GetType())
                {
                    InlineCheckBox chk = cntrl as InlineCheckBox;
                    if (chk.IsChecked == true)
                    {
                        ret.Add(chk.Caption);
                    }
                }
            }
            this.Result = ret;
            this.DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ChkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            bool check = (sender as CheckBox).IsChecked == true;
            foreach (var cntrl in this.Options.Items)
            {
                if (typeof(InlineCheckBox) == cntrl.GetType())
                {
                    (cntrl as InlineCheckBox).IsChecked = check;
                }
            }
        }
    }
}
