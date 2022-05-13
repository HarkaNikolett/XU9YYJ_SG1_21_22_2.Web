using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XU9YYJ_HFT_2021221.Logic.Interfaces;
using System.Reflection;


namespace XU9YYJ_HFT_2021221.Logic.Validation
{
    public class MaxLengthValidation :IValidation
    {
        int _maxLength;

        public MaxLengthValidation(MaxLengthAttribute attribute)
        {
            _maxLength = attribute.MaxLength;
        }

        public bool Validate(object instance, PropertyInfo propertyInfo)
        {
            var value = propertyInfo.GetValue(instance);

            var stringValue = (string)value;

            return stringValue.Length <= _maxLength;
        }
    }
}
