using System;
using System.Globalization;
using System.Threading;
using FarsiLibrary.FX.Utils;

namespace Sesa.Desktop
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool _doHandle = true;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture =
            Thread.CurrentThread.CurrentCulture = CultureHelper.FarsiCulture;
            Thread.CurrentThread.CurrentCulture.NumberFormat = Thread.CurrentThread.CurrentUICulture.NumberFormat =
                new NumberFormatInfo();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            MessageBox.Show(ex.Message, "خطای سیستمی",
                            MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool DoHandle
        {
            get { return _doHandle; }
            set { _doHandle = value; }
        }

        private void Application_DispatcherUnhandledException(object sender,
                               System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (DoHandle)
            {
                //Handling the exception within the UnhandledException handler.
                MessageBox.Show(e.Exception.Message, "خطای سیستمی",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
            else
            {
                //If you do not set e.Handled to true, the application will close due to crash.
                MessageBox.Show("برنامه به دلیل خطای سیستمی بسته خواهد شد! ", "خطای سیستمی");
                e.Handled = false;
            }
        }
    }
}