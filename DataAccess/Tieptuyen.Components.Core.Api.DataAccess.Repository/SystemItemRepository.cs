using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tieptuyen.Components.Core.Api.BusinessObjects;
using Tieptuyen.Components.Core.Api.DataAccess.SqlServer;

namespace Tieptuyen.Components.Core.Api.DataAccess.Repository
{
    public class SystemItemRepository : KDataRepository
    {
        public void AddAccount(Account account, IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.AddAccount";
            SqlDynamicParameters paras = new SqlDynamicParameters();
            paras.Add("AccountNo", account.AccountNo);            
            paras.Add("AccountName", account.AccountName);
            paras.Add("BankName", account.BankName);
            paras.Add("Phone", account.Phone);
            paras.Add("IsOwn", account.IsOwn);
            unitOfWork.Execute(StoredProcedure, paras, commandType: CommandType.StoredProcedure);
        }

        public IList<Account> GetAllAccounts(IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.GetAllAccounts";          
            var accounts = unitOfWork.Query<Account>(StoredProcedure).ToList();
            return accounts;
        }

        public void UpdateAccount(Account account, IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.UpdateAccount";
            SqlDynamicParameters paras = new SqlDynamicParameters();
            paras.Add("AccountNo", account.AccountNo);
            paras.Add("AccountName", account.AccountName);
            paras.Add("BankName", account.BankName);
            paras.Add("Phone", account.Phone);
            paras.Add("ID", account.Id);
            paras.Add("IsOwn", account.IsOwn);
            unitOfWork.Execute(StoredProcedure, paras, commandType: CommandType.StoredProcedure);
        }


        public void AddInputCash(InputCash inputCash, IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.AddInputCash";
            SqlDynamicParameters paras = new SqlDynamicParameters();
            paras.Add("Cash", inputCash.Cash);
            paras.Add("FromAccount", inputCash.FromAccount);
            paras.Add("ToAccount", inputCash.ToAccount);
            paras.Add("Payer", inputCash.Payer);
            paras.Add("PayDate", inputCash.PayDate);
            unitOfWork.Execute(StoredProcedure, paras, commandType: CommandType.StoredProcedure);
        }

        public void UpdateInputCash(InputCash inputCash, IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.UpdateInputCash";
            SqlDynamicParameters paras = new SqlDynamicParameters();
            paras.Add("Cash", inputCash.Cash);
            paras.Add("FromAccount", inputCash.FromAccount);
            paras.Add("ToAccount", inputCash.ToAccount);
            paras.Add("Payer", inputCash.Payer);
            paras.Add("PayDate", inputCash.PayDate);
            paras.Add("ID", inputCash.Id);
            unitOfWork.Execute(StoredProcedure, paras, commandType: CommandType.StoredProcedure);
        }

        public IList<InputCash> GetAlInputCash(IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.GetAllInputCash";
            var alInputCash = unitOfWork.Query<InputCash>(StoredProcedure).ToList();
            return alInputCash;
        }

        public void AddCustomer(Customer customer, IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.AddCustomer";
            SqlDynamicParameters paras = new SqlDynamicParameters();
            paras.Add("CustomerName", customer.CustomerName);
            paras.Add("Address", customer.Address);
            paras.Add("Phone", customer.Phone);
            unitOfWork.Execute(StoredProcedure, paras, commandType: CommandType.StoredProcedure);
        }

        public IList<Customer> GetAllCustomers(IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.GetAllCustomers";
            var customers = unitOfWork.Query<Customer>(StoredProcedure).ToList();
            return customers;
        }

        public void UpdateCustomer(Customer customer, IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.UpdateCustome";
            SqlDynamicParameters paras = new SqlDynamicParameters();
            paras.Add("CustomerName", customer.CustomerName);
            paras.Add("Address", customer.Address);
            paras.Add("Phone", customer.Phone);
            paras.Add("ID", customer.Id);
            unitOfWork.Execute(StoredProcedure, paras, commandType: CommandType.StoredProcedure);
        }


