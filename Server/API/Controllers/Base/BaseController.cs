using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected Guid UserId
        {
            get
            {
                var id = this.User.FindFirst("userId");
                if (id is null)
                {
                    return Guid.Empty;
                }

                return new Guid(id.Value);
            }
        }

        protected string UserName
        {
            get => this.User.FindFirst(ClaimTypes.Name)?.Value ?? "User Unknown"; 
        }
    }
}
