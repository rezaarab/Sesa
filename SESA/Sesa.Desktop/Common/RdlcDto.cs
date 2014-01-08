using System.ComponentModel;
using Sesa.Desktop.Annotations;

namespace Sesa.Desktop.Common
{
    public class RdlcDto : INotifyPropertyChanged
    {
        private string _ReportName;

        public string ReportName
        {
            get { return _ReportName; }
            set
            {
                _ReportName = value; 
                OnPropertyChanged("ReportName");
            }
        }

        private string _reportDataSetName = "DataSet1";

        public string ReportDataSetName
        {
            get { return _reportDataSetName; }
            set { _reportDataSetName = value; }
        }

        public string ReportEmbededPath { get; set; }

        public string ReportPath { get; set; }

        public bool IsShowPrintDialog { get; set; }

        public System.Action AfterPrintAction { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
