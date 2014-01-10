using Microsoft.Reporting.WinForms;

namespace Sesa.Desktop.Common
{
    /// <summary>
    /// Interaction logic for RdlcPrintViewer.xaml
    /// </summary>
    public partial class RdlcPrintViewer
    {
        public RdlcPrintViewer()
        {
            InitializeComponent();
            ReportViewer.LocalReport.ReportEmbeddedResource = "Sesa.Desktop.Reports.EmptyRdlc.rdlc";
            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            ReportViewer.ZoomMode = ZoomMode.PageWidth;
        }
    }
}
