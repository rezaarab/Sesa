using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using Sesa.Desktop.Common;

namespace Sesa.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ReportListView.xaml
    /// </summary>
    public partial class ReportListView
    {
        public ReportListView()
        {
            InitializeComponent();
        }

        private void Report1Button_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationManagert.NavigateTo(SimpleIoc.Default.GetInstance<INavigation>("WarehouseBillTestimonyReportView"));
        }

        private void Report2Button_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationManagert.NavigateTo(SimpleIoc.Default.GetInstance<INavigation>("MaterialWarehouseBillReportView"));
        }
    }
}
