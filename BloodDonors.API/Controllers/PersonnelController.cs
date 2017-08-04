using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;
using BloodDonors.Infrastructure.EntryData;
using BloodDonors.Infrastructure.Exceptions;
using BloodDonors.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetName()
        {
            var pesel = GetPeselFromRequest(Request);
            var personnelName = await personnelService.GetNameAsync(pesel);

            if (personnelName == null)
                return StatusCode(410);

            return Ok(personnelName);
        }

        [HttpGet("allTakenBlood")]
        public async Task<IActionResult> AllTakenBloodByPersonnel()
        {
            var pesel = GetPeselFromRequest(Request);
            var bloodVolumeTakenByPesel = await bloodDonationService.HowMuchBloodTakenByPersonnel(pesel);
            return Ok(bloodVolumeTakenByPesel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccount()
        {
            var pesel = GetPeselFromRequest(Request);
            var personnelDto = await personnelService.GetAsync(pesel);

            if (personnelDto == null)
                return StatusCode(410);

            return Ok(personnelDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
        {
            try
            {
                await personnelService.LoginAsync(loginCredentials.Pesel, loginCredentials.Password);
            }
            catch (UserNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            return Ok(jwtService.CreateToken(loginCredentials.Pesel, "personnel"));
        }

        [HttpGet("donor/{pesel}")]
        public async Task<IActionResult> GetDonorByPesel(string pesel)
        {
            var donorDto = await donorService.GetAsync(pesel);
            if (donorDto == null)
                return NotFound("Donor with that pesel has not been found");
            return Ok(donorDto);
        }

        [HttpGet("lastDonationBy/{donorPesel}")]
        public async Task<IActionResult> WhenDonorDonatedLastTime(string donorPesel)
        {
            var donor = await donorService.GetAsync(donorPesel);
            if (donor == null)
                return NotFound();
            return Ok(donor.LastDonated);
        }

        [HttpPost("newDonor")]
        public async Task<IActionResult> NewDonor([FromBody] RegisterDonor registerDonor)
        {
            if (registerDonor == null)
                return BadRequest("New donor can't be null");

            var password = registerDonor.Pesel;

            try
            {
                await donorService.RegisterAsync(registerDonor.Pesel, registerDonor.Name, registerDonor.BloodType,
                    registerDonor.Mail, registerDonor.Phone, password);
            }
            catch (UserAlreadyExistsException e)
            {
                return StatusCode(409, e.Message);
            }
            return Created($"personnel/donor/{registerDonor.Pesel}", Json(registerDonor));
        }

        [HttpPost("newDonation")]
        public async Task<IActionResult> NewDonation([FromBody] AddDonation donation )
        {
            if (donation == null)
                return BadRequest("New donation can't be null.");

            try
            {
                await bloodDonationService.AddBloodDonationAsync(donation.DateOfDonation, donation.Volume,
                    donation.DonorPesel, donation.BloodTakerPesel);
            }
            catch (UserNotFoundException e)
            {
                return BadRequest(e.Message);
            }

            return Created("personnel/allBlood", Json(donation));
        }

        [HttpGet("allBlood")]
        public async Task<IActionResult> GetAllBloodDonations()
        {
            IEnumerable<BloodDonationDTO> allBloodDonations = await bloodDonationService.GetAllAsync();
            return Ok(allBloodDonations);
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
