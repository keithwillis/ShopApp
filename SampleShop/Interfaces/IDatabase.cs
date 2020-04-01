using System.Collections.Generic;
using SampleShop.Model;

namespace SampleShop.Interfaces
{
    /// <summary>
    /// Interface for database representation - for the purposes of this test solution 
    /// it should just expose entity's collections
    /// </summary>
    public interface IDatabase
    {
        IDictionary<int, Item> Items { get; }
        IDictionary<int, Order> Orders { get; }
    }
}