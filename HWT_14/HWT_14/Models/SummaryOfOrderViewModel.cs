namespace HWT_14.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DAL.Model;

    public class SummaryOfOrderViewModel
    {
        [Display(Name = "OrderID")]
        public int? OrderID
        {
            get;
            set;
        }

        [Display(Name = "CustomerID")]
        public string CustomerID
        {
            get;
            set;
        }

        [Display(Name = "Status")]
        public OrderStatus Status
        {
            get;
            set;
        }

        [Display(Name = "Date")]
        public DateTime? Date
        {
            get;
            set;
        }

        [Display(Name = "Amount")]
        public double? Amount
        {
            get;
            set;
        }
    }
}