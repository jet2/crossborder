using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rest10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassageController : ControllerBase
    {
        private IConfiguration configuration;
        //internal string CString;
        internal string CStringW;
        public PassageController(IConfiguration iConfig)
        {
            configuration = iConfig;
            string dbConn2 = configuration.GetValue<string>("MySettings:pathToDatabase");
            
//            CString = $"Data Source={dbConn2};Mode=ReadOnly;Cache=Shared;";
            CStringW = $"Data Source={dbConn2};Mode=ReadWrite;Cache=Shared;";
        }

        
        internal Dictionary<string, int> ParamsIndexes = new Dictionary<string, int>
        {
            { "card", 0 },
            { "tabnom", 1 },
            { "fio", 2 },
            { "operation", 3 },
            { "delivered", 4 },
        };

        internal int hoursMaxInterval = 240;

        /*
        // GET: api/<passage>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        //api/passage/hot?isdaily=1
        [HttpGet("hot")]
        public IEnumerable<PassageFIO> getHotPassagesFIODB(int isDaily)
        {
            List<PassageFIO> lwp = new List<PassageFIO>();
            long timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string where_clause = " where p.isСhecked = 0";
            if (isDaily == 1)
            {
                where_clause = $" where {timestampUTC}-p.timestampUTC <= 60*60*24 ";
            }
            try
            {
                string qry_select = "SELECT p.passageID, p.timestampUTC, p.card, p.isOut, p.kppId, w.tabnom, p.isManual, p.isDelivered, p.description, p.isСhecked, p.toDelete, w.fio, w.userguid" +
                $" FROM buffer_passage p left join buffer_workers w on p.userguid=w.userguid {where_clause} order by timestampUTC";
                lwp.AddRange(selectPassagesFIO(qry_select));
            }
            catch { };
            return lwp;
        }

        //api/passage/history?key=card&value=11+2345&tsbegin=161231231&hours=72
        [HttpGet("history")]
        public IEnumerable<PassageFIO> getFilteredPassagesFIODBhttp(string card, string tabnom, string fio, string operation, string delivered, string tsbeg, string tsend )
        {
            Dictionary<string,string> filters = new Dictionary<string,string>();   
            if (card != null &&  card != "") { filters.Add("card", card); };
            if (tabnom != null && tabnom != "") { filters.Add("tabnom", tabnom); };
            if (fio != null && fio != "") { filters.Add("fio", fio); };
            if (operation != null && operation != "") { filters.Add("operation", operation); };
            if (delivered != null && delivered != "") { filters.Add("delivered", delivered); };
            if (tsbeg != null && tsbeg != "" && tsend!="") { filters.Add("tsbeg", tsbeg); filters.Add("tsend", tsend); };
            
            List<PassageFIO> lwp = new List<PassageFIO>();
            List<string> filters_array = new List<string>();
            string from_clause = " FROM buffer_passage p left join buffer_workers w on p.userguid = w.userguid ";

            if (filters.ContainsKey("tsbeg") && filters.ContainsKey("tsend"))
            {
                filters_array.Add($" p.timestampUTC >= {filters["tsbeg"]} and p.timestampUTC <= {filters["tsend"]} ");
            }
            if (filters.ContainsKey("card"))
            {
                filters_array.Add($" p.card='{filters["card"]}' ");
            }
            if (filters.ContainsKey("tabnom"))
            {
                filters_array.Add($" w.tabnom='{filters["tabnom"]}' ");
            }
            if (filters.ContainsKey("fio"))
            {
                filters_array.Add($" w.fio LIKE '%{filters["fio"]}%'");
            }
            if (filters.ContainsKey("operation"))
            {
                filters_array.Add($" p.isOut = {filters["operation"]} ");
            }
            if (filters.ContainsKey("delivered"))
            {
                filters_array.Add($" p.isDelivered = {filters["delivered"]} ");
            }

            string assembly_filters = String.Join(" and ", filters_array);
            string where_clause = $" where 1=1 " + ((assembly_filters.Length > 0) ? " and " + assembly_filters : "");

            try
            {
                string qry_select = "SELECT p.passageID, p.timestampUTC, p.card, p.isOut, p.kppId, w.tabnom, p.isManual, p.isDelivered, p.description, p.isСhecked, p.toDelete, w.fio, w.userguid " +
                $" {from_clause} {where_clause} order by p.timestampUTC";
                lwp.AddRange(selectPassagesFIO(qry_select));
            }
            catch { };
            return lwp;
        }


        //api/passage/single?id=33333
        [HttpGet("single")]
        public IEnumerable<PassageFIO> getPassageFIOByPassageIdDB(string id)
        {
            List<PassageFIO> lwp = new List<PassageFIO>();
            string from_clause = " FROM buffer_passage p left join buffer_workers w on p.userguid = w.userguid";

            string where_clause = $" where p.passageID={id} ";
            try
            {
                string qry_select = "SELECT p.passageID, p.timestampUTC, p.card, p.isOut, p.kppId, w.tabnom, p.isManual, p.isDelivered, p.description, p.isСhecked, p.toDelete, w.fio, w.userguid " +
                        $" {from_clause}  {where_clause} order by p.timestampUTC";
                lwp.AddRange(selectPassagesFIO(qry_select));
            }
            catch { }
            return lwp;
        }


        //api/passage/lastcard?card=11+5678
        [HttpGet("lastcard")]
        public IEnumerable<PassageFIO> getLastPassageFIOByCardDB(string card)
        {
            List<PassageFIO> lwp = new List<PassageFIO>();
            string from_clause = " FROM buffer_passage p left join buffer_workers w on p.userguid = w.userguid";

            string where_clause = $" where p.card='{card}'";
            try
            {
                string qry_select = "SELECT p.passageID, p.timestampUTC, p.card, p.isOut, p.kppId, w.tabnom, p.isManual, p.isDelivered, p.description, p.isСhecked, p.toDelete, w.fio, w.userguid " +
                        $" {from_clause}  {where_clause} order by passageID desc LIMIT 1";
                lwp.AddRange(selectPassagesFIO(qry_select));
            }
            catch { }
            return lwp;
        }

        // internal selector with parametrized SQL
        private List<PassageFIO> selectPassagesFIO(string qry_select)
        {
            List<PassageFIO> myPassages = new List<PassageFIO>();
            using (var connection = new SqliteConnection(CStringW))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = qry_select;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PassageFIO single_pass = new PassageFIO();
                        try
                        {
                            single_pass.passageID = reader.GetInt64(0);
                            single_pass.timestampUTC = reader.GetDouble(1);
                            single_pass.card = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            single_pass.operCode = reader.GetInt16(3);
                            single_pass.kppId = reader.GetString(4);
                            single_pass.tabnom = reader.IsDBNull(5) ? 0 : reader.GetInt64(5);
                            single_pass.isManual = reader.GetInt16(6);
                            single_pass.isDelivered = reader.GetInt16(7);
                            single_pass.description += reader.IsDBNull(8) ? String.Empty : reader.GetString(8);
                            single_pass.toDelete = reader.GetInt16(10);
                            single_pass.fio = reader.IsDBNull(11) ? "" : reader.GetString(11).Replace('@', ' ');
                            single_pass.userguid = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{single_pass.passageID} " + ex.Message);
                        }
                        myPassages.Add(single_pass);
                    }
                }
                connection.Close();
            }
            return myPassages;
        }

        // POST api/Passage
        [HttpPost]
        public void Post([FromBody] Passage myPassage)
        {
            Console.WriteLine("INSERT:"+ CStringW);
            //Passage myPassage = JsonConvert.DeserializeObject<Passage>(value);
            using (SqliteConnection Connect = new SqliteConnection(CStringW))
                {
                    string commandText = "INSERT INTO buffer_passage ([timestampUTC], [card], [IsOUT], [KPPID], [userguid],[isManual],[description],[isСhecked])"+
                                       $"VALUES({myPassage.timestampUTC}, '{myPassage.card}', {myPassage.operCode}, '{Environment.MachineName}', '{myPassage.userguid}',"+
                                       $"{myPassage.isManual},'{myPassage.description}', 0)";
                    SqliteCommand Command = new SqliteCommand(commandText, Connect);
                    Connect.Open();
                    Command.ExecuteNonQuery();
                    Connect.Close();
                    Console.WriteLine("INSERTED!!!!!!!!!!!!!!");
                }
        }

        // PUT api/Passage/good
        // PUT api/Passage/red
        // PUT api/Passage/check
        // PUT api/Passage/markdelete
        [HttpPut("{mode}")]
        public void updatePassage(string mode, [FromBody] Passage p)
        {
            if (mode == "good")
            {
                using (var connection = new SqliteConnection(CStringW))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"update buffer_passage set card='{p.card}', IsOUT={p.operCode}, userguid='{p.userguid}', description='{p.description}' where passageId={p.passageID} and isDelivered=0";
                    command.ExecuteNonQuery();
                    command.CommandText = $"update buffer_passage set card='{p.card}', IsOUT={p.operCode}, userguid='{p.userguid}', description='{p.description}', isDelivered=2 where passageId={p.passageID} and isDelivered>0";
                    command.ExecuteNonQuery();
                    Console.WriteLine("GOOD Updated!!!!!!!!!!!!!!");
                    connection.Close();
                }
            }
            if (mode == "red")
            {
                using (var connection = new SqliteConnection(CStringW))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"update buffer_passage set description='{p.description}', isOut={p.operCode} where passageID = {p.passageID} and isDelivered=0";
                    command.ExecuteNonQuery();
                    command.CommandText = $"update buffer_passage set description='{p.description}', isOut={p.operCode}, isDelivered=2 where passageID = {p.passageID} and isDelivered>0";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Red Updated!!!!!!!!!!!!!!");
                    connection.Close();
                }
            }
            if (mode == "check")
            {
                using (var connection = new SqliteConnection(CStringW))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"update buffer_passage set isСhecked=1 where isСhecked=0";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Checked!!!!!!!!!!!!!!");
                    connection.Close();
                }
            }
            if (mode == "markdelete")
            {
                using (var connection = new SqliteConnection(CStringW))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"update buffer_passage set toDelete=1 where passageID = {p.passageID} and isDelivered=0";
                    command.ExecuteNonQuery();
                    // просим обновить доставленное, и в БД УЯ тоже
                    command.CommandText = $"update buffer_passage set toDelete=1, isDelivered=2 where passageID = {p.passageID} and isDelivered>0";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Delete marked!!!!!!!!!!!!!!");
                    connection.Close();
                }
            }
        }

        // DELETE api/<passage>/5
        [HttpDelete("{id}")]
        public void deleteManualPassageByID(int id)
        {
                using (var connection = new SqliteConnection(CStringW))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"delete from buffer_passage where passageID = {id} and isDelivered=0";
                    command.ExecuteNonQuery();
                    command.CommandText = $"update buffer_passage set toDelete=1 and description = '[deleted manually]' + description where passageID = {id} and isDelivered>0";
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            Console.WriteLine("KILLED!!!!!!!!!!!!!!");
        }


        /*
         *         // GET api/<passage>/5
                [HttpGet("{id}")]

                public string Get(int id)
                {
                    return "value";
                }

                // POST api/<passage>
                [HttpPost]
                public void Post([FromBody] string value)
                {
                }

                // PUT api/<passage>/5
                [HttpPut("{id}")]
                public void Put(int id, [FromBody] string value)
                {
                }

                // DELETE api/<passage>/5
                [HttpDelete("{id}")]
                public void Delete(int id)
                {
                }
        */
    }




}
/*
       private void updateWorkers(object sender, DoWorkEventArgs e)
        {
            return;
            restSrvState = false;
            // получаем стамп последнего обновления работников с сервера
            var client = new RestClient($"{restServerAddr}/workers/update_ts");
            client.Timeout = 5000;
            var request = new RestRequest(Method.GET);
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            tsUpdated local_updated = new tsUpdated();
            local_updated.timestampUTC = -1;
            tsUpdated remote_updated = new tsUpdated();
            remote_updated.timestampUTC = -2;
            try
            {
                IRestResponse response = client.Execute(request);
                remote_updated = JsonConvert.DeserializeObject<tsUpdated>(response.Content);
                // получаем стамп последнего обновления работников локальный

                restSrvState = true;
            }
            catch { }
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT timestampUTC FROM workers_lastupdate LIMIT 1
                ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        local_updated.timestampUTC = reader.GetDouble(0);
                    }
                }
                // если даты отличаются - скачиваем и обновляем локальный персонал
                if (remote_updated != null)
                {
                    if (remote_updated.timestampUTC != local_updated.timestampUTC)
                    {
                        // скачиваем персонал
                        var client2 = new RestClient($"{restServerAddr}/workers/");
                        client2.Timeout = 5000;
                        var request2 = new RestRequest(Method.GET);
                        var body2 = @"";
                        request2.AddParameter("text/plain", body2, ParameterType.RequestBody);
                        // получем json array
                        IRestResponse response2 = client2.Execute(request);
                        // заполняем список записей
                        List<WorkerPerson> remote_workers = JsonConvert.DeserializeObject<List<WorkerPerson>>(response2.Content);

                        // очищаем приемную таблицу
                        var command3 = connection.CreateCommand();
                        command3.CommandText = $"delete from buffer_workers_input";
                        command3.ExecuteNonQuery();

                        if (remote_workers.Count > 0)
                        {
                            // каждую персону из списка вливаем в приемную таблицу
                            foreach (WorkerPerson wp in remote_workers)
                            {
                                if (wp.card != "" & wp.fio != "" & wp.tabnom != 0)
                                {
                                    command3.CommandText = $"insert into buffer_workers_input(card,fio,tabnom,userguid,isGuardian) values('{wp.card}','{wp.fio}',{wp.tabnom},'{wp.userguid}',0)";
                                    command3.ExecuteNonQuery();
                                }
                            }

                            // очищаем локальную таблицу работников
                            command3.CommandText = $"delete from buffer_workers";
                            command3.ExecuteNonQuery();

                            // мгновенно переливаем скачанный персонал в таблицу работников
                            command3.CommandText = $"insert into buffer_workers select * from buffer_workers_input";
                            command3.ExecuteNonQuery();

                            // обновляем локальный стамп персон
                            command3.CommandText = $"update workers_lastupdate set timestampUTC={remote_updated.timestampUTC}";
                            command3.ExecuteNonQuery();
                        }
                    }
                }
            }
        }


        private void sendPassage(object sender, DoWorkEventArgs e)
        {
            // restsharp
            // выбираем первый неотправленный passage и если массив непустой - отправляем через rest
            // если успешно отправлось - помечаем passageID отправленным и обновляем главную таблицу
            // получить наименьший локальный проход
            // отправить 
            // оценить результат
            // обновить состояние или не обновлять
            // удаление доставленных - другим методом
            long exID = -2;

            // Первый бит формируем тело
            Passage firstUndelivered = getFirstUndelivered();
            Passage1bit firstUndelivered1bit = bit1PassageByPassage(firstUndelivered);

            if (firstUndelivered.passageID > -1)
            {
                var client = new RestClient($"{restServerAddr}/passages/");
                client.Timeout = 5000;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                do
                {
                    var body = JsonConvert.SerializeObject(firstUndelivered1bit);
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.IsSuccessful)
                    {
                        string qry_update_mark_id_asdelivered = @"update buffer_passage set isDelivered=1
                            where isDelivered=0 and passageID=" + $"{firstUndelivered.passageID}";
                        using (var connection = new SQLiteConnection(sqlite_connectionstring))
                        {
                            connection.Open();
                            var command = connection.CreateCommand();
                            command.CommandText = qry_update_mark_id_asdelivered;
                            command.ExecuteNonQuery();

                            send_cnt++;
                        }
                    }
                    exID = firstUndelivered.passageID;
                    firstUndelivered = getFirstUndelivered();
                    firstUndelivered1bit = bit1PassageByPassage(firstUndelivered);

                    if (exID == firstUndelivered.passageID)
                    {

                        break;
                    }
                } while (firstUndelivered.passageID != -1);
            }

        }
  

        private Passage getFirstUndelivered()
        {
            long tsUTC = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            // для отправки выбирается только первая запись старше 45 секунд
            string qry_select_first_undelivered = @"SELECT passageID, timestampUTC, card, isOut, kppId, tabnom, isManual,description
                FROM buffer_passage " +
                $" where isDelivered=0 and {tsUTC}-timestampUTC>={delaySendSecods} " +
                " order by passageID limit 1";

            Passage first_pass = new Passage();
            first_pass.passageID = -1;
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = qry_select_first_undelivered;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        first_pass.passageID = reader.GetInt64(0);
                        first_pass.rowID = runningInstanceGuid +"::"+ first_pass.passageID.ToString();
                        first_pass.timestampUTC = reader.GetDouble(1);
                        first_pass.card = reader.GetString(2);
                        first_pass.operCode = reader.GetInt16(3);
                        first_pass.kppId = reader.GetString(4);
                        first_pass.tabnom = reader.GetInt32(5);
                        first_pass.isManual = reader.GetInt16(6);
                        first_pass.description += reader.IsDBNull(7) ? String.Empty : reader.GetString(7);
                    }
                }
            }
            return first_pass;
        }



 очистка сверх 30 дней????
       private void button1_Click(object sender, EventArgs e)
        {
            string qry_clean_delivered = @"delete 
                FROM buffer_passage
                where isDelivered=1";

            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = qry_clean_delivered;
                command.ExecuteNonQuery();
            }
            MainTableReload(sender, e);
        }

это и есть очистка
        private void timerEraser_Tick(object sender, EventArgs e)
        {
            long myNowUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"delete from buffer_passage where {myNowUTC}-timestampUTC > {60*60*24*30} and isDelivered=1";
                command.ExecuteNonQuery();
            }
        }
// для воркера отправки

private void buttonPOST_Click(object sender, EventArgs e)
{
    Passage1bit bit = new Passage1bit();

    using (var connection = new SQLiteConnection(sqlite_connectionstring))
    {
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = $"select card, tabnom, isOut, timestampUTC, description from buffer_passage where passageID={labelGreenEventID.Text}";

        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                bit.bit1_system = "desktop_app";
                bit.bit1_lon = 14.0;
                bit.bit1_lat = 14.0;
                bit.bit1_id = "0";
                bit.bit1_reader_id = Environment.MachineName;
                //
                bit.bit1_card = reader.GetString(0);
                bit.bit1_tabnom = $"{reader.GetInt64(1)}";
                bit.bit1_opercode = $"{reader.GetInt64(2)}";

                bit.bit1_timestampUTC = (int)reader.GetDouble(3);
                bit.bit1_comment = reader.GetString(4);
                break;
            }
        }
    }


    // restsharp
    // выбираем первый неотправленный passage и если массив непустой - отправляем через rest
    // если успешно отправлось - помечаем passageID отправленным и обновляем главную таблицу
    // получить наименьший локальный проход
    // отправить 
    // оценить результат
    // обновить состояние или не обновлять
    // удаление доставленных - другим методом

    
    var client = new RestClient($"{restServerAddr}/reading-event/");
    client.Timeout = 5000;
    var request = new RestRequest(Method.POST);

    //            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "password");

    //          request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
    request.AddHeader("Accept", "*"+"/"+"*");
    request.AddHeader("Accept-Encoding", "gzip, deflate, br");

    request.AddHeader("Content-Type", "application/json");
    var body = JsonConvert.SerializeObject(bit);
    request.AddParameter("application/json", body, ParameterType.RequestBody);
    IRestResponse response = client.Execute(request);
    if (response.IsSuccessful)
    {
        string qry_update_mark_id_asdelivered = @"update buffer_passage set isDelivered=1
                            where isDelivered=0 and passageID=" + $"{labelGreenEventID.Text}";
        using (var connection = new SQLiteConnection(sqlite_connectionstring))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = qry_update_mark_id_asdelivered;
            command.ExecuteNonQuery();

            send_cnt++;
        }
    }
}
*/


