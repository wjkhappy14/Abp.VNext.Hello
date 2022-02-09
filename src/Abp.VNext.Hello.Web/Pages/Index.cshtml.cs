using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Abp.VNext.Hello.Web.Pages;

public class IndexModel : HelloPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
