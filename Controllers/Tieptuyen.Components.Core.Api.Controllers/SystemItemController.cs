using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tieptuyen.Components.Core.Api.BusinessObjects;
using Tieptuyen.Components.Core.Api.Services;

namespace Tieptuyen.Components.Core.Api.Controllers
{
    public class SystemItemController
    {
        private SystemItemService systemItemService;

        public SystemItemController()
        {
            this.systemItemService = new SystemItemService();
        }

        public void AddAccount(Account account)
        {
            this.systemItemService.AddAccount(account);
        }

        public IList<Account> GetAccounts(bool ? isOwn)
        {
            return this.systemItemService.GetAllAccounts(isOwn);
        }

        public void UpdateAccount(Account account)
        {
            this.systemItemService.UpdateAccount(account);
        }

        public void AddInputCash(InputCash inputCash)
        {
            this.systemItemService.AddInputCash(inputCash);
        }

        public void UpdateInputCash(InputCash inputCash)
        {
            this.systemItemService.UpdateInputCash(inputCash);
        }

        public IList<InputCash> GetAllInputCash()
        {

            return this.systemItemService.GetAllInputCash();
        }

        public void AddCustomer(Customer customer)
        {
            this.systemItemService.AddCustomer(customer);
        }

        public IList<Customer> GetAllCustomers()
        {
           return this.systemItemService.GetAllCustomers();
        }

        public void UpdateCustomer(Customer customer)
        {
            this.systemItemService.UpdateCustomer(customer);
        }

        public void AddTickets(IList<Ticket> tickets)
        {
            this.systemItemService.AddTickets(tickets);
        }

        public IList<Ticket> GetAllTickets()
        {
            return this.systemItemService.GetAllTicket();
        }

        public void UpdateCustomer(Ticket ticket)
        {
            this.systemItemService.UpdateTicket(ticket);
        }
    }
}
