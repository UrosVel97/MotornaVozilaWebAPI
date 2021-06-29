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
    public class VlasnikController : ControllerBase
    {
        [HttpGet]
        [Route("VratiVlasnike")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetVlasnike()
        {
            try
            {
                return new JsonResult(DataProvider.VratiVlasnike());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        #region NeregistrovaniKupac

        [HttpGet]
        [Route("VratiNeregistrovaneKupce")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNeregistrovaniKupci()
        {
            try
            {
                return new JsonResult(DataProvider.VratiNeregistrovaneKupce());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("PromeniNeregistrovanogKupca")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajNeregistrovanogKupca([FromBody] VlasnikNeregistrovaniKupacView vlasnik)
        {
            try
            {
                DataProvider.AzurirajNeregistrovanogKupca(vlasnik);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajNeregistrovanogKupca")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNeregistrovanogKupca([FromBody] VlasnikNeregistrovaniKupacAddView r)
        {
            try
            {
                DataProvider.DodajNeregistrovanogKupca(r);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        #endregion

        #region RegistrovaniKupac
        [HttpGet]
        [Route("VratiRegistrovaneKupce")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetRegistrovaniKupci()
        {
            try
            {
                return new JsonResult(DataProvider.VratiRegistrovaneKupce());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DodajRegistrovanogKupca")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddRegistrovanogKupca([FromBody] VlasnikRegistrovaniKupacAddView r)
        {
            try
            {
                DataProvider.DodajRegistrovanogKupca(r);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion




    }
}
