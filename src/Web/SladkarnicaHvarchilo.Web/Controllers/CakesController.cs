namespace SladkarnicaHvarchilo.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Services.Data.Contracts;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.Cakes;

    using static SladkarnicaHvarchilo.Common.GlobalConstants;

    public class CakesController : Controller
    {
        private readonly IDessertService cakesService;

        public CakesController(IDessertService cakesService)
            => this.cakesService = cakesService;

        [HttpGet]
        public async Task<IActionResult> AllCakes(string selectedOrderCriteria = null, string searchQuery = null, string userMessage = null)
        {
            AllCakesViewModel model = new AllCakesViewModel(selectedOrderCriteria);

            if (string.IsNullOrEmpty(selectedOrderCriteria) && string.IsNullOrEmpty(searchQuery))
            {
                model.Cakes = await this.cakesService
                                      .GetAllDesserInSaleByType(DessertType.Cake)
                                      .To<CakesShortInfoViewModel>()
                                      .ToArrayAsync();
            }
            else
            {
                model.Cakes = await this.GetFilteredCakesAsync(selectedOrderCriteria, searchQuery);
                model.SearchQuery = searchQuery;
                model.SelectedOrderCriteria = selectedOrderCriteria;
            }

            this.ViewBag.UserMessage = userMessage;

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CakeDetails(string id, string userMessage = null)
        {
            Dessert cake = await this.cakesService.GetDessertByIdAsync(id, DessertType.Cake);

            if (cake == null)
            {
                return this.View(nameof(this.AllCakes), new { UserMessage.CakeDoesNotExist });
            }

            CakeFullDataViewModel model = AutoMapperConfig.MapperInstance.Map<CakeFullDataViewModel>(cake);

            return this.View(model);
        }

        private async Task<CakesShortInfoViewModel[]> GetFilteredCakesAsync(string selectedOrderCriteria, string searchQuery)
        {
            IQueryable<Dessert> result = Enumerable.Empty<Dessert>().AsQueryable();

            if (!string.IsNullOrEmpty(selectedOrderCriteria) && !string.IsNullOrEmpty(searchQuery))
            {
                result = this.cakesService.GetDessertsAccoringToFilters(selectedOrderCriteria, searchQuery, DessertType.Cake);
            }
            else if (!string.IsNullOrEmpty(searchQuery))
            {
                result = this.cakesService.GetDessertsContainingTheQuery(searchQuery, DessertType.Cake);
            }
            else
            {
                result = this.cakesService.GetDessertByOrderCriteria(selectedOrderCriteria, DessertType.Cake);
            }

            return await result.To<CakesShortInfoViewModel>().ToArrayAsync();
        }
    }
}
