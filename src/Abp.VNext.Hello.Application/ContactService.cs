using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public class ContactService : ApplicationService, IContactService
    {
        IContactRepository ContactRepository { get; }
        public ContactService(IContactRepository contactRepository)
        {
            ContactRepository = contactRepository;
        }

        public Task DeleteAsync(int id) => ContactRepository.DeleteAsync(id);

        public Task<Contact> GetAsync(int id) => ContactRepository.GetAsync(id, true);

        public async Task<ContactDto> GetByNameAsync(string name) => ObjectMapper.Map<Contact, ContactDto>(await ContactRepository.GetByNameAsync(name));

        public Task<long> GetCountAsync() => ContactRepository.GetCountAsync();

        public Task<List<Contact>> GetPagedListAsync(int skip, int count = 50, string sorting = "name") => ContactRepository.GetPagedListAsync(skip, count, sorting, true);

        public Task<Contact> InsertAsync(Contact item) => ContactRepository.InsertAsync(item);

        public Task<Contact> UpdateAsync(Contact entity) => ContactRepository.UpdateAsync(entity);

        public async Task<List<ContactDto>> SearchAsync(string sorting = "name", int maxResultCount = 50, int skipCount = 0, string filter = null)
        {
            List<Contact> items = await ContactRepository.SearchAsync(sorting, maxResultCount, skipCount, filter);
            return ObjectMapper.Map<List<Contact>, List<ContactDto>>(items);
        }
    }
}
