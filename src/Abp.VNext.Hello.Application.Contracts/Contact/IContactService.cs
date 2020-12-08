using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public  interface IContactService : IApplicationService
    {
        Task<ContactDto> GetByNameAsync(string name);
    }
}
