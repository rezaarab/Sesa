using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sesa.Desktop.Models
{
    public interface IBaseModel : IDataErrorInfo
    {
        Guid Id { get; set; }

        Dictionary<string, string> GetErrors();
    }
}
