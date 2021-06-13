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

        [HttpGet]
        [Route("VratiZaposlene")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetZaposleni()
        {
            try
            {
                return new JsonResult(DataProvider.VratiZaposlene());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiRadnikaEkonomskeStruke")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetRadnikEkonomskeStruke()
        {
            try
            {
                return new JsonResult(DataProvider.VratiRadnikaEkonomskeStruke());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiRadnikaTehnickeStruke")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetRadnikTehnickeStruke()
        {
            try
            {
                return new JsonResult(DataProvider.VratiRadnikaTehnickeStruke());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiNekeDrugeZaposlene")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNekiDrugiZaposleni()
        {
            try
            {
                return new JsonResult(DataProvider.VratiNekeDrugeZaposlene());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
