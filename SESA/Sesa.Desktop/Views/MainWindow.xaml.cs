using System.Management;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using Sesa.Desktop.Common;
using Sesa.Desktop.ViewModels;

namespace Sesa.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            SimpleIoc.Default.Register(() => this);
        }

        public MenuCommandObject Current { get;private set; }
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var co = (sender as TabControl).SelectedItem as MenuCommandObject;
            if (co != Current)
            {
                Current = co;
                if (co != null && co.Navigator != null &&
                    (co.Navigator as FrameworkElement).DataContext is MyViewModelBase &&
                    ((co.Navigator as FrameworkElement).DataContext as MyViewModelBase).RefreshOnEnter())
                    co.Navigator.NavigateEnter();
            }
        }

        public MainWindowViewModel ViewModel
        {
            get
            {
                return DataContext as MainWindowViewModel;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Design & Implementation: \"REZA ARAB\" \n (MINOOTA@GMAIL.COM <--> 09119792045)");
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
