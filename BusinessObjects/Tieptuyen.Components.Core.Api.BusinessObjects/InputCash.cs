using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    [DataContract]
    public class InputCash
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "cash")]
        public decimal Cash { get; set; }

        [DataMember(Name = "fromAccount")]
        public string FromAccount { get; set; }

        [DataMember(Name = "toAccount")]
        public string ToAccount { get; set; }

        [DataMember(Name = "payer")]
        public string Payer { get; set; }

        [DataMember(Name = "payDate")]
        public DateTime  PayDate { get; set; }
    }
}
