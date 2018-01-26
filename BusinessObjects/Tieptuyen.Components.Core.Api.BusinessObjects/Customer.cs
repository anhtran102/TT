using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    [DataContract]
    public class Customer
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "customerName")]
        public string CustomerName { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }       

        [DataMember(Name = "phone")]
        public string Phone { get; set; }      
    }
}
