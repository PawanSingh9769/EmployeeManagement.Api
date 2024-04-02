using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.ModelBinders
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            /*We are creating a model binder for the IEnumerable type. Therefore, we 
              have to check if our parameter is the same type. */
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            /*we extract the value (a comma-separated string of GUIDs) with the 
             ValueProvider.GetValue() expression*/
            var providedValue = bindingContext.ValueProvider
           .GetValue(bindingContext.ModelName)
           .ToString();
            if (string.IsNullOrEmpty(providedValue))
            {
                //if true return null
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }


            /*In our case, it is GUID. With the  converter variable, we create a converter to a GUID type. As you can 
            see, we didn’t just force the GUID type in this model binder;*/

            var genericType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];

            var converter = TypeDescriptor.GetConverter(genericType);

            var objectArray = providedValue.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim()))
                .ToArray();

            var guidArray = Array.CreateInstance(genericType, objectArray.Length);
            objectArray.CopyTo(guidArray, 0);
            bindingContext.Model = guidArray;

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;

        }
    }
}
