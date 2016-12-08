using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace WebApiFiltersAndHandlers.ActionFilters
{
    public class FirstActionFilter : IActionFilter
    {
        public bool AllowMultiple { get { return false; } }

        // This is a ready-to-go filter that you can modify as you need.
        // It is already registered with WebApi, so just fill it with code.
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            // 1. Stuff you do here will happen before the controller action method
            Debug.WriteLine("Before controller method - First filter");

            HttpResponseMessage response = null;

            var header = actionContext.Request.Headers.FirstOrDefault(h => h.Key == "X-Version");
            if (header.Key != null)
            {
                var version = header.Value.FirstOrDefault();
                if (version == "42")
                {
                    // 2. Now we send the request to the next link in the chain of filters
                    // (or to the controller if we are at the end)
                    response = await continuation();
                }
            }

            if (response == null)
            {
                var result = new StatusCodeResult((HttpStatusCode)418, actionContext.Request);
                response = await result.ExecuteAsync(cancellationToken);
            }

            // 3. Stuff you do here will happen after the controller action method
            Debug.WriteLine("After controller method - First filter");

            // 4. Return response thereby continuing back up the chain of filters
            return response;
        }
    }
}