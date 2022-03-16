using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;

namespace CoreWebAPI.Business
{
    public class CustomBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// Creates a <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" /> based on <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext" />.</param>
        /// <returns>
        /// An <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" />.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (context.Metadata.ModelType == typeof(int[]) || context.Metadata.ModelType == typeof(List<int>))
                return new BinderTypeModelBinder(typeof(CommaDelimitedArrayModelBinder));

            return null;
        }
    }
}
