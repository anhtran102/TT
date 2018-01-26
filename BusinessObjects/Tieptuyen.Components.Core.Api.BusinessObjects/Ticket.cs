using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    [DataContract]
    public class Ticket
    {        
        public int Id { get; set; }        
        public DateTime? IssueDate { get; set; }
        public string TicketNo { get; set; }
        public int NoSeats { get; set; }
        public string FlyNo { get; set; }
        public string Brand { get; set; }
        public string Class { get; set; }
        public string Journey { get; set; }
        public DateTime? FlyDate { get; set; }
        public string FlyTime { get; set; }
        public string FlyTime1 { get; set; }
        public string Passenger { get; set; }
        [DataMember(Name = "doB")]
        public DateTime? DoB { get; set; }
        public string Title { get; set; }
        public decimal NetPrice { get; set; }
        public decimal CommissionL2 { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal PackageFee { get; set; }
        public decimal Diffirence { get; set; }
        public decimal Paid { get; set; }
        public decimal Remain { get; set; }
        public decimal TotalPrice { get; set; }
        public bool  IsCancel { get; set; }
        public decimal CancelFee { get; set; }
        public decimal ExtraFee { get; set; }
        public decimal Refund { get; set; }
        public bool IsChanged { get; set; }
        public decimal ChangeFee { get; set; }
        public decimal Penalty { get; set; }
        public int CustomerID { get; set; }
        public bool XuatNgoai { get; set; }    
        public string Note { get; set; }
        public bool IsClose { get; set; }
        public bool Imported { get; set; }

    }
}
