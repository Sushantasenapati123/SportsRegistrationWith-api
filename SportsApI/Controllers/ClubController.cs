using Exam.Domain.Sports;
using Exam.Irepository.ISport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {

        private readonly SpotInterface _productService;
        public ClubController(SpotInterface prodService)
        {
            _productService = prodService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spot>>> GetallClub()
        {
            return await _productService.BindClub();
        }
    }
}
