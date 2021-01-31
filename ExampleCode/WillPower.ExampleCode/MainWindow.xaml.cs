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

using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;

namespace WillPower.ExampleCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// edit this as you see fit.  this is a good place to copy and paste from
        /// </summary>
        IFileLayout Layout2D
        {
            get
            {
                FileParserEncoder encoder = new FileParserEncoder();
                return new FileLayout(encoder)
                {
                    RecordLength = 25,
                    HeaderRecord = new FileRecord(encoder)
                    {
                        Fields = new IFileField[] 
                        { 
                            new FileField(encoder, typeof(DateTime), "File Date", 0, 4, FileFieldDataFormat.IBMPacked),
                            new FileField(encoder, typeof(int), "Record Count", 4, 3, FileFieldDataFormat.String)
                        }
                    },
                    MasterFields = new IFileField[]
                    {
                        new FileField(encoder, typeof(string), "Account Type", 0, 3, FileFieldDataFormat.String),
                        new FileField(encoder, typeof(string), "Account Name", 3, 15, FileFieldDataFormat.String),
                        new FileField(encoder, "Amount", 18, 4, FileFieldDataFormat.IBMPacked, 2)
                    }
                };
            }
        }

        /// <summary>
        /// edit this as you see fit.  this is a good place to copy and paste from
        /// </summary>
        IFileLayout LayoutTable
        {
            get
            {
                FileParserEncoder encoder = new FileParserEncoder();
                return new FileLayout(encoder)
                {
                    RecordLength = 25,
                    MasterFields = new IFileField[]
                    {
                        new FileField(encoder, typeof(string), "Marker", 0, 1, FileFieldDataFormat.String),
                        new FileField(encoder, typeof(string), "Remark", 1, 24, FileFieldDataFormat.String),
                    }
                };
            }
        }

        /// <summary>
        /// edit this as you see fit.  this is a good place to copy and paste from
        /// </summary>
        IFileLayout Layout3D
        {
            get
            {
                FileParserEncoder encoder = new FileParserEncoder();
                return new FileLayout(encoder)
                {
                    RecordLength = 235,
                    HeaderRecord = new FileRecord(encoder)
                    {
                        Fields = new IFileField[]
                        {
                            new FileField(encoder, typeof(DateTime), "File Date", 0, 4, FileFieldDataFormat.IBMPacked),
                            new FileField(encoder, typeof(int), "Record Count", 4, 3, FileFieldDataFormat.String)
                        }
                    },
                    MasterFields = new IFileField[]
                    {
                        new FileField(encoder, typeof(string), "Rec Type", 0, 1, FileFieldDataFormat.String),
                    },
                    Conditions = new IFileConditional[]
                    {
                        new FileConditional(rec => { return rec.Get<string>("Rec Type") == "A"; })
                        {
                            Name = "Rec Type A",
                            Fields = new IFileField[]
                            {
                                new FileField(encoder, typeof(string), "Account Type", 1, 3, FileFieldDataFormat.String),
                                new FileField(encoder, typeof(string), "Account Name", 4, 15, FileFieldDataFormat.String),
                                new FileField(encoder, "Amount", 19, 4, FileFieldDataFormat.IBMPacked, 2)
                            }
                        },
                        new FileConditional(rec => { return rec.Get<string>("Rec Type") == "B"; })
                        {
                            Name = "Rec Type B",
                            Fields = new IFileField[]
                            {
                                new FileField(encoder, typeof(string), "Account Review", 1, 5, FileFieldDataFormat.String),
                                new FileField(encoder, typeof(string), "Reviewer Name", 6, 25, FileFieldDataFormat.String),
                                new FileField(encoder, typeof(DateTime), "Review Date", 31, 2, FileFieldDataFormat.IBMPacked),
                                new FileTable(encoder, "Review Findings", 33, 200)
                                {
                                    Layout = LayoutTable
                                }
                            }
                        },
                   }

                };
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.DisposeOf(this);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void BtnShowMoods_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog.ShowInfo("This will walk through the various dialog moods. This is Info.", "MessageDialog Moods");
            MessageDialog.ShowQuestion("This will walk through the various dialog moods. This is Question.", MessageBoxButton.OK, 
                "MessageDialog Moods");
            MessageDialog.ShowWarning("This will walk through the various dialog moods. This is Warning.", "MessageDialog Moods");
            MessageDialog.ShowAlert("This will walk through the various dialog moods. This is Alert.", "MessageDialog Moods");

        }

        private void BtnShowOptions_Click(object sender, RoutedEventArgs e)
        {
            var res = OptionsDialog.Invoke("This is an example of an options dialog:", 
                new string[] { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5" },
                MessageBoxImage.Question, "OptionsDialog Example");
            if (res?.Count > 0)
            {
                string msg = "You selected ";
                foreach (string s in res)
                {
                    msg += $"{s}, ";
                }
                msg = msg.Trim().TrimEnd(',');
                MessageDialog.ShowInfo(msg, "OptionsDialog Example");
            }
        }

        private void BtnShowButtons_Click(object sender, RoutedEventArgs e)
        {
            string res = ButtonsDialog.Invoke("This is an exmaple of the buttons dialog...", 
                new string[] { "Button 1", "Button 2", "Button 3", "Button 4", "Button 5" },
                MessageBoxImage.Question, "ButtonsDialog Example");
            if (!string.IsNullOrWhiteSpace(res))
            {
                MessageDialog.ShowInfo($"You selected {res}", "ButtonsDialog Example");
            }
        }

        private void Btn2DFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Binary EBCDIC 2D File...";
            if (ofd.ShowDialog(this) != true)
            {
                return;
            }
            FileParser parser = new FileParser(Layout2D);
            parser.LoadFile(ofd.FileName);
            System.Data.DataSet ds = parser.ToDataSet();
            //load the dataset somewhere  or...
            foreach (var rec in parser.Records)
            {
                string type = rec.Get<string>("Account Type");
                // ...do something here...
            }
        }

        private void Btn3DFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Binary EBCDIC 3D File...";
            if (ofd.ShowDialog(this) != true)
            {
                return;
            }
            FileParser parser = new FileParser(Layout3D);
            parser.LoadFile(ofd.FileName);
            System.Data.DataSet ds = parser.ToDataSet();
            //load the dataset somewhere  or...
            foreach (var rec in parser.Records)
            {
                string type = rec.Get<string>("Rec Type");
                // ...do something here...
            }
        }
    }
}
