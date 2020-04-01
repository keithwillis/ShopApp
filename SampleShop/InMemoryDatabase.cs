using System.Collections.Concurrent;
using System.Collections.Generic;
using SampleShop.Interfaces;
using SampleShop.Model;

namespace SampleShop
{
    /// <summary>
    /// Implementation of database for the purposes of test solution (we don't want to test "real" db access).
    /// It's a simple set of dictionaries (faster access and primary key constraint "simulation") 
    /// containing entity's entries.
    /// </summary>
    public class InMemoryDatabase : IDatabase
    {
        public IDictionary<int, Item> Items { get; private set; }
        public IDictionary<int, Order> Orders { get; private set; }

        public InMemoryDatabase()
        {
            Items = new ConcurrentDictionary<int, Item>();
            Orders = new ConcurrentDictionary<int, Order>();
        }

        public InMemoryDatabase(IDictionary<int, Item> items, IDictionary<int, Order> orders)
        {
            Items = items;
            Orders = orders;
        }
    }
}
