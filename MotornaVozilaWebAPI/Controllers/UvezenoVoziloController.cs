using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MotornaVozilaLibrary.DTOs;
using MotornaVozilaLibrary;

namespace MotornaVozilaWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UvezenoVoziloController : Controller
    {
        [HttpGet]
        [Route("VratiVoziloKojeJeProdato")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetVoziloKojeJeProdato()
        {
            try
            {
                return new JsonResult(DataProvider.VratiVoziloKojeJeProdato());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiVoziloKojeNijeProdato")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetVoziloKojeNijeProdato()
        {
            try
            {
                return new JsonResult(DataProvider.VratiVoziloKojeNijeProdato());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("PromeniVoziloKojeNijeProdato")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajVoziloKojeNijeProdato([FromBody] VoziloKojeNijeProdatoView vozilo)
        {
            try
            {
                DataProvider.AzurirajVoziloKojeNijeProdato(vozilo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("PromeniVoziloKojeJeProdato")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajVoziloKojeJeProdato([FromBody] VoziloKojeJeProdatoView vozilo)
        {
            try
            {
                DataProvider.AzurirajVoziloKojeJeProdato(vozilo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DodajVoziloKojeNijeProdato")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddVoziloKojeNijeProdato([FromBody] VoziloKojeNijeProdatoAddView v)
        {
            try
            {
                DataProvider.DodajVoziloKojeNijeProdato(v);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DodajVoziloKojeJeProdato")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddVoziloKojeJeProdato([FromBody] VoziloKojeJeProdatoAddView v)
        {
            try
            {
                DataProvider.DodajVoziloKojeJeProdato(v);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("IzbrisiVoziloKojeNijeProdato/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVoziloKojeNijeProdato(int id)
        {
            try
            {
                DataProvider.IzbrisiVoziloKojeNijeProdato(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("IzbrisiVoziloKojeJeProdato/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVoziloKojeJeProdato(int id)
        {
            try
            {
                DataProvider.IzbrisiVoziloKojeJeProdato(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
