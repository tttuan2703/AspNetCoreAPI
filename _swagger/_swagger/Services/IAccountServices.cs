using _swagger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _swagger.Services
{
    public interface IAccountServices
    {
        public Task<IEnumerable<Account>> getListAccounts();
        public Task<Account> getAccount(string _id);
        public Account createAccount(AccountViewModel _appUser);
    }
}
