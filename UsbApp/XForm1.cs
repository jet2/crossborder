using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace kppApp
{

    public partial class XForm1 : Form
    {
        public static Sniffer mySnifferForm;

        public static Dictionary<string, string> Persons;
        public static Dictionary<string, WorkerPerson> PersonsDictStruct;

        IniFile INI;
        private string restServerAddr = "http://localhost:3002";
        internal string sqlite_connectionstring = "Data Source=c:\\appkpp\\kppbuffer.db;Version=3;New=False;";
        private string statusCodeOK = "201";
        //WcfServer srv;

        public XForm1()
        {

            InitializeComponent();
            if (!settings_read())
            {
                MessageBox.Show("Не удалось прочитать настройки из config.ini");
            };
        }

        private bool settings_read()
        {
            bool result = false;
            /*
            string exe = Assembly.GetExecutingAssembly().GetName().Name;
            string Path = new FileInfo(exe + ".ini").FullName;
            bool exist = File.Exists(Path);
            if (!exist)
            {
                return false;
            }
            */
            IniFile INI = new IniFile();
            bool rest_in_settings = INI.KeyExists("restapi_path", "settings");
            bool sqlite_in_settings = INI.KeyExists("sqlite_connectionstring", "settings");
            bool ok_status_in_settings = INI.KeyExists("status_code_ok", "settings");

            if (rest_in_settings & sqlite_in_settings & ok_status_in_settings)
            {
                restServerAddr = INI.Read("restapi_path", "settings");
                restapi_path_label.Text = restServerAddr;
                sqlite_connectionstring = INI.Read("sqlite_connectionstring", "settings");
                result = true;
            }
            else
            {
                // заполняем примерами значений важных ключей
                if (!rest_in_settings) INI.Write("restapi_path", "http://www.google.com", "settings");
                if (!sqlite_in_settings) INI.Write("sqlite_connectionstring", "Data Source=c:\\appkpp\\kppbuffer.db;Version=3;New=False;", "settings");
                if (!ok_status_in_settings) INI.Write("status_code_ok", "201", "settings");
                sqlite_connectionstring = "Data Source=c:\\appkpp\\kppbuffer.db;Version=3;New=False;";
                restapi_path_label.Text = "Неизвестно";
                result = false;
            }
            return result;
        }

        private void dictionaryWorkersUpdater()
        {

            Persons = new Dictionary<string, string>();
            Persons.Clear();

            PersonsDictStruct = new Dictionary<string, WorkerPerson>();
            PersonsDictStruct.Clear();


            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT card, tabnom, fio, job, isGuardian FROM buffer_workers
                ";
                //                command.Parameters.AddWithValue("$card", "74 4669");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WorkerPerson myWP = new WorkerPerson();
                        myWP.card = reader.GetString(0);
                        myWP.tabnom = reader.GetInt64(1);
                        myWP.fio = reader.GetString(2);
                        myWP.job = reader.GetString(3);
                        myWP.isGuardian = reader.GetInt16(4);
                        if (myWP.card != "")
                        {
                            PersonsDictStruct.Add($"{myWP.card}", myWP);
                        }
                    }
                }
            }
            if (PersonsDictStruct.Count == 0)
            {
                MessageBox.Show("Справочник персонала поврежден!\r\nСервис не позволяет идентифицировать персонал!");
            }
            mySnifferForm.UpdatePersons(PersonsDictStruct);
            // Unix timestamp is seconds past epoch

            tsUpdated local_updated = new tsUpdated();
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

            }
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(local_updated.timestampUTC).ToLocalTime();
            this.toolStripStatusLabel6.Text = dtDateTime.ToShortDateString() + " " + dtDateTime.ToLongTimeString();
        }

        private void XForm1_Load(object sender, EventArgs e)
        {

            mySnifferForm = new Sniffer(this);
            mySnifferForm.Left = this.Width - mySnifferForm.Width;
            mySnifferForm.Top = this.Height - mySnifferForm.Height;
            mySnifferForm.Show();
            mySnifferForm.Hide();
            dictionaryWorkersUpdater();
            label10_DoubleClick(this, e);
            threadWorkersUpdater.DoWork += updateWorkers;
            threadWorkersUpdater.RunWorkerCompleted += updateWorkers_ResultHandler;
            threadPassageSender.DoWork += sendPassage;
            threadPassageSender.RunWorkerCompleted += sendPassage_ResultHandler;
            timerWorkersUpdate_Tick(this, e);
            timerPassageSender_Tick(this, e);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection breakfast = this.listView1.SelectedItems;


            foreach (ListViewItem item in breakfast)
            {
                int idx = item.Index;
                tabControl1.SelectTab(idx);
                break;
            }
        }

        public void setRFIDLost()
        {
            this.toolStripStatusLabel3.Text = "Отключен";
            this.toolStripStatusLabel3.BackColor = Color.Salmon;
        }
        public void setRFIDFound()
        {
            this.toolStripStatusLabel3.Text = "Подключен";
            this.toolStripStatusLabel3.BackColor = Color.PaleGreen;
        }

        private void timerWorkersUpdate_Tick(object sender, EventArgs e)
        {
            timerWorkersUpdate.Enabled = false;
            threadWorkersUpdater.RunWorkerAsync();
        }

        private void updateWorkers(object sender, DoWorkEventArgs e)
        {

            // получаем стамп последнего обновления работников с сервера
            var client = new RestClient($"{restServerAddr}/workers/update_ts");
            client.Timeout = 5000;
            var request = new RestRequest(Method.GET);
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            tsUpdated remote_updated = JsonConvert.DeserializeObject<tsUpdated>(response.Content);
            // получаем стамп последнего обновления работников локальный
            tsUpdated local_updated = new tsUpdated();
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
                                command3.CommandText = $"insert into buffer_workers_input(card,fio,tabnom,job,isGuardian) values('{wp.card}','{wp.fio}',{wp.tabnom},'{wp.job}',{wp.isGuardian})";
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

        private void updateWorkers_ResultHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            // обновляем словарь работников в памяти из локальной БД
            dictionaryWorkersUpdater();
            timerWorkersUpdate.Enabled = true;
        }

        private void sendPassage(object sender, DoWorkEventArgs e)
        {
            // restsharp
            // выбираем первый неотправленный passage и если массив непустой - отправялем через rest
            // если успешно отправлось - помечаем passageID отправленным и обновляем главную таблицу
            // получить наименьший локальный проход
            // отправить 
            // оценить результат
            // обновить состояние или не обновлять
            // удаление доставленных - другим методом
            long exID = -2;
            Passage firstUndelivered = getFirstUndelivered();
            if (firstUndelivered.passageID > -1)
            {
                var client = new RestClient("http://testapi.sytes.net:3002/kpp/v1/passages/");
                client.Timeout = 5000;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                do
                {
                    var body = JsonConvert.SerializeObject(firstUndelivered);
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
                        }
                    }
                    exID = firstUndelivered.passageID;
                    firstUndelivered = getFirstUndelivered();
                    if (exID == firstUndelivered.passageID)
                    {
                        break;
                    }
                } while (firstUndelivered.passageID != -1);
            }
        }

        private void sendPassage_ResultHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            timerPassageSender.Enabled = true;

        }

        private void timerPassageSender_Tick(object sender, EventArgs e)
        {
            timerPassageSender.Enabled = false;
            threadPassageSender.RunWorkerAsync();
        }


        private Passage getFirstUndelivered()
        {
            string qry_select_first_undelivered = @"SELECT passageID, timestampUTC, card, isOut, kppId, tabnom, isManual
                FROM buffer_passage
                where isDelivered=0
                order by passageID 
                limit 1";

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
                        first_pass.timestampUTC = reader.GetDouble(1);
                        first_pass.card = reader.GetString(2);
                        first_pass.isOut = reader.GetInt16(3);
                        first_pass.kppId = reader.GetString(4);
                        first_pass.tabnom = reader.GetInt32(5);
                        first_pass.isManual = reader.GetInt16(6);
                    }
                }
            }

            return first_pass;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        public void label10_DoubleClick(object sender, EventArgs e)
        {
            while (listView2.Items.Count > 0) { listView2.Items.RemoveAt(0); };
            string qry_select_first_undelivered = @"SELECT passageID, timestampUTC, card, isOut, kppId, tabnom, isManual, isDelivered
                FROM buffer_passage
                order by timestampUTC";

            int cnt = 1;
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = qry_select_first_undelivered;

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Passage first_pass = new Passage();
                        first_pass.passageID = reader.GetInt64(0);
                        first_pass.timestampUTC = reader.GetDouble(1);
                        first_pass.card = reader.GetString(2);
                        first_pass.isOut = reader.GetInt16(3);
                        first_pass.kppId = reader.GetString(4);
                        first_pass.tabnom = reader.GetInt64(5);
                        first_pass.isManual = reader.GetInt16(6);
                        int isDelivered = reader.GetInt16(7);
                        ListViewItem lvi = new ListViewItem();
                        int zIdx = 0;
                        if (isDelivered == 1)
                        {
                            zIdx = 1;
                        }

                        lvi.Text = $"{cnt}";
                        lvi.SubItems.Add(first_pass.card);
                        if (PersonsDictStruct.ContainsKey(first_pass.card))
                        {
                            lvi.SubItems.Add($"{PersonsDictStruct[first_pass.card].tabnom}");
                            lvi.SubItems.Add($"{PersonsDictStruct[first_pass.card].fio}");
                        }
                        else
                        {
                            lvi.SubItems.Add("-");
                            lvi.SubItems.Add("-");
                            zIdx = 2;
                        }
                        lvi.ImageIndex = zIdx;

                        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(first_pass.timestampUTC).ToLocalTime();
                        string timeText = dtDateTime.ToShortDateString() + " " + dtDateTime.ToLongTimeString();
                        lvi.SubItems.Add(timeText);
                        string myMan = "Вход";
                        switch (first_pass.isOut)
                        {
                            case 1:{ myMan = "Выход";  break; };
                            case 3: { myMan = "Ошибка"; break; };
                            case 2: { myMan = "Авторизация"; break; };
                        }
                        lvi.SubItems.Add($"{myMan}");
                        myMan = first_pass.isManual == 1 ? "Да" : "Нет";
                        lvi.SubItems.Add($"{myMan}");
                        listView2.Items.Add(lvi);
                        cnt++;

                    }

                }
            }
            label10.Text = $"Счетчик событий: {cnt - 1}";
        }

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
            label10_DoubleClick(sender, e);
        }

        public void setDuty(string fio)
        {
            dutyBox.Text = fio;
        }

        private void startBtnSelect_Click(object sender, EventArgs e)
        {
            startBtnSelect.Enabled = false;
            try
            {
                DateTime dtbeg = begPickerSelect.Value;
                long dtbegLong = (int)dtbeg.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                DateTime dtend = endPickerSelect.Value;
                long dtendLong = (int)dtend.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                var client = new RestClient($"{restServerAddr}/passages/?tsbeg={dtbegLong}&tsend={dtendLong}&page=1&limit=20&kppid={Environment.MachineName}");
                client.Timeout = 5000;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                //textBoxJSON.Text = $"{dtbegLong}\r\n{dtendLong}\r\n" +  response.Content;


                while (listView3.Items.Count > 0) { listView3.Items.RemoveAt(0); };


                // заполняем список записей
                List<Passage> remote_passages = JsonConvert.DeserializeObject<List<Passage>>(response.Content);

                int cnt = 1;
                if (remote_passages.Count > 0)
                {
                    // каждую персону из списка вливаем в приемную таблицу
                    foreach (Passage first_pass in remote_passages)
                    {

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = $"{cnt}";
                        lvi.SubItems.Add(first_pass.card);
                        if (PersonsDictStruct.ContainsKey(first_pass.card))
                        {
                            lvi.SubItems.Add($"{PersonsDictStruct[first_pass.card].tabnom}");
                            lvi.SubItems.Add($"{PersonsDictStruct[first_pass.card].fio}");
                            lvi.SubItems.Add($"{PersonsDictStruct[first_pass.card].job}");
                        }
                        else
                        {
                            lvi.SubItems.Add("-");
                            lvi.SubItems.Add("-");
                            lvi.SubItems.Add("-");

                        }
                        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(first_pass.timestampUTC).ToLocalTime();
                        string timeText = dtDateTime.ToShortDateString() + " " + dtDateTime.ToLongTimeString();
                        lvi.SubItems.Add(timeText);
                        string myMan = "Вход";
                        switch (first_pass.isOut)
                        {
                            case 1: { myMan = "Выход"; break; };
                            case 3: { myMan = "Ошибка"; break; };
                            case 2: { myMan = "Авторизация"; break; };
                        }
                        lvi.SubItems.Add($"{myMan}");
                        myMan = first_pass.isManual == 1 ? "Да" : "Нет";
                        lvi.SubItems.Add($"{myMan}");
                        listView3.Items.Add(lvi);
                        cnt++;
                    }
                }
            }
            finally
            {
                startBtnSelect.Enabled = true;
            }

        }
    }
}
