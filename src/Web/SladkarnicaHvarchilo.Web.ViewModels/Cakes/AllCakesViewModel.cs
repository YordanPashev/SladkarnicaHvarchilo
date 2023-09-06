namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System.Collections.Generic;
    using System.Linq;

    using SladkarnicaHvarchilo.Common;

    public class AllCakesViewModel
    {
        public AllCakesViewModel(string selectedOrderCriteria)
            => this.OrderCriteria = this.GetOrderCriterias(selectedOrderCriteria);

        public CakesShortInfoViewModel[] Cakes { get; set; }

        public string SearchQuery { get; set; }

        public string[] OrderCriteria { get; }

        public string SelectedOrderCriteria { get; set; }

        private string[] GetOrderCriterias(string selectedOrderCriteria)
        {
            if (string.IsNullOrEmpty(selectedOrderCriteria))
            {
                return GlobalConstants.OrderCriteria.AllOrderCriteria;
            }

            List<string> orderCiterias = GlobalConstants.OrderCriteria.AllOrderCriteria
                                    .Where(oc => !oc.ToUpper().Contains(selectedOrderCriteria.ToUpper()))
                                    .ToList();

            orderCiterias.Insert(0, selectedOrderCriteria);

            return orderCiterias.ToArray();
        }
    }
}
