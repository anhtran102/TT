using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tieptuyen.Components.Core.Api.DataAccess;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    public interface ISystemItemRepository: IRepository
    {
        /// <summary>
        /// Add new account
        /// </summary>
        /// <param name="account">Account to add</param>
        /// <param name="unitOfWork">Unit of work</param>
        void AddAccount(Account account, IUnitOfWork unitOfWork);

        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <param name="unitOfWork">unit of work</param>
        /// <returns></returns>
        IList<Account> GetAllAccounts(IUnitOfWork unitOfWork);
    }
}
