using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    public interface ISystemItemService
    {

        /// <summary>
        /// Add account
        /// </summary>
        /// <param name="account"></param>
        void AddAccount(Account account);

        /// <summary>
        /// Get all account
        /// </summary>
        /// <returns>List of accounts</returns>
        IList<Account> GetAllAccounts();
    }
}
