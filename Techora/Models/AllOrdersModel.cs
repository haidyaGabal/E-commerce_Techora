namespace Techora.Models
{
    public class AllOrdersModel
    {
        public List<Order> lstOrder { get; set; }
        public List<OrderDetail> lstOrderDetails { get; set; }

        public AllOrdersModel() 
        {
            lstOrder = new List<Order>();
            lstOrderDetails = new List<OrderDetail>();
        }
    }
}
