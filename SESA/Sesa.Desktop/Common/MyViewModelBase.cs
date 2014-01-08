using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Sesa.Desktop.Common
{
    public class MyViewModelBase : ViewModelBase, INavigation
    {
        private bool _isBusy;

        public MyViewModelBase()
        {
            CommandObjects = new ObservableCollection<CommandObject>();
        }

        public ObservableCollection<CommandObject> CommandObjects { get; set; }

        public virtual string Title { get { return ResourceHelper.GetResource(GetType().Name); } }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged("IsBusy"); }
        }

        public virtual bool NavigateExit()
        {
            return true;
        }

        public virtual void NavigateEnter()
        {
            
        }

        public virtual bool RefreshOnEnter()
        {
            return false;
        }
    }
}
