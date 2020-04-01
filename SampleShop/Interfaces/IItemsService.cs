using System;
using System.Collections.Generic;
using SampleShop.Model;

namespace SampleShop.Interfaces
{
    /// <summary>
    /// Interface for item's service
    /// </summary>
    public interface IItemsService
    {
        IEnumerable<Item> All();
    }
}
