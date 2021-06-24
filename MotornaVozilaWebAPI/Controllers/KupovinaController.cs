using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotornaVozilaLibrary;
using MotornaVozilaLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotornaVozilaWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KupovinaController : ControllerBase
    {

        [HttpGet]
        [Route("VratiKupovinu")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetKupovina()
        {
            try
            {
                return new JsonResult(DataProvider.VratiKupovine());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("PromeniKupovinu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajKupovine([FromBody] KupovinaView kupovina)
        {
            try
            {
                DataProvider.AzurirajKupovine(kupovina);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("IzbrisiKupovinu/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteKupovina(int id)
        {
            try
            {
                DataProvider.IzbrisiKupovinu(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DodajKupovinu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddKupovina([FromBody] KupovinaAddView r)
        {
            try
            {
                DataProvider.DodajKupovinu(r);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
