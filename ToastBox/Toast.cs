using System;
using System.Windows;

namespace ToastBox
{
    public class Toast
    {
        public TimeSpan Delay { get; set; }
        public Window Owner { get; set; }
        public string Message { get; set; }
        private ToastWindow tw;

        public Toast(Window owner)
        {
            this.Owner = owner;
            Delay = new TimeSpan(0, 0, 1);
        }

        public Toast(Window owner, string message) : this(owner)
        {
            this.Message = message;
        }

        public Toast(Window owner, TimeSpan delay)
        {
            this.Owner = owner;
            this.Delay = delay;
        }

        public Toast(Window owner, TimeSpan delay, string message) : this(owner, delay)
        {
            this.Message = message;
        }

        public void Show(){

            tw = new ToastWindow();
            tw.Delay = this.Delay;
            tw.Owner = this.Owner;
            tw.Message = this.Message;
            tw.Show();

        }

        public void Close()
        {
            if (tw != null)
            {
                tw.Close();
            }
        }
    }
}
