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

namespace WillPower
{
    /// <summary>
    /// Static Container for Extension Methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The <see cref="System.Windows.Application">Application</see> Current instance shortcut.
        /// </summary>
        private static System.Windows.Application App
        {
            get
            {
                return System.Windows.Application.Current;
            }
        }

        /// <summary>
        /// Threadsafe Invocation of <see cref="System.Action">delegate code</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.Windows.Application">Application</see> Current instance.</param>
        /// <param name="action">The <see cref="System.Action">delegate code</see> to execute.</param>
        public static void Invoke(this System.Windows.Application value, System.Action action)
        {
            if (value == null || value.Dispatcher == null)
            {
                action.Invoke();
            }
            else
            {
                value.Dispatcher.Invoke(action);
            }
        }

        /// <summary>
        /// Threadsafe Invocation of <see cref="System.Action">delegate code</see> returning a value.
        /// </summary>
        /// <typeparam name="T">System.Type: The System.Type of the return value.</typeparam>
        /// <param name="value">The <see cref="System.Windows.Application">Application</see> Current instance.</param>
        /// <param name="action">System.Func(T): The <see cref="System.Action">delegate code</see>(<see cref="System.Func{TResult}">Func<![CDATA[<T>]]></see>) to execute and return.</param>
        /// <returns>T: The value returned from the delegate as T.</returns>
        public static T Invoke<T>(this System.Windows.Application value, System.Func<T> action)
        {
            if (value == null || value.Dispatcher == null)
            {
                return action.Invoke();
            }
            return value.Dispatcher.Invoke<T>(action);
        }

        /// <summary>
        /// Threadsafe Close the Window and Optionally End the Application.
        /// </summary>
        /// <param name="value">The <see cref="System.Windows.Application">Application Instance</see>.</param>
        /// <param name="window"><see cref="System.Windows.Window">Window</see> The <see cref="System.Windows.Window">Window</see> to Close.</param>
        /// <param name="endApplication">bool; If True, Shutdown the Application. Default is False.</param>
        /// <exception cref="System.NullReferenceException">Can occur when either the Application or its Dispatcher object is Null.</exception>
        public static void DisposeOf(this System.Windows.Application value, System.Windows.Window window, bool endApplication = false)
        {
            value.Invoke(delegate
            {
                window?.Close();
                if (endApplication)
                {
                    value?.Shutdown();
                }
            });
        }

        /// <summary>
        /// Threadsafe Application Shutdown.
        /// </summary>
        /// <param name="value">The <see cref="System.Windows.Application">Application Instance</see> to End.</param>
        /// <exception cref="System.NullReferenceException">Can occur when either the The <see cref="System.Windows.Application">Application Instance</see> or its The <see cref="System.Windows.Application">Application Instance</see> Dispatcher object is null.</exception>
        public static void Close(this System.Windows.Application value)
        {
            value.Invoke(delegate
            {
                value?.Shutdown();
            });
        }

        /// <summary>
        /// Threadsafe Collapse.
        /// </summary>
        /// <param name="value">System.Windows.UIElement: The UIElement to set Visibility to System.Windows.Visibility.Collapsed.</param>
        /// <exception cref="System.NullReferenceException">Can occur when either the Current Application or its Dispatcher object is Null.</exception>
        public static void Collapse(this System.Windows.UIElement value)
        {
            App.Invoke(delegate
            {
                value.Visibility = System.Windows.Visibility.Collapsed;
            });
        }

        /// <summary>
        /// Threadsafe Hide.
        /// </summary>
        /// <param name="value">System.Windows.UIElement: The UIElement to set Visibility to System.Windows.Visibility.Hidden.</param>
        /// <exception cref="System.NullReferenceException">Can occur when either the Current Application or its Dispatcher object is Null.</exception>
        public static void Hide(this System.Windows.UIElement value)
        {
            App.Invoke(delegate
            {
                value.Visibility = System.Windows.Visibility.Hidden;
            });
        }

        /// <summary>
        /// Threadsafe Show.
        /// </summary>
        /// <param name="value">System.Windows.UIElement: The UIElement to set Visibility to System.Windows.Visibility.Visible.</param>
        /// <exception cref="System.NullReferenceException">Can occur when either the Current Application or its Dispatcher object is Null.</exception>
        public static void Show(this System.Windows.UIElement value)
        {
            App.Invoke(delegate
            {
                value.Visibility = System.Windows.Visibility.Visible;
            });
        }

