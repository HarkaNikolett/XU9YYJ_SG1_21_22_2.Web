using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using XU9YYJ_HFT_2021221.Logic.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XU9YYJ_HFT_2021221.Logic.Interfaces;
using XU9YYJ_HFT_2021221.Logic.Services;




namespace XU9YYJ_HFT_2021221.Logic.Validation
{
    public class Validator
    {
        public bool Validate(object instance)
        {
            var type = instance.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);

                foreach (Attribute attribute in attributes)
                {
                    var validation = ValidationFactory.GetValidation(attribute);

                    if (!validation.Validate(instance, property))
                        return false;
                }
            }

            return true;
        }
    }
}
