using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MotornaVozilaLibrary.DTOs;
using MotornaVozilaLibrary;

namespace MotornaVozilaWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZaposleniController : ControllerBase
    {
        [HttpPost]
        [Route("DodajRadnikaTehnickeStruke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddRadnikaTehnickeStruke([FromBody] RadnikTehnickeStrukeView r)
        {
            try
            {
                DataProvider.DodajRadnikaTehnickeStruke(r);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpPost]
        [Route("DodajRadnikaEkonomskeStruke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddRadnikaEkonomskeStruke([FromBody] RadnikEkonomskeStrukeView r)
        {
            try
            {
                DataProvider.DodajRadnikaEkonomskeStruke(r);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
