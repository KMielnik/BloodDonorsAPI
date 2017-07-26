using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;
using BloodDonors.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IEnumerable<DonorScoreDTO>> GetHonoraryDonors()
        {
            return await bloodDonationService.GetHonoraryDonorsAsync();
        }

        [HttpGet("allBloodVolume")]
        public async Task<int> GetAllBloodVolume()
        {
            return await bloodDonationService.HowMuchBloodHasBeenDonatedEver();
        }

        [HttpGet("allBlood")]
        [Authorize(Roles = "personnel")]
        public async Task<IEnumerable<BloodDonationDTO>> GetAllBloodDonations()
        {
            return await bloodDonationService.GetAllAsync();
        }
    }
}