        public void AddTickets(IList<Ticket> tickets, IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.AddTicket";
            SqlDynamicParameters paras = new SqlDynamicParameters();
            //paras.Add("IssueDate ", ticket.IssueDate);
            //paras.Add("TicketNo ", ticket.TicketNo);
            //paras.Add("Brand ", ticket.Brand);
            //paras.Add("Class", ticket.Class);
            //paras.Add("Journey ", ticket.Journey);
            //paras.Add("FlyDate", ticket.FlyDate);
            //paras.Add("FlyTime ", ticket.FlyTime);
            //paras.Add("FlyTime1 ", ticket.FlyTime1);
            //paras.Add("FlyTime2", ticket.FlyTime2);
            //paras.Add("Passenger ", ticket.Passenger);
            //paras.Add("DOB", ticket.DoB);
            //paras.Add("Title ", ticket.Title);
            //paras.Add("NetPrice ", ticket.NetPrice);
            //paras.Add("CommissionL2 ", ticket.CommissionL2);
            //paras.Add("ServiceFee ", ticket.ServiceFee);
            //paras.Add("PackageFee", ticket.PackageFee);
            //paras.Add("Diffirence ", ticket.Diffirence);
            //paras.Add("Paid ", ticket.Paid);
            //paras.Add("Remain ", ticket.Remain);
            //paras.Add("TotalPrice ", ticket.TotalPrice);
            //paras.Add("IsCancel ", ticket.IsCancel);
            //paras.Add("CancelFee ", ticket.CancelFee);
            //paras.Add("Refund ", ticket.Refund);
            //paras.Add("IsChanged ", ticket.IsChanged);
            //paras.Add("ChangeFee ", ticket.ChangeFee);
            //paras.Add("CustomerID ", ticket.CustomerId);
            //paras.Add("IsTour ", ticket.IsTour);
            paras.AddAsTable("Tickets", tickets);
            unitOfWork.Execute(StoredProcedure, paras, commandType: CommandType.StoredProcedure);
        }

        public IList<Ticket> GetAllTickets(IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.GetAllTicket";
            var tickets = unitOfWork.Query<Ticket>(StoredProcedure).ToList();
            return tickets;
        }

        public void UpdateTicket(Ticket ticket, IUnitOfWork unitOfWork)
        {
            const string StoredProcedure = "cmn.UpdateTicket";
            SqlDynamicParameters paras = new SqlDynamicParameters();
            paras.Add("IssueDate ", ticket.IssueDate);
            paras.Add("TicketNo ", ticket.TicketNo);
            paras.Add("Brand ", ticket.Brand);
            paras.Add("Class", ticket.Class);
            paras.Add("Journey ", ticket.Journey);
            paras.Add("FlyDate", ticket.FlyDate);
            paras.Add("FlyTime ", ticket.FlyTime);
            paras.Add("FlyTime1 ", ticket.FlyTime1);
            paras.Add("Passenger ", ticket.Passenger);
            paras.Add("DOB", ticket.DoB);
            paras.Add("Title ", ticket.Title);
            paras.Add("NetPrice ", ticket.NetPrice);
            paras.Add("CommissionL2 ", ticket.CommissionL2);
            paras.Add("ServiceFee ", ticket.ServiceFee);
            paras.Add("PackageFee", ticket.PackageFee);
            paras.Add("Diffirence ", ticket.Diffirence);
            paras.Add("Paid ", ticket.Paid);
            paras.Add("Remain ", ticket.Remain);
            paras.Add("TotalPrice ", ticket.TotalPrice);
            paras.Add("IsCancel ", ticket.IsCancel);
            paras.Add("CancelFee ", ticket.CancelFee);
            paras.Add("Refund ", ticket.Refund);
            paras.Add("IsChanged ", ticket.IsChanged);
            paras.Add("ChangeFee ", ticket.ChangeFee);
            paras.Add("CustomerID ", ticket.CustomerID);
            unitOfWork.Execute(StoredProcedure, paras, commandType: CommandType.StoredProcedure);
        }
    }
}
