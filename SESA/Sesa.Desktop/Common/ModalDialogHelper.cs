using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Sesa.Desktop.Common
{
    public static class ModalDialogHelper
    {
        static readonly Stack<ModalDialog> stack = new Stack<ModalDialog>();
        public static void Show(MenuCommandObject menuCommandObject, bool isMaximaize = false)
        {
            var userControlBase = menuCommandObject.Navigator as UserControlBase;
            var myViewModelBase = userControlBase != null ? userControlBase.DataContext as MyViewModelBase : null;
            var dialog = new ModalDialog
            {
                Content = menuCommandObject,
                ShowCloseButton = myViewModelBase != null && (myViewModelBase.CommandObjects.Any(p => p.Key == "Cancel" || p.Key == "Close")),
                ShowInTaskbar = false,
                ShowIconOnTitleBar = false,
                ShowMinButton = false,
                ShowMaxRestoreButton = false,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowState = isMaximaize ? WindowState.Maximized : WindowState.Normal,
                ResizeMode = ResizeMode.NoResize,
                Owner = stack.FirstOrDefault(p => p.IsActive) ?? Application.Current.MainWindow,
            };
            menuCommandObject.Navigator.NavigateEnter();
            stack.Push(dialog);
            dialog.ShowDialog();
        }
        public static void Close()
        {
            var dialog = stack.Pop();
            if (dialog != null)
            {
                if ((dialog.Content as MenuCommandObject) != null && (dialog.Content as MenuCommandObject).Navigator.NavigateExit())
                    dialog.Close();
            }
        }
    }
}
