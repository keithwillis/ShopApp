using System;
using Xunit;
using SampleShop.Interfaces;
using SampleShop.Services;
using SampleShop.Queries;
using SampleShop.Tests;
using System.Linq;

namespace SampleShop.Tests
{
    public class OrdersServiceTests
    {
        private GetAllOrdersQuery mockQueryAllOrders;
        private GetAllItemsQuery mockQueryAllItems;
        private GetOrderByIdQuery mockQueryById;
        private AddOrderQuery mockQueryAddOrder;
        private DeleteOrderQuery mockQueryDeleteOrder;
        private IDatabase mockDb;

        private OrdersService CreateOrdersService(bool empty = false)
        {
            mockDb = empty ? TestDatabaseFactory.CreateEmptyDatabase() : TestDatabaseFactory.CreateDatabase();

            mockQueryAllOrders = new GetAllOrdersQuery(mockDb);
            mockQueryAllItems = new GetAllItemsQuery(mockDb);
            mockQueryById = new GetOrderByIdQuery(mockDb);
            mockQueryAddOrder = new AddOrderQuery(mockDb);
            mockQueryDeleteOrder = new DeleteOrderQuery(mockDb);

            return new OrdersService(mockQueryAllOrders, mockQueryAllItems, mockQueryById, mockQueryAddOrder, mockQueryDeleteOrder);
        }

        [Fact]
        public void When_GetByDatesRunsWithNonExistingOrderDates_EmptyCollectionIsReturned()
        {
            // Arrange
            var service = CreateOrdersService();

            // Act
            var result = service.GetByDates(new DateTime(2017, 9, 1), new DateTime(2018, 1, 1));

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void When_GetByDatesRunsWithExistingOrderDates_ExistingOrdersAreReturned()
        {
            // Arrange
            var service = CreateOrdersService();

            // Act
            var result = service.GetByDates(new DateTime(2018, 1, 1), new DateTime(2018, 3, 1));

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void When_GetItemsSoldByDayRunsWithNonExistingOrdersDate_EmptyCollectionReturned()
        {
            // Arrange
            var service = CreateOrdersService();

            // Act
            var result = service.GetItemsSoldByDay(new DateTime(2017, 6, 3));

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void When_GetItemsSoldByDayRunsWithExistingOrdersDate_AllItemsSoldAreReturned()
        {
            // Arrange
            var service = CreateOrdersService();

            // Act
            var result = service.GetItemsSoldByDay(new DateTime(2018, 3, 3));

            // Assert
            Assert.Equal(4, result.Count());
        }
    }
}
