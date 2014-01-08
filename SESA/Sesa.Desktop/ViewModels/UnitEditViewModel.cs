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
    public class UnitEditViewModel : EditViewModelBase<Unit>
    {

        public override Tokens Token
        {
            get
            {
                return Tokens.EditUnit;
            }
        }

        protected override void LoadData(string key)
        {
            
        }
        protected override void OnEntityChanged()
        {
            base.OnEntityChanged();
            if (Entity != null)
                Entity.Symbol = string.Empty;
        }
    }
}