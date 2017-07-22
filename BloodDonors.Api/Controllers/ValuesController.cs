using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloodDonors.Infrastructure.Services;
using Newtonsoft.Json;

namespace BloodDonors.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IDonorService donorService;

        public ValuesController(IDonorService donorService)
        {
            this.donorService = donorService;
        }

        // GET api/values
        [HttpGet("{pesel}")]
        public async Task<IActionResult> Get(string pesel)
        {
            var donorsDto = await donorService.GetAllAsync();
            return Json(donorsDto);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
