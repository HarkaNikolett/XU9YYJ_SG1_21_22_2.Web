using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Logic.Interfaces;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace XU9YYJ_HFT_2021221.Logic.Validation
{
    public class RequiredValidation : IValidation
    {
        
        public RequiredValidation(RequiredAttribute reqAttribute)
        {
           
        }
        public bool Validate(object instance, PropertyInfo propertyInfo)
        {
            var value = propertyInfo.GetValue(instance);

            string stringValue = value.ToString();

          
            if (string.IsNullOrEmpty(stringValue)==false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
