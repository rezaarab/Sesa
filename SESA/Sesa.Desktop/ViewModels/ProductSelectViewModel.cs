using Sesa.Desktop.Common;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.ViewModels
{
    public class ProductSelectViewModel : SelectViewModelBase<Product>
    {
        public override Tokens SelectToken
        {
            get
            {
                return Tokens.SelectProduct;
            }
        }
    }
}
