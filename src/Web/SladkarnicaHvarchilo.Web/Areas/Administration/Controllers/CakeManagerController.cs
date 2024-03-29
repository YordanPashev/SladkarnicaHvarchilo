﻿namespace SladkarnicaHvarchilo.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
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

            if (await this.cakesService.CheckIfDessertAlreadyExists(userIputModel.Name, DessertType.Cake))
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
            Dessert cakeForEdit = await this.cakesService.GetDessertByIdAsync(id, DessertType.Cake);

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
            userIputModel.PriceInfo = userIputModel.PriceInfo.Where(pi => pi.Price != null && pi.Pieces != null).ToList();
            Dessert originalCake = await this.cakesService.GetDessertByIdForEditAsync(userIputModel.Id, DessertType.Cake);

            if (originalCake == null || !this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.EditCake), new { userMessage = GlobalConstants.UserMessage.InvalidInputData });
            }

            Dessert userIputCakeData = AutoMapperConfig.MapperInstance.Map<Dessert>(userIputModel);
            bool isCakeImageUpdated = await this.UpdateDessertImage(originalCake.ImageFileDirectoryPath, userIputCakeData, userIputModel.ImageFile);

            if (!this.CheckIfCakeHasBeenEdited(originalCake, userIputCakeData, isCakeImageUpdated))
            {
                return this.RedirectToAction(nameof(this.EditCake), new { userMessage = GlobalConstants.UserMessage.NoChangesHaveBeenMade });
            }

            await this.cakesService.UpdateDessertDataAsync(originalCake, userIputCakeData);

            return this.RedirectToAction("CakeDetails", "Cakes", new { area = string.Empty, id = originalCake.Id, userMessage = GlobalConstants.UserMessage.SuccessfullyEditedCake });
        }

        [HttpGet]
        public async Task<IActionResult> DeletedCake(string id)
        {
            Dessert cake = await this.cakesService.GetDessertByIdForEditAsync(id, DessertType.Cake);

            if (cake == null)
            {
                return this.RedirectToAction("AllCakes", "Cakes", new { userMessage = GlobalConstants.UserMessage.CakeDoesNotExist });
            }

            await this.cakesService.DeleteDessert(cake);

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

            await this.cakesService.AddNewDessert(cake);
        }

        private bool CheckIfCakeHasBeenEdited(Dessert originalCake, Dessert userIputCakeData, bool isCakeImageUpdated)
        {
            bool isPriceInfoChanged = this.CheckPriceInfo(originalCake.PriceInfo.ToArray(), userIputCakeData.PriceInfo.ToArray());

            if (originalCake.Name == userIputCakeData.Name && originalCake.Description == userIputCakeData.Description &&
                originalCake.Ingredients == userIputCakeData.Ingredients && originalCake.Allergens == userIputCakeData.Allergens &&
                !isCakeImageUpdated && originalCake.NutritionInfo.Fat == userIputCakeData.NutritionInfo.Fat &&
                originalCake.NutritionInfo.Carbs == userIputCakeData.NutritionInfo.Carbs &&
                originalCake.NutritionInfo.Sugar == userIputCakeData.NutritionInfo.Sugar &&
                originalCake.NutritionInfo.Protein == userIputCakeData.NutritionInfo.Protein &&
                originalCake.NutritionInfo.Salt == userIputCakeData.NutritionInfo.Salt && !isPriceInfoChanged)
            {
                return false;
            }

            return true;
        }

        private bool CheckPriceInfo(PriceInfo[] originalPriceInfo, PriceInfo[] userInputPriceInfo)
        {
            if (originalPriceInfo.Length == userInputPriceInfo.Length)
            {
                for (int infoIndex = 0; infoIndex < originalPriceInfo.Length; infoIndex++)
                {
                    if (originalPriceInfo[infoIndex].Price != userInputPriceInfo[infoIndex].Price ||
                        originalPriceInfo[infoIndex].Pieces != userInputPriceInfo[infoIndex].Pieces)
                    {
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

        private async Task<bool> UpdateDessertImage(string originalFileImageName, Dessert userIputCakeData, IFormFile iamgeFile)
        {
            if (iamgeFile != null)
            {
                await this.imageManager.SaveImageToFileAsync(iamgeFile);
                userIputCakeData.ImageFileDirectoryPath = iamgeFile.FileName;

                return true;
            }

            userIputCakeData.ImageFileDirectoryPath = originalFileImageName;

            return false;
        }
    }
}
