using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    [DataContract]
    public class Account
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "accountName")]
        public string AccountNo { get; set; }

        [DataMember(Name = "accountName")]
        public string AccountName { get; set; }

        [DataMember(Name = "bankName")]
        public string BankName { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        [DataMember(Name = "isOwn")]
        public bool  IsOwn { get; set; }
    }
}
