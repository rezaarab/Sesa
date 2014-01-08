using System;
using System.Collections.Generic;
using System.Linq;
using Sesa.Desktop.Common;

namespace Sesa.Desktop.Models
{
    public partial class WarehouseBill : IBaseModel
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
            if (string.IsNullOrWhiteSpace(this.EmissionDate))
                errors.Add("EmissionDate", "EmissionDateRequired");
            if (string.IsNullOrWhiteSpace(this.RowNumber))
                errors.Add("RowNumber", "RowNumberRequired");
            if (this.WarehouseBillDetails.Count <= 0)
                errors.Add("ValidInputPercent", "WarehouseBillDetailsGreaterThanZero");
            if (this.WarehouseBillDetails.SelectMany(p => p.GetErrors()).Any())
                errors.Add("WarehouseBillDetails", "WarehouseBillDetailsError");
            if (this.WarehouseBillDetails.GroupBy(p => p.Material != null ? p.Material.Id : Guid.Empty).Any(p => p.Count() > 1))
                errors.Add("WarehouseBillDetails", "SameWarehouseBillDetailsError");
            return errors;
        }

    }
}
