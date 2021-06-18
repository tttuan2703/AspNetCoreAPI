using _swagger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _swagger.Services
{
    public interface IElasticSearchAccountService
    {
        Task<IEnumerable<Account>> GetAccounts(int count, int skip = 0);
        Task<Account> GetAccountFollowId(string id);
        Task<IEnumerable<Account>> GetAccountsFollowFullName(string FullName);
        Task DeleteAsync(Account account);
        Task SaveSingleAsync(Account account);
        Task SaveManyAsync(Account[] accounts);
        Task SaveBulkAsync(Account[] accounts);
    }
}
