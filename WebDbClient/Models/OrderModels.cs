using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebDbClient.Models
{
    public class Order
    {
        public virtual int OrderId { get; protected set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual IList<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public virtual decimal ItemPrice { get; set; }
        public virtual int ItemCount { get; set; }
        public virtual Product Product { get; set; }
    }

    public class Product
    {
        public virtual int ProductCode { get; protected set; }
        public virtual string Name { get; set; }
    }
}