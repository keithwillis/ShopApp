using System;
using System.Linq;
using SampleShop.Interfaces;
using SampleShop.Model;

namespace SampleShop.Queries
{
    /// <summary>
    /// Object representation of query adding new order entry to db
    /// </summary>
    public class AddOrderQuery
    {
        private readonly IDatabase db;

        public AddOrderQuery(IDatabase db)
        {
            this.db = db;
        }

        public void Execute(Order newOrder)
        {
            var id = db.Orders.Keys.Any() ? db.Orders.Keys.Max() + 1 : 1;

            newOrder.Id = id;
            newOrder.CreateDate = DateTime.Now;

            db.Orders.Add(id, newOrder);
        }
    }
}
