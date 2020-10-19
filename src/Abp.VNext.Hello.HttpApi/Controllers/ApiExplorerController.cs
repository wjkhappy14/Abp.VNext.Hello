using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Pipelines;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VNext.Hello.Controllers
{
    [RemoteService(Name = "ApiExplorer")]
    [RequestSizeLimit(500)]
    public class ApiExplorerController : AbpController
    {
        public ApiExplorerController(IHttpContextAccessor httpContextAccessor)
        {

        }
        [HttpPost]
        [Route("{culture}/[controller]/[action]/{id}/{name}/{detail}")]
        [MiddlewareFilter(typeof(LocalizationPipeline))]
        public IActionResult Set(string id, string name, [FromBody] string detail)
        {
            return Json((id, name, detail));
        }
        [HttpPost]
        //[ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult XUpload([FromServices] IWebHostEnvironment webHostEnv, IFormFileCollection files) => Json(new { webHostEnv.ApplicationName });

        [HttpGet("/license")]
        public string GetLicense()
        {
            return ControllerContext.ActionDescriptor.Properties["license"].ToString();
        }
        [HttpGet("/export/pdf")]
        [Produces("application/pdf", Type = typeof(Stream))]
        public IActionResult PDF() => null;


        [HttpGet]
        public IActionResult Pipe()
        {
            Pipe pipe = new Pipe(PipeOptions.Default);
            return Json(new { pipe });
        }

    }
}
