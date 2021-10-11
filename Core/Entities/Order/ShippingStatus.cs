using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Entities.Order
{
    public enum ShippingStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Shipped")]
        PaymentReceived,

        [EnumMember(Value = "Shipping Canceled")]
        PaymentFailed
    }
}
