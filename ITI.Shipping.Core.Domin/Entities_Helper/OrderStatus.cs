using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities_Helper
{
    public enum OrderStatus
    {
        Pending = 0,
        WaitingForConfirmation = 1,
        InProgress = 2,
        Delivered = 3,
        DeliveredToCourier = 4,
        Declined = 5,
        UnreachableCustomer = 5,
        PartialDelivery = 7,
        CanceledByRecipient = 8,
        DeclinedWithPartialPayment = 8,
        DeclinedWithFullPayment = 10
    }
}