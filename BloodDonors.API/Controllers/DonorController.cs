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

namespace BloodDonors.API.Controllers
{
    [Route("api/[controller]")]
    public class DonorController : Controller
    {
        private readonly IDonorService donorService;
        private readonly IBloodDonationService bloodDonationService;
        private readonly IJwtService jwtService;

        public DonorController(IDonorService donorService, IBloodDonationService bloodDonationService,
            IJwtService jwtService)
        {
            this.donorService = donorService;
            this.bloodDonationService = bloodDonationService;
            this.jwtService = jwtService;
        }

        [HttpGet("name")]
        [Authorize(Roles = "donor")]
        public async Task<string> GetName()
        {
            var pesel = GetPeselFromRequest(Request);

            var donorName = await donorService.GetNameAsync(pesel);
            return donorName;
        }

        [Authorize(Roles = "donor")]
        [HttpGet("account")]
        public async Task<IActionResult> GetAccount()
        {
            var pesel = GetPeselFromRequest(Request);

            var donorDto = await donorService.GetAsync(pesel);
            return Json(donorDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] LoginCredentials loginCredentials)
        {
            await donorService.LoginAsync(loginCredentials.Pesel, loginCredentials.Password);
            return Json(jwtService.CreateToken(loginCredentials.Pesel, "donor"));
        }

        [Authorize(Roles = "donor")]
        [HttpGet("volume")]
        public async Task<int> GetOverallBloodVolume()
        {
            var pesel = GetPeselFromRequest(Request);

            var bloodVolumeDonatedByDonor = (await bloodDonationService.GetAllAsync())
                .Where(x => x.Donor.Pesel == pesel)
                .Sum(x => x.Volume);
            return bloodVolumeDonatedByDonor;
        }

        [Authorize(Roles = "donor")]
        [HttpGet("whenabletodonate")]
        public async Task<DateTime> GetWhenAbleToDonateAgain()
        {
            var pesel = GetPeselFromRequest(Request);
            return await donorService.WhenWillBeAbleToDonateAgainAsync(pesel);
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
