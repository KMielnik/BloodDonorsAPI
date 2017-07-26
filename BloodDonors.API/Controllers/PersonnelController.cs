using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;
using BloodDonors.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BloodDonors.API.Controllers
{
    [Authorize(Roles = "personnel")]
    [Route("api/[controller]")]
    public class PersonnelController : Controller
    {
        private readonly IPersonnelService personnelService;
        private readonly IBloodDonationService bloodDonationService;
        private readonly IDonorService donorService;
        private readonly IJwtService jwtService;

        public PersonnelController(IPersonnelService personnelService, IBloodDonationService bloodDonationService,
            IDonorService donorService, IJwtService jwtService)
        {
            this.personnelService = personnelService;
            this.bloodDonationService = bloodDonationService;
            this.donorService = donorService;
            this.jwtService = jwtService;
        }

        [HttpGet("name")]
        public async Task<string> GetName()
        {
            var pesel = GetPeselFromRequest(Request);
            var personnelName = await personnelService.GetNameAsync(pesel);
            return personnelName;
        }

        [HttpGet("allTakenBlood")]
        public async Task<int> AllTakenBloodByPersonnel()
        {
            var pesel = GetPeselFromRequest(Request);
            var bloodVolumeTakenByPesel = await bloodDonationService.HowMuchBloodTakenByPersonnel(pesel);
            return bloodVolumeTakenByPesel;
        }

        [HttpGet("account")]
        public async Task<IActionResult> GetAccount()
        {
            var pesel = GetPeselFromRequest(Request);
            var personnel = await personnelService.GetAsync(pesel);

            return Json(personnel);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
        {
            await personnelService.LoginAsync(loginCredentials.Pesel,loginCredentials.Password);
            return Json(jwtService.CreateToken(loginCredentials.Pesel, "personnel"));
        }

        [HttpGet("lastDonationBy/{donorPesel}")]
        public async Task<IActionResult> WhenDonorDonatedLastTime(string donorPesel)
        {
            var donor = await donorService.GetAsync(donorPesel);
            if (donor == null)
                return NotFound();
            return Json(donor.LastDonated);
        }

        [HttpPost("newDonor")]
        public async Task NewDonor([FromBody] RegisterDonor registerDonor)
        {
            var password = registerDonor.Pesel;
            await donorService.RegisterAsync(registerDonor.Pesel, registerDonor.Name, registerDonor.BloodType, registerDonor.Mail,
                registerDonor.Phone, password);
        }

        [HttpPost("newDonation")]
        public async Task NewDonation([FromBody] AddDonation donation )
        {
            await bloodDonationService.AddBloodDonationAsync(donation.DateOfDonation, donation.Volume,
                donation.DonorPesel, donation.BloodTakerPesel);
        }

        private JwtSecurityToken GetTokenFromRequest(HttpRequest request)
        {
            string header = Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization")).Value;
            var token = header.Replace("Bearer ", string.Empty);

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwt;
        }

        private string GetPeselFromRequest(HttpRequest request)
        {
            var jwt = GetTokenFromRequest(Request);
            return jwt.Id;
        }
    }
}
