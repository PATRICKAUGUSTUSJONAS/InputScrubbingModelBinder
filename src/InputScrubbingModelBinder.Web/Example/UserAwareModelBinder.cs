using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InputScrubbingModelBinder.Web.Example
{
    public class UserAwareModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Check the value sent in
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            //var user = bindingContext.HttpContext.User;
            //if (user != null)
            //{
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
            bindingContext.Result = ModelBindingResult.Success(Guid.NewGuid());
            //    bindingContext.Result = ModelBindingResult.Success(user.GetUserIdFromClaims());

            //}
            return Task.CompletedTask;
        }
    }

    public class UserAwareModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (typeof(IUserAware).IsAssignableFrom(context.Metadata.ContainerType) && context.Metadata.ModelType == typeof(Guid))
            {
                return new UserAwareModelBinder();
            }
            else
            {
                return null;
            }
        }
    }
}
