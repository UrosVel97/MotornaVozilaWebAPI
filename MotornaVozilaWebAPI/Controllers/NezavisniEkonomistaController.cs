using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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



        [HttpPost]
        [Route("DodajNEkonomistu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNEkonomistu([FromBody] NezavisniEkonomistaView n)
        {
            try
            {
                DataProvider.DodajNEkonomistu(n);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("IzbrisiNEkonomistu/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteNEkonomistu(int id)
        {
            try
            {
                DataProvider.IzbrisiNEkonomistu(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        /*Ovo je komentar 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */




        [HttpPut]
        [Route("PromeniNezavisnogEkonomistu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajNezavisneEkonomiste([FromBody] NezavisniEkonomistaView nezavisni)
        {
            try
            {


                /*
                 * 
                 * 
                 * 
                 * 
                 * 
                 */
                DataProvider.AzurirajNezavisneEkonomiste(nezavisni);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
