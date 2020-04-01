using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SampleShop.Interfaces;
using SampleShop.Model;

namespace SampleShop.Tests
{
    /// <summary>
    /// Responsible for db creation and filling it with initial sample data 
    /// </summary>
    public static class TestDatabaseFactory
    {
        public static IDatabase CreateEmptyDatabase()
        {
            return new InMemoryDatabase(new ConcurrentDictionary<int, Item>(),
                                        new ConcurrentDictionary<int, Order>());
        }

        public static IDatabase CreateDatabase()
        {
            var items = new ConcurrentDictionary<int, Item>();
            var orders = new ConcurrentDictionary<int, Order>();

            SetInitialData(items, orders);

            return new InMemoryDatabase(items, orders);
        }

        private static void SetInitialData(IDictionary<int, Item> items,
                                           IDictionary<int, Order> orders)
        {
            // Items

            var lh101 = new Item
            {
                Id = 1,
                Name = "Book",
                Price = 10
            };

            var lh102 = new Item
            {
                Id = 2,
                Name = "Magazine",
                Price = 8.2M
            };

            var lh105 = new Item
            {
                Id = 3,
                Name = "Soda",
                Price = 2
            };

            var lh108 = new Item
            {
                Id = 4,
                Name = "Lotery",
                Price = 20
            };

            var lh201 = new Item
            {
                Id = 5,
                Name = "Socks",
                Price = 6.5M
            };

            var lh202 = new Item
            {
                Id = 6,
                Name = "Shirt",
                Price = 12
            };

            var lh210 = new Item
            {
                Id = 7,
                Name = "Toy",
                Price = 14.5M
            };

            items.Add(lh101.Id, lh101);
            items.Add(lh102.Id, lh102);
            items.Add(lh105.Id, lh105);
            items.Add(lh108.Id, lh108);
            items.Add(lh201.Id, lh201);
            items.Add(lh202.Id, lh202);
            items.Add(lh210.Id, lh210);

            // Orders

            var r1 = new Order
            {
                Id = 1,
                CreateDate = new DateTime(2018, 3, 3),
                OrderItems = new Dictionary<int, int>
                {
                    { lh101.Id, 1 },
                    { lh108.Id, 3 }
                }
            };

            var r2 = new Order 
            {
                Id = 2,
                CreateDate = new DateTime(2018, 1, 23),
                OrderItems = new Dictionary<int, int>
                {
                    { lh105.Id, 5 },
                    { lh108.Id, 2 },
                    { lh202.Id, 4 }
                }
            };

            var r3 = new Order
            {
                Id = 3,
                CreateDate = new DateTime(2018, 3, 4),
                OrderItems = new Dictionary<int, int>
                {
                    { lh201.Id, 1 },
                }
            };

            var r4 = new Order
            {
                Id = 4,
                CreateDate = new DateTime(2018, 1, 8),
                OrderItems = new Dictionary<int, int>
                {
                    { lh101.Id, 1 },
                    { lh210.Id, 2 },
                    { lh105.Id, 2 },
                    { lh202.Id, 1 },
                    { lh108.Id, 3 }
                }
            };

            var r5 = new Order
            {
                Id = 5,
                CreateDate = new DateTime(2018, 2, 5),
                OrderItems = new Dictionary<int, int>
                {
                    { lh108.Id, 1 },
                    { lh201.Id, 2 },
                    { lh202.Id, 3 },
                    { lh210.Id, 1 },
                    { lh105.Id, 6 }
                }
            };

            var r6 = new Order
            {
                Id = 6,
                CreateDate = new DateTime(2018, 3, 3),
                OrderItems = new Dictionary<int, int>
                {
                    { lh201.Id, 1 },
                    { lh108.Id, 2 },
                    { lh202.Id, 4 }
                }
            };

            orders.Add(r1.Id, r1);
            orders.Add(r2.Id, r2);
            orders.Add(r3.Id, r3);
            orders.Add(r4.Id, r4);
            orders.Add(r5.Id, r5);
            orders.Add(r6.Id, r6);
        }
    }
}
