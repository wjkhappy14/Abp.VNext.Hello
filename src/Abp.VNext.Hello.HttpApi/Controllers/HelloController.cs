using Abp.VNext.Hello.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VNext.Hello.Controllers
{

    public  class HelloController : AbpController
    {
        public HelloController(IHttpContextAccessor httpContextAccessor)
        {
            LocalizationResource = typeof(HelloResource);
        }
        [Authorize(Roles = "Administrator")]//授权
        [EnableCors("WithCredentials")]
        [HttpOptions]
        public IDictionary Variables()
        {
            IDictionary variables = Environment.GetEnvironmentVariables();
            return variables;
        }
        /// <summary>
        /// 限制请求正文
        /// </summary>
        /// <returns></returns>
        [DisableCors]
        [AcceptVerbs("HEAD", "GET", "POST")]
        [RequestSizeLimit(100000000)]
        [ResponseCache(Duration = 60)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Hello()
        {
            return Json(new
            {
                Msg = "How are you ?",
                DateAndTime.Now
            });
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }
        [RequestFormLimits(ValueCountLimit = 2)]
        [RequestSizeLimit(100)]
        [ValidateAntiForgeryToken]
        [HttpGet]
        public IActionResult Features()
        {
            IFeatureCollection features = HttpContext.Features;
            return Json(new
            {
                Features = features
            });
        }


        [HttpGet]
        public IActionResult Feature(string name)
        {
            IEndpointFeature feature = HttpContext.Features.Get<IEndpointFeature>();
            return Json(new
            {
                Name = name,
                Feature = feature
            });
        }
    }
}