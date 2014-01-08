using System.Collections.Generic;
using Sesa.Desktop.Common;

namespace Sesa.Desktop.Models
{
    public partial class Material : IBaseModel
    {
        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                var errors = GetErrors();
                if (errors.ContainsKey(columnName))
                    return ResourceHelper.GetResource(errors[columnName]);
                return null;
            }
        }

        public Dictionary<string, string> GetErrors()
        {
            var errors = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(this.Caption))
                errors.Add("Caption", "CaptionRequired");
            if (this.Unit == null)
                errors.Add("Unit", "UnitRequired");
            return errors;
        }

    }
}
