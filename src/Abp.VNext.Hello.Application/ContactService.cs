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

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Contact> GetAsync(int id) => ContactRepository.GetAsync(id, true);

        public async Task<ContactDto> GetByNameAsync(string name)
        {
            Contact item = await ContactRepository.GetByNameAsync(name);
            return ObjectMapper.Map<Contact, ContactDto>(item);
        }

        public Task<long> GetCountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Contact>> GetPagedListAsync(int skip, int count, string sorting)
        {
            throw new System.NotImplementedException();
        }

        public Task<Contact> InsertAsync(Contact item) => ContactRepository.InsertAsync(item);

        private IQueryable<ContactDto> Search(string keyword)
        {
            throw new System.NotImplementedException();
        }

        public Task<Contact> UpdateAsync(Contact entity) => ContactRepository.UpdateAsync(entity);


    }
}
