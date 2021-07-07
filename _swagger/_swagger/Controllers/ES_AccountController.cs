using _swagger.DataMongoDB;
using _swagger.Models;
using _swagger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ES_AccountController : Controller
    {
        private readonly IElasticSearchAccountService _searchAccountService;
        private readonly IElasticClient _elasticClient;
        private readonly IOptionsSnapshot<AccountSetting> _setting;
        private readonly ILogger _logger;

        public ES_AccountController(IElasticSearchAccountService accountServices, ElasticClient elasticClient, IOptionsSnapshot<AccountSetting> settings)
        {
            _searchAccountService = accountServices;
            _elasticClient = elasticClient;
            _setting = settings;
        }

        // GET: api/<ES_AccountController>
        [HttpGet("find")]
        public async Task<IActionResult> Get(string query, int page = 5, int pageSize = 5)
        {
            var response = await _elasticClient.SearchAsync<Account>(account =>
                                                                        account.Query(q => q.QueryString(d => d.Query('*' + query + '*')))
                                                                        .From((page - 1) * pageSize)
                                                                        .Size(pageSize));
            if (!response.IsValid)
            {
                _logger.LogError("Failed to search documents");
                return Ok(new Account[] { });
            }
            return Ok(response.Documents);
        }
        [HttpPost]
        public IEnumerable<Account> ImportMongoDB()
        {
            try
            {
                MyDB myDB = new MyDB("10.100.0.164", "bookStoreDb");
                IAccountServices account = new AccountSevices(myDB);
                var accounts = account.getListAccounts();
                _searchAccountService.SaveManyAsync(accounts.Result.ToArray());
                return accounts.Result;
            }
            catch { return null; }
        }
    }
}
