using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiFiltersAndHandlers.ActionFilters
{
    public class SecondActionFilter : IActionFilter
    {
        public bool AllowMultiple { get { return false; } }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            // 1. Stuff you do here will happen before the controller action method
            Debug.WriteLine("Before controller method - Second filter");

            // 2. Now we send the request to the next link in the chain of filters
            // (or to the controller if we are at the end)
            var response = await continuation();

            // 3. Stuff you do here will happen after the controller action method
            Debug.WriteLine("After controller method - Second filter");

            // 4. Return response thereby continuing back up the chain of filters
            return response;
        }
    }
}