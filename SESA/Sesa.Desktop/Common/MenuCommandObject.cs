using System.ComponentModel;
using Sesa.Desktop.Annotations;

namespace Sesa.Desktop.Common
{
    public class MenuCommandObject : INotifyPropertyChanged
    {
        private INavigation _navigator;

        public MenuCommandObject(INavigation navigator, string key)
        {
            Navigator = navigator;
            Key = key;
        }
        public INavigation Navigator
        {
            get { return _navigator; }
            set
            {
                _navigator = value;
                OnPropertyChanged("Navigator");
            }
        }

        public string Key { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
