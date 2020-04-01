using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleShop.Model;
using Xunit;

namespace SampleShop.Tests.Integration
{
    /// <summary>
    /// Test fixture for integration testing items and orders API
    /// </summary>
    public class SampleShopApiTests : IClassFixture<BaseIntegrationTest>
    {
        /// <summary>
        /// Performs checks of all item-related API methods one by one with checks of sanity in between
        /// </summary>
        /// <returns></returns>

        private readonly BaseIntegrationTest fixture;

        public SampleShopApiTests(BaseIntegrationTest fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task IntegrationItemsEndpointMethods()
        {
            using (var httpClient = new HttpClient())
            {
                var item = new Item()
                {
                    Id = 1,
                    Name = "Book",
                    Brand = "Europe Publishing",
                    Price = 10
                };

                // Get all items test
                var getResult = await httpClient.GetAsync(fixture.ApiAction("Items"));
                var itemLoaded = JsonConvert.DeserializeObject<IEnumerable<Item>>(getResult.Content.ReadAsStringAsync().Result).First();

                Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);
                Assert.Equal(item.Id, itemLoaded.Id);
                Assert.Equal(item.Name, itemLoaded.Name);
                Assert.Equal(item.Brand, itemLoaded.Brand);
                Assert.Equal(item.Price, itemLoaded.Price);
            }
        }

        [Fact]
        public async Task IntegrationOrdersEndpointMethods()
        {
            using (var httpClient = new HttpClient())
            {
                var order = new Order
                {
                    OrderItems = new Dictionary<int, int>
                    {
                        { 1, 1 },
                        { 5, 2 },
                        { 4, 2 }
                    }
                };

                // New order test
                var postResult1 = await httpClient.PostAsJsonAsync(fixture.ApiAction("Orders"), order);
                var response = JObject.Parse(postResult1.Content.ReadAsStringAsync().Result);
                int orderId = int.Parse(response["id"].ToString());
                Assert.Equal(HttpStatusCode.Created, postResult1.StatusCode);
                Assert.True(orderId > 0);

                // Load newly created order test
                var getResult = await httpClient.GetAsync(fixture.ApiAction(string.Format("Orders/{0}", orderId)));
                var orderLoaded = JsonConvert.DeserializeObject<Order>(getResult.Content.ReadAsStringAsync().Result);
                var itemLoaded = orderLoaded.OrderItems.First();
                var item = order.OrderItems.First();
                Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);
                Assert.Equal(orderId, orderLoaded.Id);
                Assert.True(orderLoaded.CreateDate > DateTime.MinValue);
                Assert.Equal(item.Key, itemLoaded.Key);
                Assert.Equal(item.Value, itemLoaded.Value);

                // Deleting the order we just created
                var deleteResult = await httpClient.DeleteAsync(fixture.ApiAction(string.Format("Orders/{0}", orderId)));
                Assert.Equal(HttpStatusCode.OK, deleteResult.StatusCode);

                // List all orders again to ensure that order is deleted
                var listResult2 = await httpClient.GetAsync(fixture.ApiAction("Orders"));
                var listItems2 = JsonConvert.DeserializeObject<IEnumerable<Order>>(listResult2.Content.ReadAsStringAsync().Result);
                Assert.Equal(HttpStatusCode.OK, listResult2.StatusCode);
                Assert.False(listItems2.Any(p => p.Id == orderId));

                // Get past orders test
                var listResult3 = await httpClient.GetAsync(fixture.ApiAction(string.Format("Orders/Dates/?start=2018-01-03&end=2018-02-03")));
                var listItems3 = JsonConvert.DeserializeObject<IEnumerable<Order>>(listResult3.Content.ReadAsStringAsync().Result);
                Assert.Contains(listItems3, p => p.Id == 99);

                // Get past items statistics test
                var getResult2 = await httpClient.GetAsync(fixture.ApiAction(string.Format("Orders/Items/?day=2018-01-15")));
                var listItems4 = JsonConvert.DeserializeObject<IEnumerable<ItemSoldStatistics>>(getResult2.Content.ReadAsStringAsync().Result);
                Assert.Contains(listItems4, p => p.ItemId == 1);
                Assert.Contains(listItems4, p => p.Total == 30);
            }
        }
    }
}