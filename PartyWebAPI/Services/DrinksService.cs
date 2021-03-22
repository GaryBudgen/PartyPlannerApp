using Microsoft.Extensions.Configuration;
using PartyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PartyWebAPI.Services
{
    public class DrinksService : IDrinksService
    {
        private static class StoredProcedures
        {
            public const string GET_DRINKS_LIST = "usp_GetDrinksList";
            public const string ADD_DRINK = "usp_AddDrink";
        }

        private static class Parameters
        {
            public const string DRINK_NAME = "@DrinkName";
        }

            private readonly IConfiguration _configuration;        

        public DrinksService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable getAllDrinks()
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PartyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(StoredProcedures.GET_DRINKS_LIST, myCon))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }

                return table;
            }
        }

        public bool AddDrink(DrinkModel drink)
        {
            string sqlDataSource = _configuration.GetConnectionString("PartyAppCon");
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(StoredProcedures.ADD_DRINK, myCon))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add(Parameters.DRINK_NAME
                        , SqlDbType.NVarChar).Value = drink.DrinkName;

                    var result = myCommand.ExecuteNonQuery();

                    myCon.Close();
                }         
            }
            return true;
        }



    }
}
