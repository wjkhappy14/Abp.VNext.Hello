using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Abp.VNext.Hello.Controllers
{



    public class KlineController: HelloController
    {

        /// <summary>
        /// //string url = $"http://42.51.45.70:7100/kline/list?contractNo=1906&count=1000&time=1560333861068&type=MINUTE15&commodityNo=AD&X={x}";
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetItemsAsync()
        {
            Claim userIdClaim = CurrentUser.FindClaim("id");
            int userId = int.Parse(userIdClaim.Value);
          
            return new JsonResult(new { });
        }
    }
}
