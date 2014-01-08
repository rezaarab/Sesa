using System.Collections.Generic;
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
    public class MaterialEditViewModel : EditViewModelBase<Material>
    {
        private IEnumerable<Unit> _units;

        public override Tokens Token
        {
            get
            {
                return Tokens.EditMaterial;
            }
        }

        public IDataService<Unit> UnitAccessService { get; private set; }

        public IEnumerable<Unit> Units
        {
            get { return _units; }
            private set { _units = value; RaisePropertyChanged(() => Units); }
        }

        protected override void LoadData(string key)
        {
            UnitAccessService = SimpleIoc.Default.GetInstance<IDataService<Unit>>(key);
            UnitAccessService.SyncContext(key);
            Units = UnitAccessService.Get();
        }

    }
}