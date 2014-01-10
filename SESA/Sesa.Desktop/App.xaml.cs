using System;
using System.Globalization;
using System.IO;
using System.Threading;
using FarsiLibrary.FX.Utils;
using Sesa.Desktop.Common;

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
            ExceptionHelper.ReportException(ex, "خطای سیستمی");
        }

        public bool DoHandle
        {
            get { return _doHandle; }
            set { _doHandle = value; }
        }

        private void Application_DispatcherUnhandledException(object sender,
                               System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = DoHandle;
            ExceptionHelper.ReportException(e.Exception, "خطای سیستمی");
            if (!DoHandle)
                MessageBoxHelper.Show("برنامه به دلیل خطای سیستمی بسته خواهد شد! ", "خطای سیستمی");
        }
    }
}