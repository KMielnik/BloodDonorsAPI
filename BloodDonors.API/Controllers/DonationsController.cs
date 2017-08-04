using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;
using BloodDonors.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BloodDonors.API.Controllers
{
    [Route("api/[controller]")]
    public class DonationsController : Controller
    {
        private readonly IBloodDonationService bloodDonationService;

        public DonationsController(IBloodDonationService bloodDonationService)
        {
            this.bloodDonationService = bloodDonationService;
        }

        [HttpGet("honorary")]
        public async Task<IActionResult> GetHonoraryDonors()
        {
            IEnumerable<DonorScoreDTO> honoraryDonors = await bloodDonationService.GetHonoraryDonorsAsync();
            return Ok(honoraryDonors);
        }

        [HttpGet("allBloodVolume")]
        public async Task<IActionResult> GetAllBloodVolume()
        {
            var allBloodVolume = await bloodDonationService.HowMuchBloodHasBeenDonatedEver();
            return Ok(allBloodVolume);
        }
    }
}
