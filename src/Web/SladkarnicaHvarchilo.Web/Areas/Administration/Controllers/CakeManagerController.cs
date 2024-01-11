namespace SladkarnicaHvarchilo.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Data.Contracts;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.Helpers;
    using SladkarnicaHvarchilo.Web.ViewModels.Cakes;

    public class CakeManagerController : AdministrationController
    {
        private readonly IDessertService cakesService;
        private ImageManager imageManager = new ImageManager();

        public CakeManagerController(IDessertService cakesService)
            => this.cakesService = cakesService;

        [HttpGet]
        public IActionResult AddNewCake(string userMessage = null)
        {
            this.ViewBag.UserMessage = userMessage;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCake(CreateCakeViewModel userIputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.AddNewCake), new { userMessage = GlobalConstants.UserMessage.InvalidInputData });
            }

            if (await this.cakesService.CheckIfCakeAlreadyExists(userIputModel.Name))
            {
                return this.RedirectToAction(nameof(this.AddNewCake), new { userMessage = GlobalConstants.UserMessage.CakeAlreadyExist });
            }

            if (userIputModel.ImageFile.Length <= 0)
            {
                return this.RedirectToAction(nameof(this.AddNewCake), new { userMessage = GlobalConstants.UserMessage.InvalidImageFile });
            }

            await this.CreateAndAddNewCakeToDbAsync(userIputModel);

            return this.RedirectToAction("CakeDetails", "Cakes", new { area = string.Empty, id = userIputModel.Id, userMessage = GlobalConstants.UserMessage.SuccessfullyAddedNewCake });
        }

        [HttpGet]
        public async Task<IActionResult> EditCake(string id, string userMessage = null)
        {
            Dessert cakeForEdit = await this.cakesService.GetCakeByIdAsync(id);

            if (cakeForEdit == null)
            {
                return this.RedirectToAction("AllCakes", "Cakes", new { userMessage = GlobalConstants.UserMessage.CakeDoesNotExist });
            }

            EditCakeViewModel model = AutoMapperConfig.MapperInstance.Map<EditCakeViewModel>(cakeForEdit);
            this.ViewBag.UserMessage = userMessage;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditCake(EditCakeViewModel userIputModel)
        {
            Dessert cakeBeforeEdit = await this.cakesService.GetCakeByIdForEditAsync(userIputModel.Id);

            if (cakeBeforeEdit == null || !this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.AddNewCake), new { userMessage = GlobalConstants.UserMessage.InvalidInputData });
            }

            Dessert userIputCakeData = AutoMapperConfig.MapperInstance.Map<Dessert>(userIputModel);

            if (!this.cakesService.CheckIfCakeHasBeenEdited(cakeBeforeEdit, userIputCakeData))
            {
                return this.RedirectToAction(nameof(this.AddNewCake), new { userMessage = GlobalConstants.UserMessage.CakeAlreadyExist });
            }

            await this.UpdateCake(cakeBeforeEdit, userIputCakeData, userIputModel.ImageFile);

            return this.RedirectToAction("Details", "Cakes", new { userMessage = GlobalConstants.UserMessage.SuccessfullyEditedCake });
        }

        [HttpGet]
        public async Task<IActionResult> DeletedCake(string id)
        {
            Dessert cake = await this.cakesService.GetCakeByIdForEditAsync(id);

            if (cake == null)
            {
                return this.RedirectToAction("AllCakes", "Cakes", new { userMessage = GlobalConstants.UserMessage.CakeDoesNotExist });
            }

            await this.cakesService.DeteleCake(cake);

            return this.RedirectToAction("AllCakes", "Cakes", new { userMessage = GlobalConstants.UserMessage.SuccessfullyDeletedCake + cake.Name });
        }

        private async Task CreateAndAddNewCakeToDbAsync(CreateCakeViewModel model)
        {
            await this.imageManager.SaveImageToFileAsync(model.ImageFile);

            Dessert cake = AutoMapperConfig.MapperInstance.Map<Dessert>(model);
            cake.ImageFileDirectoryPath = model.ImageFile.FileName;
            cake.PriceInfo = cake.PriceInfo
                                .Where(c => c.Price > 0.00m & c.Pieces > 0)
                                .ToList();

            await this.cakesService.AddNewCake(cake);
        }

        private async Task UpdateCake(Dessert cakeBeforeEdit, Dessert userIputCakeData, IFormFile iamgeFile)
        {
            if (cakeBeforeEdit.ImageFileDirectoryPath != userIputCakeData.ImageFileDirectoryPath)
            {
                await this.imageManager.SaveImageToFileAsync(iamgeFile);
                userIputCakeData.ImageFileDirectoryPath = iamgeFile.FileName;
            }

            await this.cakesService.UpdateCakeDataAsync(cakeBeforeEdit, userIputCakeData);
        }
    }
}
