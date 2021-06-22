using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotornaVozilaLibrary;
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

    }
}
