using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebDbClient.Models;

namespace WebDbClient.Controllers
{
    public class WebController : ApiController
    {
        private OrderRepository repo = OrderRepository.Instance;

        public IEnumerable<ReportItem> GetAllOrders()
        {
            return repo.GetAllOrders().SelectMany(x => OrderToReportItems(x));
        }

        public IEnumerable<ReportItem> GetOrdersFromTo(DateTime dateFrom, DateTime dateTo)
        {
            return repo.GetOrdersFromInterval(dateFrom, dateTo).SelectMany(x => OrderToReportItems(x));
        }

        public IEnumerable<ReportItem> GetOrder(int id)
        {
            return OrderToReportItems(repo.GetOrder(id));
        }

        //public Order PostOrder(Order order)
        //{
        //    return repo.Add(order);
        //}

        //public bool PutOrder(Order order)
        //{
        //    return repo.Update(order);
        //}

        //public void DeleteOrder(int id)
        //{
        //    repo.Remove(id);
        //}

        [NonAction]
        protected IEnumerable<ReportItem> OrderToReportItems(Order order)
        {
            List<ReportItem> reportItems = new List<ReportItem>();
            foreach (var orderItem in order.OrderItems)
            {
                reportItems.Add(new ReportItem()
                {
                    ItemCount = orderItem.ItemCount,
                    ItemPrice = orderItem.ItemPrice,
                    Name = orderItem.Product.Name,
                    OrderDate = order.OrderDate,
                    OrderId = order.OrderId,
                    ProductCode = orderItem.Product.ProductCode
                });
            }
            return reportItems;
        }

        public class ReportItem
        {
            public int OrderId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal ItemPrice { get; set; }
            public int ItemCount { get; set; }
            public int ProductCode { get; set; }
            public string Name { get; set; }
        }
    }
}
