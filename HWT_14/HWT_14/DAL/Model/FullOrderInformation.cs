namespace HWT_14.DAL.Model
{
    using System.Collections.Generic;

    public class FullOrderInformation
    {
        public FullOrderInformation()
        {
            this.Order = new Order();
            this.Products = new List<Product>();
            this.OrderDetails = new List<OrderDetails>();
        }

        public Order Order
        {
            get;
            set;
        }

        public List<Product> Products
        {
            get;
            set;
        }

        public List<OrderDetails> OrderDetails
        {
            get;
            set;
        }
    }
}