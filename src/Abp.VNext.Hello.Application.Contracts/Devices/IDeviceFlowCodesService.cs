using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello.Devices
{
    public interface IDeviceFlowCodesService: IApplicationService
    {
        Task<DeviceFlowCodesDto> FindByUserCodeAsync(
            string userCode
        );

        Task<DeviceFlowCodesDto> FindByDeviceCodeAsync(
            string deviceCode
        );

        Task<List<DeviceFlowCodesDto>> GetListByExpirationAsync(
            DateTime maxExpirationDate,
            int maxResultCount
        );
    }
}
