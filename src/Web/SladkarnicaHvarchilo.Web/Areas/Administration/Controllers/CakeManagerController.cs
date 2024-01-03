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
    using SladkarnicaHvarchilo.Web.ViewModels.CakePiecesInfo;
    using SladkarnicaHvarchilo.Web.ViewModels.Cakes;

    public class CakeManagerController : AdministrationController
    {
        private readonly ICakesService cakesService;
        private readonly ICakePiecesInfoService cakePiecesInfoService;

        private ImageManager imageManager = new ImageManager();

        public CakeManagerController(ICakesService cakesService, ICakePiecesInfoService cakePiecesInfoService)
        {
            this.cakesService = cakesService;
            this.cakePiecesInfoService = cakePiecesInfoService;
        }

        [HttpGet]
        public IActionResult AddNewCake()
        {
            CreateCakeViewModel model = new CreateCakeViewModel();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCake(CreateCakeViewModel userIputModel)
        {
            for (int infoIndex = 0; infoIndex < userIputModel.PriceInfo.Count; infoIndex++)
            {
                userIputModel.PriceInfo[infoIndex].PastryId = userIputModel.Id;
            }

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

            return this.RedirectToAction("Details", "Cakes", new { userMessage = GlobalConstants.UserMessage.SuccessfullyAddedNewCake });
        }

        [HttpGet]
        public async Task<IActionResult> EditCake(string id, string userMessage = null)
        {
            Cake cakeForEdit = await this.cakesService.GetCakeByIdAsync(id);

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
            Cake cakeBeforeEdit = await this.cakesService.GetCakeByIdForEditAsync(userIputModel.Id);

            if (cakeBeforeEdit == null || !this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.AddNewCake), new { userMessage = GlobalConstants.UserMessage.InvalidInputData });
            }

            Cake userIputCakeData = AutoMapperConfig.MapperInstance.Map<Cake>(userIputModel);

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
            Cake cake = await this.cakesService.GetCakeByIdForEditAsync(id);

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

            Cake cake = AutoMapperConfig.MapperInstance.Map<Cake>(model);
            cake.ImageFileDirectoryPath = model.ImageFile.FileName;
            List<PriceInfo> cakePiecesInfo = AutoMapperConfig.MapperInstance.Map<List<PriceInfo>>(model.PriceInfo);

            await this.cakePiecesInfoService.AddCakePiecesInfo(cakePiecesInfo);
            await this.cakesService.AddNewCake(cake);
        }

        private async Task UpdateCake(Cake cakeBeforeEdit, Cake userIputCakeData, IFormFile iamgeFile)
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
