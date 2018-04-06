namespace HWT_14.Models
{  
    using System.Collections.Generic;
    using DAL.Model;

    public class OrderDetailsViewModel
    {
        public Order Order
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