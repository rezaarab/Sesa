using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Reporting.WinForms;
using MiscUtil.Collections;
using Sesa.Desktop.Common;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.ViewModels
{
    public class TestimonyReportViewModel : PrintViewModelBase
    {
        public TestimonyReportViewModel()
        {
            MessengerInstance.Register<string>(this, Tokens.TestimonyReport, number =>
                {
                    TestimonyNumber = number;
                });
        }

        public string TestimonyNumber
        {
            get { return _testimonyNumber; }
            set { _testimonyNumber = value; RaisePropertyChanged(() => TestimonyNumber); ShowCommand.RaiseCanExecuteChanged(); }
        }

        IDataService<Testimony> TestimonyDataAccessService { get; set; }

        protected override bool CanShow()
        {
            return !string.IsNullOrWhiteSpace(TestimonyNumber);
        }

        protected override void OnShow()
        {
            var testimony = TestimonyDataAccessService.Get(p => p.HeaderNumber == TestimonyNumber).FirstOrDefault();
            if (testimony == null)
            {
                MessageBoxHelper.Show("گواهی با این شماره یافت نشد");
                NavigationManagert.NavigateClose();
                return;
            }
            var dataSourceValue1 = new[]{  new
                {
                    testimony.HeaderDate,
                    testimony.HeaderNumber,
                    testimony.PackingType,
                    ProductCaption=testimony.Product.Caption,
                    testimony.Product.AddedValuePercent,
                    testimony.Product.ValidInputPercent,
                    TestimonyAddedValueNumber=ResourceHelper.GetResource("TestimonyAddedValueNumber"),
                    TestimonyAddedValueDate=ResourceHelper.GetResource("TestimonyAddedValueDate"),
                    testimony.ProductCount,
                    testimony.PureWeight,
                    testimony.RealWeight,
                    testimony.RequestDate,
                    testimony.RequestNumber,
                }};
            var internalOrder = testimony.Product.InternalProductMaterials.OrderBy(p=>p.Sort).Select(p => p.Material).ToList();
            var dataSourceValue2 = testimony.TestimonyDetails.Where(p => p.IsInternal).AsEnumerable()
                .OrderBy(p => p.WarehouseBill.RowNumber)
                .OrderBy(p => p.Material, ProjectionComparer<Material>.Create(internalOrder.IndexOf))
                .Select(p => new
                {
                    MaterialCaption = p.Material.Caption,
                    ValueCaption = string.Format("{0} {1}", p.MockValue, p.Material.Unit.Caption),
                    WarehouseBillRowNumber = p.WarehouseBill.RowNumber,
                    WarehouseBillDate = p.WarehouseBill.EmissionDate,
                    p.Weight
                }).ToArray();

            var externalOrder = testimony.Product.InternalProductMaterials.OrderBy(p => p.Sort).Select(p => p.Material).ToList();
            var dataSourceValue3 = testimony.TestimonyDetails.Where(p => !p.IsInternal).AsEnumerable()
              .OrderBy(p => p.WarehouseBill.RowNumber)
              .OrderBy(p => p.Material, ProjectionComparer<Material>.Create(externalOrder.IndexOf))
              .Select(p => new
            {
                MaterialCaption = p.Material.Caption,
                ValueCaption = string.Format("{0} {1}", p.MockValue, p.Material.Unit.Caption),
                WarehouseBillRowNumber = p.WarehouseBill.RowNumber,
                WarehouseBillDate = p.WarehouseBill.EmissionDate,
                p.Weight
            }).ToArray();
            var setting = new RdlcReportSetting
                {
                    //ReportPath = Path.Combine(Environment.CurrentDirectory, @"Reports\Testimony.rdlc"),
                    ReportEmbeddedResource = "Sesa.Desktop.Reports.Testimony.rdlc",
                    ReportSource = new[] 
                    { 
                        new ReportDataSource("DataSet1", dataSourceValue1),
                        new ReportDataSource("DataSet2", dataSourceValue2),
                        new ReportDataSource("DataSet3", dataSourceValue3)
                    },
                };
            var navigation = SimpleIoc.Default.GetInstance<INavigation>("ReportViewer");
            MessengerInstance.Send(setting, Tokens.ReportViewer);
            ModalDialogHelper.Show(new MenuCommandObject(navigation, Title), true);
            NavigationManagert.NavigateClose();
        }

        private string _key;
        private string _testimonyNumber;

        public override void NavigateEnter()
        {
            base.NavigateEnter();
            _key = Guid.NewGuid().ToString();
            TestimonyDataAccessService = SimpleIoc.Default.GetInstance<IDataService<Testimony>>(_key);
            TestimonyDataAccessService.SyncContext(_key);
            ShowCommand.Execute(null);
        }

        public override bool RefreshOnEnter()
        {
            return false;
        }
    }
}
