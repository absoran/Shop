using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Entities.Identity
{
    public enum Authority
    {
        [EnumMember(Value = "Admin")]
        Admin,

        [EnumMember(Value = "Merchant")]
        Merchant,

        [EnumMember(Value = "Customer")]
        Customer,

        [EnumMember(Value = "NotRegistered")]
        NotRegistered
    }
    
}
