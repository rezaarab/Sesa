using System;
using System.IO;
using System.Linq;
using System.Windows;
using FarsiLibrary.FX.Utils;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.Common
{
    public abstract class EditViewModelBase<T> : MyViewModelBase where T : System.Data.Objects.DataClasses.EntityObject, IBaseModel, new()
    {
        public EditViewModelBase()
        {
            MessengerInstance.Register<Guid>(this, Token, id =>
            {
                if (id != Guid.Empty)
                {
                    Mode = FormMode.Edit;
                    Entity = DataAccessService.GetByID(id);
                }
                else
                {
                    Mode = FormMode.New;
                    Entity = new T { Id = Guid.NewGuid() };
                }
                LoadData(_key);
            });

            CommandObjects.Add(new CommandObject(SaveCommand, "Save"));
            CommandObjects.Add(new CommandObject(CancelCommand, "Cancel"));
        }

        public virtual Tokens Token { get; private set; }

        public FormMode Mode
        {
            get { return _mode; }
            private set { _mode = value; RaisePropertyChanged(() => Mode); }
        }

        private RelayCommand _saveCommand;

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand
                    ?? (_saveCommand = new RelayCommand(() => OnSave()));
            }
        }

        protected virtual bool OnSave()
        {
            try
            {
                var errors = Entity.GetErrors();
                if (errors.Any())
                {
                    MessageBoxHelper.Show(string.Join(Environment.NewLine, errors.Values.Select(ResourceHelper.GetResource)), "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                if (Mode == FormMode.New)
                    DataAccessService.Insert(Entity);
                DataAccessService.Save();
                _isValidExit = true;
                NavigationManagert.NavigateClose();
                return true;

            }
            catch (Exception ex)
            {
                ExceptionHelper.ReportException(ex, "خطای پایگاه داده");
                return false;
            }
        }

        private bool _isValidExit = false;

        private RelayCommand _cancelCommand;

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand
                    ?? (_cancelCommand = new RelayCommand(OnCancel));
            }
        }

        protected virtual void OnCancel()
        {
            if (_isValidExit || true ||
                MessageBoxHelper.Show(ResourceHelper.GetResource("CancelQuestion"), ResourceHelper.GetResource("Cancel"),
                                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Entity = DataAccessService.GetByID(Entity.Id);
                NavigationManagert.NavigateClose();
            }
        }

        protected IDataService<T> DataAccessService { get; set; }
        private T _entity = null;

        /// <summary>
        /// Sets and gets the Entity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public T Entity
        {
            get
            {
                return _entity;
            }

            set
            {
                _entity = value;
                RaisePropertyChanged("Entity");
                OnEntityChanged();
            }
        }

        protected virtual void OnEntityChanged()
        {

        }
        public override string Title { get { return ResourceHelper.GetResource(string.Format("{0}Edit", typeof(T).Name)); } }

        private string _key;
        private FormMode _mode;

        public override void NavigateEnter()
        {
            base.NavigateEnter();
            _key = Guid.NewGuid().ToString();
            DataAccessService = SimpleIoc.Default.GetInstance<IDataService<T>>(_key);
            DataAccessService.SyncContext(_key);
            _isValidExit = false;
            if (Entity != null && Mode == FormMode.Edit)
            {
                Entity = DataAccessService.GetByID(Entity.Id);
                LoadData(_key);
            }
        }

        protected abstract void LoadData(string key);
    }
}
