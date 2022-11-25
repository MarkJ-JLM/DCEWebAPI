using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DCEWebAPI.Models
{
    [DataContract]
    public class ActiveOrdersModel
    {
        [DataMember(Name = "UserId")]
        public string UserId { get; set; }

        [DataMember(Name = "FullName")]
        public string FullName { get; set; }

        [DataMember(Name = "OrderId")]
        public string OrderId { get; set; }

        [DataMember(Name = "ProductName")]
        public string ProductName { get; set; }

        [DataMember(Name = "OrderStatus")]
        public int OrderStatus { get; set; }

        [DataMember(Name = "OrderType")]
        public int OrderType { get; set; }

        [DataMember(Name = "OrderedOn")]
        public DateTime OrderedOn { get; set; }

        [DataMember(Name = "ShippedOn")]
        public DateTime ShippedOn { get; set; }

        [DataMember(Name = "SupplierName")]
        public string SupplierName { get; set; }
    }
}
