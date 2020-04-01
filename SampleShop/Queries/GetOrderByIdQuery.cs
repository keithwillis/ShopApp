using SampleShop.Interfaces;
using SampleShop.Model;

namespace SampleShop.Queries
{
    /// <summary>
    /// Object representation of query returning order by its id from db
    /// </summary>
    public class GetOrderByIdQuery
    {
        private readonly IDatabase db;

        public GetOrderByIdQuery(IDatabase db)
        {
            this.db = db;
        }

        public Order Execute(int id)
        {
            return db.Orders.ContainsKey(id) ? db.Orders[id] : null;
        }
    }
}
