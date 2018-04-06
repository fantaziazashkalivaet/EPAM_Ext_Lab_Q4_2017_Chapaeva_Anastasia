namespace HWT_14.DAL.Model
{
    using System;

    public class SummaryOfOrder
    {
        public int? OrderID
        {
            get;
            set;
        }

        public string CustomerID
        {
            get;
            set;
        }

        public OrderStatus Status
        {
            get;
            set;
        }

        public DateTime? Date
        {
            get;
            set;
        }

        public decimal Amount
        {
            get;
            set;
        }
    }
}