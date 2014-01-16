using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using FarsiLibrary.FX.Utils;

namespace Sesa.Desktop.Common
{
    public static class ExceptionHelper
    {
        public static void ReportException(Exception ex, string headerError)
        {
            string messageBoxText = string.Format("{0} {1} {2}", headerError, Environment.NewLine, GetExceptionMessages(ex));
            MessageBoxHelper.Show(messageBoxText, "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);
            try
            {
                File.AppendAllText("C:\\Sesa\\Errors.txt",
                    string.Format("{0} {1}{2}{3}{2}{2}{2}",
                    CultureHelper.GetCurrentDate(),
                    CultureHelper.GetCurrentTime(),
                    Environment.NewLine,
                    messageBoxText));
            }
            catch { }
        }

        public static string GetExceptionMessages(Exception ex)
        {
            var message = ex.Message;
            while (ex.InnerException != null)
            {
                message += string.Format("{0} InnerException:{0}{1}", Environment.NewLine, ex.InnerException.Message);
                ex = ex.InnerException;
            }
            return message;
        }
    }
}
