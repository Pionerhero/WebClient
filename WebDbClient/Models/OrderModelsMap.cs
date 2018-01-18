using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDbClient.Models
{
    public class ProductMap : ClassMap<Product>
    {
        ProductMap()
        {
            Id(x => x.ProductCode).
                Column("ID");
            Map(x => x.Name);
        }
    }

    public class OrderItemMap : ClassMap<OrderItem>
    {
        public OrderItemMap()
        {
            Id();
            //Id(x => x.OrderItemId);
            Map(x => x.ItemCount).Column("Quantity");
            Map(x => x.ItemPrice).Column("UnitPrice");
            References(x => x.Product).Column("ProductID");
            Table("OrderDetail");
        }
    }

    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(x => x.OrderId).Column("ID");
            Map(x => x.OrderDate);
            HasMany(x => x.OrderItems).KeyColumn("OrderID");
        }
    }
}