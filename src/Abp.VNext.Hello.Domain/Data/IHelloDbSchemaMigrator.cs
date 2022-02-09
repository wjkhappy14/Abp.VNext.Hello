using System.Threading.Tasks;

namespace Abp.VNext.Hello.Data;

public interface IHelloDbSchemaMigrator
{
    Task MigrateAsync();
}
