using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Modells;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace WebApi.Modells
    {
    class  User
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

}
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IDataProtector _protector;
        List<User> list = null;
        public ValuesController(IDataProtectionProvider _provider)
        {
            _protector = _provider.CreateProtector("somedata");

             list = new List<User>()
            {
                new User() {Id= 1,Name="Ajay" },
                new User() {Id= 2,Name="Sagar" },
                new User() {Id= 3,Name="Deepak" },
                new User() {Id= 4,Name="Jay" },
                new User() {Id= 5,Name="Vjay" }
            };
        }
    
        // GET: api/<ValuesController>
        [HttpGet]

        public IActionResult Get()
        {
            //return list.ToList();
            
            var output = list.Select(x => new {
                Id = _protector.Protect(x.Id.ToString()),
            x.Name});
            return Ok(output);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        { 
            int id1 = int.Parse(_protector.Unprotect(id));
            var user = list.FirstOrDefault(x => x.Id == id1);
            if (user != null)
            {
               
                return Ok(user);
            }
            else
                return BadRequest();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
