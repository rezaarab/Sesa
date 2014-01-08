using System.Collections.Generic;
using Sesa.Desktop.Common;

namespace Sesa.Desktop.Models
{
    public partial class WarehouseBillDetail : IBaseModel
    {
        public WarehouseBillDetail()
        {
            MaterialReference.AssociationChanged += (sender, args) => OnPropertyChanged("Material");
        }
        public decimal MockValue
        {
            get { return Material != null && !Material.IsFloatValue ? decimal.Floor(Value) : Value; }
            set { Value = Material != null && !Material.IsFloatValue ? decimal.Floor(value) : value; }
        }

        public decimal WeightUnit
        {
            get { return decimal.Round((Value == 0 ? 0 : Weight / Value), 2); }
        }

        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);
            if (property == "Value")
                OnPropertyChanged("MockValue");
            if (property == "Value" || property == "Weight")
                OnPropertyChanged("WeightUnit");
            if (property == "Material" || property == "Value")
                if (Material != null && Material.Unit != null && !string.IsNullOrWhiteSpace(Material.Unit.Symbol) &&
                    Material.Unit.Symbol.ToLower().Contains("kg"))
                    Weight = Value;
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
            if (this.Material == null)
                errors.Add("Material", "MaterialRequired");
            if (this.Value <= 0)
                errors.Add("Value", "ValueGreaterThanZero");
            if (this.Weight <= 0)
                errors.Add("Weight", "WeightGreaterThanZero");
            return errors;
        }
    }
}
