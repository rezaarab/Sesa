using System;
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
    /// </para>``
    /// </summary>
    public class TestimonyListViewModel : ListViewModelBase<Testimony>
    {
        private const string Testimonylist = "TestimonyList";

        public TestimonyListViewModel()
        {
            CommandObjects.Add(new CommandObject(PrintCommand, "Print"));

            MessengerInstance.Register<Tuple<string, Guid>>(this, Tokens.SelectProduct, tuple =>
            {
                if (tuple.Item1 == Testimonylist && tuple.Item2 != Guid.Empty)
                {
                    NavigationManagert.NavigateTo(
                        SimpleIoc.Default.GetInstance<INavigation>("TestimonyEditView"));
                    MessengerInstance.Send(Guid.Empty, EditToken);
                    MessengerInstance.Send(tuple.Item2, Tokens.SelectProductEditTestimony);
                }
            });
        }

        private RelayCommand _PrintCommand;
        public RelayCommand PrintCommand
        {
            get
            {
                return _PrintCommand
                    ?? (_PrintCommand = new RelayCommand(
                                          () =>
                                          {
                                              var navigation = SimpleIoc.Default.GetInstance<INavigation>("TestimonyReportView");
                                              MessengerInstance.Send(SelectedEntity.HeaderNumber, Tokens.TestimonyReport);
                                              NavigationManagert.NavigateTo(navigation);
                                          },
                                          () => SelectedEntity != null));
            }
        }

        public override Tokens EditToken
        {
            get
            {
                return Tokens.EditTestimony;
            }
        }

        protected override void OnNew()
        {
            var navigation = SimpleIoc.Default.GetInstance<INavigation>("ProductSelectView");
            MessengerInstance.Send(Testimonylist, Tokens.RegisterSelectIdentity);
            ModalDialogHelper.Show(new MenuCommandObject
                (navigation, "ProductSelect"));
        }

        protected override void OnSelectedEntityChanged()
        {
            base.OnSelectedEntityChanged();
            PrintCommand.RaiseCanExecuteChanged();
        }
    }
}