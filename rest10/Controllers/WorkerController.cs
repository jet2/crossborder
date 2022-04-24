using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rest10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private IConfiguration configuration;
        //internal string CString;
        internal string CStringW;
        public WorkerController(IConfiguration iConfig)
        {
            configuration = iConfig;
            string dbConn2 = configuration.GetValue<string>("MySettings:pathToDatabase");

            //CString = $"Data Source={dbConn2};Mode=ReadOnly;Cache=Shared;";
            CStringW = $"Data Source={dbConn2};Mode=ReadWrite;Cache=Shared;";

        }
        // GET: api/Worker/byfilter?fieldname=card?fieldvalue=11+4567
        [HttpGet("byfilter")]
        public IEnumerable<WorkerPerson> getFilteredWorkersByEntityDB(string fieldname, string fieldvalue)
        {
            Console.WriteLine($"1/getFilteredWorkersByEntityDB({fieldname},{fieldvalue})");
            List<WorkerPerson> results = new List<WorkerPerson>();

            using (var connection = new SqliteConnection(CStringW))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = $"select card, tabnom, userguid, fio  from buffer_workers where {fieldname} LIKE '%{fieldvalue}%'";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WorkerPerson wp = new WorkerPerson();
                        wp.card = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        wp.tabnom = reader.IsDBNull(1) ? 0 : reader.GetInt64(1);
                        wp.userguid = reader.GetString(2);
                        wp.fio = reader.GetString(3);
                        results.Add(wp);
                    }
                }
                connection.Close();
            }
            Console.WriteLine($"2/getFilteredWorkersByEntityDB({fieldname},{fieldvalue})");
            return results;
        }
        // GET: api/Worker/byguid?userguid=ok1-acsvasf
        [HttpGet("byguid")]
        public List<WorkerPerson> getGUIDOwnerWorkerDB(string userguid)
        {
            Console.WriteLine($"1/getGUIDOwnerWorkerDB({userguid})");
            List<WorkerPerson> results = new List<WorkerPerson>();
            using (var connection = new SqliteConnection(CStringW))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                $"SELECT userguid, fio, tabnom, jobDescription, card FROM buffer_workers_described where userguid='{userguid}' LIMIT 1";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WorkerPerson myWP = new WorkerPerson();
                        myWP.userguid = reader.GetString(0);
                        myWP.fio = reader.GetString(1);
                        myWP.tabnom = reader.IsDBNull(2) ? 0 : reader.GetInt64(2);
                        myWP.jobDescription = reader.GetString(3);
                        myWP.isGuardian = 0;
                        myWP.card = reader.IsDBNull(4) ? "" : reader.GetString(4);

                        results.Add(myWP);
                        break;
                    }
                }
                connection.Close();
            }
            Console.WriteLine($"2/getGUIDOwnerWorkerDB({userguid})");
            return results;
        }
        // GET: api/Worker/bycard?card=11+1234
        [HttpGet("bycard")]
        public List<WorkerPerson> getCardOwnerWorkerDB(string card)
        {
            Console.WriteLine($"1/getCardOwnerWorkerDB({card})");
            List<WorkerPerson> results = new List<WorkerPerson>();
            using (var connection = new SqliteConnection(CStringW))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                $"SELECT userguid, fio, tabnom, jobDescription,card FROM buffer_workers_described where card='{card}' LIMIT 1";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WorkerPerson myWP = new WorkerPerson();
                        myWP.userguid = reader.GetString(0);
                        myWP.fio = reader.GetString(1);
                        myWP.tabnom = reader.IsDBNull(2) ? 0 : reader.GetInt64(2);
                        myWP.jobDescription = reader.GetString(3);
                        myWP.isGuardian = 0;
                        myWP.card = reader.IsDBNull(4) ? "" : reader.GetString(4);
                        results.Add(myWP);
                        break;
                    }
                }
                connection.Close();
            }
            Console.WriteLine($"2/getCardOwnerWorkerDB({card})");
            return results;
        }
        /*
        // GET: api/<WorkerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WorkerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        // POST api/<WorkerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WorkerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WorkerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
