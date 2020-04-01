using System;
using System.Linq;
using SampleShop.Interfaces;
using SampleShop.Model;

namespace SampleShop.Queries
{
    /// <summary>
    /// Object representation of query returning all items from db
    /// </summary>
    public class GetAllOrdersQuery
    {
        private readonly IDatabase db;

        public GetAllOrdersQuery(IDatabase db)
        {
            this.db = db;
        }

        public IQueryable<Order> Execute()
        {
            return db.Orders.Values.AsQueryable();
        }
    }
}
