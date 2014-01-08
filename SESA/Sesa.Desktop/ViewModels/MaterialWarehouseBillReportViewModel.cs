using System;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Reporting.WinForms;
using Sesa.Desktop.Common;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.ViewModels
{
    public class MaterialWarehouseBillReportViewModel : PrintViewModelBase
    {
        IDataService<Material> MaterialDataAccessService { get; set; }

        protected override void OnShow()
        {
            var materials = MaterialDataAccessService.Get();
            var dataSourceValue = materials.SelectMany(p => p.WarehouseBillDetails).AsEnumerable().Select(p => new
                {
                    p.Material.Caption,
                    p.WarehouseBill.RowNumber,
                    p.MockValue,
                    ConsumeValue = p.WarehouseBill.TestimonyDetails.Where(q => q.Material == p.Material).Sum(q => q.MockValue),
                    RemindValue = p.MockValue - p.WarehouseBill.TestimonyDetails.Where(q => q.Material == p.Material).AsEnumerable().Sum(q => q.MockValue),
                    UnitCaption = p.Material.Unit.Caption,
                }).OrderBy(p=>p.Caption).ThenBy(p=>p.RowNumber).ToArray();
            var setting = new RdlcReportSetting
                {
                    ReportPath = Path.Combine(Environment.CurrentDirectory, @"Reports\MaterialWarehouseBill.rdlc"),
                    ReportSource = new[] { new ReportDataSource("DataSet1", dataSourceValue) },
                };
            var navigation = SimpleIoc.Default.GetInstance<INavigation>("ReportViewer");
            MessengerInstance.Send(setting, Tokens.ReportViewer);
            ModalDialogHelper.Show(new MenuCommandObject(navigation, Title), true);
            NavigationManagert.NavigateClose();
        }

        private string _key;

        public override void NavigateEnter()
        {
            base.NavigateEnter();
            _key = Guid.NewGuid().ToString();
            MaterialDataAccessService = SimpleIoc.Default.GetInstance<IDataService<Material>>(_key);
            MaterialDataAccessService.SyncContext(_key);
            ShowCommand.Execute(null);
        }

        public override bool RefreshOnEnter()
        {
            return false;
        }
    }
}
