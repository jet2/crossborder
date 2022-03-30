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
using UsbLibrary;
using System.Text;
using Newtonsoft.Json.Linq;

namespace kppApp
{

    public partial class XForm1 : Form
    {
        int sensibleTextLenght = 6;
        private string symbol_pencil = "🖉";
        private string symbol_comment = "💬";
        private int prevScan = 0;
        //public static Sniffer mySnifferForm;
        private bool restSrvState = false;
        bool preventorManualEventCard = false;
        bool preventorManualEventFIO = false;
        bool preventorManualEventGUID = false;
        bool preventorGreenEventCard = false;
        bool preventorGreenEventFIO = false;
        bool preventorGreenEventGUID = false;

        Passage lastPassage = new Passage();

        public static Dictionary<string, string> Persons;
        public static Dictionary<string, WorkerPerson> PersonsDictStruct;
        public static Dictionary<int, string> OperationsSelector = new Dictionary<int, string>();
        public static Dictionary<string, string> OperationsSelector4View = new Dictionary<string, string>();
        private WcfServer srv;
        IniFile INI;
        private string restServerAddr = "http://localhost:3002";
        internal string sqlite_connectionstring = "Data Source=c:\\appkpp\\kppbuffer.db;Version=3;New=False;";
        private string statusCodeOK = "201";
        private int prev_passageID = -2;
        private bool was_sended = false;
        long send_cnt = 0;
        string[] operSource;
        //WcfServer srv;

        public XForm1()
        {

            InitializeComponent();
            if (!settings_read())
            {
                MessageBox.Show("Не удалось прочитать настройки из config.ini");
            };

            listViewHistory.Columns[1].ImageIndex = 0;
            listViewHistory.Columns[2].ImageIndex = 0;
            listViewHistory.Columns[3].ImageIndex = 0;
            listViewHistory.Columns[4].ImageIndex = 0;
            listViewHistory.Columns[5].ImageIndex = 0;
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
                if (!sqlite_in_settings) INI.Write("sqlite_connectionstring", $"Data Source={AppDomain.CurrentDomain.BaseDirectory}kppbuffer.db;Version=3;New=False;", "settings");
                if (!ok_status_in_settings) INI.Write("status_code_ok", "201", "settings");
                sqlite_connectionstring = $"Data Source={AppDomain.CurrentDomain.BaseDirectory}kppbuffer.db;Version=3;New=False;";
                restapi_path_label.Text = "Неизвестно";
                result = false;
            }
            // read JSON directly from a file
            //            string mypath = AppDomain.CurrentDomain.BaseDirectory  + @"operations.json";

            using (StreamReader file = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"operations.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                List<perimeterOperation> perop = (List<perimeterOperation>)serializer.Deserialize(file, typeof(List<perimeterOperation>));
                if (perop.Count > 0)
                {
                    //xList<string> xList= new List<string>(); 
                    foreach (perimeterOperation oper in perop)
                    {
                        if (oper.operhide != 1)
                        {
                            //xList.Add($"{oper.operid}-{oper.operdesc}");
                            OperationsSelector.Add(oper.operid, $"{oper.operid}-{oper.operdesc}");
                            OperationsSelector4View.Add($"{oper.operid}", oper.operdesc);
                        }
                    }
                    //operSource = xList.ToArray();

                    comboBoxOperationsMain.DataSource = new BindingSource(OperationsSelector, null);
                    comboBoxOperationsMain.DisplayMember = "Value";
                    comboBoxOperationsMain.ValueMember = "Key";


                    comboManualEventOperation.DataSource = new BindingSource(OperationsSelector, null);
                    comboManualEventOperation.DisplayMember = "Value";
                    comboManualEventOperation.ValueMember = "Key";


                    comboRedEventOperation.DataSource = new BindingSource(OperationsSelector, null);
                    comboRedEventOperation.DisplayMember = "Value";
                    comboRedEventOperation.ValueMember = "Key";

                    comboGreenEventOperation.DataSource = new BindingSource(OperationsSelector, null);
                    comboGreenEventOperation.DisplayMember = "Value";
                    comboGreenEventOperation.ValueMember = "Key";

                    comboBoxHistoryOperations.DataSource = new BindingSource(OperationsSelector, null);
                    comboBoxHistoryOperations.DisplayMember = "Value";
                    comboBoxHistoryOperations.ValueMember = "Key";
                }
            }
            /*
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
                        }

              */
            return result;
        }

        private void usb_OnDeviceArrived(object sender, EventArgs e)
        {
            //this.lb_message.Items.Add("Found a Device");
            this.setRFIDFound();
        }

