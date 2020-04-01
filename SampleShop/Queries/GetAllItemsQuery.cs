using System;
using System.Linq;
using SampleShop.Interfaces;
using SampleShop.Model;

namespace SampleShop.Queries
{
    /// <summary>
    /// Object representation of query returning all items from db
    /// </summary>
    public class GetAllItemsQuery
    {
        private readonly IDatabase db;

        public GetAllItemsQuery(IDatabase db)
        {
            this.db = db;
        }

        public IQueryable<Item> Execute()
        {
            return db.Items.Values.AsQueryable();
        }
    }
}
