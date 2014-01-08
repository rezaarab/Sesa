using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.Common
{
    public class SelectViewModelBase<T> : MyViewModelBase where T : System.Data.Objects.DataClasses.EntityObject, IBaseModel, new()
    {
        public SelectViewModelBase()
        {
            MessengerInstance.Register<string>(this, Tokens.RegisterSelectIdentity, p =>
                {
                    CurrentRegister = p;
                });

            CommandObjects.Add(new CommandObject(SelectCommand, "Select"));
            CommandObjects.Add(new CommandObject(CancelCommand, "Cancel"));
        }

        #region ViewModel Property

        public override string Title { get { return ResourceHelper.GetResource(string.Format("{0}Select", typeof(T).Name)); } }

        /// <summary>
        /// The <see cref="Entities" /> property's name.
        /// </summary>
        public const string EntitiesPropertyName = "Entities";

        private ObservableCollection<T> _entities = null;

        /// <summary>
        /// Sets and gets the Entities property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<T> Entities
        {
            get
            {
                return _entities;
            }

            set
            {
                if (_entities == value)
                {
                    return;
                }

                _entities = value;
                RaisePropertyChanged(EntitiesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedEntity" /> property's name.
        /// </summary>
        public const string SelectedEntityPropertyName = "SelectedEntity";

        private T _SelectedEntity = null;

        /// <summary>
        /// Sets and gets the SelectedEntity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public T SelectedEntity
        {
            get
            {
                return _SelectedEntity;
            }

            set
            {
                if (_SelectedEntity == value)
                {
                    return;
                }

                _SelectedEntity = value;
                SelectCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(SelectedEntityPropertyName);
            }
        }

        IDataService<T> DataAccessService { get; set; }

        #endregion Model Property

        public string CurrentRegister { get; private set; }
        public virtual Tokens SelectToken { get; private set; }
        #region Commands

        #endregion Commands
        private RelayCommand _selectCommand;
        public RelayCommand SelectCommand
        {
            get
            {
                return _selectCommand ?? (_selectCommand = new RelayCommand(OnSelect, () => SelectedEntity != null));
            }
        }

        protected virtual void OnSelect()
        {
            ModalDialogHelper.Close();
            MessengerInstance.Send(new Tuple<string, Guid>(CurrentRegister, SelectedEntity.Id), SelectToken);
        }

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand(ModalDialogHelper.Close, () => true));
            }
        }

        private string _key;
        public override void NavigateEnter()
        {
            base.NavigateEnter();
            _key = Guid.NewGuid().ToString();
            DataAccessService = SimpleIoc.Default.GetInstance<IDataService<T>>(_key);
            DataAccessService.SyncContext(_key);
            Entities = new ObservableCollection<T>(DataAccessService.Get().ToList());
        }

        public override bool RefreshOnEnter()
        {
            return true;
        }
    }
}
