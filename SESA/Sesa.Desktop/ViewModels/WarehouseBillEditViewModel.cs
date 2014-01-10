using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using Sesa.Desktop.Common;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class WarehouseBillEditViewModel : EditViewModelBase<WarehouseBill>
    {
        private bool _isInternal = true;
        private IEnumerable<Unit> _units;
        private IEnumerable<Material> _materials;

        public WarehouseBillEditViewModel()
        {
            MessengerInstance.Register<bool>(this, Tokens.IsInternal, p =>
                {
                    IsInternal = p;
                });
        }

        public bool IsInternal
        {
            get { return _isInternal; }
            set
            {
                _isInternal = value;
                RaisePropertyChanged(() => IsInternal);
            }
        }

        public override Tokens Token
        {
            get
            {
                return Tokens.EditWarehouseBill;
            }
        }

        public IDataService<Unit> UnitAccessService { get; private set; }

        public IEnumerable<Unit> Units
        {
            get { return _units; }
            private set { _units = value; RaisePropertyChanged(() => Units); }
        }

        public IDataService<Material> MaterialAccessService { get; private set; }

        public IDataService<WarehouseBillDetail> WarehouseBillDetailAccessService { get; private set; }

        private ObservableCollection<WarehouseBillDetail> _details;
        public ObservableCollection<WarehouseBillDetail> Details
        {
            get { return _details; }
            set
            {
                _details = value;
                RaisePropertyChanged(() => Details);
            }
        }


        public IEnumerable<Material> Materials
        {
            get { return _materials; }
            private set { _materials = value; RaisePropertyChanged(() => Materials); }
        }

        protected override void OnEntityChanged()
        {
            if (Entity != null && Mode == FormMode.New)
                Entity.IsInternal = IsInternal;
        }

        protected override void LoadData(string key)
        {
            UnitAccessService = SimpleIoc.Default.GetInstance<IDataService<Unit>>(key);
            UnitAccessService.SyncContext(key);
            Units = UnitAccessService.Get();

            MaterialAccessService = SimpleIoc.Default.GetInstance<IDataService<Material>>(key);
            MaterialAccessService.SyncContext(key);
            Materials = MaterialAccessService.Get();

            WarehouseBillDetailAccessService = SimpleIoc.Default.GetInstance<IDataService<WarehouseBillDetail>>(key);
            WarehouseBillDetailAccessService.SyncContext(key);
            Details = new ObservableCollection<WarehouseBillDetail>(Entity.WarehouseBillDetails);

        }

        protected override bool OnSave()
        {
            if (Mode == FormMode.Edit && Entity.TestimonyDetails.Count > 0)
            {
                MessageBoxHelper.Show("به دلیل صدور گواهی تولید برای این قبض مجاز به ویرایش قبض انبار نیستید");
                return false;
            }
            UpdateDetails();
            return base.OnSave();
        }

        private void UpdateDetails()
        {
            var addeds = Details.Where(p => p.Id == Guid.Empty).ToArray();
            var deleteds = Entity.WarehouseBillDetails.Except(Details).ToArray();

            foreach (var addedObj in addeds)
            {
                Entity.WarehouseBillDetails.Add(addedObj);
            }

            foreach (var deleteObj in deleteds)
            {
                WarehouseBillDetailAccessService.Delete(deleteObj);
            }
        }
    }
}