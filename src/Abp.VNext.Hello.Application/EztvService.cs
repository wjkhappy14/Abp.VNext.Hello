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

        public Task<string> DownloadAsync(int limit=20, int page=1)
        {
            string url = $"https://eztv.io/api/get-torrents?limit={limit}&page={page}";
            return Client.GetStringAsync(url);
        }
    }
}
