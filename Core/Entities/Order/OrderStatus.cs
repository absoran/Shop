using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Entities.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "PaymentReceived")]
        PaymentReceived,

        [EnumMember(Value = "Processing")]
        Processing,

        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed,

        [EnumMember(Value = "Refunded")]
        Refunded
    }
}
