using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PartyWebAPI.Models;
using PartyWebAPI.Services;

namespace PartyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {        
        private readonly IConfiguration _configuration;
        private readonly IDrinksService _drinksservice;

        public DrinksController(IConfiguration configuration, IDrinksService drinksservice)
        {
            _configuration = configuration;
            _drinksservice = drinksservice;
        }

        [HttpGet]
        [Route("GetAllDrinks")]
        public JsonResult Get()
        {
            var result = _drinksservice.getAllDrinks();

            if (result.HasErrors)
            {
                return new JsonResult("Could not retrieve drinks list");
            }
            
                return new JsonResult(result);
        }

        [HttpPost]
        [Route("AddDrink")]
        public JsonResult AddDrink([FromBody] DrinkModel drink)
        {
            if(drink == null)
            {
                return new JsonResult("Drink name is blank");
            }

            var result = _drinksservice.AddDrink(drink);
            if (!result)
            {
                return new JsonResult("Drink could not be added");
            }
            return new JsonResult("Drink Added");
        }        
    }
}
