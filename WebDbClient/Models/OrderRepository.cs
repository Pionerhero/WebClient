using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;

namespace WebDbClient.Models
{
    public sealed class OrderRepository
    {
        ISessionFactory _sessionFactory;
        ISession _session;

        private static readonly OrderRepository _instance = new OrderRepository();

        private OrderRepository()
        {
            InitializeSession();
        }

        public static OrderRepository Instance
        {
            get
            {
                return _instance;
            }
        }

        void InitializeSession()
        {
            try
            {
                _sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(@"Server=(localdb)\MSSQLLocalDB; Database=Northwind; Integrated Security=SSPI;"))
                    .Mappings(m => m
                    .FluentMappings.AddFromAssemblyOf<OrderRepository>())
                    .BuildSessionFactory();
                _session = _sessionFactory.OpenSession();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Order GetOrder(int orderId)
        {
            try
            {
                return _session.QueryOver<Order>().Where( x => x.OrderId == orderId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Order> GetAllOrders()
        {
            try
            {
                return _session.QueryOver<Order>().List();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Order> GetOrdersFromInterval(DateTime from, DateTime to)
        {
            try
            {
                return _session.QueryOver<Order>().
                    Where(x => (x.OrderDate >= from) && (x.OrderDate <= to)).List();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}