namespace HWT_14.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class CreateOrderViewModel
    {
        public IEnumerable<SelectListItem> CustomerID
        {
            get;
            set;
        }

        public IEnumerable<SelectListItem> EmployeeID
        {
            get;
            set;
        }
    }
}