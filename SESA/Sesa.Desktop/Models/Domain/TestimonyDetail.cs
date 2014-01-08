using System.Collections.Generic;
using System.Linq;
using Sesa.Desktop.Common;

namespace Sesa.Desktop.Models
{
    public partial class TestimonyDetail : IBaseModel
    {
        public TestimonyDetail()
        {
            TestimonyReference.AssociationChanged += (sender, args) => OnPropertyChanged("Testimony");
            MaterialReference.AssociationChanged += (sender, args) => OnPropertyChanged("Material");
            WarehouseBillReference.AssociationChanged += (sender, args) => OnPropertyChanged("WarehouseBill");
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
            {
                OnPropertyChanged("MockValue");
                Weight = Value * (WarehouseBill != null && WarehouseBill.WarehouseBillDetails.Any(p => p.Material == Material) ?
                    WarehouseBill.WarehouseBillDetails.First(p => p.Material == Material).WeightUnit : 0);
            }
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
            if (WarehouseBill == null)
                errors.Add("WarehouseBill", "WarehouseBillRequired");
            if (Material == null)
                errors.Add("Material", "MaterialRequired");
            if (Value <= 0)
                errors.Add("Value", "ValueGreaterThanZero");
            return errors;
        }
    }
}
