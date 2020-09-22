﻿//
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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WillPower
{ 
    /// <summary>
    /// Interaction logic for ButtonsDialog.xaml
    /// </summary>
    public partial class ButtonsDialog : Window
    {

        /// <summary>
        /// Threadsafe execution of Dialog.
        /// </summary>
        /// <param name="Message">string: The caption message to display to the User</param>
        /// <param name="ButtonLabels">string[]: The List of Button Labels to be Presented to the User</param>
        /// <param name="Mood">System.Windows.MessageBoxImage enum: Used to derive the background color schema</param>
        /// <param name="Title">string: Optional. Title Bar Content</param>
        /// <returns>string: The selected Button Label or Null</returns>
        /// <exception cref="System.NullReferenceException">Can occur when either the Application or its Dispatcher object is Null</exception>
        public static string Invoke(string Message, string[] ButtonLabels, MessageBoxImage Mood = MessageBoxImage.None, string Title = null)
        {
            string result = null;
            Application.Current.Invoke(delegate
            {
                ButtonsDialog dlg = new ButtonsDialog(Message, ButtonLabels, Mood, Title);
                if (dlg.ShowDialog() == true)
                {
                    result = dlg.Result;
                }
            });
            return result;
        }

        /// <summary>
        /// The selected Button Label or Null.
        /// </summary>
        public string Result { get; private set; }
        /// <summary>
        /// The topmost background (fade from)
        /// </summary>
        public GradientStop ColorFadeTop
        {
            get
            {
                return this.FadeFrom;
            }
            set
            {
                this.FadeFrom = value;
            }
        }
        /// <summary>
        /// The bottommost background (fade to)
        /// </summary>
        public GradientStop ColorFadeBottom
        {
            get
            {
                return this.FadeTo;
            }
            set
            {
                this.FadeTo = value;
            }
        }
        /// <summary>
        /// The title label, completely accessible and modifiable.
        /// </summary>
        public Label TitleLabel
        {
            get
            {
                return this.Caption;
            }
            set
            {
                this.Caption = value;
            }
        }
        /// <summary>
        /// The message area TextBlock, completely accessible and modifiable.
        /// </summary>
        public TextBlock MessageBlock
        {
            get
            {
                return this.Message;
            }
            set
            {
                this.Message = value;
            }
        }

        private ButtonsDialog()
        {
            InitializeComponent();
            this.Result = null;
        }
        /// <summary>
        /// Instantiates a new CustomButtonsDialog.
        /// </summary>
        /// <param name="message">string: The caption message to display to the User</param>
        /// <param name="buttonLabels">string[]: The List of Button Labels to be Presented to the User</param>
        /// <param name="mood">System.Windows.MessageBoxImage enum: Used to derive the background color schema</param>
        /// <param name="title">string: Optional. Title Bar Content</param>
        public ButtonsDialog(string message, string[] buttonLabels, MessageBoxImage mood, string title = null) 
            : this()
        {
            const int BUTTONSPERROW = 5;
            const int BUTTONWIDTH = 70;
            const int BUTTONHEIGHT = 20;
            this.Caption.Content = title ?? "";
            this.Message.Text = message;
            for (int i = 0; i < BUTTONSPERROW; i++)
            {
                this.Options.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < Math.Ceiling((double)buttonLabels.Length / BUTTONSPERROW); i++)
            {
                this.Options.RowDefinitions.Add(new RowDefinition());
                this.Options.Height += BUTTONHEIGHT;
            }
            int row = 0, col = 0;
            foreach (string button in buttonLabels)
            {
                if (!string.IsNullOrEmpty(button))
                {
                    Button btn = new Button()
                    {
                        Content = button,
                        Margin = new Thickness(4),
                        Width = BUTTONWIDTH,
                        Height = BUTTONHEIGHT,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    btn.Click += Btn_Click;
                    this.Options.Children.Add(btn);
                    Grid.SetRow(btn, row);
                    Grid.SetColumn(btn, col);
                }
                col++;
                if (col >= BUTTONSPERROW)
                {
                    row++;
                    col = 0;
                }
            }
            SetTheMood(mood);
            UpdateLayout();
        }
        /// <summary>
        /// Instantiates a new CustomButtonsDialog.
        /// </summary>
        /// <param name="message">string: The caption message to display to the User</param>
        /// <param name="buttonLabels">string[]: The List of Button Labels to be Presented to the User</param>
        /// <param name="title">string: Optional. Title Bar Content</param>
        public ButtonsDialog(string message, string[] buttonLabels, string title = null)
            : this(message, buttonLabels, MessageBoxImage.None, title)
        { }

        private void SetTheMood(MessageBoxImage image)
        {
            switch ((int)image)
            {
                case 64:// MessageBoxImage.Information
                    this.FadeTo = new System.Windows.Media.GradientStop(System.Windows.Media.Color.FromRgb(36, 9, 205), 0.4);
                    break;
                case 48:// MessageBoxImage.Warning
                    this.FadeTo = new System.Windows.Media.GradientStop(System.Windows.Media.Color.FromRgb(205, 169, 9), 0.4);
                    break;
                case 32:// MessageBoxImage.Question
                    this.FadeTo = new System.Windows.Media.GradientStop(System.Windows.Media.Color.FromRgb(9, 205, 80), 0.4);
                    break;
                case 16:// MessageBoxImage.Error
                    this.FadeTo = new System.Windows.Media.GradientStop(System.Windows.Media.Color.FromRgb(205, 9, 45), 0.4);
                    break;
            }

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Result = (sender as Button).Content.ToString();
            this.DialogResult = true;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.DisposeOf(this);
        }

    }
}
