using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities_Helper
{
    public enum OrderStatus
    {
        WaitingForConfirmation,
        Pending,
        InProgress,
        Delivered,
        Cancelled,
        Declined
    }
}