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

        #region RadnikTehnickeStruke
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

        [HttpDelete]
        [Route("IzbrisiRadnikaTehnickeStruke/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteRadnikaTehnickeStruke(int id)
        {
            try
            {
                DataProvider.IzbrisiRadnikaTehnickeStruke(id);
                return Ok();
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

        [HttpPut]
        [Route("AzurirajRadnikaTehnickeStruke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajRadnikaTehnickeStruke([FromBody] RadnikTehnickeStrukeView rts)
        {
            try
            {

                DataProvider.AzurirajRadnikaTehnickeStruke(rts);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
        #endregion

        #region RadnikEkonomskeStruke
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

        [HttpDelete]
        [Route("IzbrisiRadnikaEkonomskeStruke/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteRadnikaEkonomskeStruketruke(int id)
        {
            try
            {
                DataProvider.IzbrisiRadnikaEkonomskeStruke(id);
                return Ok();
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

        [HttpPut]
        [Route("AzurirajRadnikaEkonomskeStruke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajRadnikaEkonomskeStruke([FromBody] RadnikEkonomskeStrukeView rts)
        {
            try
            {

                DataProvider.AzurirajRadnikaEkonomskeStruke(rts);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
        #endregion

        #region NekiDrugiZaposleni
        [HttpPost]
        [Route("DodajNekogDrugogZaposlenog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNekiDrugiZaposleni([FromBody] ZaposleniView r)
        {
            try
            {
                DataProvider.DodajNekogDrugogZaposlenog(r);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("IzbrisiNekogDrugogZaposlenog/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteNekiDrugiZaposleni(int id)
        {
            try
            {
                DataProvider.IzbrisiNekogDrugogZaposlenog(id);
                return Ok();
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
        #endregion

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
    }
}
