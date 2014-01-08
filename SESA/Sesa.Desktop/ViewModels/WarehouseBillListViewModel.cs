using System;
using System.Linq;
using GalaSoft.MvvmLight.Command;
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
    public class WarehouseBillListViewModel : ListViewModelBase<WarehouseBill>
    {
        public WarehouseBillListViewModel()
        {
            CommandObjects.Remove(CommandObjects.First(p => p.Command == NewCommand));
            CommandObjects.Insert(0,new CommandObject(NewExternalCommand, "External"));
            CommandObjects.Insert(0, new CommandObject(NewInternalCommand, "Internal"));
        }

        private RelayCommand _newInternalCommand;
        public RelayCommand NewInternalCommand
        {
            get
            {
                return _newInternalCommand ?? (_newInternalCommand = new RelayCommand(OnNewInternal, () => true));
            }
        }

        protected virtual void OnNewInternal()
        {
            NavigationManagert.NavigateTo(
                SimpleIoc.Default.GetInstance<INavigation>(string.Format("{0}EditView", typeof(WarehouseBill).Name)));
            MessengerInstance.Send(true,Tokens.IsInternal);
            MessengerInstance.Send(Guid.Empty, EditToken);
        }

        private RelayCommand _newExternalCommand;
        public RelayCommand NewExternalCommand
        {
            get
            {
                return _newExternalCommand ?? (_newExternalCommand = new RelayCommand(OnNewExternal, () => true));
            }
        }

        protected virtual void OnNewExternal()
        {
            NavigationManagert.NavigateTo(
                SimpleIoc.Default.GetInstance<INavigation>(string.Format("{0}EditView", typeof(WarehouseBill).Name)));
            MessengerInstance.Send(false,Tokens.IsInternal);
            MessengerInstance.Send(Guid.Empty, EditToken);
        }


        public override Tokens EditToken
        {
            get
            {
                return Tokens.EditWarehouseBill;
            }
        }
    }
}