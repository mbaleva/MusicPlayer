namespace MusicPlayer.Web.Server
{
    using MusicPlayer.Web.Shared;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiController : Controller
    {
        protected ApiResponse<TData> Error<TData>(string item, string message)
        {
            return new ApiResponse<TData>(new ApiError(item, message));
        }

        protected ApiResponse<TData> ModelStateErrors<TData>()
        {
            if (this.ModelState == null || this.ModelState.Count == 0)
            {
                return new ApiResponse<TData>(new ApiError("Model", "Empty or null model."));
            }

            var errors = new List<ApiError>();
            foreach (var item in this.ModelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    errors.Add(new ApiError(item.Key, error.ErrorMessage));
                }
            }

            return new ApiResponse<TData>(errors);
        }
    }
}
