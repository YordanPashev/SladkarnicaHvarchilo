namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using SladkarnicaHvarchilo.Common;

    public class AllCakesViewModel
    {
        public CakesShortInfoViewModel[] Cakes { get; set; }

        public string[] OderCriteria => GlobalConstants.OrderCriteria.AllOrderCriteria;

        public string SearchQuery { get; set; }

        public string OrderCriteria { get; set; }
    }
}
