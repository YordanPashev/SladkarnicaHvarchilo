namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using SladkarnicaHvarchilo.Common;

    public class AllCakesViewModel
    {
        public AllCakesViewModel() => this.OrderCriteria = GlobalConstants.OrderCriteria.AllOrderCriteria;

        public CakesShortInfoViewModel[] Cakes { get; set; }

        public string SearchQuery { get; set; }

        public string[] OrderCriteria { get; }

        public string SelectedOrderCriteria { get; set; }
    }
}
