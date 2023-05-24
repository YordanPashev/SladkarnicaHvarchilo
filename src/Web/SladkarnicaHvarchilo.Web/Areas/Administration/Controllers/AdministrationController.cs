namespace SladkarnicaHvarchilo.Web.Areas.Administration.Controllers
{
    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
