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
    public class VoziloPrimljenoNaServisController : ControllerBase
    {
        [HttpPost]
        [Route("DodajVozilo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddVozilo([FromBody] VoziloPrimljenoNaServisAddView r)
        {
            try
            {
                DataProvider.DodajVoziloNaServis(r);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("IzbrisiVozilo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVozilo(int id)
        {
            try
            {
                DataProvider.IzbrisiVoziloSaServisa(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
