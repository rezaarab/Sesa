using System.Collections.Generic;
using Sesa.Desktop.Common;

namespace Sesa.Desktop.Models
{
    public partial class ExternalProductMaterial : IBaseModel
    {
        public ExternalProductMaterial()
        {
            MaterialReference.AssociationChanged += (sender, args) => OnPropertyChanged("Material");
        }

        public decimal MockValue
        {
            get { return Material != null && !Material.IsFloatValue ? decimal.Floor(Value) : Value; }
            set { Value = Material != null && !Material.IsFloatValue ? decimal.Floor(value) : value; }
        }

        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);
            if (property == "Value")
                OnPropertyChanged("MockValue");
        }

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
            if (this.Product == null)
                errors.Add("Product", "ProductRequired");
            if (this.Material == null)
                errors.Add("Material", "MaterialRequired");
            if (this.Value <= 0)
                errors.Add("Value", "ValueGreaterThanZero");
            if (this.Pert <= 0 || this.Pert > 100)
                errors.Add("Pert", "PercentNotValid");
            return errors;
        }
    }
}
