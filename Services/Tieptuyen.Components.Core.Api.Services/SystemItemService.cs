using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tieptuyen.Components.Core.Api.BusinessObjects;
using Tieptuyen.Components.Core.Api.DataAccess;
using Tieptuyen.Components.Core.Api.DataAccess.Repository;
using Tieptuyen.Components.Core.Api.Util;

namespace Tieptuyen.Components.Core.Api.Services
{
    public class SystemItemService
    {

        public SystemItemRepository systemItemRepository;

        public SystemItemService()
        {
            this.systemItemRepository = new SystemItemRepository();
        }

        /// <inheritdoc/>
        public void AddAccount(Account account)
        {
            if (account == null)
            {
                throw new ArgumentException("Account can not be null");
            }

            if (string.IsNullOrEmpty(account.AccountNo) || string.IsNullOrEmpty(account.AccountName))
            {
                throw new ArgumentException("Account can not be null");
            }

            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                this.systemItemRepository.AddAccount(account, unitOf);
                unitOf.Commit();
            }
        }

        public IList<Account> GetAllAccounts(bool? isOwn)
        {
            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                var accounts = this.systemItemRepository.GetAllAccounts(unitOf);
                if (isOwn.HasValue == true)
                {
                    return accounts.Where(x => x.IsOwn == isOwn.Value).ToList();
                }               

                return accounts;
            }
        }

        public void UpdateAccount(Account acc)
        {
            if (acc == null)
            {
                throw new ArgumentException("Account can not be null");
            }

            if (string.IsNullOrEmpty(acc.AccountNo) || string.IsNullOrEmpty(acc.AccountName))
            {
                throw new ArgumentException("Account can not be null");
            }

            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                this.systemItemRepository.UpdateAccount(acc, unitOf);
                unitOf.Commit();
            }
        }

        /// <inheritdoc/>
        public void AddInputCash(InputCash inputCash)
        {
            if (inputCash == null)
            {
                throw new ArgumentException("IputCash can not be null");
            }

            if (string.IsNullOrEmpty(inputCash.FromAccount) || string.IsNullOrEmpty(inputCash.ToAccount) ||
                string.IsNullOrEmpty(inputCash.Payer))
            {
                throw new ArgumentException("IputCash can not be null");
            }

            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                this.systemItemRepository.AddInputCash(inputCash, unitOf);
                unitOf.Commit();
            }


        }

        /// <inheritdoc/>
        public void UpdateInputCash(InputCash inputCash)
        {
            if (inputCash == null)
            {
                throw new ArgumentException("IputCash can not be null");
            }

            if (string.IsNullOrEmpty(inputCash.FromAccount) || string.IsNullOrEmpty(inputCash.ToAccount) || string.IsNullOrEmpty(inputCash.Payer))
            {
                throw new ArgumentException("IputCash can not be null");
            }

            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                this.systemItemRepository.UpdateInputCash(inputCash, unitOf);
                unitOf.Commit();
            }
        }

        public IList<InputCash> GetAllInputCash()
        {
            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                return this.systemItemRepository.GetAlInputCash(unitOf);                
            }
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.CustomerName))
            {
                throw new ArgumentException("Customer can not be null");
            }

            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                this.systemItemRepository.AddCustomer(customer, unitOf);
                unitOf.Commit();
            }
        }

        public IList<Customer> GetAllCustomers()
        {
            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                return this.systemItemRepository.GetAllCustomers(unitOf);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.CustomerName) || string.IsNullOrEmpty(customer.Address) || string.IsNullOrEmpty(customer.Phone))
            {
                throw new ArgumentException("Customer can not be null");
            }

            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                this.systemItemRepository.UpdateCustomer(customer, unitOf);
                unitOf.Commit();
            }
        }

        public void AddTickets(IList<Ticket> tickets)
        {
            if (tickets == null)
            {
                throw new ArgumentException("Ticket can not be null");
            }
            var notImportedTickets = tickets.Where(x => x.Imported == false).ToList();
            if (notImportedTickets == null || notImportedTickets.Count == 0)
            {
                throw new ArgumentException("Ticket be null or all imported");
            }

            foreach (var ticket in notImportedTickets)
            {
                if (!CheckTicket(ticket))
                {
                    throw new ArgumentException(string.Format("Thông tin vé không hợp lệ: Ve {0}", ticket.TicketNo));
                }   
            }            

            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                this.systemItemRepository.AddTickets(notImportedTickets, unitOf);
                unitOf.Commit();
            }
        }

        public IList<Ticket> GetAllTicket()
        {
            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                return this.systemItemRepository.GetAllTickets(unitOf);
            }
        }

        public void UpdateTicket(Ticket ticket)
        {
            if (!CheckTicket(ticket))
            {
                throw new ArgumentException(string.Format("Thông tin vé không hợp lệ: Ve {0}",ticket.TicketNo));
            }

            using (IUnitOfWork unitOf = this.systemItemRepository.BeginWork())
            {
                this.systemItemRepository.UpdateTicket(ticket, unitOf);
                unitOf.Commit();
            }
        }

        public bool CheckTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(ticket.TicketNo)||
                string.IsNullOrEmpty(ticket.Passenger))
            {
                return false;
            }

            //if (ticket.NetPrice == 0 || ticket.TotalPrice == 0)
            //{
            //    return false;
            //}

            if (ticket.TotalPrice !=
                (ticket.NetPrice + ticket.Diffirence + ticket.PackageFee + ticket.ChangeFee))
            {
                return false;
            }

            //Refund = Net price - cancel fee
            //if (ticket.Refund != 0  && ticket.Refund != (ticket.NetPrice + ticket.PackageFee - ticket.CancelFee - ticket.ExtraFee))
            //{
            //    return false;
            //}

            //Remain = total Price - CommisiocnL2 - Paid - cancelFee + refund
            if (ticket.Remain !=
                ticket.TotalPrice - ticket.CommissionL2 - ticket.Paid - ticket.Penalty)
            {
                return false;
            }

            return true;
        }
    }
}
