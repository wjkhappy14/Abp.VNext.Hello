using Volo.Abp.Identity;

namespace Abp.VNext.Hello.Users
{

    public class AppUser : IdentityUser
    {
        public virtual string OrgName { get; private set; }

        public virtual string Avatar { get; private set; }

        private AppUser()
        {

        }
    }
}
