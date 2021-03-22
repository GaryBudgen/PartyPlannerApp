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
    public class PartyService : IPartyService
    {
        private static class StoredProcedures
        {
            public const string GET_PARTY_LIST = "usp_GetPartyList";
            public const string GET_PARTIES_BY_PERSON = "usp_GetPartiesByPerson";
            public const string ADD_PARTY = "usp_AddParty";
            public const string ADD_PERSON_TO_PARTY = "usp_AddPersonToParty";
        }

        private static class Parameters
        {
            public const string PARTY_ID = "@PartyId";
            public const string PARTY_NAME  = "@PartyName";
            public const string LOCATION = "@Location";
            public const string START_TIME = "@StartTime";
            public const string PERSON_ID = "@PersonId";
            public const string DRINK_ID = "@DrinkId";
        }

        private readonly IConfiguration _configuration;

        public PartyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public DataTable getAllParties()
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PartyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(StoredProcedures.GET_PARTY_LIST, myCon))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return table;
        }
        public DataTable getPartiesByPerson(int PersonId)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PartyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(StoredProcedures.GET_PARTIES_BY_PERSON, myCon))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add(Parameters.PERSON_ID, SqlDbType.Int).Value = PersonId;
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return table;
        }
        public bool AddParty(PartyModel party)
        {
            string sqlDataSource = _configuration.GetConnectionString("PartyAppCon");
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(StoredProcedures.ADD_PARTY, myCon))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add(Parameters.PARTY_NAME, SqlDbType.NVarChar).Value = party.PartyName;
                    myCommand.Parameters.Add(Parameters.LOCATION, SqlDbType.NVarChar).Value = party.Location;
                    myCommand.Parameters.Add(Parameters.START_TIME, SqlDbType.DateTime).Value = party.StartTime;

                    myCommand.ExecuteNonQuery();

                    myCon.Close();
                }
            }
            return true;
        }

        public bool AddPersonToParty(GuestlistModel guestlist)
        {
            string sqlDataSource = _configuration.GetConnectionString("PartyAppCon");
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(StoredProcedures.ADD_PERSON_TO_PARTY, myCon))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add(Parameters.PARTY_ID, SqlDbType.Int).Value = guestlist.PartyId;
                    myCommand.Parameters.Add(Parameters.PERSON_ID, SqlDbType.Int).Value = guestlist.PersonId;
                    myCommand.Parameters.Add(Parameters.DRINK_ID, SqlDbType.Int).Value = guestlist.DrinkId;

                    myCommand.ExecuteNonQuery();

                    myCon.Close();
                }
                return true;
            }
        }
    }
}
