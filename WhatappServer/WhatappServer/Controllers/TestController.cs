using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WhatappServer.Controllers
{
    public class TestController : ApiController
    {
        // GET: api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public string Get(int id)
        {
            return "value";
        }

        
        [HttpGet]
        public string GetTest(string user, string pass, string name)
        {
            return user + " , " + pass + " , " + name;
        }
        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        [Route("api/Test/hello")]
        [HttpGet]
        public async Task<string> SayHello(string name)
        {
            await Task.Delay(1000);
            return "Hello " + name;
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}
