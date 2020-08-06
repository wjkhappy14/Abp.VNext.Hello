using Abp.VNext.Hello.Localization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VNext.Hello.Controllers
{

    public class HelloController : AbpController
    {
        public HelloController(IHttpContextAccessor httpContextAccessor)
        {
            LocalizationResource = typeof(HelloResource);
        }
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
        public IActionResult Hello()
        {
            return Json(new
            {
                DateAndTime.Now
            });
        }
    }
}