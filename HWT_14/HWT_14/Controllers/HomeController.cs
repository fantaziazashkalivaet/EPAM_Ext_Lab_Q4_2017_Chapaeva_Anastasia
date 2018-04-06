namespace HWT_14.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using DAL;
    using DAL.Model;
    using Models;   

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var orderManager = new OrderManagment();

            var orders =
                Mapper.Map<List<SummaryOfOrder>, List<SummaryOfOrderViewModel>>(orderManager.GetSummaryOfOrders());

            return View(orders);
        }

        public ActionResult GetDetails(int id)
        {
            var orderManager = new OrderManagment();

            var order = 
                Mapper.Map<FullOrderInformation, OrderDetailsViewModel>(orderManager.GetOrderInformation(id));

            if (order != null)
            {
                return View(order);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateOrder()
        {
            var orderManager = new OrderManagment();
            var infoForCreateOrder = new CreateOrderViewModel(); // какой-то он случайно получился не вьюмодельный, но пока так :с
            infoForCreateOrder.CustomerID = orderManager.GetCustomersID();
            infoForCreateOrder.EmployeeID = orderManager.GetEmployeesID();

            ViewBag.info = infoForCreateOrder;

            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(Order newOrder)
        {
            var orderManager = new OrderManagment();
            newOrder.OrderDate = DateTime.Now;
            var orderId = orderManager.CreateNewOrder(newOrder);

            return RedirectToAction("SetDetailsNewOrder", new { orderID = orderId });
        }

        [HttpGet]
        public ActionResult SetDetailsNewOrder(int? orderID)
        {            
            var orderManager = new OrderManagment();

            ViewBag.ID = orderID; // когда-нибудь избавлюсь от вьюбага, честно-честно
            ViewBag.ProductID = orderManager.GetProductsID();

            return View();
        }

        [HttpPost]
        public ActionResult SetDetailsNewOrder(OrderDetails details, int? orderID)
        {
            var orderManager = new OrderManagment();
            orderManager.AddOrderDetails(details);
            
            return RedirectToAction("SetDetailsNewOrder", new { orderID = details.OrderID });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Пусть будет.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "И эта тоже пусть.";

            return View();
        }
    }
}