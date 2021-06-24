﻿using Microsoft.AspNetCore.Http;
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
    public class KupacController : ControllerBase
    {

        #region PravnoLice

        [HttpGet]
        [Route("VratiPravnoLice")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetKupacPravnoLice()
        {
            try
            {
                return new JsonResult(DataProvider.VratiPravnoLice());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        #endregion

        #region FizickoLice

        [HttpGet]
        [Route("VratiFizickoLice")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetKupacFizickoLice()
        {
            try
            {
                return new JsonResult(DataProvider.VratiFizickoLice());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        #endregion

        [HttpGet]
        [Route("VratiKupce")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetKupac()
        {
            try
            {
                return new JsonResult(DataProvider.VratiKupce());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajKupce")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajKupce([FromBody] KupacView kupac)
        {
            try
            {
                DataProvider.AzurirajKupce(kupac);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
