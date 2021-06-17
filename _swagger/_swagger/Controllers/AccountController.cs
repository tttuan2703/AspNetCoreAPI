using _swagger.Models;
using _swagger.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;
        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        //GET: UserController
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(_accountServices.getListAccounts().Result);
        }

        // GET: UserController/Details/5
        [HttpGet("{id}", Name = "GetAccount")]
        public ActionResult GetAccount(string id)
        {
            return Ok(_accountServices.getAccount(id).Result);
        }

        // POST: UserController/Create
        [HttpPost]
        public IActionResult CreateAccount(AccountViewModel _account)
        {
            Account account = _accountServices.createAccount(_account);
            if (account != null)
            {
                ConnectKafka.KafkaProcedurerHostedService.SendToKafka("Regiter_Account1", JsonConvert.SerializeObject(account));
                return CreatedAtRoute("GetAccount", new { id = account.Id }, account);
            }
            else
                return Ok("No success with validation!");
        }

    }
}
