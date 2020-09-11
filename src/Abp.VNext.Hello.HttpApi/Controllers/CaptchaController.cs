using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VNext.Hello.Controllers
{
    [Route("api/captcha")]
    public class CaptchaController : AbpController
    {
        private readonly ICaptchaService _captchaService;
        public CaptchaController(ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        /// <summary>
        /// 生成图形验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("code")]
        public async Task<IActionResult> Code()
        {
            string code = $"{Thread.CurrentThread.ManagedThreadId} {DateTime.Now.Millisecond:D4}";
            CaptchaItem item = new CaptchaItem() { Code = code, SessionId = Guid.NewGuid() };
            byte[] buf = await _captchaService.CreateImageAsync(item);
            Response.Headers.Add("Session", $"{item.SessionId}");
            return File(buf, "image/png");
        }

        /// <summary>
        /// 检测验证码是否正确
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("verify/{key}/{code}")]
        public async Task<IActionResult> VerifyAsync(string key, string code)
        {
            CaptchaItem item = await _captchaService.GetItemAsync(key);
            return Json(new
            {
                Code = item == null ? false : item.Code == code
            });
        }
    }

}
