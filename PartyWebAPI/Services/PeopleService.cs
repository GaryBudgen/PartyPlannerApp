using Microsoft.Extensions.Configuration;
using PartyWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace PartyWebAPI.Services
{
    public class PeopleService : IPeopleService
    {
        private static class StoredProcedures
        {
            public const string GET_PEOPLE_LIST = "usp_GetPeopleList";
            public const string GET_PEOPLE_BY_PARTY = "usp_GetPeopleByParty";
            public const string ADD_PERSON = "usp_AddPerson";            
        }

        private static class Parameters
        {
            public const string PARTY_ID = "@PartyId";
            public const string FULL_NAME = "@FullName";
            public const string EMAIL_ADDRESS = "@EmailAddress";
        }

        private readonly IConfiguration _configuration;

        public PeopleService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public DataTable getAllPeople()
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PartyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(StoredProcedures.GET_PEOPLE_LIST, myCon))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return table;
        }

        public DataTable getPeopleByParty(int PartyId)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PartyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(StoredProcedures.GET_PEOPLE_BY_PARTY, myCon))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add(Parameters.PARTY_ID, SqlDbType.Int).Value = PartyId;
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return table;
        }

        public bool AddPerson(PersonModel person)
        {
            string sqlDataSource = _configuration.GetConnectionString("PartyAppCon");
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(StoredProcedures.ADD_PERSON, myCon))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add(Parameters.FULL_NAME, SqlDbType.NVarChar).Value = person.FullName;
                    myCommand.Parameters.Add(Parameters.EMAIL_ADDRESS, SqlDbType.NVarChar).Value = person.Email;

                    var add = myCommand.ExecuteNonQuery();

                    myCon.Close();
                }
            }
            return true;
        }
    }
}
