using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SampleShop.Interfaces;
using SampleShop.Model;

namespace SampleShop
{
    /// <summary>
    /// Responsible for db creation and filling it with initial data 
    /// </summary>
    public static class DatabaseFactory
    {
        private static IDictionary<int, Item> Items;
        private static IDictionary<int, Order> Orders;

        public static IDatabase CreateEmptyDatabase()
        {
            return new InMemoryDatabase(new ConcurrentDictionary<int, Item>(),
                                        new ConcurrentDictionary<int, Order>());
        }

        public static IDatabase CreateDatabase()
        {
            SetInitialData();
            return new InMemoryDatabase(Items, Orders);
        }

        private static void SetInitialData()
        {
            Items = new ConcurrentDictionary<int, Item>();
            Orders = new ConcurrentDictionary<int, Order>();

            // Items
            var lh101 = new Item
            {
                Id = 1,
                Name = "Book",
                Brand = "Europe Publishing",
                Price = 10
            };

            var lh102 = new Item
            {
                Id = 2,
                Name = "Magazine",
                Brand = "Times Publishing",
                Price = 8.2M
            };

            var lh105 = new Item
            {
                Id = 3,
                Name = "Soda",
                Brand = "Coca Cola",
                Price = 2
            };

            var lh108 = new Item
            {
                Id = 4,
                Name = "Lotery",
                Brand = "Euromillions",
                Price = 20
            };

            var lh201 = new Item
            {
                Id = 5,
                Name = "Socks",
                Brand = "H&M",
                Price = 6.5M
            };

            var lh202 = new Item
            {
                Id = 6,
                Name = "Shirt",
                Brand = "Calvin Klein",
                Price = 12
            };

            var lh210 = new Item
            {
                Id = 7,
                Name = "Toy",
                Brand = "Mattel",
                Price = 14.5M
            };

            Items.Add(lh101.Id, lh101);
            Items.Add(lh102.Id, lh102);
            Items.Add(lh105.Id, lh105);
            Items.Add(lh108.Id, lh108);
            Items.Add(lh201.Id, lh201);
            Items.Add(lh202.Id, lh202);
            Items.Add(lh210.Id, lh210);

            var r1 = new Order
            {
                Id = 99,
                CreateDate = new DateTime(2018, 1, 15),
                OrderItems = new Dictionary<int, int>
                {
                    { lh101.Id, 3 },
                }
            };

            Orders.Add(r1.Id, r1);
        }
    }
}
