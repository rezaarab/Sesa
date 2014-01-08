using GalaSoft.MvvmLight.Command;

namespace Sesa.Desktop.Common
{
    public class PrintViewModelBase : MyViewModelBase
    {
        private RelayCommand _showCommand;
        public RelayCommand ShowCommand
        {
            get
            {
                return _showCommand ?? (_showCommand = new RelayCommand(OnShow, CanShow));
            }
        }

        protected virtual void OnShow()
        {
            
        }

        protected virtual bool CanShow()
        {
            return true;
        }


        public override bool RefreshOnEnter()
        {
            return true;
        }
    }
}
