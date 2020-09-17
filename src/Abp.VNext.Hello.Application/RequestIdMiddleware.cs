using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.VNext.Hello
{
    public class RequestIdMiddleware : IMiddleware, ITransientDependency
    {
        private readonly RequestDelegate _next;

        public RequestIdMiddleware()
        {
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            StringValues requestId = context.Request.Headers[HeaderNames.RequestId];
            System.Diagnostics.Debug.WriteLine(requestId);
            return next(context);
        }
    }

}
