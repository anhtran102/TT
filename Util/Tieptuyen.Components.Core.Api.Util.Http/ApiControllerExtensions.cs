using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace Tieptuyen.Components.Core.Api.Util.Http
{
    /// <summary>
    /// Provides extension methods for sub-classes of the <see cref="ApiController"/> class.
    /// </summary>
    public static class ApiControllerExtensions
    {
        /// <summary>
        /// Gets the current user from the incoming request.
        /// </summary>
        /// <param name="apiController">The API controller.</param>
        /// <returns>The current user's username as a <see cref="String"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="apiController"/> is null.</exception>
        public static string GetCurrentUser(this ApiController apiController)
        {
            if (apiController != null)
            {
                if (apiController.User.Identity.IsAuthenticated)
                {
                    return apiController.User.Identity.Name;
                }
                else
                {
                    AuthenticationHeaderValue authHeader = apiController.Request.Headers.Authorization;
                    if (authHeader != null && !string.IsNullOrEmpty(authHeader.Parameter))
                    {
                        string[] credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(new char[] { ':' });
                        if (credentials.Length == 2 && !string.IsNullOrEmpty(credentials[0]))
                            return credentials[0].ToLower();
                        else
                            return string.Empty;
                    }
                    else
                        return string.Empty;
                }
            }
            else
                throw new ArgumentNullException("apiController", "The API controller cannot be null.");
        }
    }
}
