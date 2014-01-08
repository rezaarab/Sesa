using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.Common
{
    public class ListViewModelBase<T> : MyViewModelBase where T : System.Data.Objects.DataClasses.EntityObject, IBaseModel, new()
    {
        public ListViewModelBase()
        {
            CommandObjects.Add(new CommandObject(NewCommand, "New"));
            CommandObjects.Add(new CommandObject(EditCommand, "Edit"));
            CommandObjects.Add(new CommandObject(RefreshCommand, "Refresh"));
        }

        #region ViewModel Property

        public override string Title { get { return ResourceHelper.GetResource(string.Format("{0}List", typeof(T).Name)); } }

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
                EditCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(SelectedEntityPropertyName);
                OnSelectedEntityChanged();
            }
        }

        protected virtual void OnSelectedEntityChanged()
        {
            
        }

        IDataService<T> DataAccessService { get; set; }

        #endregion Model Property

        public virtual Tokens EditToken { get; private set; }
        #region Commands

        #endregion Commands
        private RelayCommand _newCommand;
        public RelayCommand NewCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new RelayCommand(OnNew, () => true));
            }
        }

        protected virtual void OnNew()
        {
            Common.NavigationManagert.NavigateTo(
                SimpleIoc.Default.GetInstance<INavigation>(string.Format("{0}EditView", typeof(T).Name)));
            MessengerInstance.Send(Guid.Empty, EditToken);
        }

        private RelayCommand _EditCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return _EditCommand
                    ?? (_EditCommand = new RelayCommand(
                                          () =>
                                          {
                                              Common.NavigationManagert.NavigateTo(SimpleIoc.Default.GetInstance<INavigation>(string.Format("{0}EditView", typeof(T).Name)));
                                              MessengerInstance.Send(SelectedEntity.Id, EditToken);
                                          },
                                          () => SelectedEntity != null));
            }
        }

        private RelayCommand _RefreshCommand;

        /// <summary>
        /// Gets the RefreshCommand.
        /// </summary>
        public RelayCommand RefreshCommand
        {
            get
            {
                return _RefreshCommand
                    ?? (_RefreshCommand = new RelayCommand(
                                          () =>
                                          {
                                              Entities = new ObservableCollection<T>(DataAccessService.Get().ToList());
                                          }));
            }
        }

        private string _key;
        public override void NavigateEnter()
        {
            IsBusy = true;
            base.NavigateEnter();
            _key = Guid.NewGuid().ToString();
            DataAccessService = SimpleIoc.Default.GetInstance<IDataService<T>>(_key);
            DataAccessService.SyncContext(_key);
            RefreshCommand.Execute(null);
            IsBusy = false;
        }

        public override bool RefreshOnEnter()
        {
            return true;
        }
    }
}
