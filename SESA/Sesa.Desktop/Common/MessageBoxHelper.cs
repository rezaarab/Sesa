using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace Sesa.Desktop.Common
{
    public class MessageBoxHelper
    {
        public static MessageBoxResult Show(string message, string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Information)
        {
            return MessageBox.Show(message, caption, button, icon);
        }
    }
}
