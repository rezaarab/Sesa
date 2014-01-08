using System;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

namespace Sesa.Desktop.Common
{
    public class RdlcReportSetting
    {
        public ReportDataSource[] ReportSource { get; set; }
        public string ReportEmbeddedResource { get; set; }
        public string ReportPath { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public bool IsShowPrintDialog { get; set; }
        public Action AfterPrintAction { get; set; }
    }
}