using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace XU9YYJ_HFT_2021221.Logic.Interfaces
{
    public interface IValidation
    {
        bool Validate(object instance, PropertyInfo propertyInfo);
    }
}
