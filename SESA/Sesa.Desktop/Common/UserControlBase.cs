using System;
using System.Windows;
using System.Windows.Controls;

namespace Sesa.Desktop.Common
{
    public class UserControlBase : UserControl, INavigation
    {
        public UserControlBase()
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml") });
        }
        public virtual bool NavigateExit()
        {
            return (DataContext as INavigation) == null || (DataContext as INavigation).NavigateExit();
        }

        public virtual void NavigateEnter()
        {
            if ((DataContext as INavigation) != null)
                (DataContext as INavigation).NavigateEnter();
        }
    }
}
