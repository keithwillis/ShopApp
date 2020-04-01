using System;
using SampleShop.Interfaces;

namespace SampleShop.Queries
{
    /// <summary>
    /// Object representation of query deleting order entry from db
    /// </summary>
    public class DeleteOrderQuery
    {
        private readonly IDatabase db;

        public DeleteOrderQuery(IDatabase db)
        {
            this.db = db;
        }
        public void Execute(int id)
        {
            if (db.Orders.ContainsKey(id))
            {
                db.Orders.Remove(id);
            }
        }

    }
}
