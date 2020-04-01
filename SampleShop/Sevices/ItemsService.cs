using System;
using System.Collections.Generic;
using System.Linq;
using SampleShop.Interfaces;
using SampleShop.Model;
using SampleShop.Queries;

namespace SampleShop.Services
{
    public class ItemsService : IItemsService
    {
        private readonly GetAllItemsQuery _query;

        public ItemsService(GetAllItemsQuery query)
        {
            _query = query;
        }

        /// <summary>
        /// Lists all items that exist in db
        /// </summary>
        public IEnumerable<Item> All()
        {
            return _query.Execute().ToList();
        }
    }
}
