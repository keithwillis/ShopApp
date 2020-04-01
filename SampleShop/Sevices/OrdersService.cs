using System;
using System.Collections.Generic;
using System.Linq;
using SampleShop.Interfaces;
using SampleShop.Model;
using SampleShop.Queries;

namespace SampleShop.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly GetAllOrdersQuery queryAll;
        private readonly GetAllItemsQuery queryAllItems;
        private readonly GetOrderByIdQuery queryById;
        private readonly AddOrderQuery queryAdd;
        private readonly DeleteOrderQuery queryDelete;

        public OrdersService(GetAllOrdersQuery queryAllOrders, GetAllItemsQuery queryAllItems,
                             GetOrderByIdQuery queryById, AddOrderQuery queryAdd, 
                             DeleteOrderQuery queryDelete)
        {
            this.queryAll = queryAllOrders;
            this.queryAllItems = queryAllItems;
            this.queryById = queryById;
            this.queryAdd = queryAdd;
            this.queryDelete = queryDelete;
        }

        /// <summary>
        /// Lists all orders that exist in db
        /// </summary>
        public IEnumerable<Order> All()
        {
            return queryAll.Execute().ToList();
        }

        /// <summary>
        /// Gets single order by its id
        /// </summary>
        public Order GetById(int id)
        {
            return queryById.Execute(id);
        }

        /// <summary>
        /// Tries to add given order to db, after validating it
        /// </summary>
        public Order Add(Order newOrder)
        {
            if (newOrder == null)
            {
                throw new ArgumentNullException("newOrder");
            }

            var result = ValidateNewOrder(newOrder);
            if ((result & ValidationResult.Ok) == ValidationResult.Ok)
            {
                queryAdd.Execute(newOrder);
                return newOrder;
            }

            return null;
        }

        /// <summary>
        /// Checks whether given order can be added.
        /// Performs logical and business validation.
        /// </summary>
        public ValidationResult ValidateNewOrder(Order newOrder)
        {
            var result = ValidationResult.Default;

            if (newOrder == null)
            {
                throw new ArgumentNullException("newOrder");
            }

            var items = queryAllItems.Execute();

            foreach (var item in newOrder.OrderItems)
            {
                if (item.Value <= 0) result |= ValidationResult.NoItemQuantity; 
                if (!items.Any(p => p.Id == item.Key))
                {
                    result |= ValidationResult.ItemDoesNotExist;
                }
            }

            if (result == ValidationResult.Default)
            {
                result = ValidationResult.Ok;
            }

            return result;
        }

        /// <summary>
        /// Deletes (if exists) order from db (by its id)
        /// </summary>
        public void Delete(int id)
        {
            queryDelete.Execute(id);
        }

        /// <summary>
        /// Returns all orders (listed chronologically) between a given start date and end date.
        /// Start and end dates must be from the past (not in the future or today).
        /// </summary>
        public IEnumerable<Order> GetByDates(DateTime start, DateTime end)
        {
            // TODO
            // Implement method returning all orders (use queryAll) between a give start date and end date.
            // Method should check if dates are in the past (not in the future or today) else
            // throw an ArgumentException. 
            // Return all orders in chronological order. 

            if (start >= DateTime.Today || end >= DateTime.Today)
                throw new ArgumentException("Dates must be in the past and cannot be today");

            var results = queryAll.Execute().Where(i => i.CreateDate >= start && i.CreateDate <= end).OrderBy(i => i.CreateDate);

            if (results == null) return new List<Order>();

            return results;
        }

        /// <summary>
        /// Returns statistics (list of pairs [ItemId, Total] listed by id) on a given day.
        /// Day must be from the past (not in the future or today).
        /// </summary>
        public IEnumerable<ItemSoldStatistics> GetItemsSoldByDay(DateTime day)
        {
            // TODO
            // Implement method returning data described in a method summary.
            // The order have duplicate items, the total have to be summed. 
            // Use queryAll and queryAllOrders.

            if(day >= DateTime.Today)
                throw new ArgumentException("Dates must be in the past and cannot be today");

            var results = queryAll.Execute().Where(o => o.CreateDate == day)
                                  .SelectMany(o => o.OrderItems.Select(
                                                oi => new ItemSoldStatistics
                                                {
                                                    ItemId = oi.Key,
                                                    Total = oi.Value
                                                })).ToList<ItemSoldStatistics>()
                                  .GroupBy(i => i.ItemId)
                                  .Select(iss => new ItemSoldStatistics
                                  {
                                      ItemId = iss.Key,
                                      Total = iss.Sum(i => i.Total)
                                  }).ToList()
                                  .Join(queryAllItems.Execute(),
                                        o => o.ItemId,
                                        i => i.Id,
                                        (o, i) => new ItemSoldStatistics
                                        {
                                            ItemId = o.ItemId,
                                            Total = i.Price * o.Total
                                        }
                                  );

            if (results == null) return new List<ItemSoldStatistics>();

            return results;
        }
    }
} 
