namespace SladkarnicaHvarchilo.Web.Areas.Administration.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Data.Contracts;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.Helpers;
    using SladkarnicaHvarchilo.Web.ViewModels.Cakes;

    public class CakeManagerController : AdministrationController
    {
        private readonly ICakesService cakesService;
        private ImageManager imageManager = new ImageManager();

        public CakeManagerController(ICakesService cakesService) => this.cakesService = cakesService;

        [HttpGet]
        public IActionResult AddNewCake(string userMessage = null)
        {
            this.ViewBag.UserMessage = userMessage;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCake(CreateCakeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.AddNewCake), new { userMessage = GlobalConstants.UserMessage.InvalidInputData });
            }

            if (await this.cakesService.CheckIfCakeAlreadyExists(model.Name))
            {
                return this.RedirectToAction(nameof(this.AddNewCake), new { userMessage = GlobalConstants.UserMessage.CakeAlreadyExist });
            }

            if (model.ImageFile.Length <= 0)
            {
                return this.RedirectToAction(nameof(this.AddNewCake), new { userMessage = GlobalConstants.UserMessage.InvalidImageFile });
            }

            await this.CreateAndAddNewCakeAsync(model);

            return this.RedirectToAction(nameof(this.AddNewCake), new { userMessage = GlobalConstants.UserMessage.SuccessfullyAddedNewCake + model.Name });
        }

        private async Task CreateAndAddNewCakeAsync(CreateCakeViewModel model)
        {
            string currentDirectoryPath = Directory.GetCurrentDirectory();
            string imagesFolderPath = Path.GetFullPath(Path.Combine(currentDirectoryPath, @"..\..\Data\SladkarnicaHvarchilo.Data.Common\Images\"));
            string imagePath = imagesFolderPath + model.ImageFile.FileName;

            await this.imageManager.SaveImageToFile(imagesFolderPath, imagePath, model.ImageFile);

            Cake cake = AutoMapperConfig.MapperInstance.Map<Cake>(model);
            cake.ImageFileDirectoryPath = imagePath;

            await this.cakesService.AddNewCake(cake);
        }
    }
}
