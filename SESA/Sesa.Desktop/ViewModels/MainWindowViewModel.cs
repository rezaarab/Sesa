using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Ioc;
using Sesa.Desktop.Common;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.ViewModels
{
    /// <summary>
    /// MainWindow view model.
    /// </summary>
    public class MainWindowViewModel : MyViewModelBase
    {
        public MainWindowViewModel()
        {
            MenuCommandObjects = new ObservableCollection<MenuCommandObject>
                {
                    new MenuCommandObject(SimpleIoc.Default.GetInstance<INavigation>("EmptyView"), "ஃ"),//"ஃ※⁂∷╸─✣"፧
                    new MenuCommandObject(SimpleIoc.Default.GetInstance<INavigation>("WarehouseBillListView"), "WarehouseBillList"),
                    new MenuCommandObject(SimpleIoc.Default.GetInstance<INavigation>("TestimonyListView"), "TestimonyList"),
                    new MenuCommandObject(SimpleIoc.Default.GetInstance<INavigation>("ReportListView"), "ReportList"),
                    new MenuCommandObject(SimpleIoc.Default.GetInstance<INavigation>("ProductListView"), "ProductList"),
                    new MenuCommandObject(SimpleIoc.Default.GetInstance<INavigation>("MaterialListView"), "MaterialList"),
                    new MenuCommandObject(SimpleIoc.Default.GetInstance<INavigation>("UnitListView"), "UnitList"),
                };
        }
        public ObservableCollection<MenuCommandObject> MenuCommandObjects { get; set; }

        public override string Title { get { return ResourceHelper.GetResource("MainWindow"); } }
    }
}
