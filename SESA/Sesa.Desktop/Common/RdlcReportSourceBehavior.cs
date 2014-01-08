using System.Linq;
using System.Windows;
using System.Windows.Forms.Integration;
using Microsoft.Reporting.WinForms;

namespace Sesa.Desktop.Common
{
    public static class RdlcReportSourceBehavior
    {
        public static readonly DependencyProperty ReportSettingProperty =
            DependencyProperty.RegisterAttached("ReportSetting", typeof(object), typeof(RdlcReportSourceBehavior), new PropertyMetadata(ReportSettingChanged));

        private static void ReportSettingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var host = d as WindowsFormsHost;
            if (host != null)
            {
                var viewer = host.Child as ReportViewer;
                if (viewer != null)
                {
                    var data = e.NewValue as RdlcReportSetting;
                    if (data != null)
                    {
                        viewer.Reset();
                        data.ReportSource.ToList().ForEach(p => viewer.LocalReport.DataSources.Add(p));
                        viewer.LocalReport.ReportEmbeddedResource = data.ReportEmbeddedResource;
                        viewer.LocalReport.ReportPath = data.ReportPath;
                        if (data.Parameters != null && data.Parameters.Count > 0)
                            viewer.LocalReport.SetParameters(data.Parameters.Select(p => new ReportParameter(p.Key, p.Value.ToString())));
                        viewer.RefreshReport();
                        if (data.IsShowPrintDialog)
                        {
                            bool isPrint = false;
                            viewer.RenderingComplete += (sender, e1) =>
                                {
                                    if (!isPrint)
                                    {
                                        viewer.PrintDialog();
                                        isPrint = true;
                                        if (data.AfterPrintAction != null)
                                            data.AfterPrintAction();
                                    }
                                };
                        }
                    }
                }
            }
        }

        public static void SetReportSetting(DependencyObject target, object value)
        {
            target.SetValue(ReportSettingProperty, value);
        }

        public static object GetReportSetting(DependencyObject target)
        {
            return target.GetValue(ReportSettingProperty);
        }
    }
}