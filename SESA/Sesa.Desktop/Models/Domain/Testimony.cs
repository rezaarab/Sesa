using System;
using System.Collections.Generic;
using System.Linq;
using Sesa.Desktop.Common;

namespace Sesa.Desktop.Models
{
    public partial class Testimony : IBaseModel
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
            if (string.IsNullOrWhiteSpace(this.HeaderDate))
                errors.Add("HeaderDate", "HeaderDateRequired");
            if (string.IsNullOrWhiteSpace(this.HeaderNumber))
                errors.Add("HeaderNumber", "HeaderNumberRequired");
            if (string.IsNullOrWhiteSpace(this.PackingType))
                errors.Add("PackingType", "PackingTypeRequired");
            if (this.Product == null)
                errors.Add("Product", "ProductRequired");
            if (this.ProductCount <= 0)
                errors.Add("ProductCount", "ProductCountGreatThanZero");
            if (this.PureWeight <= 0)
                errors.Add("PureWeight", "PureWeightGreatThanZero");
            if (this.RealWeight <= 0)
                errors.Add("RealWeight", "RealWeightGreatThanZero");
            if (string.IsNullOrWhiteSpace(this.RequestDate))
                errors.Add("RequestDate", "RequestDateRequired");
            if (string.IsNullOrWhiteSpace(this.RequestNumber))
                errors.Add("RequestNumber", "RequestNumberRequired");
            if (this.TestimonyDetails.SelectMany(p => p.GetErrors()).Any())
                errors.Add("TestimonyDetails", "TestimonyDetailsError");
            if (this.TestimonyDetails.GroupBy(p => p.Material != null ? p.Material.Id : Guid.Empty).Any(p => p.Count() > 1))
                errors.Add("TestimonyDetails", "SameTestimonyDetailsError");
            return errors;
        }
    }
}
