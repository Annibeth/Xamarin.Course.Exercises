using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;

namespace WebApiFiltersAndHandlers.DelegatingHandlers
{
    public class FirstDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage>
            SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            if (request.Headers.Any(h => h.Key == "X-Version"))
            {
                var versionHeader = request.Headers.First(h => h.Key == "X-Version");
                var version = versionHeader.Value.FirstOrDefault();

                if (version != null && version == "42")
                {
                    response = await base.SendAsync(request, cancellationToken);
                }

            }


            if (response == null)
            {
                var result = new StatusCodeResult((HttpStatusCode)418, request);
                response = await result.ExecuteAsync(cancellationToken);
            }

            return response;
        }
    }
}