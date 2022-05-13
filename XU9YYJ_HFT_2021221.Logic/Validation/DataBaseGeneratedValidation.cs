using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Logic.Interfaces;

namespace XU9YYJ_HFT_2021221.Logic.Validation
{
    public class DataBaseGeneratedValidation : IValidation
    {
        public bool Validate(object instance, PropertyInfo propertyInfo)
        {
            return true;
        }
    }
}
