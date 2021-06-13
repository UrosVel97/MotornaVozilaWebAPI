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
    }
}
