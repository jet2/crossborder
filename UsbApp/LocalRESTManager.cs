using Newtonsoft.Json;
using RestSharp;
using SQLite;
using System;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kppApp
{
    internal class LocalRESTManager
    {
        private static NLog.Logger logger;

        internal Dictionary<string, int> ParamsIndexes = new Dictionary<string, int>
        {
            { "card", 0 },
            { "tabnom", 1 },
            { "fio", 2 },
            { "operation", 3 },
            { "delivered", 4 },
        };
        private string CString;
        private string restServerAddr = "http://localhost:5000/";
        private bool useRest = false;
        private SQLiteConnection memdb=null;
        public LocalRESTManager(SQLiteConnection memDatabase, string connectionstring, bool useRest, NLog.Logger nlogger)
        {
            logger = nlogger;
            this.CString = connectionstring;
            this.useRest = useRest;
            this.memdb = memDatabase;
        }

        #region passage insert update table.db 

        public void insertPassage(Passage myPassage, bool useRest)
        {
            if (useRest)
            {
                insertPassage_REST(myPassage);
            }
            else
            {
                insertPassageDB(myPassage);
            }
        }
        public void insertPassage_REST(Passage myPassage)
        {
            var client = new RestClient($"{restServerAddr}api/Passage/");
            client.Timeout = 5000;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(myPassage);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.StatusCode.ToString());  
            }
        }
        public void insertPassageDB(Passage myPassage)
        {
            using (SQLiteConnection dbdisk = new SQLiteConnection(CString))
            {
                myPassage.kppId = Environment.MachineName;
                dbdisk.Insert(myPassage);
                logger.Info($"Проход card={myPassage.card}, userguid={myPassage.userguid}");
            }
            
            
            //db.Query<PrettyWorker>($"SELECT userguid, fio, tabnom, job, card FROM prettyworker where card='{card}' LIMIT 1");
            /*
            using (SQLiteConnection Connect = new SQLiteConnection(CString))
            {
                string commandText = @"INSERT INTO buffer_passage ([timestampUTC], [card], [IsOUT], [KPPID], [userguid],[isManual],[description],[isСhecked]) 
                                       VALUES(@timestampUTC, @card, @IsOUT, @KPPID, @userguid,@isManual,@description, 0)";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Command.Parameters.AddWithValue("@timestampUTC", myPassage.timestampUTC);
                Command.Parameters.AddWithValue("@card", myPassage.card);
                Command.Parameters.AddWithValue("@userguid", myPassage.userguid);
                Command.Parameters.AddWithValue("@IsOut", myPassage.operCode);
                Command.Parameters.AddWithValue("@KPPID", Environment.MachineName);
                Command.Parameters.AddWithValue("@isManual", myPassage.isManual);
                Command.Parameters.AddWithValue("@description", myPassage.description);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
                // MessageBox.Show("Проход записан в базу данных");
            }
            */

        }

        public void updatePassage_REST(string mode, Passage p)
        {
            var client = new RestClient($"{restServerAddr}api/Passage/{mode}");
            client.Timeout = 200;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(p);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.StatusCode.ToString());
            }
        }

        public void updatePassage(string mode, Passage p, bool useRest)
        {
            if (useRest)
            {
                updatePassage_REST(mode, p);
            }
            else
            {
                updatePassageDB(mode, p);
            }

        }
        public void updatePassageDB(string mode, Passage p)
        {
            
            if (mode == "good")
            {
                using (SQLiteConnection dbdisk = new SQLiteConnection(new SQLiteConnectionString(CString)))
                {
                    // ожидающие isDelivery=0 доставляются через POST
                    string commandText = $"update passage set card='{p.card}', tabnom='{p.tabnom}', operCode={p.operCode}, userguid='{p.userguid}', description='{p.description}' where passageId={p.passageID} and isDelivered=0";
                    dbdisk.CreateCommand(commandText).ExecuteNonQuery();

                    // доставленные isDelivery=1 обновляются через PUT
                    commandText = $"update passage set card='{p.card}', tabnom='{p.tabnom}', operCode={p.operCode}, userguid='{p.userguid}', description='{p.description}', isDelivered=2 where passageId={p.passageID} and isDelivered>0";
                    dbdisk.CreateCommand(commandText).ExecuteNonQuery();
                }
            }
            if (mode == "red")
            {
                using (SQLiteConnection dbdisk = new SQLiteConnection(new SQLiteConnectionString(CString)))
                {
                    string commandText = $"update passage set description='{p.description}', operCode={p.operCode} where passageID = {p.passageID} and isDelivered=0";
                    dbdisk.CreateCommand(commandText).ExecuteNonQuery();

                    commandText = $"update passage set description='{p.description}', operCode={p.operCode}, isDelivered=2 where passageID = {p.passageID} and isDelivered>0";
                    dbdisk.CreateCommand(commandText).ExecuteNonQuery();

                }

            }
            if (mode == "check")
            {
                using (SQLiteConnection dbdisk = new SQLiteConnection(new SQLiteConnectionString(CString)))
                {
                    string commandText = @"update passage set isChecked=1 where isChecked=0";
                    dbdisk.CreateCommand(commandText).ExecuteNonQuery();
                }
            }
 
            if (mode == "markdelete")
            {
                using (SQLiteConnection dbdisk = new SQLiteConnection(new SQLiteConnectionString(CString)))
                {
                    string commandText = $"update passage set toDelete=1 where passageID = {p.passageID} and isDelivered=0";
                    dbdisk.CreateCommand(commandText).ExecuteNonQuery();

                    commandText = $"update passage set toDelete=1, isDelivered=2 where passageID = {p.passageID} and isDelivered>0"; 
                    dbdisk.CreateCommand(commandText).ExecuteNonQuery();
                    Console.WriteLine("Delete marked!!!!!!!!!!!!!!");
                }
            }
            
        }
        #endregion passage insert update table.db 

        #region passage selectors from db 
        public List<PassageFIO> getHotPassagesFIODB(int isDaily)
        {
            List<PassageFIO> lwp = new List<PassageFIO>();
            long timestampUTC = TimeLord.UTCNow();
            string where_clause = " where p.isChecked = 0";
            if (isDaily == 1)
            {
                where_clause = $" where {timestampUTC}-p.timestampUTC <= 60*60*24 ";
            }

            using (var db = new SQLiteConnection(new SQLiteConnectionString(CString)))
            {
                // Преливаем dbdisk.Passage с сохранением PassageID в dbmem.PassageMem тогда PassageID останется как в дисковой
                memdb.DropTable<PassageMem>();
                memdb.CreateTable<PassageMem>();
                memdb.InsertAll(db.Query<PassageMem>($"select p.* from Passage p {where_clause}"));
                lwp.AddRange(memdb.Query<PassageFIO>(@"select p.*, w.second_name||' '||w.first_name||' '||w.last_name as fio
                                                       from PassageMem p 
                                                       left join workerpersonpure w on w.asup_guid = p.userguid"));
                for(int i = 0; i < lwp.Count; i++)
                {
                     if (lwp[i].description == null) { lwp[i].description = ""; };
                     if (lwp[i].tabnom == null) { lwp[i].tabnom = ""; };
                     if (lwp[i].fio == null) { lwp[i].fio = ""; };
                }
            }
            return lwp;
        }

        public List<PassageFIO> getFilterSumPassagesFIODB(string key, string value, long tsbegin, int hours)
        {
            List<PassageFIO> lwp = new List<PassageFIO>();

            string from_clause = " FROM buffer_passage p left join buffer_workers w on p.userguid = w.userguid";

            // готовим фильтрацию
            long tsUTCend = tsbegin + (long)hours * 3600;
            long tsUTCbeg = tsbegin;

            string where_clause = $" where p.timestampUTC >= {tsUTCbeg} and p.timestampUTC <= {tsUTCend} ";
            int filter_switcher = -1;
            if (key != null)
            {
                if (ParamsIndexes.ContainsKey(key))
                {
                    filter_switcher = ParamsIndexes[key];
                }
            };

            switch (filter_switcher)
            {
                case 0:
                    where_clause += $" and p.card='{value}' ";
                    break;
                case 1:
                    where_clause += $" and w.tabnom={value} ";
                    break;
                case 2:
                    where_clause += $" and w.fio is not null and w.fio LIKE '%{value}%' ";
                    break;
                case 3:
                    where_clause += $" and p.isOut={value} ";
                    break;
                case 4:
                    where_clause += $" and p.isDelivered={value}";
                    break;
            }
            try
            {
                string qry_select = "SELECT p.passageID, p.timestampUTC, p.card, p.isOut, p.kppId, w.tabnom, p.isManual, p.isDelivered, p.description, p.isСhecked, p.toDelete, w.fio, w.userguid " +
                $" {from_clause} {where_clause} order by p.timestampUTC";
                lwp.AddRange(selectPassagesFIO(qry_select));
            }
            catch { };
            return lwp;

        }


        public List<PassageFIO> getFilteredPassagesFIODB(Dictionary<string,string> filters )
        {
            List<PassageFIO> lwp = new List<PassageFIO>();
            List<string> filters_array = new List<string>();
            string from_clause = " FROM passage p left join workerpersonpure w on p.userguid = w.asup_guid ";

            if (filters.ContainsKey("tsbeg") && filters.ContainsKey("tsend")){
                filters_array.Add($" p.timestampUTC >= {filters["tsbeg"]} and p.timestampUTC <= {filters["tsend"]} ");
            }
            if (filters.ContainsKey("card"))
            {
                filters_array.Add($" p.card='{filters["card"]}' ");
            }
            if (filters.ContainsKey("tabnom"))
            {
                filters_array.Add($" p.tabnom='{filters["tabnom"]}' ");
            }
            if (filters.ContainsKey("fio"))
            {
                filters_array.Add($" w.first_name||'@'||w.second_name||'@'||w.last_name LIKE '%{filters["fio"]}%'");
            }
            if (filters.ContainsKey("operation"))
            {
                filters_array.Add($" p.operCode = {filters["operation"]} ");
            }
            if (filters.ContainsKey("delivered"))
            {
                if (filters["delivered"] == "0")
                {
                    filters_array.Add($" p.isDelivered <>1  ");
                }
                else
                {
                    filters_array.Add($" p.isDelivered = 1 ");
                }
            }

            string assembly_filters = String.Join(" and ", filters_array);
            string where_clause = $" where 1=1 " + ((assembly_filters.Length>0) ? " and "+ assembly_filters : "") ;
            
            try
            {
                string qry_select = "SELECT p.passageID, p.timestampUTC, p.card, p.operCode, p.kppId, p.tabnom, p.isManual, p.isDelivered, p.description, p.isChecked, p.toDelete, w.second_name||' '||w.first_name||' '||w.last_name as fio, p.userguid " +
                $" {from_clause} {where_clause} order by p.timestampUTC";
                lwp.AddRange(selectPassagesFIO(qry_select));
            }
            catch { };
            return lwp;

        }
        private List<PassageFIO> getPassageFIOByPassageIdDB(string id)
        {
            List<PassageFIO> lwp = new List<PassageFIO>();
            long timestampUTC = TimeLord.UTCNow();
            string where_clause = $" where p.passageID={id} ";

            using (var db = new SQLiteConnection(new SQLiteConnectionString(CString)))
            {
                memdb.DropTable<Passage>();
                memdb.CreateTable<Passage>();
                memdb.InsertAll(db.Query<Passage>($"select p.* from Passage p {where_clause} order by passageID desc LIMIT 1"));
                lwp.AddRange(memdb.Query<PassageFIO>($"select p.*, w.second_name||' '||w.first_name||' '||w.last_name as fio from Passage p left join workerpersonpure w on w.asup_guid = p.userguid"));
                for (int i = 0; i < lwp.Count; i++)
                {
                    if (lwp[i].description == null) { lwp[i].description = ""; };
                    if (lwp[i].tabnom == null) { lwp[i].tabnom = ""; };
                }
            }
            return lwp;

            /*

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
            */
        }
        private List<PassageFIO> getLastPassageFIOByCardDB(string card)
        {
            List<PassageFIO> lwp = new List<PassageFIO>();
            long timestampUTC = TimeLord.UTCNow();
            string where_clause = $" where p.card='{card}'"; 

            using (var db = new SQLiteConnection(new SQLiteConnectionString(CString)))
            {
                memdb.DropTable<Passage>();
                memdb.CreateTable<Passage>();
                memdb.InsertAll(db.Query<Passage>($"select p.* from Passage p {where_clause}  order by passageID desc LIMIT 1"));
                lwp.AddRange(memdb.Query<PassageFIO>($"select p.*, w.second_name||' '||w.first_name||' '||w.last_name as fio from Passage p left join workerpersonpure w on w.asup_guid = p.userguid"));
                for (int i = 0; i < lwp.Count; i++)
                {
                    if (lwp[i].description == null) { lwp[i].description = ""; };
                    if (lwp[i].tabnom == null) { lwp[i].tabnom = ""; };
                }
            }
            return lwp;



/*
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
*/
        }
        private List<PassageFIO> selectPassagesFIO(string qry_select)
        {
            List<PassageFIO> myPassages = new List<PassageFIO>();
            myPassages.AddRange(memdb.Query<PassageFIO>(qry_select));
            /*
            using (var connection = new SQLiteConnection(CString))
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
                            single_pass.userguid = reader.GetString(12);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        myPassages.Add(single_pass);
                    }
                }
            }
            */
            return myPassages;
        }
        #endregion passage selectors from db 

        #region passage no sql, no db 
        public string getCommentByPassageID(string id)
        {
            string myComment = "";
            List<PassageFIO> xlist = new List<PassageFIO>();
            
            if (useRest) { 
                xlist = getPassageFIOByPassageId_REST(id);
            }
            else{ 
                xlist = getPassageFIOByPassageIdDB(id); 
            }
            if (xlist.Count > 0)
            {
                myComment += xlist[0].description;
            }
            return myComment;
        }
        public string getGUIDByPassageID(string id)
        {
            string userguid = "";
            List<PassageFIO> xlist = new List<PassageFIO>();

            if (useRest)
            {
                xlist = getPassageFIOByPassageId_REST(id);
            }
            else
            {
                xlist = getPassageFIOByPassageIdDB(id);
            }


            if (xlist.Count > 0)
            {
                userguid += xlist[0].userguid;
            }
            return userguid;
        }
        public double getLastPassageByCard(string card)
        {
            double tsUTC = 0;
            List<PassageFIO> xlist = new List<PassageFIO>();
            if (useRest)
            {
                xlist = getLastPassageFIOByCard_REST(card);
            }
            else
            {
                xlist = getLastPassageFIOByCardDB(card);
            }

            if (xlist.Count > 0)
            {
                tsUTC = xlist[0].timestampUTC;
            }
            return tsUTC;
        }

        public void deleteManualPassageByID(string DBString, string id)
        {

            using (var connection = new SQLiteConnection(DBString))
            {
                var command = connection.CreateCommand($"delete from passage where passageID = {id} and isDelivered=0");
                //command.CommandText = $"delete from buffer_passage where passageID = {id} and isDelivered=0";
                command.ExecuteNonQuery();
                var command2 = connection.CreateCommand($"update passage set toDelete=1, isDelivered=2 where passageID = {id} and isDelivered>0");
                command2.ExecuteNonQuery();
            }
            
        }
        #endregion passage no sql, no db 


        #region Workers sql + db 
        /*
         *
         *  List<PassageFIO> xlist = new List<PassageFIO>();
            var client = new RestClient(restServerAddr);
            client.Timeout = 200;
            var request = new RestRequest($"api/passage/lastcard?card={card.Replace(' ', '+')}", Method.GET);
            var response = client.Execute<List<PassageFIO>>(request);
            try
            {
                xlist.AddRange(response.Data);
            }
            catch { }
            return xlist;

         */
        public List<PrettyWorker> getGUIDOwnerWorker(string userguid, bool useRest)
        {
            List<PrettyWorker> xlist = new List<PrettyWorker>();
            if (useRest)
            {
                //xlist.AddRange(getGUIDOwnerWorker_REST(userguid));
            }
            else
            {
                xlist.AddRange(getGUIDOwnerWorkerDB(userguid));
            }
            return xlist;

        }

        public List<PrettyWorker> getCardOwnerWorker(string card, bool useRest)
        {
            List<PrettyWorker> xlist = new List<PrettyWorker>();
            if (useRest)
            {
                //xlist.AddRange(getCardOwnerWorker_REST(card));
            }
            else
            {
                xlist.AddRange(getCardOwnerWorkerDB(card));
            }
            return xlist;

        }

        public List<PrettyWorker> getFilteredWorkersByEntity(string entityName, string entityValue, bool useRest)
        {
            List<PrettyWorker> xlist = new List<PrettyWorker>();
            if (useRest)
            {
                //xlist.AddRange(getFilteredWorkersByEntity_REST(entityName, entityValue));
            }
            else
            {
                xlist.AddRange(getFilteredWorkersByEntityDB(entityName, entityValue));
            }
            return xlist;

        }


        public List<WorkerPerson> getFilteredWorkersByEntity_REST(string entityName, string entityValue)
        {
            List<WorkerPerson> xlist = new List<WorkerPerson>();
            var client = new RestClient(restServerAddr);
            client.Timeout = 200;
            var request = new RestRequest($"api/worker/byfilter?fieldname={entityName}&fieldvalue={entityValue.Replace(' ', '+')}", Method.GET);
            var response = client.Execute<List<WorkerPerson>>(request);
            try
            {
                xlist.AddRange(response.Data);
            }
            catch { }
            return xlist;

        }
        public List<PrettyWorker> getFilteredWorkersByEntityDB(string entityName, string entityValue) 
        {
            List<PrettyWorker> results = new List<PrettyWorker>();

            results.AddRange(memdb.Query<PrettyWorker>($"select card, tabnom, userguid, fio  from prettyworker where {entityName} LIKE '%{entityValue}%'"));

            /*
            using (var connection = new SQLiteConnection(CString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = $"select card, tabnom, userguid, fio  from buffer_workers where {entityName} LIKE '%{entityValue}%'";

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
            }
            */
            return results;
        }
        public List<WorkerPerson> getGUIDOwnerWorker_REST(string userguid)
        {
            List<WorkerPerson> xlist = new List<WorkerPerson>();
            var client = new RestClient(restServerAddr);
            client.Timeout = 200;
            var request = new RestRequest($"api/worker/byguid?userguid={userguid}", Method.GET);
            var response = client.Execute<List<WorkerPerson>>(request);
            try
            {
                xlist.AddRange(response.Data);
            }
            catch { }
            return xlist;
        }
        public List<PrettyWorker> getGUIDOwnerWorkerDB(string userguid)
        {
            List<PrettyWorker> results = new List<PrettyWorker>();
            results.AddRange(memdb.Query<PrettyWorker>($"select card, tabnom, userguid, fio,job  from prettyworker where userguid='{userguid}' LIMIT 1"));
            /*
            using (var connection = new SQLiteConnection(CString))
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
                        myWP.tabnom = reader.IsDBNull(2)  ? 0 : reader.GetInt64(2);
                        myWP.jobDescription = reader.GetString(3);
                        myWP.isGuardian = 0;
                        myWP.card = reader.IsDBNull(4) ? "" : reader.GetString(4);

                        results.Add(myWP);
                        break;
                    }
                }
            }
            */
            return results;
        }
        public List<WorkerPerson> getCardOwnerWorker_REST(string card)
        {
            List<WorkerPerson> xlist = new List<WorkerPerson>();
            var client = new RestClient(restServerAddr);
            client.Timeout = 200;
            var request = new RestRequest($"api/worker/bycard?card={card}", Method.GET);
            var response = client.Execute<List<WorkerPerson>>(request);
            try
            {
                xlist.AddRange(response.Data);
            }
            catch { }
            return xlist;
        }
        public List<PrettyWorker> getCardOwnerWorkerDB(string card)
        {
            List<PrettyWorker> results = new List<PrettyWorker>();
            results.AddRange(memdb.Query<PrettyWorker>($"SELECT userguid, fio, tabnom, job, card FROM prettyworker where card='{card}' LIMIT 1"));
/*            
            using (var connection = new SQLiteConnection(CString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                $"SELECT userguid, fio, tabnom, jobDescription, card FROM buffer_workers_described where card='{card}' LIMIT 1";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WorkerPerson myWP = new WorkerPerson(); 
                        myWP.userguid = reader.GetString(0);
                        myWP.fio = reader.GetString(1);
                        myWP.tabnom = reader.IsDBNull(2)  ? 0 : reader.GetInt64(2);
                        myWP.jobDescription = reader.GetString(3);
                        myWP.isGuardian = 0;
                        myWP.card = reader.IsDBNull(4) ? "" : reader.GetString(4);
                        results.Add(myWP);
                        break;
                    }
                }
            }
            */
            return results;
        }

        /*
        public List<WorkerPerson> getWorkerGUIDByTabnomDB(string tabnom)
        {
            List<WorkerPerson> results = new List<WorkerPerson>();
            using (var connection = new SQLiteConnection(CString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select userguid,fio,tabnom,jobDescription from buffer_workers_described where tabnom={tabnom}";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WorkerPerson myWP = new WorkerPerson();
                        myWP.userguid = reader.GetString(0);
                        myWP.fio = reader.GetString(1);
                        myWP.tabnom = reader.GetInt64(2);
                        myWP.jobDescription = reader.GetString(3);
                        results.Add(myWP);
                        break;
                    }
                }
            }
            return results;
        }
        */
        #endregion Workers sql + db 

        #region workers no sql, no db 
        /*
        public string getWorkerGUIDByTabnom(string myTabnom) 
        {
            string myGUID = "";
            List<WorkerPerson> xlist = getWorkerGUIDByTabnomDB(myTabnom);
            if (xlist.Count > 0)
            {
                myGUID = xlist[0].userguid;
            }
            return myGUID;
        }
        */
        public void getGUIDOwnerWorker(string userguid, ref PrettyWorker wp)
        {
            List<PrettyWorker> xlist = getGUIDOwnerWorker(userguid,useRest);
            if (xlist.Count > 0)
            {
                wp.userguid = xlist[0].userguid;
                wp.card = xlist[0].card;
                wp.fio = xlist[0].fio;
                wp.tabnom = xlist[0].tabnom;
                wp.job = xlist[0].job;    
            }
        }
        public void getCardOwnerWorker(string card, ref PrettyWorker wp)
        {
            List<PrettyWorker> xlist = getCardOwnerWorker(card,useRest);
            if (xlist.Count > 0)
            {
                wp.userguid = xlist[0].userguid;
                wp.card = xlist[0].card;
                wp.fio = xlist[0].fio;
                wp.tabnom = xlist[0].tabnom;
                wp.job = xlist[0].job;
            }
        }
        #endregion workers no sql, no db 

        // этот метод вызывать по команде сигналистера
        public double getLastWorkersUpdateTimestamp()
        {
            double timestampUTC = 0;
            /*
            using (var connection = new SQLiteConnection(CString))
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
                        timestampUTC = reader.GetDouble(0);
                    }
                }
            }
            */
            return timestampUTC;
        }
        /*
        // здесь неверно выбираются ВСЕ для обновления словаря
        // а словарь не нужен!!!!
        public List<WorkerPerson> getNewWorkersList()
        {
            List<WorkerPerson> lwp = new List<WorkerPerson>();
            using (var connection = new SQLiteConnection(CString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT card, tabnom, fio, userguid, isGuardian FROM buffer_workers
                ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WorkerPerson myWP = new WorkerPerson();
                        myWP.card = reader.GetString(0);
                        myWP.tabnom = reader.GetInt64(1);
                        myWP.fio = reader.GetString(2).Replace("@", " ");
                        myWP.userguid = reader.GetString(3);
                        myWP.isGuardian = 0;
                        lwp.Add(myWP);
                    }
                }
            }
            return lwp;
        }
        */
        #region restcalls


        
        public List<PassageFIO> getHotPassagesFIO_REST(int isDaily)
        {
            List<PassageFIO> xlist = new List<PassageFIO>();    
            //var client = new RestClient($"{restServerAddr}/hot?isdaily={isDaily}");
            var client = new RestClient(restServerAddr);
            client.Timeout = 200;
            var request = new RestRequest($"api/Passage/hot?isdaily={isDaily}", Method.GET);
            var response = client.Execute<List<PassageFIO>>(request);
            try
            {
                xlist.AddRange(response.Data);
            }
            catch { }
            return xlist;
        }

        public List<PassageFIO> getFilteredPassagesFIO_REST(Dictionary<string, string> filters)
        {
            List<string> filters_array = new List<string>();

            if (filters.ContainsKey("tsbeg") && filters.ContainsKey("tsend"))
            {
                filters_array.Add($"tsbeg={filters["tsbeg"]}&tsend={filters["tsend"]} ");
            }
            if (filters.ContainsKey("card"))
            {
                filters_array.Add($"card='{filters["card"]}'");
            }
            if (filters.ContainsKey("tabnom"))
            {
                filters_array.Add($"tabnom='{filters["tabnom"]}'");
            }
            if (filters.ContainsKey("fio"))
            {
                filters_array.Add($"fio={filters["fio"]}");
            }
            if (filters.ContainsKey("operation"))
            {
                filters_array.Add($"operation={filters["operation"]}");
            }
            if (filters.ContainsKey("delivered"))
            {
                filters_array.Add($"delivered={filters["delivered"]}");
            }
            List<PassageFIO> xlist = new List<PassageFIO>();
            var client = new RestClient(restServerAddr);
            client.Timeout = 200;
            var request = new RestRequest($"api/passage/history?{String.Join("&", filters_array)}", Method.GET);
            var response = client.Execute<List<PassageFIO>>(request);
            try
            {
                xlist.AddRange(response.Data);
            }
            catch { }
            return xlist;

        }

        public List<PassageFIO> getFilteredPassagesFIO_REST(string key, string value, long tsbegin, int hours)
        {
            List<PassageFIO> xlist = new List<PassageFIO>();
            var client = new RestClient(restServerAddr);
            client.Timeout = 200;
            var request = new RestRequest($"api/passage/history?key={key}&value={value.Replace(' ', '+')}&tsbegin={tsbegin}&hours={hours}", Method.GET);
            var response = client.Execute<List<PassageFIO>>(request);
            try
            {
                xlist.AddRange(response.Data);
            }
            catch { }
            return xlist;
        }

        private List<PassageFIO> getPassageFIOByPassageId_REST(string id)
        {
            List<PassageFIO> xlist = new List<PassageFIO>();
            var client = new RestClient(restServerAddr);
            client.Timeout = 200;
            var request = new RestRequest($"api/passage/single?id={id}", Method.GET);
            var response = client.Execute<List<PassageFIO>>(request);
            try
            {
                xlist.AddRange(response.Data);
            }
            catch { }
            return xlist;
        }

        private List<PassageFIO> getLastPassageFIOByCard_REST(string card)
        {
            List<PassageFIO> xlist = new List<PassageFIO>();
            var client = new RestClient(restServerAddr);
            client.Timeout = 200;
            var request = new RestRequest($"api/passage/lastcard?card={card.Replace(' ', '+')}", Method.GET);
            var response = client.Execute<List<PassageFIO>>(request);
            try
            {
                xlist.AddRange(response.Data);
            }
            catch { }
            return xlist;
        }
        #endregion restcalls


    }
}