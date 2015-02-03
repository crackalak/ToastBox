using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace ToastBox
{
    /// <summary>
    /// Toast notification overlay with fade in & out animation
    /// </summary>
    internal partial class ToastWindow : Window
    {
        private DispatcherTimer autoCloseTimer;
        
        public TimeSpan Delay { get; set; }
        
        private string _message;
        public string Message {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                lblToast.Content = value;
            } 
        }

        public ToastWindow()
        {
            InitializeComponent();
        }

        private void ToastBox1_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = this.Owner.ActualLeft() + (this.Owner.ActualWidth / 2) - (this.ActualWidth / 2);
            this.Top = this.Owner.ActualTop() + 50;
        }
        
        private void animationOpen_Completed(object sender, EventArgs e)
        {
            autoCloseTimer = new DispatcherTimer();
            autoCloseTimer.Tick += autoCloseTimer_Tick;
            autoCloseTimer.Interval = Delay;
            autoCloseTimer.Start();
        }

        private void autoCloseTimer_Tick(object sender, EventArgs e)
        {
            autoCloseTimer.Stop();
            DoubleAnimation animation = new DoubleAnimation(2, 0, new Duration(TimeSpan.FromSeconds(1)));
            animation.Completed += animationClose_Completed;
            borderToast.BeginAnimation(Border.OpacityProperty, animation);
        }

        private void animationClose_Completed(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
    
    static class Layout
        {
            /// <summary>
            /// Get the actual left position of the window, .Left returns incorrect value when maximized after restore
            /// </summary>
            /// <param name="window"></param>
            /// <returns></returns>
            public static double ActualLeft(this Window window)
            {
                if (window.WindowState == WindowState.Maximized)
                {
                    var leftField = typeof(Window).GetField("_actualLeft", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    return (double)leftField.GetValue(window);
                }
                else
                    return window.Left;
            }

            /// <summary>
            /// Get the actual top position of the window, .Top returns incorrect value when maximized after restore
            /// </summary>
            /// <param name="window"></param>
            /// <returns></returns>
            public static double ActualTop(this Window window)
            {
                if (window.WindowState == WindowState.Maximized)
                {
                    var topField = typeof(Window).GetField("_actualTop", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    return (double)topField.GetValue(window);
                }
                else
                    return window.Top;
            }

        }
}
