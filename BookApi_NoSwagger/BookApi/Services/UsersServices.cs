using BookApi.Models.Account;
using BookApi.Models.DatabaseSettings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class UsersServices
    {
        private readonly IMongoCollection<AppUser> _users;
        public UsersServices(IAppUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<AppUser>(settings.UsersCollectionName);
        }
        public async Task<IEnumerable<AppUser>> getListUser()
        {
            var users = _users.Find(user => true).ToListAsync();
            return await users;
        }
        public async Task<AppUser> getUser(string _id)
        {
            var user = _users.Find(user => user._id == _id).FirstOrDefaultAsync();
            return await user;
        }
        public async Task<AppUser> createUser(AppUser _appUser)
        {
            await _users.InsertOneAsync(_appUser);
            return _appUser;
        }
    }
}
