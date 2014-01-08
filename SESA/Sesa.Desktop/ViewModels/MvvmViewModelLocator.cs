/*
  In App.xaml:
  <Application.Resources>
      <vm:MvvmViewModelLocator xmlns:vm="clr-namespace:Sesa.Desktop.ViewModels"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Sesa.Desktop.Common;
using Sesa.Desktop.Models;
using Sesa.Desktop.Views;

namespace Sesa.Desktop.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MvvmViewModelLocator
    {
        static MvvmViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
            }
            else
            {
            }
            SimpleIoc.Default.Register(() => new SesaModelContainer());
            SimpleIoc.Default.Register<IDataService<Unit>, UnitDataService>();
            SimpleIoc.Default.Register<IDataService<Product>, ProductDataService>();
            SimpleIoc.Default.Register<IDataService<WarehouseBill>, WarehouseBillDataService>();
            SimpleIoc.Default.Register<IDataService<Material>, MaterialDataService>();
            SimpleIoc.Default.Register<IDataService<ExternalProductMaterial>, ExternalProductMaterialDataService>();
            SimpleIoc.Default.Register<IDataService<InternalProductMaterial>, InternalProductMaterialDataService>();
            SimpleIoc.Default.Register<IDataService<WarehouseBillDetail>, WarehouseBillDetailDataService>();
            SimpleIoc.Default.Register<IDataService<Testimony>, TestimonyDataService>();
            SimpleIoc.Default.Register<IDataService<TestimonyDetail>, TestimonyDetailDataService>();

            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<ProductListViewModel>();
            SimpleIoc.Default.Register<ProductEditViewModel>();
            SimpleIoc.Default.Register<UnitListViewModel>();
            SimpleIoc.Default.Register<UnitEditViewModel>();
            SimpleIoc.Default.Register<WarehouseBillListViewModel>();
            SimpleIoc.Default.Register<WarehouseBillEditViewModel>();
            SimpleIoc.Default.Register<MaterialListViewModel>();
            SimpleIoc.Default.Register<MaterialEditViewModel>();
            SimpleIoc.Default.Register<TestimonyListViewModel>();
            SimpleIoc.Default.Register<ProductSelectViewModel>();
            SimpleIoc.Default.Register<TestimonyEditViewModel>();
            SimpleIoc.Default.Register<WarehouseBillTestimonyReportViewModel>();
            SimpleIoc.Default.Register<MaterialWarehouseBillReportViewModel>();
            SimpleIoc.Default.Register<RdlcPrintViewerViewModel>();
            SimpleIoc.Default.Register<TestimonyReportViewModel>();

            SimpleIoc.Default.Register<INavigation>(() => new EmptyView(), "EmptyView");
            SimpleIoc.Default.Register<INavigation>(() => new ProductListView(), "ProductListView");
            SimpleIoc.Default.Register<INavigation>(() => new ProductEditView(), "ProductEditView");
            SimpleIoc.Default.Register<INavigation>(() => new UnitListView(), "UnitListView");
            SimpleIoc.Default.Register<INavigation>(() => new UnitEditView(), "UnitEditView");
            SimpleIoc.Default.Register<INavigation>(() => new MaterialListView(), "MaterialListView");
            SimpleIoc.Default.Register<INavigation>(() => new MaterialEditView(), "MaterialEditView");
            SimpleIoc.Default.Register<INavigation>(() => new WarehouseBillListView(), "WarehouseBillListView");
            SimpleIoc.Default.Register<INavigation>(() => new WarehouseBillEditView(), "WarehouseBillEditView");
            SimpleIoc.Default.Register<INavigation>(() => new TestimonyListView(), "TestimonyListView");
            SimpleIoc.Default.Register<INavigation>(() => new ProductSelectView(), "ProductSelectView");
            SimpleIoc.Default.Register<INavigation>(() => new TestimonyEditView(), "TestimonyEditView");
            SimpleIoc.Default.Register<INavigation>(() => new ReportListView(), "ReportListView");
            SimpleIoc.Default.Register<INavigation>(() => new WarehouseBillTestimonyReportView(), "WarehouseBillTestimonyReportView");
            SimpleIoc.Default.Register<INavigation>(() => new MaterialWarehouseBillReportView(), "MaterialWarehouseBillReportView");
            SimpleIoc.Default.Register<INavigation>(() => new RdlcPrintViewer(), "ReportViewer");
            SimpleIoc.Default.Register<INavigation>(() => new TestimonyReportView(), "TestimonyReportView");
        }

        /// <summary>
        /// Gets the MainWindowViewModel property.
        /// </summary>
        public MainWindowViewModel MainWindowViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowViewModel>();
            }
        }

        /// <summary>
        /// Gets the ProductListViewModel property.
        /// </summary>
        public ProductListViewModel ProductListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProductListViewModel>();
            }
        }

        /// <summary>
        /// Gets the ProductEditViewModel property.
        /// </summary>
        public ProductEditViewModel ProductEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProductEditViewModel>();
            }
        }

        /// <summary>
        /// Gets the UnitListViewModel property.
        /// </summary>
        public UnitListViewModel UnitListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UnitListViewModel>();
            }
        }

        /// <summary>
        /// Gets the UnitEditViewModel property.
        /// </summary>
        public UnitEditViewModel UnitEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UnitEditViewModel>();
            }
        }

        /// <summary>
        /// Gets the MaterialListViewModel property.
        /// </summary>
        public MaterialListViewModel MaterialListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MaterialListViewModel>();
            }
        }

        /// <summary>
        /// Gets the MaterialEditViewModel property.
        /// </summary>
        public MaterialEditViewModel MaterialEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MaterialEditViewModel>();
            }
        }

        /// <summary>
        /// Gets the WarehouseBillListViewModel property.
        /// </summary>
        public WarehouseBillListViewModel WarehouseBillListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WarehouseBillListViewModel>();
            }
        }

        /// <summary>
        /// Gets the WarehouseBillEditViewModel property.
        /// </summary>
        public WarehouseBillEditViewModel WarehouseBillEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WarehouseBillEditViewModel>();
            }
        }

        /// <summary>
        /// Gets the ProductSelectViewModel property.
        /// </summary>
        public ProductSelectViewModel ProductSelectViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProductSelectViewModel>();
            }
        }

        /// <summary>
        /// Gets the TestimonyListViewModel property.
        /// </summary>
        public TestimonyListViewModel TestimonyListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TestimonyListViewModel>();
            }
        }

        /// <summary>
        /// Gets the TestimonyEditViewModel property.
        /// </summary>
        public TestimonyEditViewModel TestimonyEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TestimonyEditViewModel>();
            }
        }

        /// <summary>
        /// Gets the RdlcPrintViewerViewModel property.
        /// </summary>
        public RdlcPrintViewerViewModel RdlcPrintViewerViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RdlcPrintViewerViewModel>();
            }
        }

        /// <summary>
        /// Gets the WarehouseBillTestimonyReportViewModel property.
        /// </summary>
        public WarehouseBillTestimonyReportViewModel WarehouseBillTestimonyReportViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WarehouseBillTestimonyReportViewModel>();
            }
        }

        /// <summary>
        /// Gets the MaterialWarehouseBillReportViewModel property.
        /// </summary>
        public MaterialWarehouseBillReportViewModel MaterialWarehouseBillReportViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MaterialWarehouseBillReportViewModel>();
            }
        }

        /// <summary>
        /// Gets the TestimonyReportViewModel property.
        /// </summary>
        public TestimonyReportViewModel TestimonyReportViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TestimonyReportViewModel>();
            }
        }
    }
}