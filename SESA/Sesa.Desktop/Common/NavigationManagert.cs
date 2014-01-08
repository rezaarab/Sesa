using System;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Ioc;
using Sesa.Desktop.Views;

namespace Sesa.Desktop.Common
{
    public interface INavigation
    {
        bool NavigateExit();
        void NavigateEnter();
    }
    public class NavigationManagert
    {
        private static INavigation _current = SimpleIoc.Default.GetInstance<INavigation>("EmptyView");

        static MainWindow mainWindow
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainWindow>();
            }
        }

        static INavigation Current
        {
            get { return _current; }
            set { _current = value; }
        }

        private static int lastTabOpen = 0;
        private static readonly INavigation _emptyWindow = SimpleIoc.Default.GetInstance<INavigation>("EmptyView");

        public static bool NavigateTo(INavigation navigator)
        {
            if (navigator != null)
            {
                var t = mainWindow.TabMenu.SelectedIndex;
                if (Current != _emptyWindow)
                    mainWindow.TabMenu.SelectedIndex = 0;
                if (Current.NavigateExit())
                {
                    lastTabOpen = t;
                    mainWindow.ViewModel.MenuCommandObjects[0].Navigator = navigator;
                    Current = navigator;
                    navigator.NavigateEnter();
                    mainWindow.TabMenu.SelectedIndex = 0;
                    return true;
                }
                mainWindow.TabMenu.SelectedIndex = t;
            }
            return false;
        }

        public static bool NavigateClose()
        {
            if (Current != null
                && Current != _emptyWindow
                && Current.NavigateExit())
            {
                mainWindow.Dispatcher.BeginInvoke(new Action(() =>
                {
                    mainWindow.ViewModel.MenuCommandObjects[0].Navigator = _emptyWindow;
                    Current = _emptyWindow;
                    mainWindow.TabMenu.SelectedIndex = lastTabOpen;
                }), DispatcherPriority.DataBind);
                return true;
            }
            return false;
        }
        //public static bool NavigatePrevious()
        //{
        //    PreviousNavigations.Pop();
        //    var navigator = PreviousNavigations.Pop();
        //    if (navigator != null)
        //        return NavigateTo(navigator);
        //    return false;
        //}
    }
}
