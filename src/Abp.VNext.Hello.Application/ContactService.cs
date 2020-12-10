using System.Collections.Generic;
using System.Linq;
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

        public Task<List<Contact>> GetPagedListAsync(int skip, int count, string sorting = "name") => ContactRepository.GetPagedListAsync(skip, count, sorting,true);

        public Task<Contact> InsertAsync(Contact item) => ContactRepository.InsertAsync(item);

        private IQueryable<ContactDto> Search(string keyword)
        {
            throw new System.NotImplementedException();
        }

        public Task<Contact> UpdateAsync(Contact entity) => ContactRepository.UpdateAsync(entity);
    }
}
