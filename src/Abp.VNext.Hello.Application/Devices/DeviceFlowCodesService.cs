using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.Devices;

namespace Abp.VNext.Hello.Devices
{
    public class DeviceFlowCodesService : ApplicationService, IDeviceFlowCodesService
    {
        IDeviceFlowCodesRepository DeviceFlowCodesRepository;
        public DeviceFlowCodesService(IDeviceFlowCodesRepository deviceFlowCodesRepository, IDeviceFlowStore deviceFlowStore)
        {
            DeviceFlowCodesRepository = deviceFlowCodesRepository;
        }

        public async Task<DeviceFlowCodesDto> FindByUserCodeAsync(string userCode)
        {
            DeviceFlowCodes item = await DeviceFlowCodesRepository.FindByUserCodeAsync(userCode);
            return ObjectMapper.Map<DeviceFlowCodes, DeviceFlowCodesDto>(item);
        }

        public async Task<DeviceFlowCodesDto> FindByDeviceCodeAsync(string deviceCode)
        {
            DeviceFlowCodes item = await DeviceFlowCodesRepository.FindByDeviceCodeAsync(deviceCode);
            return ObjectMapper.Map<DeviceFlowCodes, DeviceFlowCodesDto>(item);
        }

        public async Task<List<DeviceFlowCodesDto>> GetListByExpirationAsync(DateTime maxExpirationDate, int maxResultCount = 50)
        {
            List<DeviceFlowCodes> items = await DeviceFlowCodesRepository.GetListByExpirationAsync(maxExpirationDate, maxResultCount);
            return ObjectMapper.Map<List<DeviceFlowCodes>, List<DeviceFlowCodesDto>>(items);
        }
        public async Task<List<DeviceFlowCodesDto>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting)
        {
            List<DeviceFlowCodes> items = await DeviceFlowCodesRepository.GetPagedListAsync(skipCount, maxResultCount, sorting);
            return ObjectMapper.Map<List<DeviceFlowCodes>, List<DeviceFlowCodesDto>>(items);
        }
    }
}
