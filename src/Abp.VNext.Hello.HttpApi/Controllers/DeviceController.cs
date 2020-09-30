using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.IdentityServer.Devices;

namespace Abp.VNext.Hello.Controllers
{

    [RemoteService(Name = "device")]
    [Area("device")]
    [Route("/device")]
    public class DeviceController : AbpController
    {

        IDeviceFlowCodesRepository DeviceFlowCodesRepository { get; }

        public DeviceController(IDeviceFlowCodesRepository deviceFlowCodesRepository)
        {
            DeviceFlowCodesRepository = deviceFlowCodesRepository;
        }


        [HttpGet]
        public async Task<IActionResult> VerificationAsync(string userCode)
        {
            DeviceFlowCodes deviceFlowCode = await DeviceFlowCodesRepository.FindByUserCodeAsync(userCode);
            return Json(deviceFlowCode);
        }
    }
}
