using _swagger.DataMongoDB;
using _swagger.Models;
using _swagger.Validation;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _swagger.Services
{
    public class AccountSevices : IAccountServices
    {
        private readonly IMongoCollection<Account> _users;
        AccountValidation _validation = new AccountValidation();
        public AccountSevices(MyDB dbClient)
        {
            _users = dbClient.GetAccountsCollection();
        }
        public async Task<IEnumerable<Account>> getListAccounts()
        {
            var users = _users.Find(user => true).ToListAsync();
            return await users;
        }
        public async Task<Account> getAccount(string _id)
        {
            var user = _users.Find(user => user.Id == _id).FirstOrDefaultAsync();
            return await user;
        }
        public Account createAccount(AccountViewModel _appUser)
        {
            string errorMess = _validation.checkValidateAccount(_appUser);
            if (!errorMess.Equals(""))
            {
                Console.WriteLine("Error: " + errorMess);
                return null;
            }
            //var accounts = _accounts.Find(account => true).ToList();
            var account = new Account();
            account = Startup.map_account._mapper.Map<Account>(_appUser);
            _users.InsertOne(account);

            return account;
        }
    }
}
