namespace SladkarnicaHvarchilo.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Data.Contracts;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.Cakes;

    public class CakesController : Controller
    {
        private readonly ICakesService cakesService;

        public CakesController(ICakesService cakesService)
            => this.cakesService = cakesService;

        [HttpGet]
        public async Task<IActionResult> AllCakes(string selectedOrderCriteria, string userMessage = null)
        {
            AllCakesViewModel model = new AllCakesViewModel()
            {
                Cakes = await this.cakesService
                                  .GetAllCakesInSale(selectedOrderCriteria)
                                  .To<CakesShortInfoViewModel>()
                                  .ToArrayAsync(),
            };

            this.ViewBag.UserMessage = userMessage;

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CakeDetails(string id)
        {
            Cake cake = await this.cakesService.GetCakeByIdAsync(id);

            if (cake == null)
            {
                return this.View(nameof(this.AllCakes), new { GlobalConstants.UserMessage.CakeDoesNotExist });
            }

            CakeDetailsViewModel model = AutoMapperConfig.MapperInstance.Map<CakeDetailsViewModel>(cake);

            return this.View(model);
        }
    }
}
