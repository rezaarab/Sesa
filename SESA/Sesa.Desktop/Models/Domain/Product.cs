using System.Collections.Generic;
using System.Linq;
using Sesa.Desktop.Common;

namespace Sesa.Desktop.Models
{
    public partial class Product : IBaseModel
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
            if (this.AddedValuePercent <= 0 || this.AddedValuePercent > 100)
                errors.Add("AddedValuePercent", "PercentNotValid");
            if (string.IsNullOrWhiteSpace(this.Caption))
                errors.Add("Caption", "CaptionRequired");
            if (this.ValidInputPercent <= 0 || this.ValidInputPercent > 100)
                errors.Add("ValidInputPercent", "PercentNotValid");
            if (this.InternalProductMaterials.SelectMany(p => p.GetErrors()).Any())
            {
                errors.Add("InternalProductMaterials", "InternalProductMaterialsError");
                var temp = this.InternalProductMaterials.SelectMany(p => p.GetErrors()).First();
                errors.Add(string.Format("InternalProductMaterial{0}",temp.Key),temp.Value);
            }
            if (this.ExternalProductMaterial.SelectMany(p => p.GetErrors()).Any())
            {
                errors.Add("ExternalProductMaterial", "ExternalProductMaterialError");
                var temp = this.ExternalProductMaterial.SelectMany(p => p.GetErrors()).First();
                errors.Add(string.Format("ExternalProductMaterial{0}", temp.Key), temp.Value);
            }
            return errors;
        }

    }
}
