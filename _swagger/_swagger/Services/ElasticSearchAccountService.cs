using _swagger.Models;
using Microsoft.Extensions.Logging;
using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace _swagger.Services
{
    public class ElasticSearchAccountService : IElasticSearchAccountService
    {
        private List<Account> _cache = new List<Account>();
        private readonly IElasticClient _elasticClient;
        private readonly ILogger _logger;

        public ElasticSearchAccountService(IElasticClient elasticClient, ILogger<ElasticSearchAccountService> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public virtual Task<IEnumerable<Account>> GetAccounts(int count, int skip = 0)
        {
            var accounts = _cache
                .Skip(skip)
                .Take(count);
            return Task.FromResult(accounts);
        }
        public virtual Task<Account> GetAccountFollowId(string id)
        {
            var account = _cache
                            .FirstOrDefault(a => a.Id == id);
            return Task.FromResult(account);
        }
        public virtual Task<IEnumerable<Account>> GetAccountsFollowFullName(string FullName)
        {
            var accounts = _cache
                            .Where(a => a.FullName == FullName)
                            .AsEnumerable();
            return Task.FromResult(accounts);
        }
        public async Task DeleteAsync(Account account)
        {
            await _elasticClient.DeleteAsync<Account>(account);
            if (_cache.Contains(account))
            {
                _cache.Remove(account);
            }
        }
        public async Task SaveSingleAsync(Account account)
        {
            if (_cache.Any(a => a.Id == account.Id))
            {
                await _elasticClient.UpdateAsync<Account>(account, a => a.Doc(account));
            }
            else
            {
                _cache.Add(account);
                await _elasticClient.IndexDocumentAsync<Account>(account);
            }
        }
        public async Task SaveManyAsync(Account[] accounts)
        {
            _cache.AddRange(accounts);
            var result = await _elasticClient.IndexManyAsync(accounts);
            if (result.Errors)
            {
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }
        }
        public async Task SaveBulkAsync(Account[] accounts)
        {
            _cache.AddRange(accounts);
            var result = await _elasticClient.BulkAsync(b => b.Index("accounts").IndexMany(accounts));
            if (result.Errors)
            {
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}:{1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }
        }
    }
}
