using System.Windows;
using System.Windows.Controls;

namespace Sesa.Desktop.Common
{
    /// <summary>
    /// Interaction logic for LayoutItem.xaml
    /// </summary>
    public partial class LayoutItem : UserControl
    {
        public LayoutItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof (string), typeof (LayoutItem), new PropertyMetadata(default(string)));

        public string Label
        {
            get { return (string) GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
    }
}
