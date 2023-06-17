namespace SladkarnicaHvarchilo.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SladkarnicaHvarchilo.Services.Data.Contracts;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.Cakes;

    public class CakesController : Controller
    {
        private readonly ICakesService cakesService;

        public CakesController(ICakesService cakesService)
            => this.cakesService = cakesService;

        public async Task<IActionResult> AllCakes(string orderCriteria)
        {
            CakesShortInfoViewModel[] model = await this.cakesService
                                                        .GettAllCakesInSale(orderCriteria)
                                                        .To<CakesShortInfoViewModel>()
                                                        .ToArrayAsync();
            return this.View(model);
        }
    }
}
