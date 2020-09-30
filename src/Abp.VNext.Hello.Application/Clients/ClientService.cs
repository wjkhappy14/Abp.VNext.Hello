using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Stores;
using JetBrains.Annotations;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.Clients;

namespace Abp.VNext.Hello.Clients
{
    public class ClientService : ApplicationService, IClientService
    {
        IClientRepository ClientRepository { get; }

        public ClientService(IClientRepository clientRepository, IClientStore clientStore)
        {
            ClientRepository = clientRepository;
        }
        public async Task<ClientDto> FindByCliendIdAsync(
             [NotNull] string clientId
         )
        {
            Client item = await ClientRepository.FindByCliendIdAsync(clientId);
            return ObjectMapper.Map<Client, ClientDto>(item);
        }

        public Task<bool> CheckClientIdExistAsync(
            string clientId,
            Guid? expectedId = null
        ) => ClientRepository.CheckClientIdExistAsync(clientId, expectedId);

        public async Task<List<ClientDto>> GetListAsync(string sorting, int skipCount, int maxResultCount, string filter)
        {
            List<Client> items = await ClientRepository.GetListAsync(sorting, skipCount, maxResultCount, filter, true);
            return ObjectMapper.Map<List<Client>, List<ClientDto>>(items);
        }

        public Task<List<string>> GetAllDistinctAllowedCorsOriginsAsync() => ClientRepository.GetAllDistinctAllowedCorsOriginsAsync();
    }
}