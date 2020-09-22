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

using System.Windows.Controls;

namespace WillPower
{
    /// <summary>
    /// Interaction logic for InlineCheckBox.xaml
    /// </summary>
    public partial class InlineCheckBox : UserControl
    {
        /// <summary>
        /// The IsChecked Property value of CheckBox
        /// </summary>
        public bool? IsChecked
        {
            get
            {
                return this.Czhek.IsChecked;
            }
            set
            {
                this.Czhek.IsChecked = value;
            }
        }
        /// <summary>
        /// CheckBox compoenent.
        /// </summary>
        public CheckBox CheckBox
        {
            get
            {
                return this.Czhek;
            }
            set
            {
                this.Czhek = value;
            }
        }
        /// <summary>
        /// Label component.
        /// </summary>
        public string Caption
        {
            get
            {
                return this.CaptionLabel.Content?.ToString();
            }
            set
            {
                this.CaptionLabel.Content = value;
            }
        }

        /// <summary>
        /// Creates a new instance of InlineCheckBox.
        /// </summary>
        public InlineCheckBox()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Creates a new instance of InlineCheckBox.
        /// </summary>
        /// <param name="caption">string: Caption label</param>
        /// <param name="isChecked">bool?: IsChecked value for CheckBox</param>
        public InlineCheckBox(string caption, bool? isChecked = null)
        {
            this.Czhek.IsChecked = isChecked;
            this.CaptionLabel.Content = caption;
        }

    }
}
