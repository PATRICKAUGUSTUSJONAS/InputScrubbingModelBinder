using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
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

            // read value from HttpContext.Session - set in GET call to /Account
            var userId = bindingContext.HttpContext.Session.GetObjectFromJson<Guid>("userId");
            if (userId == null) userId = Guid.Empty;
            bindingContext.Result = ModelBindingResult.Success(userId);

            //bindingContext.Result = ModelBindingResult.Success(Guid.NewGuid());
            
            //bindingContext.Result = ModelBindingResult.Success(user.GetUserIdFromClaims());

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
