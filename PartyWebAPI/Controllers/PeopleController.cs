using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PartyWebAPI.Models;
using PartyWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PartyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPeopleService _peopleservice;        

        public PeopleController(IConfiguration configuration, IPeopleService peopleservice)
        {
            _configuration = configuration;
            _peopleservice = peopleservice;
        }

        [HttpGet]
        [Route("GetAllPeople")]
        public JsonResult Get()
        {
            var result = _peopleservice.getAllPeople();

            if (result.HasErrors)
            {
                return new JsonResult("Could not retrieve drinks list");
            }

            return new JsonResult(result);
        }

        [HttpGet("Id")]
        [Route("PeopleByParty")]
        public JsonResult PeopleByParty(int PartyId)
        {
            var result = _peopleservice.getPeopleByParty(PartyId);

            if (result.HasErrors)
            {
                return new JsonResult("Could not retrieve guest list");
            }

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("AddPerson")]
        public JsonResult AddPerson([FromBody] PersonModel person)
        {
            if(person == null)
            {
                return new JsonResult("Person is blank");
            }

            var result = _peopleservice.AddPerson(person);
            if (!result)
            {
                return new JsonResult("Person could not be added");
            }
            return new JsonResult("Person Added");
        }
    }
}
