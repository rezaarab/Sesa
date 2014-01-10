using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Reporting.WinForms;
using Sesa.Desktop.Common;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.ViewModels
{
    public class WarehouseBillTestimonyReportViewModel : PrintViewModelBase
    {
        private string _rowNumber;

        public string RowNumber
        {
            get { return _rowNumber; }
            set { _rowNumber = value; RaisePropertyChanged(() => RowNumber); ShowCommand.RaiseCanExecuteChanged(); }
        }

        protected override bool CanShow()
        {
            return !string.IsNullOrEmpty(RowNumber);
        }

        IDataService<WarehouseBill> WarehouseDataAccessService { get; set; }

        protected override void OnShow()
        {
            var warehouseBill = WarehouseDataAccessService.Get(p => p.RowNumber == RowNumber).FirstOrDefault();
            if (warehouseBill == null)
            {
                MessageBoxHelper.Show(@"قبض انبار با این شماره یافت نشد");
                return;
            }
            var dataSourceValue1 = warehouseBill.TestimonyDetails.AsEnumerable().Select(p => new
                {
                    warehouseBill.RowNumber,
                    warehouseBill.EmissionDate,
                    MaterialCaption = p.Material.Caption,
                    ProductCaption = p.Testimony.Product.Caption,
                    p.Testimony.HeaderNumber,
                    p.Testimony.HeaderDate,
                    Value = p.MockValue,
                    OriginalValue = warehouseBill.WarehouseBillDetails.Where(q => q.Material == p.Material).AsEnumerable().Sum(q => q.MockValue),
                    UnitCaption = p.Material.Unit.Caption
                }).OrderBy(p => p.MaterialCaption).ThenBy(p => p.HeaderNumber).ToList();
            var dataSourceValue = warehouseBill.WarehouseBillDetails.AsEnumerable().Select(p => new
                {
                    warehouseBill.RowNumber,
                    warehouseBill.EmissionDate,
                    MaterialCaption = p.Material.Caption,
                    ProductCaption = warehouseBill.TestimonyDetails.Any(q => q.Material == p.Material) ? warehouseBill.TestimonyDetails.First(q => q.Material == p.Material).Testimony.Product.Caption : string.Empty,
                    HeaderNumber = warehouseBill.TestimonyDetails.Any(q => q.Material == p.Material) ? warehouseBill.TestimonyDetails.First(q => q.Material == p.Material).Testimony.HeaderNumber : string.Empty,
                    HeaderDate = warehouseBill.TestimonyDetails.Any(q => q.Material == p.Material) ? warehouseBill.TestimonyDetails.First(q => q.Material == p.Material).Testimony.HeaderDate : string.Empty,
                    Value = warehouseBill.TestimonyDetails.Any(q => q.Material == p.Material) ? warehouseBill.TestimonyDetails.First(q => q.Material == p.Material).MockValue : 0,
                    OriginalValue = p.MockValue,
                    UnitCaption = p.Material.Unit.Caption
                }).OrderBy(p => p.MaterialCaption).ThenBy(p => p.HeaderNumber).ToList();
            var setting = new RdlcReportSetting
                {
                    //ReportPath = Path.Combine(Environment.CurrentDirectory, @"Reports\WarehouseBillTestimony.rdlc"),
                    ReportEmbeddedResource = "Sesa.Desktop.Reports.WarehouseBillTestimony.rdlc",
                    ReportSource = new[] { new ReportDataSource("DataSet1", dataSourceValue) },
                };
            var navigation = SimpleIoc.Default.GetInstance<INavigation>("ReportViewer");
            MessengerInstance.Send(setting, Tokens.ReportViewer);
            ModalDialogHelper.Show(new MenuCommandObject(navigation, Title), true);
        }

        private string _key;

        public override void NavigateEnter()
        {
            base.NavigateEnter();
            _key = Guid.NewGuid().ToString();
            WarehouseDataAccessService = SimpleIoc.Default.GetInstance<IDataService<WarehouseBill>>(_key);
            WarehouseDataAccessService.SyncContext(_key);
        }
    }
}
