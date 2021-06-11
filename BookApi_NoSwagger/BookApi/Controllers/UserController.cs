using AutoMapper;
using BookApi.Models.Account;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UsersServices _users;
        private UserViewModel _userView;
        private IMapper _mapper;
        public UserController(UsersServices usersServices, IMapper mapper)
        {
            _users = usersServices;
            _mapper = mapper;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetListUser()
        {
            var users = _users.getListUser();
            return await users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}", Name = "getUser")]
        public async Task<AppUser> GetUser(string _id)
        {
            var user = _users.getUser(_id);
            return await user;
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<string> Post([FromBody] UserViewModel user)
        {
            AppUser appUser = new AppUser();
            appUser = _mapper.Map<AppUser>(user);
            await _users.createUser(appUser);
            //appUser._id = (new ObjectId())+"";
            CreatedAtRoute("GetBook", new { id = appUser._id.ToString() }, appUser);
            return "";
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
