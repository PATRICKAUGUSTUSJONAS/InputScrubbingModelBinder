using InputScrubbingModelBinder.Web.Interfaces;
using Microsoft.AspNet.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace InputScrubbingModelBinder.Web.ModelBinders
{
    public class ScrubbingModelBinder : IModelBinder
    {
        public Task<ModelBindingResult> BindModelAsync(ModelBindingContext bindingContext)
        {
            // Ignore complex types
            if (bindingContext.ModelMetadata.IsComplexType || bindingContext.IsTopLevelObject)
                return ModelBindingResult.NoResultAsync;

            // If there's no value sent in
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None) return ModelBindingResult.NoResultAsync;

            // Look for scrubber attributes
            var propName = bindingContext.ModelMetadata.PropertyName;
            var propInfo = bindingContext.ModelMetadata.ContainerType.GetProperty(propName);
            var modelAsString = valueProviderResult.ToString();

            // Only one scrubber attribute can be applied to each property
            var attribute = propInfo.GetCustomAttributes(typeof(IScrubberAttribute), false).FirstOrDefault();
            if (attribute != null)
            {
                var success = true;
                var result = (attribute as IScrubberAttribute).Scrub(modelAsString, out success);
                if (success)
                {
                    bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                    return ModelBindingResult.SuccessAsync(bindingContext.ModelName, result);
                }
            }

            return ModelBindingResult.NoResultAsync;
        }
    }
}