        private void usb_OnDeviceRemoved(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(usb_OnDeviceRemoved), new object[] { sender, e });
            }
            else
            {
                // this.lb_message.Items.Add("Device was removed");
                this.setRFIDLost();
            }
        }

        private void usb_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            this.setRFIDFound();
            //this.lb_message.Items.Add("My device was found");

            ////setting string form for sending data
            //string text = "";
            //for (int i = 0; i < this.usb.SpecifiedDevice.OutputReportLength - 1; i++)
            //{
            //    text += "000 ";
            //}
            //this.tb_send.Text = text;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            usb.RegisterHandle(Handle);
        }

        protected override void WndProc(ref Message m)
        {
            usb.ParseMessages(ref m);
            base.WndProc(ref m);	// pass message on to base form
        }


        private void usb_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(usb_OnSpecifiedDeviceRemoved), new object[] { sender, e });
            }
            else
            {
                //this.lb_message.Items.Add("My device was removed");
                this.setRFIDLost();
            }
        }

        private WorkerPerson getWorkerByCard(string card)
        {
            WorkerPerson myWP = new WorkerPerson();
            myWP.userguid = "";
            myWP.fio = "";
            myWP.card = card;
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                $"SELECT userguid, fio, tabnom FROM buffer_workers where card='{card}' LIMIT 1";
                //                command.Parameters.AddWithValue("$card", "74 4669");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        myWP.userguid = reader.GetString(0);
                        myWP.fio = reader.GetString(1);
                        myWP.tabnom = reader.GetInt64(2);
                        myWP.isGuardian = 0;
                        break;
                    }
                }
            }
            return myWP;
        }

        private void uodate2sqlite(Passage p, string old_id)
        {
            using (SQLiteConnection Connect = new SQLiteConnection(sqlite_connectionstring))
            {
                string commandText = $"update buffer_passage set  card='{p.card}', IsOUT={p.operCode}, tabnom={p.tabnom}, description='{p.description}' where passageId={old_id} and isDelivered=0";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
            }
            label10_DoubleClick(this, new EventArgs());
        }

        private void write2sqlite(Passage myPassage)
        {
            // записываем информацию в базу данных
            using (SQLiteConnection Connect = new SQLiteConnection(sqlite_connectionstring))
            {
                string commandText = @"INSERT INTO buffer_passage ([timestampUTC], [card], [IsOUT], [KPPID], [tabnom],[isManual],[description],[isСhecked]) 
                                       VALUES(@timestampUTC, @card, @IsOUT, @KPPID, @tabnom,@isManual,@description, 0)";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Command.Parameters.AddWithValue("@timestampUTC", myPassage.timestampUTC);
                Command.Parameters.AddWithValue("@card", myPassage.card);
                Command.Parameters.AddWithValue("@tabnom", myPassage.tabnom);
                Command.Parameters.AddWithValue("@IsOut", myPassage.operCode);
                Command.Parameters.AddWithValue("@KPPID", Environment.MachineName);
                Command.Parameters.AddWithValue("@isManual", myPassage.isManual);
                Command.Parameters.AddWithValue("@description", myPassage.description);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
                // MessageBox.Show("Проход записан в базу данных");
            }
            label10_DoubleClick(this, new EventArgs());
        }

        private void usb_OnDataRecieved(object sender, DataRecievedEventArgs args)
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new DataRecievedEventHandler(usb_OnDataRecieved), new object[] { sender, args });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {

                byte[] bdata = new byte[100];
                //args.data.CopyTo(bdata, 2);
                Array.Copy(args.data, 1, bdata, 0, 100);

                string readerBytes = BytesToString(bdata);
                readerBytes = readerBytes.TrimEnd('\0');
                if (readerBytes.Length < 1) return;
                lastPassage.timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;




                this.BackColor = Color.DimGray;

                if (readerBytes.Length > 0)
                {
                    lastPassage.card = readerBytes;
                    labelEventName.Text = "";
                    labelEventFamOtc.Text = "";
                    labelEventUserguid.Text = "";
                    labelEventName.ForeColor = Color.Black;
                    labelEventFamOtc.ForeColor = Color.Black;
                    labelEventUserguid.ForeColor = Color.Black;

                    WorkerPerson myWorkerPerson = getWorkerByCard(readerBytes);
                    if (myWorkerPerson.userguid != "")
                    {
                        panelSignal2.BackColor = Color.Transparent;
                    }
                    else
                    {
                        labelEventName.Text = labelTPL.Text;
                        labelEventName.ForeColor = Color.Coral;
                        labelEventFamOtc.Text = "";
                        labelEventUserguid.Text = labelTPL.Text;
                        labelEventUserguid.ForeColor = Color.Coral;
                        panelSignal2.BackColor = Color.Red;
                    }
                    System.DateTime dtDateTime = DateTime.Now;
                    string timeText = dtDateTime.ToShortDateString() + " " + dtDateTime.ToShortTimeString();
                    labelEventDate.Text = timeText;
                    labelEventCard.Text = readerBytes;
                    string[] arr = new string[0];
                    if (myWorkerPerson.fio != "")
                    {
                        arr = myWorkerPerson.fio.Split('@');
                    };
                    if (myWorkerPerson.userguid != "")
                    {
                        labelEventUserguid.Text = myWorkerPerson.userguid;
                    };

                    if (arr.Length > 0)
                    {
                        labelEventName.Text = arr[0];
                        string s = "";
                        if (arr.Length > 1)
                        {
                            s += arr[1];
                        }
                        if (arr.Length > 2)
                        {
                            s += " " + arr[2];
                        }
                        labelEventFamOtc.Text = s;
                    }
                    lastPassage.tabnom = myWorkerPerson.tabnom;
                    lastPassage.card = myWorkerPerson.card;
                    if (comboBoxOperationsMain.SelectedIndex != -1)
                    {
                        //string key = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
                        //string value = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Value;
                        object xxx = comboBoxOperationsMain.SelectedItem;
                        lastPassage.operCode = ((KeyValuePair<int, string>)xxx).Key;
                        write2sqlite(lastPassage);
                    }
                }

            }
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
                    SELECT card, tabnom, fio, userguid, isGuardian FROM buffer_workers
                ";
                //                command.Parameters.AddWithValue("$card", "74 4669");

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
            //            mySnifferForm.UpdatePersons(PersonsDictStruct);
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
            //listView2.DrawColumnHeader += listView2_DrawColumnHeader;
            //listView2.DrawItem += listView2_DrawItem;
            try
            {
                this.usb.ProductId = Int32.Parse(this.tb_product.Text, System.Globalization.NumberStyles.HexNumber);
                this.usb.VendorId = Int32.Parse(this.tb_vendor.Text, System.Globalization.NumberStyles.HexNumber);
                this.usb.CheckDevicePresent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Length > 1)
            {
                if (arguments[1] == "-emucards")
                {
                    srv = new WcfServer();
                    srv.Start();
                    srv.Received += OnWCFReceived;
                }
            }

            //mySnifferForm = new Sniffer(this);
            //mySnifferForm.Left = this.Width - mySnifferForm.Width;
            //mySnifferForm.Top = this.Height - mySnifferForm.Height;
            //mySnifferForm.Show();
            //mySnifferForm.Hide();
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
            labelHostAccess.Text = "Недоступен";
            timerWorkersUpdate.Enabled = false;
            threadWorkersUpdater.RunWorkerAsync();
        }

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

        private void updateWorkers_ResultHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            labelHostAccess.Text = restSrvState ? "Доступен" : "Недоступен";
            // обновляем словарь работников в памяти из локальной БД
            dictionaryWorkersUpdater();
            timerWorkersUpdate.Enabled = true;
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

            Passage firstUndelivered = getFirstUndelivered();
            if (firstUndelivered.passageID > -1)
            {
                var client = new RestClient($"{restServerAddr}/passages/");
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

                            send_cnt++;
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
            //label17.Text = send_cnt.ToString();
            if (send_cnt > 0)
            {
                label10_DoubleClick(this, new EventArgs());
            };
            send_cnt = 0;
        }

        private void timerPassageSender_Tick(object sender, EventArgs e)
        {
            timerPassageSender.Enabled = false;
            threadPassageSender.RunWorkerAsync();
        }

        private Passage getFirstUndelivered()
        {
            string qry_select_first_undelivered = @"SELECT passageID, timestampUTC, card, isOut, kppId, tabnom, isManual,description
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


        public void label10_DoubleClick(object sender, EventArgs e)
        {
            listViewHotBuffer.Visible = false;
            int cnt = 1;
            string where_clause = "where isСhecked = 0";
            if (radioButtonDaily.Checked)
            {
                long timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                where_clause = $"where {timestampUTC}-timestampUTC <= 60*60*24 ";
            }
            try
            {
                while (listViewHotBuffer.Items.Count > 0) { listViewHotBuffer.Items.RemoveAt(0); };
                string qry_select_first_undelivered = "SELECT passageID, timestampUTC, card, isOut, kppId, tabnom, isManual, isDelivered, description, isСhecked" +
                $" FROM buffer_passage {where_clause} order by timestampUTC";

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
                            first_pass.operCode = reader.GetInt16(3);
                            first_pass.kppId = reader.GetString(4);
                            first_pass.tabnom = reader.GetInt64(5);
                            first_pass.isManual = reader.GetInt16(6);
                            first_pass.isDelivered = reader.GetInt16(7);
                            first_pass.description += reader.IsDBNull(8) ? String.Empty : reader.GetString(8);

                            ListViewItem lvi = new ListViewItem();
                            int zIdx = 0;
                            if (first_pass.isDelivered == 1)
                            {
                                zIdx = 1;
                            }

                            //lvi.Text = $"{cnt}";
                            lvi.Text = "";
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

                            string myOperation = "?";
                            if (OperationsSelector4View.ContainsKey($"{first_pass.operCode}"))
                            {
                                myOperation = OperationsSelector4View[$"{first_pass.operCode}"];
                            };
                            lvi.SubItems.Add($"{myOperation}");

                            string finalManual = "";
                            if (first_pass.isManual == 1)
                            {
                                finalManual += symbol_pencil;
                            }

                            if (first_pass.description != "")
                            {
                                if (finalManual != "")
                                {
                                    finalManual += " ";
                                }
                                finalManual += symbol_comment;
                            }

                            lvi.SubItems.Add($"{finalManual}");
                            string eventType = "a";
                            if (first_pass.isManual == 1)
                            {
                                eventType = "m";
                            }
                            if (first_pass.tabnom == 0)
                            {
                                eventType = "r";
                            }
                            lvi.SubItems.Add($"{first_pass.passageID}-{eventType}-{first_pass.operCode}");
                            listViewHotBuffer.Items.Insert(0, lvi);
                            cnt++;
                        }
                    }
                }
            }
            finally
            {
                listViewHotBuffer.Visible = true;
            }
            labelEventCounter.Text = $"{cnt - 1}";
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


                while (listViewHistory.Items.Count > 0) { listViewHistory.Items.RemoveAt(0); };


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
                            lvi.SubItems.Add($"{PersonsDictStruct[first_pass.card].userguid}");
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
                        switch (first_pass.operCode)
                        {
                            case 1: { myMan = "Выход"; break; };
                            case 3: { myMan = "Ошибка"; break; };
                            case 2: { myMan = "Авторизация"; break; };
                        }
                        lvi.SubItems.Add($"{myMan}");
                        myMan = first_pass.isManual == 1 ? "Да" : "Нет";
                        lvi.SubItems.Add($"{myMan}");
                        listViewHistory.Items.Add(lvi);
                        cnt++;
                    }
                }
                else
                {
                    MessageBox.Show($"Событий для КПП {Environment.MachineName} - не найдено!");
                }
            }
            finally
            {
                startBtnSelect.Enabled = true;
            }

        }
        private void OnWCFReceived(object sender, DataReceivedEventArgs args)
        {
            byte[] decBytes1 = Encoding.ASCII.GetBytes(args.Data);
            UsbLibrary.DataRecievedEventArgs argz = new UsbLibrary.DataRecievedEventArgs(decBytes1);
            usb_OnDataRecieved(sender, argz);
        }

        static string BytesToString(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        #region
        private void listView2_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
            e.DrawText();
            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default
                // to Left if it has not been set to Center or Right.
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        break;
                }

                // Draw the standard header background.
                e.DrawBackground();

                // Draw the header text.
                using (Font headerFont =
                            new Font("Helvetica", 10, FontStyle.Bold))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont,
                        Brushes.Black, e.Bounds, sf);
                }
            }
            return;


        }

        private void listView2_DrawItem(object sender,
                                DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }


        #endregion

        private void listView3_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column >= 1 && e.Column <= 5)
            {
                endPickerSelect.Value = DateTime.Now;
                begPickerSelect.Value = endPickerSelect.Value.AddDays(-31);
                panelFilterSelect.Visible = true;
                if (e.Column != 4)
                {
                    tabSubfilter.Visible = true;

                }
                switch (e.Column)
                {
                    case 1:
                        tabSubfilter.SelectTab(0);
                        break;
                    case 2:
                        tabSubfilter.SelectTab(1);
                        break;
                    case 3:
                        tabSubfilter.SelectTab(2);
                        break;
                    case 4:

                        tabSubfilter.Visible = false;
                        break;
                    case 5:
                        tabSubfilter.SelectTab(3);

                        break;
                }
            }
            /*
            ListView.SelectedListViewItemCollection breakfast = this.listView1.SelectedItems;

            foreach (ListViewItem item in breakfast)
            {
                int idx = item.Index;
                tabSubfilter.SelectTab(idx);
                break;
            }
            */
            //MessageBox.Show(listView3.Columns[e.Column].Text+" 🖉   💬");
        }

        private void buttonHistoryFilterHide_Click(object sender, EventArgs e)
        {
            tabSubfilter.Visible = false;
            panelFilterSelect.Visible = false;
        }

        private void buttonHistorySelect_Click(object sender, EventArgs e)
        {

            bool withFilter = tabSubfilter.Visible;
            tabSubfilter.Visible = false;
            panelFilterSelect.Visible = false;

            #region history view update
            string from_clause = " FROM buffer_passage p ";
            // готовим фильтрацию
            long tsUTCbeg = (long)begPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            long tsUTCend = (long)endPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string where_clause = $" where p.timestampUTC >= {tsUTCbeg} and p.timestampUTC <= {tsUTCend} ";

            if (withFilter)
            {
                switch (tabSubfilter.SelectedIndex)
                {
                    case 0:
                        where_clause += $" and p.card='{cardTextSelect.Text}' ";
                        break;
                    case 1:
                        where_clause += $" and p.tabnom={tabnomTextSelect.Text} ";
                        break;
                    case 2:
                        from_clause = " FROM buffer_passage p, buffer_workers w ";
                        where_clause += $" and p.tabnom=w.tabnom and w.fio is not null and w.fio LIKE '%{fioTextSelect.Text}%' ";
                        break;
                    case 3:
                        if (comboBoxHistoryOperations.SelectedIndex != -1)
                        {
                            object xxx = comboBoxHistoryOperations.SelectedItem;
                            /*
                            lastPassage.operCode = ((KeyValuePair<int, string>)xxx).Key;
                            string[] arr2 = new string[0];
                            */
                            int ch = ((KeyValuePair<int, string>)xxx).Key;
                            where_clause += $" and isOut={ch} ";
                        };
                        break;
                }
            }

            listViewHistory.Visible = false;
            int cnt = 1;

            try
            {
                while (listViewHistory.Items.Count > 0) { listViewHistory.Items.RemoveAt(0); };
                string qry_select = "SELECT p.passageID, p.timestampUTC, p.card, p.isOut, p.kppId, p.tabnom, p.isManual, p.isDelivered, p.description, p.isСhecked" +
                $" {from_clause} {where_clause} order by p.timestampUTC";

                using (var connection = new SQLiteConnection(sqlite_connectionstring))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = qry_select;

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Passage history_pass = new Passage();
                            history_pass.passageID = reader.GetInt64(0);
                            history_pass.timestampUTC = reader.GetDouble(1);
                            history_pass.card = reader.GetString(2);
                            history_pass.operCode = reader.GetInt16(3);
                            history_pass.kppId = reader.GetString(4);
                            history_pass.tabnom = reader.GetInt64(5);
                            history_pass.isManual = reader.GetInt16(6);
                            history_pass.isDelivered = reader.GetInt16(7);
                            history_pass.description += reader.IsDBNull(8) ? String.Empty : reader.GetString(8);

                            ListViewItem lvi = new ListViewItem();

                            lvi.Text = "      ";


                            //lvi.Text = "";
                            lvi.SubItems.Add(history_pass.card);
                            if (PersonsDictStruct.ContainsKey(history_pass.card))
                            {
                                lvi.SubItems.Add($"{PersonsDictStruct[history_pass.card].tabnom}");
                                lvi.SubItems.Add($"{PersonsDictStruct[history_pass.card].fio}");
                            }
                            else
                            {
                                lvi.SubItems.Add("-");
                                lvi.SubItems.Add("-");
                                lvi.ForeColor = Color.Red;
                                lvi.UseItemStyleForSubItems = false;
                                lvi.Text += "💡";
                            }

                            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                            dtDateTime = dtDateTime.AddSeconds(history_pass.timestampUTC).ToLocalTime();
                            string timeText = dtDateTime.ToShortDateString() + " " + dtDateTime.ToLongTimeString();
                            lvi.SubItems.Add(timeText);

                            string myOperation = "?";
                            if (OperationsSelector4View.ContainsKey($"{history_pass.operCode}"))
                            {
                                myOperation = OperationsSelector4View[$"{history_pass.operCode}"];
                            };
                            lvi.SubItems.Add($"{myOperation}");

                            string finalManual = "";
                            if (history_pass.isManual == 1)
                            {
                                finalManual += symbol_pencil;
                            }

                            if (history_pass.description != "")
                            {
                                if (finalManual != "")
                                {
                                    finalManual += " ";
                                }
                                finalManual += symbol_comment;
                            }

                            lvi.SubItems.Add($"{finalManual}");
                            string waitSymbol = "";
                            if (history_pass.isDelivered == 0)
                            {
                                waitSymbol += "⌛";
                            }

                            lvi.SubItems.Add($"{cnt} {waitSymbol}");
                            listViewHistory.Items.Insert(0, lvi);
                            cnt++;
                        }
                    }
                }
            }
            finally
            {
                listViewHistory.Visible = true;
            }
            labelSelectedEventsCount.Text = $"{cnt - 1}";

            #endregion history update

        }

        private void makeCheck(object sender, EventArgs e)
        {
            string qry_check = @"update buffer_passage set isСhecked=1 where isСhecked=0";
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = qry_check;
                command.ExecuteNonQuery();
            }
            label10_DoubleClick(sender, e);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            label10_DoubleClick(sender, e);
        }

        private void radioButtonDaily_Click(object sender, EventArgs e)
        {
            label10_DoubleClick(sender, e);
        }

        private void listViewHotBuffer_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void buttonMakeManual_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
            comboManualEventOperation.SelectedIndex = comboBoxOperationsMain.SelectedIndex;
        }

        private void buttonCancelRedEvent_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void buttonCancelManualEvent_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            editManualEventTabnom.Value = 0;
            editManualEventGUID.Text = "";
            editManualEventFIO.Text = "";
            editManualEventCard.Text = "";
            editManualEventComment.Text = "";

            buttonOKManualEvent.Enabled = false;
            buttonOKManualEvent.BackColor = false ? Color.Teal : Color.Gainsboro;
            while (lvManualEventSearch.Items.Count > 0) { lvManualEventSearch.Items.RemoveAt(0); };
        }

        private void buttonCancelGreenEvent_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            editGreenEventTabnom.Value = 0;
            editGreenEventGUID.Text = "";
            editGreenEventFIO.Text = "";
            editGreenEventCard.Text = "";
            editGreenEventComment.Text = "";

            buttonOkGreenEvent.Enabled = false;
            buttonOkGreenEvent.BackColor = false ? Color.Teal : Color.Gainsboro;
            while (lvGreenEventSearch.Items.Count > 0) { lvGreenEventSearch.Items.RemoveAt(0); };
        }

        private void listViewHotBuffer_MouseUp(object sender, MouseEventArgs e)
        {
            // 134123-a полностью автоматическое событие
            // 134123-m полностью ручное событие
            // 134123-r красное событие
            if (listViewHotBuffer.SelectedItems.Count < 1) return;
            labelShomItem.Text = listViewHotBuffer.SelectedItems[0].SubItems[7].Text;
            string myCard = listViewHotBuffer.SelectedItems[0].SubItems[1].Text;
            string myTabnom = listViewHotBuffer.SelectedItems[0].SubItems[2].Text;
            string myFIO = listViewHotBuffer.SelectedItems[0].SubItems[3].Text;
            string myGUID = "";
            string myComment = "";
            if (listViewHotBuffer.SelectedItems.Count > 0)
            {
                string[] spl = labelShomItem.Text.Split('-');
                // редакторы для красных на странице Красные
                // редакторы для ручных на странице Зеленые
                if (spl.Length > 1)
                {

                    using (var connection = new SQLiteConnection(sqlite_connectionstring))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = $"select description from buffer_passage where passageID={spl[0] }";

                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    myComment = reader.GetString(0);
                                }
                                break;
                            }
                        }
                    }
                    // изменять введенное вручную
                    if (spl[1] == "m")
                    {

                        using (var connection = new SQLiteConnection(sqlite_connectionstring))
                        {
                            connection.Open();
                            var command = connection.CreateCommand();
                            command.CommandText = $"select userguid from buffer_workers where tabnom={myTabnom}";

                            using (var reader = command.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    myGUID = reader.GetString(0);
                                    break;
                                }
                            }
                        }



                        editGreenEventFIO.Text = myFIO;
                        editGreenEventGUID.Text = myGUID;
                        editGreenEventComment.Text = myComment;
                        comboGreenEventOperation.SelectedValue = int.Parse(spl[2]);

                        labelGreenEventID.Text = spl[0];
                        editGreenEventCard.Text = myCard;
                        tabControl1.SelectTab(4);
                    }
                    // изменять введенное автоматически, но без персоны
                    if (spl[1] == "r")
                    {
                        editRedEventCard.Text = myCard;
                        labelRedEventID.Text = spl[0];
                        tabControl1.SelectTab(3);
                        labelRedOperation.Text = "";
                        comboRedEventOperation.SelectedValue = int.Parse(spl[2]);



                        labelRedOperation.Text = spl[2];
                        editRedEventComment.Text = myComment;

                        //    comboRedEventOperation.Items.a
                    }
                    for (int i = 0; i < listViewHotBuffer.Items.Count; i++)
                    {
                        listViewHotBuffer.Items[i].Selected = false;
                    }
                }
                //listViewHotBuffer.Select();
            }
        }
        #region hints handling

        private string getWorkerByHint(string entityName, string entityValue)
        {
            string result = "";
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = $"select card, fio, userguid from buffer_workers where {entityName}='{entityValue}'";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader.GetString(0) + "@" + reader.GetString(1).Replace("@", " ") + "@" + reader.GetString(2);
                        break;
                    }
                }
            }
            return result;
        }

        private void RiseMyHint(ListBox hintsListBox, string entityName, string entityTemplate)
        {
            string entityValue;
            hintsListBox.Items.Clear();
            hintsListBox.Visible = true;
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select {entityName} from buffer_workers where {entityName} LIKE '%{entityTemplate}%'";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entityValue = reader.GetString(0).Replace("@", " ");
                        hintsListBox.Items.Add(entityValue);
                    }
                }
            }
        }

        private void HideMyHint(ListBox hintsListBox)
        {

            hintsListBox.Items.Clear();
            hintsListBox.Visible = false;
        }

        private void editManualEventCard_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                HideMyHint(hintsManualEventCard);
                return;
            }

            if (editManualEventCard.Text.Length >= sensibleTextLenght)
            {
                RiseMyHint(hintsManualEventCard, "card", editManualEventCard.Text);
            }
            else
            {
                HideMyHint(hintsManualEventCard);
            }
        }

        private void editManualEventFIO_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                HideMyHint(hintsManualEventFIO);
                return;
            }

            if (editManualEventFIO.Text.Length >= sensibleTextLenght - 3)
            {
                RiseMyHint(hintsManualEventFIO, "fio", editManualEventFIO.Text);
            }
            else
            {
                HideMyHint(hintsManualEventFIO);
            }
        }

        private void editManualEventGUID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                HideMyHint(hintsManualEventGUID);
                return;
            }

            if (editManualEventGUID.Text.Length >= sensibleTextLenght)
            {
                RiseMyHint(hintsManualEventGUID, "userguid", editManualEventGUID.Text);
            }
            else
            {
                HideMyHint(hintsManualEventGUID);
            }
        }

        private void hintsManualEventCard_MouseClick(object sender, MouseEventArgs e)
        {
            string selectString = getWorkerByHint("card", hintsManualEventCard.GetItemText(hintsManualEventCard.SelectedItem));
            string[] arr2 = selectString.Split('@');
            editManualEventFIO.Text = arr2[1];
            editManualEventGUID.Text = arr2[2];
            editManualEventCard.Text = arr2[0];
            HideMyHint(hintsManualEventCard);
        }

        private void hintsManualEventFIO_MouseClick(object sender, MouseEventArgs e)
        {
            string selectString = getWorkerByHint("fio", hintsManualEventFIO.GetItemText(hintsManualEventFIO.SelectedItem).Replace(" ", "@"));
            string[] arr2 = selectString.Split('@');
            editManualEventFIO.Text = arr2[1];
            editManualEventGUID.Text = arr2[2];
            editManualEventCard.Text = arr2[0];
            HideMyHint(hintsManualEventFIO);
        }

        private void hintsManualEventGUID_MouseClick(object sender, MouseEventArgs e)
        {
            string selectString = getWorkerByHint("userguid", hintsManualEventGUID.GetItemText(hintsManualEventGUID.SelectedItem));
            string[] arr2 = selectString.Split('@');
            editManualEventFIO.Text = arr2[1];
            editManualEventGUID.Text = arr2[2];
            editManualEventCard.Text = arr2[0];
            HideMyHint(hintsManualEventGUID);
        }

        private void editManualEventCard_TextChanged(object sender, EventArgs e)
        {
            bool writePossible = false;
            if (editManualEventCard.Text.Length < sensibleTextLenght - 1) { return; };
            if (editManualEventGUID.Text.Length < sensibleTextLenght) { return; };
            if (comboManualEventOperation.Text == "") { return; };
            string card = editManualEventCard.Text;
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select tabnom from buffer_workers where card = '{editManualEventCard.Text}'";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        editManualEventTabnom.Value = reader.GetInt64(0);
                        writePossible = true;
                        break;
                    }
                }
            }

            buttonOKManualEvent.Enabled = writePossible;
            buttonOKManualEvent.BackColor = writePossible ? Color.Teal : Color.Gainsboro;
        }
        #endregion hints handling

        private void buttonOKManualEvent_Click(object sender, EventArgs e)
        {
            Passage p = new Passage();
            p.isManual = 1;
            p.card = editManualEventCard.Text;
            p.tabnom = (int)editManualEventTabnom.Value;
            p.description = editManualEventComment.Text;
            //object xxx = comboManualEventOperation.SelectedItem;
            p.timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            p.operCode = ((KeyValuePair<int, string>)comboManualEventOperation.SelectedItem).Key;
            write2sqlite(p);
            buttonCancelManualEvent_Click(sender, e);
            label10_DoubleClick(sender, e);

        }

        private void editRedEventComment_TextChanged(object sender, EventArgs e)
        {
            buttonOkRedEvent.Enabled = editRedEventComment.Text.Length > 0;
            buttonOkRedEvent.Enabled = editRedEventComment.Text.Length > 0;
            buttonOkRedEvent.BackColor = editRedEventComment.Text.Length > 0 ? Color.Teal : Color.Gainsboro;
        }

        private void buttonOkRedEvent_Click(object sender, EventArgs e)
        {

            string operCode = $"{((KeyValuePair<int, string>)comboRedEventOperation.SelectedItem).Key}";

            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"update buffer_passage set description='{editRedEventComment.Text}', isOut={operCode} where passageID = {labelRedEventID.Text} and isDelivered=0";
                command.ExecuteNonQuery();
            }
            tabControl1.SelectTab(0);
            editRedEventComment.Text = "";
            label10_DoubleClick(sender, e);
        }

        private void buttonDeleteGreenEvent_Click(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"delete from buffer_passage where passageID = {labelGreenEventID.Text} and isDelivered=0";
                command.ExecuteNonQuery();
            }
            buttonCancelGreenEvent_Click(sender, e);


            label10_DoubleClick(sender, e);
        }



        private void hintsManualEventCard_MouseHover(object sender, EventArgs e)
        {
            preventorManualEventCard = true;

        }

        private void hintsManualEventCard_MouseLeave(object sender, EventArgs e)
        {
            preventorManualEventCard = false;
        }

        private void editManualEventCard_Leave(object sender, EventArgs e)
        {
            if (!preventorManualEventCard) { HideMyHint(hintsManualEventCard); };
            preventorManualEventCard = false;
        }

        private void hintsManualEventFIO_MouseHover(object sender, EventArgs e)
        {

            preventorManualEventFIO = true;

        }

        private void hintsManualEventFIO_MouseLeave(object sender, EventArgs e)
        {
            preventorManualEventFIO = false;
        }

        private void editManualEventFIO_Leave(object sender, EventArgs e)
        {
            if (!preventorManualEventFIO) { HideMyHint(hintsManualEventFIO); };
            preventorManualEventFIO = false;
        }
        private void hintsManualEventGUID_MouseHover(object sender, EventArgs e)
        {

            preventorManualEventGUID = true;

        }

        private void hintsManualEventGUID_MouseLeave(object sender, EventArgs e)
        {
            preventorManualEventFIO = false;
        }


        private void editManualEventGUID_Leave(object sender, EventArgs e)
        {

            if (!preventorManualEventGUID) { HideMyHint(hintsManualEventGUID); };
            preventorManualEventGUID = false;

        }

        private void editGreenEventFIO_Leave(object sender, EventArgs e)
        {
            if (!preventorGreenEventFIO) { HideMyHint(hintsGreenEventFIO); };
            preventorGreenEventFIO = false;
        }

        private void editGreenEventFIO_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                HideMyHint(hintsGreenEventFIO);
                return;
            }

            if (editGreenEventFIO.Text.Length >= sensibleTextLenght)
            {
                RiseMyHint(hintsGreenEventFIO, "FIO", editGreenEventFIO.Text);
            }
            else
            {
                HideMyHint(hintsGreenEventFIO);
            }
        }

        private void editGreenEventCard_Leave(object sender, EventArgs e)
        {
            if (!preventorGreenEventCard) { HideMyHint(hintsGreenEventCard); };
            preventorGreenEventCard = false;
        }

        private void editGreenEventCard_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                HideMyHint(hintsGreenEventCard);
                return;
            }

            if (editGreenEventCard.Text.Length >= sensibleTextLenght)
            {
                RiseMyHint(hintsGreenEventCard, "card", editGreenEventCard.Text);
            }
            else
            {
                HideMyHint(hintsGreenEventCard);
            }
        }

        private void editGreenEventGUID_Leave(object sender, EventArgs e)
        {
            if (!preventorGreenEventGUID) { HideMyHint(hintsGreenEventGUID); };
            preventorGreenEventGUID = false;

        }
        private void editGreenEventGUID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                HideMyHint(hintsGreenEventGUID);
                return;
            }

            if (editGreenEventCard.Text.Length >= sensibleTextLenght)
            {
                RiseMyHint(hintsGreenEventGUID, "userguid", editGreenEventGUID.Text);
            }
            else
            {
                HideMyHint(hintsGreenEventCard);
            }
        }

        private void hintsGreenEventCard_MouseHover(object sender, EventArgs e)
        {
            preventorGreenEventCard = true;
        }

        private void hintsGreenEventCard_MouseLeave(object sender, EventArgs e)
        {
            preventorGreenEventCard = false;
        }

        private void hintsGreenEventFIO_MouseHover(object sender, EventArgs e)
        {
            preventorGreenEventFIO = true;
        }

        private void hintsGreenEventFIO_MouseLeave(object sender, EventArgs e)
        {
            preventorGreenEventFIO = false;
        }

        private void hintsGreenEventGUID_MouseHover(object sender, EventArgs e)
        {
            preventorGreenEventGUID = true;
        }

        private void hintsGreenEventGUID_MouseLeave(object sender, EventArgs e)
        {
            preventorGreenEventGUID = false;
        }

        private void editGreenEventCard_TextChanged(object sender, EventArgs e)
        {
            bool writePossible = false;
            if (editGreenEventCard.Text.Length < sensibleTextLenght - 1) { return; };
            if (editGreenEventGUID.Text.Length < sensibleTextLenght) { return; };
            if (comboGreenEventOperation.Text == "") { return; };
            string card = editGreenEventCard.Text;
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select tabnom from buffer_workers where card = '{editGreenEventCard.Text}'";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        editGreenEventTabnom.Value = reader.GetInt64(0);
                        writePossible = true;
                        break;
                    }
                }
            }

            buttonOkGreenEvent.Enabled = writePossible;
            buttonOkGreenEvent.BackColor = writePossible ? Color.Teal : Color.Gainsboro;
        }

        private void buttonOkGreenEvent_Click(object sender, EventArgs e)
        {
            Passage p = new Passage();
            p.isManual = 1;
            p.card = editGreenEventCard.Text;
            p.tabnom = (int)editGreenEventTabnom.Value;
            p.description = editGreenEventComment.Text;
            //object xxx = comboManualEventOperation.SelectedItem;
            p.timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            p.operCode = ((KeyValuePair<int, string>)comboGreenEventOperation.SelectedItem).Key;
            uodate2sqlite(p, labelGreenEventID.Text);
            buttonCancelGreenEvent_Click(sender, e);
            label10_DoubleClick(sender, e);
        }

        private void hintsGreenEventCard_MouseClick(object sender, MouseEventArgs e)
        {
            string selectString = getWorkerByHint("card", hintsGreenEventCard.GetItemText(hintsGreenEventCard.SelectedItem));
            string[] arr2 = selectString.Split('@');
            editGreenEventFIO.Text = arr2[1];
            editGreenEventGUID.Text = arr2[2];
            editGreenEventCard.Text = arr2[0];
            HideMyHint(hintsGreenEventCard);

        }

        private void hintsGreenEventFIO_MouseClick(object sender, MouseEventArgs e)
        {
            string selectString = getWorkerByHint("fio", hintsGreenEventFIO.GetItemText(hintsGreenEventFIO.SelectedItem).Replace(" ","@"));
            string[] arr2 = selectString.Split('@');
            editGreenEventFIO.Text = arr2[1];
            editGreenEventGUID.Text = arr2[2];
            editGreenEventCard.Text = arr2[0];
            HideMyHint(hintsGreenEventFIO);

        }

        private void hintsGreenEventGUID_MouseClick(object sender, MouseEventArgs e)
        {
            string selectString = getWorkerByHint("userguid", hintsGreenEventGUID.GetItemText(hintsGreenEventGUID.SelectedItem));
            string[] arr2 = selectString.Split('@');
            editGreenEventFIO.Text = arr2[1];
            editGreenEventGUID.Text = arr2[2];
            editGreenEventCard.Text = arr2[0];
            HideMyHint(hintsGreenEventGUID);

        }

        private void fillWorkersBy(string entityName, string entityValue, ListView LV)
        {
            if (entityValue.Length < 3) return;
            LV.Visible = false;
            while (LV.Items.Count > 0) { LV.Items.RemoveAt(0); };
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = $"select card, tabnom, userguid, fio  from buffer_workers where {entityName} LIKE '%{entityValue}%'";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = reader.GetString(0);
                        lvi.SubItems.Add($"{reader.GetInt64(1)}");
                        lvi.SubItems.Add(reader.GetString(2));
                        lvi.SubItems.Add(reader.GetString(3).Replace("@", " "));
                        LV.Items.Insert(0, lvi);
                    }
                }
            }
            LV.Visible = true;
        }

        private void buttonGreenEventSearchByCard_Click(object sender, EventArgs e)
        {
            fillWorkersBy("card", editGreenEventCard.Text, lvGreenEventSearch);
        }

        private void buttonGreenEventSearchByFIO_Click(object sender, EventArgs e)
        {
            fillWorkersBy("fio", editGreenEventFIO.Text, lvGreenEventSearch);
        }

        private void buttonGreenEventSearchByGUID_Click(object sender, EventArgs e)
        {
            fillWorkersBy("userguid", editGreenEventGUID.Text, lvGreenEventSearch);
        }



        private void buttonManualEventSearchByCard_Click(object sender, EventArgs e)
        {
            fillWorkersBy("card", editManualEventCard.Text, lvManualEventSearch);
        }

        private void buttonManualEventSearchByGUID_Click(object sender, EventArgs e)
        {
            fillWorkersBy("userguid", editManualEventGUID.Text, lvManualEventSearch);
        }

        private void buttonManualEventSearchByFIO_Click(object sender, EventArgs e)
        {
            fillWorkersBy("fio", editManualEventFIO.Text, lvManualEventSearch);
        }

        private void lvManualEventSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvManualEventSearch.SelectedItems.Count > 0)
            {
                editManualEventGUID.Text = lvManualEventSearch.SelectedItems[0].SubItems[2].Text;
                editManualEventFIO.Text = lvManualEventSearch.SelectedItems[0].SubItems[3].Text;
                editManualEventCard.Text = "";
                editManualEventCard.Text = lvManualEventSearch.SelectedItems[0].Text;

            }
        }

        private void lvGreenEventSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvGreenEventSearch.SelectedItems.Count > 0)
            {
                editGreenEventGUID.Text = lvGreenEventSearch.SelectedItems[0].SubItems[2].Text;
                editGreenEventFIO.Text = lvGreenEventSearch.SelectedItems[0].SubItems[3].Text;
                editGreenEventCard.Text = "";
                editGreenEventCard.Text = lvGreenEventSearch.SelectedItems[0].Text;
            }

        }

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
                        bit.bit1_system = "";
                        bit.bit1_lon = 0.0;
                        bit.bit1_lat = 0.0;
                        bit.bit1_id = "0";
                        bit.bit1_reader_id = 0;
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
    //        request.AddHeader("Accept", "application/json");

            //request.AddHeader("Content-Type", "application/json");
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
    }
}
