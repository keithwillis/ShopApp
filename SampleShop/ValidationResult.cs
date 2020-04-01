using System;
namespace SampleShop
{
    using System;

    /// <summary>
    /// Enum used for presenting a result of trying to add new order item.
    /// ValidationResult.Ok means that order has been added successfully, otherwise not, 
    /// and enum entries describe why in a meaningful way to the API client. 
    /// Flags attribute is used, because adding new entry can fail because of multiple reasons.
    /// </summary>
    [Flags]
    public enum ValidationResult
    {
        Default = 0,
        ItemDoesNotExist = 1,
        NoItemQuantity = 2,
        Ok = 4
    }
}
