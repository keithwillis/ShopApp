using System;
using System.Collections.Generic;
using SampleShop.Model;

namespace SampleShop.Interfaces
{
    /// <summary>
    /// Interface for order's service
    /// </summary>
    public interface IOrdersService
    {
        IEnumerable<Order> All();
        IEnumerable<Order> GetByDates(DateTime start, DateTime end);
        IEnumerable<ItemSoldStatistics> GetItemsSoldByDay(DateTime day);
        Order GetById(int id);
        Order Add(Order newOrder);
        void Delete(int id);
    }
}
