using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public class EztvService : ApplicationService, IEztvService
    {
        IDapperEztvRepository EztvRepository { get; }

        HttpClient Client { get; }

        public EztvService(IDapperEztvRepository eztvRepository, IHttpClientFactory clientFactory)
        {
            EztvRepository = eztvRepository;
            Client = clientFactory.CreateClient("eztv");
        }


        public async Task<int> DownloadAsync(int limit = 2, int page = 1)
        {
            string url = $"https://eztv.re/api/get-torrents?limit={limit}&page={page}";
            string json = await Client.GetStringAsync(url);
            EztvResult result = JsonConvert.DeserializeObject<EztvResult>(json);
            IList<EztvItem> torrents = ObjectMapper.Map<IList<EztvItemDto>, IList<EztvItem>>(result.torrents);
            await EztvRepository.BulkInsertAsync(torrents);
            return torrents.Count;
        }

        public async Task<IEnumerable<EztvItemDto>> GetPagesAsync(int page = 1, int limit = 100)
        {
            IEnumerable<EztvItem> items = await EztvRepository.GetPagesAsync(page, limit);
            IEnumerable<EztvItemDto> result = ObjectMapper.Map<IEnumerable<EztvItem>, IEnumerable<EztvItemDto>>(items);
            return result;
        }
    }
}
