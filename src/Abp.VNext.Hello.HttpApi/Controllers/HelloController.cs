using Abp.VNext.Hello.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VNext.Hello.Controllers
{

    public class HelloController : AbpController
    {
        public HelloController(IHttpContextAccessor httpContextAccessor)
        {
            LocalizationResource = typeof(HelloResource);
        }

        /// <summary>
        /// 限制请求正文
        /// </summary>
        /// <returns></returns>
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