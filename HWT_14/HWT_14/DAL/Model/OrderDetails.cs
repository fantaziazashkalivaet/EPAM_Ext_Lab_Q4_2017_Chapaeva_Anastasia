namespace HWT_14.DAL.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OrderDetails
    {
        private const double minUnitPrice = 1;
        private const double maxUnitPrice = 100000; 
        private const int minQuantity = 1;
        private const int maxQuantity = 10000;

        [Required]
        public int? OrderID
        {
            get;
            set;
        }

        [Required]
        public int? ProductID
        {
            get;
            set;
        }

        [StringLength(40)]
        public string ProductName
        {
            get;
            set;
        }

        [Required]
        [Range(minUnitPrice, maxUnitPrice)]
        public decimal? UnitPrice
        {
            get;
            set;
        }

        [Required]
        [Range(minQuantity, maxQuantity)]
        public short? Quantity
        {
            get;
            set;
        }

        public float? Discount
        {
            get;
            set;
        }
    }
}