using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public class ContactService: ApplicationService, IContactService
    {
        public ContactService(IContactRepository  contactRepository)
        {

        }
    }
}
