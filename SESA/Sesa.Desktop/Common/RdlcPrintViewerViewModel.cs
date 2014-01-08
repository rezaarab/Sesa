using GalaSoft.MvvmLight.Command;

namespace Sesa.Desktop.Common
{
    public class RdlcPrintViewerViewModel : MyViewModelBase
    {
        public RdlcPrintViewerViewModel()
        {
            CommandObjects.Add(new CommandObject(CloseCommand, "Close"));
            MessengerInstance.Register<RdlcReportSetting>(this, Tokens.ReportViewer, reportSetting =>
            {
                ReportSetting = reportSetting;
            });
        }

        private RelayCommand _closeCommand;
        private RdlcReportSetting _reportSetting;

        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand(ModalDialogHelper.Close, () => true));
            }
        }

        public RdlcReportSetting ReportSetting
        {
            get { return _reportSetting; }
            private set { _reportSetting = value; RaisePropertyChanged(() => ReportSetting); }
        }
    }
}
