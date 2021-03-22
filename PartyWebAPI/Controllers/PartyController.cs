using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PartyWebAPI.Models;
using PartyWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace PartyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartyController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPartyService _partyservice;

        public PartyController(IConfiguration configuration, IPartyService partyservice)
        {
            _configuration = configuration;
            _partyservice = partyservice;
        }

        [HttpGet]
        [Route("GetAllParties")]
        public JsonResult GetAllParties()
        {
            var result = _partyservice.getAllParties();

            if (result.HasErrors)
            {
                return new JsonResult("Could not retrieve party list");
            }

            return new JsonResult(result);
        }

        [HttpGet("Id")]
        [Route("PartiesByPerson")]
        public JsonResult PartiesByPerson(int PersonId)
        {
           
            var result = _partyservice.getPartiesByPerson(PersonId);

            if (result.HasErrors)
            {
                return new JsonResult("Could not retrieve party list");
            }

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("AddParty")]
        public JsonResult AddParty([FromBody] PartyModel party)
        {
            var result = _partyservice.AddParty(party);
            if (!result)
            {
                return new JsonResult("Party could not be added");
            }

            return new JsonResult("Party Added");
        }

        [HttpPost]
        [Route("AddPersonToParty")]
        public JsonResult AddPersonToParty([FromBody] GuestlistModel guestlist)
        {
            var result = _partyservice.AddPersonToParty(guestlist);
            if (!result)
            {
                return new JsonResult("Person could not be added To Party");
            }
                
            return new JsonResult("Person Added To Party");
        }

    }
}
