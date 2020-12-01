using Abp.VNext.Hello.EntityFrameworkCore;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VNext.Hello
{
    public class DapperEztvRepository : DapperRepository<HelloDbContext>, ITransientDependency, IDapperEztvRepository
    {
        public DapperEztvRepository(IDbContextProvider<HelloDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public Task<int> BulkInsertAsync(IList<EztvItem> items)
        {
            List<string> insertItems = new List<string>();
            foreach (EztvItem item in items)
            {
                item.filename = ToBase64String(string.IsNullOrEmpty(item.filename) ? string.Empty : item.filename);
                item.title = ToBase64String(string.IsNullOrEmpty(item.title)?string.Empty: item.title);
                item.torrent_url = ToBase64String(string.IsNullOrEmpty(item.torrent_url) ? string.Empty : item.torrent_url);
                string insert = $@"INSERT INTO Eztv (
                     id,
                     title,
                     torrent_url,
                     date_released_unix,
                     episode,
                     episode_url,
                     size_bytes,
                     create_time,
                     seeds,
                     season,
                     small_screenshot,
                     filename,
                     imdb_id,
                     hash,
                     peers,
                     large_screenshot,
                     magnet_url
                 )
                 VALUES (
                     '{item.Id}',
                     '{item.title}',
                     '{item.torrent_url}',
                     '{item.date_released_unix}',
                     '{item.episode}',
                     '{item.episode_url}',
                     '{item.size_bytes}',
                     '{item.create_time}',
                     '{item.seeds}',
                     '{item.season}',
                     '{item.small_screenshot}',
                     '{item.filename}',
                     '{item.imdb_id}',
                     '{item.hash}',
                     '{item.peers}',
                     '{item.large_screenshot}',
                     '{item.magnet_url}'
                 )";
                insertItems.Add(insert);
            }
            string sql = string.Join(";", insertItems);
            return DbConnection.ExecuteAsync(sql, new { }, transaction: DbTransaction);
        }

        public Task<IEnumerable<EztvItem>> GetPagesAsync(int page = 1, int limit = 100)
        {
            string sql = @"
                            SELECT 
                                  *
                             FROM Eztv
                            ";
            return DbConnection.QueryAsync<EztvItem>(sql, new { Page = page, Limit = limit }, transaction: DbTransaction);
        }

        private string ToBase64String(string keyWord)
        {
            byte[] bytes = Encoding.Default.GetBytes(keyWord);
            string str = Convert.ToBase64String(bytes);
            return str;
        }
    }
}
