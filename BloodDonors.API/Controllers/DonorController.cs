using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;
using BloodDonors.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonors.API.Controllers
{
    [Route("api/[controller]")]
    public class DonorController : Controller
    {
        private readonly IDonorService donorService;

        public DonorController(IDonorService donorService)
        {
            this.donorService = donorService;
        }

        [Authorize]
        [HttpGet("{pesel}")]
        public async Task<IActionResult> Get(string pesel)
        {
            var donorDto = await donorService.GetAsync(pesel);
            return Json(donorDto);
        }

        [HttpPost]
        public async Task Post()
        {
            await donorService.RegisterAsync("12345678901", "Elo", new BloodTypeDTO(){AboType = "O",RhType = '-'}, "das@", "31231", "kj57nk");
        }
    }
}