        /// <summary>
        /// Threadsafe toggle of placeholder on empty TextBox. 
        /// Use on GotFocus and LostFocus events. 
        /// Placeholder is stored in the Tag property.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// WPF:
        ///     <TextBox GotFocus="TogglePlaceholder" LostFocus="TogglePlaceholder"/>
        /// 
        /// C#:
        ///     using WillPower;
        ///     
        ///     private void TogglePlaceholder(object sender, RoutedEventArgs e)
        ///     {
        ///         (sender as TextBox).TogglePlaceholder();
        ///     }
        /// ]]>
        /// </example>
        /// <param name="value">System.Windows.Controls.TextBox: The TextBox to act upon.</param>
        /// <param name="placeholder">string: Optional. If provided (and not Null), will overwrite the Tag property with the value.</param>
        /// <exception cref="System.NullReferenceException">Can occur when either the Current Application or its Dispatcher object is Null.</exception>
        public static void TogglePlaceholder(this System.Windows.Controls.TextBox value, string placeholder = null)
        {
            App.Invoke(delegate
            {
                if (!value.IsEnabled) return;
                value.Tag = placeholder ?? value.Tag;
                if (value.Tag == null) return;
                if (value.IsKeyboardFocused)
                {
                    if (value.Text == value.Tag.ToString())
                    {
                        value.Text = "";
                    }
                    value.Foreground = System.Windows.Media.Brushes.Black;
                    value.FontStyle = System.Windows.FontStyles.Normal;
                }
                else
                {
                    if (string.IsNullOrEmpty(value.Text))
                    {
                        value.Text = value.Tag.ToString();
                        value.Foreground = System.Windows.Media.Brushes.DarkGray;
                        value.FontStyle = System.Windows.FontStyles.Italic;
                    }
                    else
                    {
                        value.Foreground = System.Windows.Media.Brushes.Black;
                        value.FontStyle = System.Windows.FontStyles.Normal;
                    }
                }
            });
        }

        /// <summary>
        /// Redundant, but to alleviate confusion, this extension sets the Tag property to the provided value.
        /// </summary>
        /// <param name="value">System.Windows.Controls.TextBox.</param>
        /// <param name="text">string: Place holder value.</param>
        public static void SetPlaceHolder(this System.Windows.Controls.TextBox value, string text)
        {
            value.Tag = text;
        }

        /// <summary>
        /// Toggles the IsEnabled Property of the UIElement. Default is True (On).
        /// </summary>
        /// <param name="value">System.Windows.UIElement: The UIElement to toggle.</param>
        /// <param name="enable">bool: Sets the IsEnabled Property of the UIElement. Default is True.</param>
        /// <exception cref="System.NullReferenceException">Can occur when either the Current Application or its Dispatcher object is Null.</exception>
        public static void Enable(this System.Windows.UIElement value, bool enable = true)
        {
            App.Invoke(delegate
            {
                value.IsEnabled = enable;
            });
        }

        /// <summary>
        /// Returns the hierarchical <see cref="UniForm">UniForm</see> parent of the <see cref="System.Windows.FrameworkElement">element</see> or null if none.
        /// Threadsafe.
        /// </summary>
        /// <param name="element">The <see cref="System.Windows.FrameworkElement">FrameworkElement</see></param>
        /// <returns>Parent <see cref="UniForm">UniForm</see> or null.</returns>
        public static UniForm GetUniForm(this System.Windows.FrameworkElement element)
        {
            UniForm ret = null;
            App.Invoke(delegate
            {
                ret = element.UnsafeGetUniForm();
            });
            return ret;
        }
        /// <summary>
        /// Returns the hierarchical <see cref="UniForm">UniForm</see> parent of the <see cref="System.Windows.FrameworkElement">element</see> or null if none.
        /// </summary>
        /// <param name="element">The <see cref="System.Windows.FrameworkElement">FrameworkElement</see></param>
        /// <returns>Parent <see cref="UniForm">UniForm</see> or null.</returns>
        private static UniForm UnsafeGetUniForm(this System.Windows.FrameworkElement element)
        {
            System.Windows.DependencyObject parent = element.Parent;
            while (parent != null)
            {
                System.Type t = parent.GetType();
                if (t == typeof(UniForm))
                {
                    return parent as UniForm;
                }
                if (t.IsAssignableFrom(typeof(System.Windows.FrameworkElement)))
                {
                    parent = (parent as System.Windows.FrameworkElement).Parent;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Sets the Value, ToolTip and (optionally) Maximum properties of the <see cref="System.Windows.Controls.ProgressBar">ProgressBar</see> instance.
        /// Threadsafe.
        /// </summary>
        /// <param name="bar">The <see cref="System.Windows.Controls.ProgressBar">ProgressBar</see> affected.</param>
        /// <param name="value">The <see cref="System.Double">value</see> to set the <see cref="System.Windows.Controls.ProgressBar">ProgressBar</see> to.</param>
        /// <param name="max">The <see cref="System.Double">value</see> to set the Maximum property of the <see cref="System.Windows.Controls.ProgressBar">ProgressBar</see> to.</param>
        public static void SetProgress(this System.Windows.Controls.ProgressBar bar, double value, double max = 100)
        {
            App.Invoke(delegate
            {
                if (bar == null)
                {
                    return;
                }
                bar.Maximum = value > max ? value : max;
                bar.Value = value;
                bar.ToolTip = $"{System.Convert.ToInt32(((value * 100) / max))}%  ({value} of {bar.Maximum})";
            });
        }

    }
}
