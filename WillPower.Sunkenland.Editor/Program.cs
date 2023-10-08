//
// ********************************************************************************************************
// ********************************************************************************************************
// ***                                                                                                  ***
// *** Code Copyright © 2023, Will `Willow' Osborn.                                                     ***
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
namespace WillPower.Sunkenland.Editor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if RELEASE
            if (MessageBox.Show(Properties.Resources.Disclaimer, "Disclaimer", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
#endif

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "..", "LocalLow", "Vector3 Studio", "Sunkenland");
            if (!Directory.Exists(path)) 
            {
                if (MessageBox.Show("Cannot find the Sunkenland save path. Has it been installed?", "Sunkenland Editor", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    MessageBox.Show("Please create a character and/or world before using this tool. Exiting...", "Sunkenland Editor");
                    return;
                }
                using FolderBrowserDialog fdg = new()
                {
                    Description = "Please select the Sunkenland Save Path",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                };
                if (fdg.ShowDialog() == DialogResult.OK)
                {
                    if (!Directory.Exists(Path.Combine(fdg.SelectedPath, "Characters")))
                    {
                        MessageBox.Show("No characters found. Please create a character and/or world before using this tool. Exiting...", "Sunkenland Editor");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please create a character and/or world before using this tool. Exiting...", "Sunkenland Editor");
                    return;
                }
            }
            ApplicationConfiguration.Initialize();
            Application.Run(new PlayerEditor(path));
        }
    }
}