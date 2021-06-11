using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotornaVozilaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotornaVozilaWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NezavisniEkonomistaController : ControllerBase
    {
        [HttpGet]
        [Route("VratiNezavisneEkonomiste")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNezavisniEkonomista()
        {
            try
            {
                return new JsonResult(DataProvider.VratiNezavisneEkonomiste());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
