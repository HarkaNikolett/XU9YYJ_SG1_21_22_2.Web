using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Logic.Interfaces;
using XU9YYJ_HFT_2021221.Logic.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace XU9YYJ_HFT_2021221.Logic.Validation
{
    public static class ValidationFactory
    {
        public static IValidation GetValidation(Attribute attribute)
        {

            if (attribute is RequiredAttribute requiredAttribute)
            {

                return new RequiredValidation(requiredAttribute);

            }
            else if (attribute is MaxLengthAttribute maxlengthAttribute)
            {

                return new MaxLengthValidation(maxlengthAttribute);
            }
            else if (attribute.GetType() == typeof(KeyAttribute))
            {
                
                return new KeyValidation();
            }
            else /*if (attribute.GetType() == typeof(DatabaseGeneratedAttribute))*/
            {
                return new DataBaseGeneratedValidation();
            }
            //else
            //{
            //    throw new InvalidOperationException($"Not supported attribute: {attribute.GetType()}");
            //}
        }
    }
}
