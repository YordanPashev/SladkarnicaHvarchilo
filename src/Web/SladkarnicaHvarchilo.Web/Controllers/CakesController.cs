namespace SladkarnicaHvarchilo.Web.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
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
        public async Task<IActionResult> AllCakes(string userMessage = null)
        {
            AllCakesViewModel model = new AllCakesViewModel(string.Empty)
            {
                Cakes = await this.cakesService
                                  .GetAllCakesInSale()
                                  .To<CakesShortInfoViewModel>()
                                  .ToArrayAsync(),
            };

            this.ViewBag.UserMessage = userMessage;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AllCakes(string selectedOrderCriteria, string searchQuery, string userMessage = null)
        {
            AllCakesViewModel model = new AllCakesViewModel(selectedOrderCriteria);

            if (string.IsNullOrEmpty(selectedOrderCriteria) && string.IsNullOrEmpty(searchQuery))
            {
                this.RedirectToAction(nameof(this.AllCakes));
            }

            model.Cakes = await this.GetFilteredCakesAsync(selectedOrderCriteria, searchQuery);
            model.SearchQuery = searchQuery;
            model.SelectedOrderCriteria = selectedOrderCriteria;

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

        private async Task<CakesShortInfoViewModel[]> GetFilteredCakesAsync(string selectedOrderCriteria, string searchQuery)
        {
            IQueryable<Cake> result = Enumerable.Empty<Cake>().AsQueryable();

            if (!string.IsNullOrEmpty(selectedOrderCriteria) && !string.IsNullOrEmpty(searchQuery))
            {
                result = this.cakesService.GetCakesAccoringToFilters(selectedOrderCriteria, searchQuery);
            }
            else if (!string.IsNullOrEmpty(searchQuery))
            {
                result = this.cakesService.GetSearchedCakes(searchQuery);
            }
            else
            {
                result = this.cakesService.GetCakesByOrderCriteria(selectedOrderCriteria);
            }

            return await result.To<CakesShortInfoViewModel>().ToArrayAsync();
        }
    }
}
