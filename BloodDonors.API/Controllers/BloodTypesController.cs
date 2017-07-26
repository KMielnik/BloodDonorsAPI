using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;
using BloodDonors.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonors.API.Controllers
{
    [Route("api/[controller]")]
    public class BloodTypesController : Controller
    {
        private readonly IBloodTypeService bloodTypeService;

        public BloodTypesController(IBloodTypeService bloodTypeService)
        {
            this.bloodTypeService = bloodTypeService;
        }

        [HttpGet]
        public async Task<IEnumerable<BloodTypeDTO>> Get()
        {
            return await bloodTypeService.GetAllAsync();
        }
    }
}
