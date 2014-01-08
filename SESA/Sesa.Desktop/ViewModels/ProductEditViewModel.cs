using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class ProductEditViewModel : EditViewModelBase<Product>
    {
        private ObservableCollection<InternalProductMaterial> _internals;

        private ObservableCollection<ExternalProductMaterial> _externals;
        private IEnumerable<Material> _materials;

        public override Tokens Token
        {
            get
            {
                return Tokens.EditProduct;
            }
        }

        public IDataService<Material> MaterialAccessService { get; private set; }

        public IDataService<InternalProductMaterial> InternalMaterialAccessService { get; private set; }

        public IDataService<ExternalProductMaterial> ExternalMaterialAccessService { get; private set; }

        public IEnumerable<Material> Materials
        {
            get { return _materials; }
            private set { _materials = value; RaisePropertyChanged(() => Materials); }
        }

        public ObservableCollection<InternalProductMaterial> Internals
        {
            get { return _internals; }
            set
            {
                _internals = value;
                RaisePropertyChanged(() => Internals);
            }
        }

        public ObservableCollection<ExternalProductMaterial> Externals
        {
            get { return _externals; }
            set
            {
                _externals = value;
                RaisePropertyChanged(() => Externals);
            }
        }

        protected override void LoadData(string key)
        {
            MaterialAccessService = SimpleIoc.Default.GetInstance<IDataService<Material>>(key);
            MaterialAccessService.SyncContext(key);
            Materials = MaterialAccessService.Get();

            InternalMaterialAccessService = SimpleIoc.Default.GetInstance<IDataService<InternalProductMaterial>>(key);
            InternalMaterialAccessService.SyncContext(key);
            Internals = new ObservableCollection<InternalProductMaterial>(Entity.InternalProductMaterials);

            ExternalMaterialAccessService = SimpleIoc.Default.GetInstance<IDataService<ExternalProductMaterial>>(key);
            ExternalMaterialAccessService.SyncContext(key);
            Externals = new ObservableCollection<ExternalProductMaterial>(Entity.ExternalProductMaterial);
        }

        protected override bool OnSave()
        {
            UpdateInternals();
            UpdateExternals();

            return base.OnSave();
        }

        private void UpdateInternals()
        {
            var addeds = Internals.Where(p => p.Id == Guid.Empty).ToArray();
            var deleteds = Entity.InternalProductMaterials.Except(Internals).ToArray();

            foreach (var addedObj in addeds)
            {
                Entity.InternalProductMaterials.Add(addedObj);
            }

            foreach (var deleteObj in deleteds)
            {
                InternalMaterialAccessService.Delete(deleteObj);
            }
        }

        private void UpdateExternals()
        {
            var addeds = Externals.Where(p => p.Id == Guid.Empty).ToArray();
            var deleteds = Entity.ExternalProductMaterial.Except(Externals).ToArray();

            foreach (var addedObj in addeds)
            {
                Entity.ExternalProductMaterial.Add(addedObj);
            }

            foreach (var deleteObj in deleteds)
            {
                ExternalMaterialAccessService.Delete(deleteObj);
            }
        }
    }
}