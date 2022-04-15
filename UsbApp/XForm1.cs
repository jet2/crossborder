using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using UsbLibrary;
using HorizontalAlignment = System.Windows.Forms.HorizontalAlignment;
using MessageBox = System.Windows.Forms.MessageBox;

namespace kppApp
{

    public partial class XForm1 : Form
    {

        private SizeF currentScaleFactor = new SizeF(1f, 1f);
        
        internal Dictionary<string, int> ParamsIndexes = new Dictionary<string, int>
        {
            { "card", 0 },
            { "tabnom", 1 },
            { "fio", 2 },
            { "operation", 3 },
            { "delivered", 4 },
        };


        int sensibleTextLenght = 6;
        private string symbol_pencil = "🖉";
        private string symbol_comment = "💬";
        private string symbol_deleteMark = "X";
        private int prevScan = 0;
        private int InacceptebleInterval = 60;
        private int delaySendSecods = 45;
        //public static Sniffer mySnifferForm;
        string runningInstanceGuid = Guid.NewGuid().ToString();
        private bool restSrvState = false;
        bool preventorManualEventCard = false;
        bool preventorManualEventFIO = false;
        bool preventorManualEventGUID = false;
        bool preventorGreenEventCard = false;
        bool preventorGreenEventFIO = false;
        bool preventorGreenEventGUID = false;

        Passage lastPassage = new Passage();
        LocalRESTManager ManRest;
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

        public bool useRest = false;

        public static float GetWindowsScaling()
        {
            return (float)(Screen.PrimaryScreen.Bounds.Width / SystemParameters.PrimaryScreenWidth);
        }
        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);

            float zoomer = GetWindowsScaling();
            currentScaleFactor.Width = 1f * zoomer;
            currentScaleFactor.Height = 1f * zoomer;
            /*
            //Record the running scale factor used
            this.currentScaleFactor = new SizeF(
               this.currentScaleFactor.Width * factor.Width,
               this.currentScaleFactor.Height * factor.Height);
            */
            //double factor2 = System.Windows.PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;
            Kit.ScaleControlElements(lvGreenEventSearch, currentScaleFactor);
            Kit.ScaleControlElements(lvManualEventSearch, currentScaleFactor); 
            Kit.ScaleControlElements(listViewHistory, currentScaleFactor); 
            Kit.ScaleControlElements(listViewHotBuffer, currentScaleFactor); 
        }
        public XForm1()
        {

            InitializeComponent();
            if (!settings_read())
            {
                MessageBox.Show("Не удалось прочитать настройки из config.ini");
            };
            //ManRest = new LocalRESTManager(sqlite_connectionstring);
            ManRest = new LocalRESTManager(sqlite_connectionstring, useRest);
            listViewHistory.Columns[1].ImageIndex = 0;
            listViewHistory.Columns[2].ImageIndex = 0;
            listViewHistory.Columns[3].ImageIndex = 0;
            //listViewHistory.Columns[4].ImageIndex = 0;
            listViewHistory.Columns[5].ImageIndex = 0;
            columnDelivery.ImageIndex = 0;
            tabControl1.ItemSize = new Size(1, 1);
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

        private WorkerPerson getWorkerByGUID(string userguid)
        {
            WorkerPerson myWP = new WorkerPerson();
            myWP.userguid = "";
            myWP.fio = "";
            ManRest.getGUIDOwnerWorker(userguid,ref myWP);
            return myWP;
        }

        private WorkerPerson getWorkerByCard(string card)
        {
            WorkerPerson myWP = new WorkerPerson();
            myWP.userguid = "";
            myWP.fio = "";
            myWP.card = card;
            ManRest.getCardOwnerWorker(card, ref myWP);
            return myWP;
        }

        private void uodate2sqlite(Passage p)
        {
            ManRest.updatePassage("good", p, useRest);
            MainTableReload(this, new EventArgs());
        }

        private void write2sqlite(Passage myPassage)
        {
            // записываем информацию в базу данных
            ManRest.insertPassage(myPassage, useRest);
            MainTableReload(this, new EventArgs());
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
                // читаем карту
                string readerBytes = BytesToString(bdata);
                readerBytes = readerBytes.TrimEnd('\0');
                // номер короток
                if (readerBytes.Length < 1) return;
                // получаем УНИВЕРСАЛЬНОЕ время
                lastPassage.timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                this.BackColor = Color.DimGray;
                bool bered_flag = false;
                panelSignal2.BackColor = Color.Transparent;
                if (readerBytes.Length > 0)
                {
                    lastPassage.card = readerBytes;
                    clearDetectionView();
                    /*
                    labelEventName.Text = "";
                    labelEventFamOtc.Text = "";
                    labelEventUserguid.Text = "";
                    labelEventName.ForeColor = Color.Black;
                    labelEventFamOtc.ForeColor = Color.Black;
                    labelEventUserguid.ForeColor = Color.Black;
                    labelEventJobDescription.Text = "";
                    */
                    long goodRest = restToGoodRepeat(lastPassage.card);
                    WorkerPerson  myWorkerPerson = getWorkerByCard(readerBytes);
                    string savedGUID = myWorkerPerson.userguid;
                    if (goodRest != 0){
                        myWorkerPerson.userguid = "";
                        myWorkerPerson.jobDescription = $"Ожидайте {InacceptebleInterval - goodRest} сек";
                    }

                    //WorkerPerson myWorkerPerson = getWorkerByCard(readerBytes);
                    // тревога по отсутствию userguid
                    // тревога повтора использует тот же механизм. но гуид сохраняется и показывается
                    if (myWorkerPerson.userguid != "")
                    {
                        panelSignal2.BackColor = Color.Transparent;
                    }
                    else
                    {
                        labelEventName.Text = labelTPL.Text;
                        
                        if (goodRest == 0 || savedGUID == "") {
                            labelEventFamOtc.Text = "";
                            labelEventName.ForeColor = Color.Coral;
                            labelEventUserguid.Text = labelTPL.Text;
                            labelEventUserguid.ForeColor = Color.Coral;
                        }
                        else {
                                labelEventName.ForeColor = Color.Black;
                                labelEventUserguid.ForeColor = Color.Black;
                                labelEventUserguid.Text = savedGUID;
                        };

                        panelSignal2.BackColor = Color.Red;
                        bered_flag = true;

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

                    //buttonMarkToDelete.Visible = labelEventUserguid.Text.Length > 3;
                    labelEventJobDescription.Text = myWorkerPerson.jobDescription;
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
                    lastPassage.userguid = myWorkerPerson.userguid;
                    lastPassage.card = myWorkerPerson.card;
                    lastPassage.rowID = "";
                    if (comboBoxOperationsMain.SelectedIndex != -1)
                    {
                        //string key = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
                        //string value = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Value;
                        object xxx = comboBoxOperationsMain.SelectedItem;
                        lastPassage.operCode = ((KeyValuePair<int, string>)xxx).Key;
                        // в базу пишем только не повторные считывания
                        if (goodRest == 0)
                        {
                            write2sqlite(lastPassage);
                        }
                        if (bered_flag)
                        {
                            buttonBeRed_Click(sender, args);
                        }
                    }
                }

            }
        }

        private long restToGoodRepeat(string card)
        {
            long Result = 0;
            double tsUTC = ManRest.getLastPassageByCard(card);
            // скан нашелся
            if (tsUTC > 0)
            {
                double tmp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds - tsUTC * 1000;
                if (tmp > 0)
                {
                    Result = (long)Math.Ceiling(tmp/1000);  
                }
            }
            return (Result>=InacceptebleInterval) ? 0 : Result;
        }

        private void dictionaryWorkersUpdater()
        {
            List<WorkerPerson> wplist = new List<WorkerPerson>();
            Persons = new Dictionary<string, string>();
            Persons.Clear();

            PersonsDictStruct = new Dictionary<string, WorkerPerson>();
            PersonsDictStruct.Clear();

            wplist.AddRange(ManRest.getNewWorkersList());

            for (int i = 0; i < wplist.Count; i++)
            {
                PersonsDictStruct.Add($"{wplist[i].card}", wplist[i]);
            }
            
            if (PersonsDictStruct.Count == 0)
            {
                MessageBox.Show("Справочник персонала поврежден!\r\nСервис не позволяет идентифицировать персонал!");
            }
            //            mySnifferForm.UpdatePersons(PersonsDictStruct);
            // Unix timestamp is seconds past epoch

            tsUpdated local_updated = new tsUpdated();
            local_updated.timestampUTC = ManRest.getLastWorkersUpdateTimestamp();
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
                if (arguments.Length > 2)
                {
                    if (arguments[2] == "-userest")
                    {
                        useRest = true;
                    }
                }
            }
            
            // ПЕРЕМЕСТИТЬ в сервис!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //dictionaryWorkersUpdater();
            MainTableReload(this, e);
            /*
            threadWorkersUpdater.DoWork += updateWorkers;
            threadWorkersUpdater.RunWorkerCompleted += updateWorkers_ResultHandler;
            threadPassageSender.DoWork += sendPassage;
            threadPassageSender.RunWorkerCompleted += sendPassage_ResultHandler;
            */
            //timerWorkersUpdate_Tick(this, e);
            //timerPassageSender_Tick(this, e);

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

  
        private void updateWorkers_ResultHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            labelHostAccess.Text = restSrvState ? "Доступен" : "Недоступен";
            // обновляем словарь работников в памяти из локальной БД
            dictionaryWorkersUpdater();
            timerWorkersUpdate.Enabled = true;
        }



        private void sendPassage_ResultHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            timerPassageSender.Enabled = true;
            //label17.Text = send_cnt.ToString();
            if (send_cnt > 0)
            {
                MainTableReload(this, new EventArgs());
            };
            send_cnt = 0;
        }

        private void timerPassageSender_Tick(object sender, EventArgs e)
        {
            timerPassageSender.Enabled = false;
            threadPassageSender.RunWorkerAsync();
        }

        private Passage1bit bit1PassageByPassage(Passage firstUndelivered) {
            Passage1bit firstUndelivered1bit = new Passage1bit();
            firstUndelivered1bit.bit1_id = firstUndelivered.rowID;
            firstUndelivered1bit.bit1_reader_id = firstUndelivered.kppId;
            firstUndelivered1bit.bit1_comment = firstUndelivered.description;
            firstUndelivered1bit.bit1_card = firstUndelivered.card;
            firstUndelivered1bit.bit1_timestampUTC = (long)firstUndelivered.timestampUTC;
            firstUndelivered1bit.bit1_system = "desktop_app";
            firstUndelivered1bit.bit1_tabnom = firstUndelivered.tabnom.ToString();
            firstUndelivered1bit.bit1_opercode = firstUndelivered.operCode.ToString();
            firstUndelivered1bit.bit1_lat = 0;
            firstUndelivered1bit.bit1_lon = 0;
            return firstUndelivered1bit;
        }





        public void MainTableReload(object sender, EventArgs e)
        {
            List<PassageFIO> hotlist = new List<PassageFIO>();
            listViewHotBuffer.Visible = false;
            int cnt = 0;

            int isDaily = radioButtonDaily.Checked ? 1 : 0;

            if (useRest)
            {
                hotlist.AddRange(ManRest.getHotPassagesFIO_REST(isDaily));
            }
            else
            {
                hotlist.AddRange(ManRest.getHotPassagesFIODB(isDaily));
            }

            try { 
                // очистка таблицы
                while (listViewHotBuffer.Items.Count > 0) { listViewHotBuffer.Items.RemoveAt(0); };
                // заполнение таблицы
                foreach (PassageFIO first_pass in hotlist)
                {

                    ListViewItem lvi = new ListViewItem();
                    int zIdx = 0;
                    if (first_pass.isDelivered == 1)
                    {
                        zIdx = 1;
                    }

                    //lvi.Text = $"{cnt}";
                    lvi.Text = "";
                    if (first_pass.card != "")
                    {
                        lvi.SubItems.Add(first_pass.card);
                    }
                    else {
                        lvi.SubItems.Add("-");
                    }
                    if (first_pass.fio != "")
                    {
                        if (first_pass.tabnom != 0) { lvi.SubItems.Add($"{first_pass.tabnom}"); }
                        else { lvi.SubItems.Add($"-"); }

                        lvi.SubItems.Add($"{first_pass.fio}");
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

                    string eventType = "a";
                    if (first_pass.isManual == 1)
                    {
                        eventType = "m";
                    }
                    if (first_pass.fio == "")
                    {
                        eventType = "r";
                    }
                    if (eventType == "a") { cnt++; if (first_pass.toDelete == 1) { cnt--; }; };
                    if (eventType == "m") { cnt++; if (first_pass.toDelete == 1) { cnt--; }; };
                    

                    lvi.SubItems.Add($"{finalManual}");

                    lvi.SubItems.Add($"{first_pass.passageID}-{eventType}-{first_pass.operCode}");
                    if (first_pass.toDelete == 1)
                    {
                        lvi.SubItems.Add(symbol_deleteMark);
                    }


                    listViewHotBuffer.Items.Insert(0, lvi);

                }
            }
            finally
            {
                listViewHotBuffer.Visible = true;
            }
            labelEventCounter.Text = $"{cnt}";
        }



        /*
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
 */
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
            cardTextSelect_Click(sender, e);
            if (e.Column >= 1 && e.Column <= 8)
            {

                panelFilterSelect.Visible = true;
                switch (e.Column)
                {
                    case 1:
                        tabSubfilter.SelectTab(0);
                        tabSubfilter.Visible = true;
                        break;
                    case 2:
                        tabSubfilter.SelectTab(1);
                        tabSubfilter.Visible = true;
                        break;
                    case 3:
                        tabSubfilter.SelectTab(2);
                        tabSubfilter.Visible = true;
                        break;
                    case 4:

                        tabSubfilter.Visible = false;

                        break;
                    case 5:
                        tabSubfilter.SelectTab(3);
                        tabSubfilter.Visible = true;
                        break;
                    case 8:
                        tabSubfilter.SelectTab(4);
                        tabSubfilter.Visible = true;

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
            List<PassageFIO> passages = new List<PassageFIO>();
            listViewHistory.Columns[1].ImageIndex = 0;
            listViewHistory.Columns[2].ImageIndex = 0;
            listViewHistory.Columns[3].ImageIndex = 0;
           // listViewHistory.Columns[4].ImageIndex = 0;
            listViewHistory.Columns[5].ImageIndex = 0;
            columnDelivery.ImageIndex = 0;

            string filterName = "";
            string filterValue = "";


            bool withFilter = tabSubfilter.Visible;
            tabSubfilter.Visible = false;
           // panelFilterSelect.Visible = false;

            #region history view update
            /*
            string from_clause = " FROM buffer_passage p ";
            // готовим фильтрацию
            long tsUTCbeg = (long)begPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            long tsUTCend = (long)begPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds + (long)numericHours.Value*3600;
            string where_clause = $" where p.timestampUTC >= {tsUTCbeg} and p.timestampUTC <= {tsUTCend} ";
            */
            if (withFilter)
            {
                listViewHistory.Columns[1].ImageIndex = 0;
                listViewHistory.Columns[2].ImageIndex = 0;
                listViewHistory.Columns[3].ImageIndex = 0;
                //listViewHistory.Columns[4].ImageIndex = 0;
                listViewHistory.Columns[5].ImageIndex = 0;
                columnDelivery.ImageIndex = 0;
                columnDate.ImageIndex = -1;
                switch (tabSubfilter.SelectedIndex)
                {
                    case 0:
                        columnCard.ImageIndex = 1;
                        filterName = "card";
                        filterValue = cardTextSelect.Text;
                        //where_clause += $" and p.card='{cardTextSelect.Text}' ";
                        break;
                    case 1:
                        columnTabnom.ImageIndex = 1;
                        filterName = "tabnom";
                        filterValue = tabnomTextSelect.Text;
                        //                        where_clause += $" and p.tabnom={tabnomTextSelect.Text} ";
                        break;
                    case 2:
                        columnFIO.ImageIndex = 1;
                        filterName = "fio";
                        filterValue = fioTextSelect.Text;

                        //from_clause = " FROM buffer_passage p, buffer_workers w ";
                        //where_clause += $" and p.tabnom=w.tabnom and w.fio is not null and w.fio LIKE '%{fioTextSelect.Text}%' ";
                        break;
                    case 3:
                        columnOperation.ImageIndex = 1;
                        if (comboBoxHistoryOperations.SelectedIndex != -1)
                        {
                            object xxx = comboBoxHistoryOperations.SelectedItem;
                            /*
                            lastPassage.operCode = ((KeyValuePair<int, string>)xxx).Key;
                            string[] arr2 = new string[0];
                            */
                            int ch = ((KeyValuePair<int, string>)xxx).Key;

                            filterName = "operation";
                            filterValue = $"{ch}";

                            //where_clause += $" and isOut={ch} ";
                        };
                        break;
                    case 4:
                        columnDelivery.ImageIndex = 1;
                        filterName = "delivered";
                        filterValue = $"{(radioDelivered.Checked ? 1 : 0)}";
                        /*
                        if (radioDelivered.Checked)
                        {
                            where_clause += $" and isDelivered=1";
                        }
                        else
                        {
                            where_clause += $" and isDelivered=0";
                        }
                        */
                        break;
                }
            }
            long timestampUTC = (long)begPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            //long timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            listViewHistory.Visible = false;
            int cnt = 1;
            // очистка
            while (listViewHistory.Items.Count > 0) { listViewHistory.Items.RemoveAt(0); };
            // заполнение
            try { 
                

                if (useRest)
                {
                    passages.AddRange(ManRest.getFilteredPassagesFIO_REST(filterName, filterValue, timestampUTC, (int)numericHours.Value));
                }
                else
                {
                    passages.AddRange(ManRest.getFilteredPassagesFIODB(filterName, filterValue, timestampUTC, (int)numericHours.Value));
                }

                foreach (var history_pass in passages)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = "      ";

                    //lvi.Text = "";
                    lvi.SubItems.Add(history_pass.card);

                    if (history_pass.fio != "")
                    {
                        if (history_pass.tabnom!=0) {lvi.SubItems.Add($"{history_pass.tabnom}");}
                        else { lvi.SubItems.Add($""); }
                        lvi.SubItems.Add($"{history_pass.fio}");
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

                    lvi.SubItems.Add($"{cnt}");

                    if (history_pass.isDelivered == 0)
                    {
                        lvi.SubItems.Add("⌛");
                    }


                    listViewHistory.Items.Insert(0, lvi);
                    cnt++;
                }

            }
            finally
            {
                listViewHistory.Visible = true;
            }
            labelSelectedEventsCount.Text = $"Всего записей: {cnt - 1}";

            #endregion history update

        }

        private void clearDetectionView()
        {
            labelEventName.Text = "-";
            labelEventName.ForeColor = Color.Black;
            labelEventFamOtc.Text = "";
            labelEventCard.Text = "-";
            labelEventCard.ForeColor = Color.Black;
            labelEventJobDescription.Text = "";
            labelEventUserguid.Text = "-";
            labelEventUserguid.ForeColor = Color.Black;
            labelEventDate.Text = "-";
            panelSignal2.BackColor = Color.Transparent;
        }

        private void makeCheck(object sender, EventArgs e)
        {
            clearDetectionView();
            ManRest.updatePassage("check", new Passage(), useRest);
            
            MainTableReload(sender, e);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            MainTableReload(sender, e);
        }

        private void radioButtonDaily_Click(object sender, EventArgs e)
        {
            MainTableReload(sender, e);
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
            //editManualEventTabnom.Value = 0;
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
            MainTableReload(sender, e);
        }


        #region hints handling

        private string getWorkerByHint(string entityName, string entityValue)
        {

            List<WorkerPerson> workerPersons = new List<WorkerPerson>();
            workerPersons.AddRange(ManRest.getFilteredWorkersByEntity(entityName, entityValue,useRest));

            string result = "";
            if (workerPersons.Count > 0)
            {
                result = workerPersons[0].card + "@" + workerPersons[0].fio.Replace("@", " ") + "@" + workerPersons[0].userguid;
            }

/*            
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
*/
            return result;
        }

        private void RiseMyHint(ref ListBox hintsListBox, string entityName, string entityTemplate)
        {
            string entityValue;
            hintsListBox.Items.Clear();
            hintsListBox.Visible = true;
            List<WorkerPerson> workerPersons = new List<WorkerPerson>();
            workerPersons.AddRange(ManRest.getFilteredWorkersByEntity(entityName, entityTemplate,useRest));

            foreach (WorkerPerson worker in workerPersons)
            {
                if (entityName == "fio")
                {
                    entityValue = worker.fio.Replace("@", " ");
                    hintsListBox.Items.Add(entityValue);
                }
                if (entityName == "card")
                {
                    entityValue = worker.card;
                    hintsListBox.Items.Add(entityValue);
                }
                if (entityName == "userguid")
                {
                    entityValue = $"{worker.userguid}";
                    hintsListBox.Items.Add(entityValue);
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
                RiseMyHint(ref hintsManualEventCard, "card", editManualEventCard.Text);
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
                RiseMyHint(ref hintsManualEventFIO, "fio", editManualEventFIO.Text);
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
                RiseMyHint(ref hintsManualEventGUID, "userguid", editManualEventGUID.Text);
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
        }
        #endregion hints handling

        private void buttonOKManualEvent_Click(object sender, EventArgs e)
        {
            panelSignal2.BackColor = Color.Transparent;
            Passage p = new Passage();
            p.isManual = 1;
            p.card = editManualEventCard.Text;
            //p.tabnom = (int)editManualEventTabnom.Value;
            p.userguid = editManualEventGUID.Text;
            p.description = editManualEventComment.Text;
            //object xxx = comboManualEventOperation.SelectedItem;
            p.timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            p.operCode = ((KeyValuePair<int, string>)comboManualEventOperation.SelectedItem).Key;
            p.kppId = Environment.MachineName;
            p.rowID = "";
            write2sqlite(p);
            clearDetectionView();
            WorkerPerson wp = getWorkerByGUID(p.userguid);

            string[] stmp = wp.fio.Split('@');
            if (stmp.Length > 0) {
                labelEventName.Text = stmp[0];
            }
            if (stmp.Length > 1)
            {
                labelEventFamOtc.Text = stmp[1];
                if (stmp.Length > 2)
                {
                    labelEventFamOtc.Text = stmp[1] + " " + stmp[2];
                }
            }

            labelEventCard.Text = wp.card;
            labelEventJobDescription.Text = wp.jobDescription;
            labelEventUserguid.Text = wp.userguid;
            System.DateTime dtDateTime = DateTime.Now;
            labelEventDate.Text = dtDateTime.ToShortDateString() + " " + dtDateTime.ToShortTimeString();
            buttonCancelManualEvent_Click(sender, e);
            MainTableReload(sender, e);
            buttonBeGreen_Click(sender, e);
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

            var p = new Passage();
            p.operCode = int.Parse(operCode);
            p.passageID = int.Parse(labelRedEventID.Text);
            p.description = editRedEventComment.Text;
            
            ManRest.updatePassage("red",p, useRest);

            tabControl1.SelectTab(0);
            editRedEventComment.Text = "";
            MainTableReload(sender, e);
        }

        private void buttonDeleteGreenEvent_Click(object sender, EventArgs e)
        {

            ManRest.deleteManualPassageByID(labelGreenEventID.Text);
            clearDetectionView();
            /*
            using (var connection = new SQLiteConnection(sqlite_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"delete from buffer_passage where passageID = {labelGreenEventID.Text} and isDelivered=0";
                command.ExecuteNonQuery();
            }
            */
            buttonCancelGreenEvent_Click(sender, e);


            MainTableReload(sender, e);
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

            if (editGreenEventFIO.Text.Length >= sensibleTextLenght - 3)
            {
                RiseMyHint(ref hintsGreenEventFIO, "fio", editGreenEventFIO.Text);
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
                RiseMyHint(ref hintsGreenEventCard, "card", editGreenEventCard.Text);
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
                RiseMyHint(ref hintsGreenEventGUID, "userguid", editGreenEventGUID.Text);
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

        }

        private void buttonOkGreenEvent_Click(object sender, EventArgs e)
        {
            Passage p = new Passage();
            p.isManual = 1;
            p.card = editGreenEventCard.Text;
            //p.tabnom = (int)editGreenEventTabnom.Value;
            p.userguid = editGreenEventGUID.Text;
            p.description = editGreenEventComment.Text;
            //object xxx = comboManualEventOperation.SelectedItem;
            p.timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            p.operCode = ((KeyValuePair<int, string>)comboGreenEventOperation.SelectedItem).Key;
            p.passageID = int.Parse(labelGreenEventID.Text);
            uodate2sqlite(p);
            buttonCancelGreenEvent_Click(sender, e);
            MainTableReload(sender, e);
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
            List<WorkerPerson> workers = new List<WorkerPerson>();

            if (entityValue.Length < 3) return;
            LV.Visible = false;
            // очистка
            while (LV.Items.Count > 0) { LV.Items.RemoveAt(0); };
            // заполнение
            workers.AddRange(ManRest.getFilteredWorkersByEntity(entityName, entityValue,useRest));

            foreach (WorkerPerson worker in workers)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = worker.card;
                lvi.SubItems.Add($"{worker.tabnom}");
                lvi.SubItems.Add(worker.userguid);
                lvi.SubItems.Add(worker.fio.Replace("@", " "));
                LV.Items.Insert(0, lvi);

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



        private void buttonResetFilter_Click(object sender, EventArgs e)
        {
            numericHours.Value = 72;
            begPickerSelect.Value = DateTime.Now.AddHours(-(long)numericHours.Value);
            tabSubfilter.Visible = false;
            buttonHistorySelect_Click(sender, e);
        }

        private void buttonMarkToDelete_Click(object sender, EventArgs e)
        {
            if (listViewHotBuffer.SelectedItems.Count > 0)
            {
                string[] spl = labelShomItem.Text.Split('-');
                if (spl.Length > 1)
                {
                    var p = new Passage();
                    p.passageID = int.Parse(spl[0]);


                    ManRest.updatePassage("markdelete", p, useRest);


                    MainTableReload(sender, e);
                }
            }
        }

        private void PaintByColor(Color col)
        {

            LayPanel.BackColor = col;
            if (listViewHotBuffer.Items.Count > 0)
            {
                listViewHotBuffer.Items[0].BackColor = col;
                for (int i = 0; i < listViewHotBuffer.Items[0].SubItems.Count - 1; i++)
                {
                    listViewHotBuffer.Items[0].SubItems[i].BackColor = col;
                }
            }

        }


        private void buttonBeRed_Click(object sender, EventArgs e)
        {
            timerCol.Enabled = true;
            PaintByColor(buttonBeRed.BackColor);
        }

        private void buttonBeGreen_Click(object sender, EventArgs e)
        {
            timerCol.Enabled = true;
            PaintByColor(buttonBeGreen.BackColor); 
        }

        private void buttonBeWhite_Click(object sender, EventArgs e)
        {
            PaintByColor(buttonBeWhite.BackColor); 
        }

        private void timerCol_Tick(object sender, EventArgs e)
        {
            timerCol.Enabled = false;
            PaintByColor(Color.White);
        }



        private void label11_DoubleClick(object sender, EventArgs e)
        {
            //timerEraser_Tick(sender, e);
        }

        private void listViewHotBuffer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 134123-a полностью автоматическое событие
            // 134123-m полностью ручное событие
            // 134123-r красное событие
            if (listViewHotBuffer.SelectedItems.Count < 1) return;
            labelShomItem.Text = listViewHotBuffer.SelectedItems[0].SubItems[7].Text;
            string toDelete = "";
            if (listViewHotBuffer.SelectedItems[0].SubItems.Count > 8)
            {
                toDelete = listViewHotBuffer.SelectedItems[0].SubItems[8].Text;
            }
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

                    myComment += ManRest.getCommentByPassageID(spl[0]);
                    myGUID = ManRest.getGUIDByPassageID(spl[0]);
                    // изменять введенное вручную
                    if (spl[1] == "m")
                    {
                        

                        editGreenEventFIO.Text = myFIO;
                        editGreenEventGUID.Text = myGUID;
                        editGreenEventComment.Text = myComment;
                        comboGreenEventOperation.SelectedValue = int.Parse(spl[2]);

                        labelGreenEventID.Text = spl[0];
                        editGreenEventCard.Text = myCard;
                        tabControl1.SelectTab(4);
                    }
                    // изменять введенное автоматически, но без персоны
                    if (spl[1] == "r" | spl[1] == "a")
                    {
                        editRedEventCard.Text = myCard;
                        editRedEventFIO.Text = "";
                        editRedEventGUID.Text = "";
                        editRedEventComment.Text = "";

                        labelRedEventID.Text = spl[0];

                        labelRedOperation.Text = "";
                        comboRedEventOperation.SelectedValue = int.Parse(spl[2]);
                        comboRedEventOperation.Enabled = true;
                        bool switchable = true;
                        // автоматическая и хорошая, но помеченная к удалению выбирается для редактирования
                        if (spl[1] == "a")
                        {
                            //switchable = false;
                            //if (toDelete == symbol_deleteMark)
                            //{
                                comboRedEventOperation.Enabled = false;
                                editRedEventFIO.Text = myFIO;
                                editRedEventGUID.Text = myGUID;
                                editRedEventComment.Text = myComment;
                                switchable = true;
                            //}
                        };

                        if (switchable)
                        {
                            tabControl1.SelectTab(3);
                        }
                        labelRedOperation.Text = spl[2];
                        editRedEventComment.Text = myComment;

                        //    comboRedEventOperation.Items.a
                    }

                    /*
                    for (int i = 0; i < listViewHotBuffer.Items.Count; i++)
                    {
                        listViewHotBuffer.Items[i].Selected = false;
                    }
                    */
                }
                //listViewHotBuffer.Select();
            }

        }

        private void listViewHotBuffer_MouseUp(object sender, MouseEventArgs e)
        {
            if (listViewHotBuffer.SelectedItems.Count > 0)
            {
                labelShomItem.Text = listViewHotBuffer.SelectedItems[0].SubItems[7].Text;
                string[] spl = labelShomItem.Text.Split('-');
                if (spl.Length <2 )
                {
                    labelShomItem.Text = "";
                }
            }
            else
            {
                labelShomItem.Text = "";
            }
            
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            buttonResetFilter_Click(sender, e);
        }

        private void editManualEventGUID_TextChanged(object sender, EventArgs e)
        {
            bool writePossible = false;
            //if (editManualEventCard.Text.Length < sensibleTextLenght - 1) { return; };
            if (editManualEventGUID.Text.Length < sensibleTextLenght) { return; };
            if (comboManualEventOperation.Text == "") { return; };
            string userguid = editManualEventGUID.Text;
            WorkerPerson workerPerson = new WorkerPerson();
            workerPerson.userguid = "";
            workerPerson.fio = "";
            workerPerson = getWorkerByGUID(userguid);  
            if (workerPerson.fio != "")
            {
                //editManualEventTabnom.Value = workerPerson.tabnom;
                writePossible = true;
            }
            buttonOKManualEvent.Enabled = writePossible;
            buttonOKManualEvent.BackColor = writePossible ? Color.Teal : Color.Gainsboro;
        }

        private void editGreenEventGUID_TextChanged(object sender, EventArgs e)
        {
            bool writePossible = false;
            //if (editManualEventCard.Text.Length < sensibleTextLenght - 1) { return; };
            if (editGreenEventGUID.Text.Length < sensibleTextLenght) { return; };
            if (comboGreenEventOperation.Text == "") { return; };
            string userguid = editGreenEventGUID.Text;
            WorkerPerson workerPerson = new WorkerPerson();
            workerPerson.userguid = "";
            workerPerson.fio = "";
            workerPerson = getWorkerByGUID(userguid);
            if (workerPerson.fio != "")
            {
                //editManualEventTabnom.Value = workerPerson.tabnom;
                writePossible = true;
            }
            buttonOkGreenEvent.Enabled = writePossible;
            buttonOkGreenEvent.BackColor = writePossible ? Color.Teal : Color.Gainsboro;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonPOST_Click(object sender, EventArgs e)
        {
            Passage1bitExt bit = new Passage1bitExt();
            try
            {
                using (var connection = new SQLiteConnection(sqlite_connectionstring))
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = $"select w.card, w.tabnom, p.isOut, p.timestampUTC, p.description,w.userguid from buffer_passage p left join buffer_workers w on p.userguid=w.userguid  where p.passageID={labelGreenEventID.Text}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            /*

                            [JsonProperty("id")] public string bit1_id { get; set; }
                            [JsonProperty("system")] public string bit1_system { get; set; }
                            [JsonProperty("timestamp")] public long bit1_timestampUTC { get; set; }
                            [JsonProperty("card_number")] public string bit1_card_number { get; set; }
                            [JsonProperty("card_guid")] public string bit1_card_guid { get; set; }
                            [JsonProperty("position_guid")] public string bit1_position_guid { get; set; }
                            [JsonProperty("individual_guid")] public string bit1_individual_guid { get; set; }
                            [JsonProperty("reader_id")] public string bit1_reader_id { get; set; }
                            [JsonProperty("description")] public string bit1_comment { get; set; }
                            [JsonProperty("personnel_number")] public string bit1_tabnom { get; set; }
                            [JsonProperty("type")] public string bit1_opercode { get; set; }
                            [JsonProperty("control_point_type_id")] public int bit1_control_point_type_id { get; set; }
                                "id": "98ac5735-68af-47d1-8d25-3edc311632a0-1",
    "system": "stop-covid",
    "timestamp": 1650440387,
    "card_number": "47 3354",
    "card_guid": "47 3354",
    "position_guid": "101",
    "reader_id": 101,
    "description": "[111111]-ok477ewk-1bit",
    "personnel_number": "674",
     "individual_guid":"0aa46cdd-9bd2-11ea-912c-00505684313d",
    "type": "input",
    "control_point_type_id": 3
                            */
                            bit.bit1_id = runningInstanceGuid + $"-{bit.bit1_timestampUTC}";
                            bit.bit1_system = "stop-covid";
                            bit.bit1_timestampUTC = (int)reader.GetDouble(3);
                            bit.bit1_card_number = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            bit.bit1_card_guid = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            bit.bit1_position_guid = "101";
                            bit.bit1_individual_guid = "0aa46cdd-9bd2-11ea-912c-00505684313d";
                            bit.bit1_reader_id = 101;
                            bit.bit1_comment = "[" + (reader.IsDBNull(4) ? "" : reader.GetString(4)) + "]-" + (reader.IsDBNull(5) ? "" : reader.GetString(5));
                            if (!reader.IsDBNull(1)) { 
                                bit.bit1_tabnom = $"{reader.GetInt64(1)}";
                            }
                            //bit.bit1_opercode = $"{reader.GetInt64(2)}";
                            bit.bit1_opercode = "input";
                            bit.bit1_control_point_type_id = 3;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Подготовка формата отправки:\n" + ex.Message);
                return;
            }

            // restsharp
            // выбираем первый неотправленный passage и если массив непустой - отправляем через rest
            // если успешно отправлось - помечаем passageID отправленным и обновляем главную таблицу
            // получить наименьший локальный проход
            // отправить 
            // оценить результат
            // обновить состояние или не обновлять
            // удаление доставленных - другим методом
            string tokentoken = "";
            try { 
                var client0 = new RestClient($"{restServerAddr}/auth/login/");
                client0.Timeout = 5000;
                var request0 = new RestRequest(Method.POST);
                request0.AddHeader("Content-Type", "application/json");
                var body0 = JsonConvert.SerializeObject(new { login = loginbox.Text, password = passwordbox.Text });
                request0.AddParameter("application/json", body0, ParameterType.RequestBody);
                IRestResponse response0 = client0.Execute(request0);

                if (response0.IsSuccessful)
                {
                    var zlist = response0.Content.Split(':');
                    if (zlist.Length > 2)
                    {
                        tokentoken = zlist[2].Replace('"',' ').Replace('}', ' ');
                    }
                }
                else
                {
                    MessageBox.Show("Неудачная авторизация!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Авторизация:\n"+ex.Message);
            }


            //            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "password");

            try
            {
                //var client = new RestClient($"{restServerAddr}/reading-event/");
                var client = new RestClient($"{restServerAddr}/reading-event/");
                client.Timeout = 5000;
                var request = new RestRequest(Method.POST);

                //            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "password");

                //          request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                request.AddHeader("Authorization", $"Bearer {tokentoken}");

                request.AddHeader("Accept", "*" + "/" + "*");
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(bit);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (true)
                {

                    string qry_update_mark_id_asdelivered = $"update buffer_passage set isDelivered=1 where (isDelivered=0 or isDelivered=2) and passageID={labelGreenEventID.Text}";
                    using (var connection = new SQLiteConnection(sqlite_connectionstring))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = qry_update_mark_id_asdelivered;
                        command.ExecuteNonQuery();

                        send_cnt++;
                    }
                    MessageBox.Show("Доставлено успешно!");
                }
                else
                {
                    MessageBox.Show("Не доставлено!\nСм. Error.txt");
                    File.WriteAllText("Error.txt", body + "\n" + response.Content.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"POST {restServerAddr}/reading-event/:\n" + ex.Message);
            }

        }

        private void buttonHistoryReload_Click(object sender, EventArgs e)
        {
            List<PassageFIO> passages = new List<PassageFIO>();
            listViewHistory.Columns[1].ImageIndex = 0;
            listViewHistory.Columns[2].ImageIndex = 0;
            listViewHistory.Columns[3].ImageIndex = 0;
            // listViewHistory.Columns[4].ImageIndex = 0;
            listViewHistory.Columns[5].ImageIndex = 0;
            columnDelivery.ImageIndex = 0;

            string filterName = "";
            string filterValue = "";


            bool withFilter = tabSubfilter.Visible;
            tabSubfilter.Visible = false;
            // panelFilterSelect.Visible = false;

            #region history view update
            /*
            string from_clause = " FROM buffer_passage p ";
            // готовим фильтрацию
            long tsUTCbeg = (long)begPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            long tsUTCend = (long)begPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds + (long)numericHours.Value*3600;
            string where_clause = $" where p.timestampUTC >= {tsUTCbeg} and p.timestampUTC <= {tsUTCend} ";
            */
            if (withFilter)
            {
                listViewHistory.Columns[1].ImageIndex = 0;
                listViewHistory.Columns[2].ImageIndex = 0;
                listViewHistory.Columns[3].ImageIndex = 0;
                //listViewHistory.Columns[4].ImageIndex = 0;
                listViewHistory.Columns[5].ImageIndex = 0;
                columnDelivery.ImageIndex = 0;
                columnDate.ImageIndex = -1;
                switch (tabSubfilter.SelectedIndex)
                {
                    case 0:
                        columnCard.ImageIndex = 1;
                        filterName = "card";
                        filterValue = cardTextSelect.Text;
                        //where_clause += $" and p.card='{cardTextSelect.Text}' ";
                        break;
                    case 1:
                        columnTabnom.ImageIndex = 1;
                        filterName = "tabnom";
                        filterValue = tabnomTextSelect.Text;
                        //                        where_clause += $" and p.tabnom={tabnomTextSelect.Text} ";
                        break;
                    case 2:
                        columnFIO.ImageIndex = 1;
                        filterName = "fio";
                        filterValue = fioTextSelect.Text;

                        //from_clause = " FROM buffer_passage p, buffer_workers w ";
                        //where_clause += $" and p.tabnom=w.tabnom and w.fio is not null and w.fio LIKE '%{fioTextSelect.Text}%' ";
                        break;
                    case 3:
                        columnOperation.ImageIndex = 1;
                        if (comboBoxHistoryOperations.SelectedIndex != -1)
                        {
                            object xxx = comboBoxHistoryOperations.SelectedItem;
                            /*
                            lastPassage.operCode = ((KeyValuePair<int, string>)xxx).Key;
                            string[] arr2 = new string[0];
                            */
                            int ch = ((KeyValuePair<int, string>)xxx).Key;

                            filterName = "operation";
                            filterValue = $"{ch}";

                            //where_clause += $" and isOut={ch} ";
                        };
                        break;
                    case 4:
                        columnDelivery.ImageIndex = 1;
                        filterName = "delivered";
                        filterValue = $"{(radioDelivered.Checked ? 1 : 0)}";
                        /*
                        if (radioDelivered.Checked)
                        {
                            where_clause += $" and isDelivered=1";
                        }
                        else
                        {
                            where_clause += $" and isDelivered=0";
                        }
                        */
                        break;
                }
            }
            long timestampUTC = (long)begPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            //long timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            listViewHistory.Visible = false;
            int cnt = 1;
            // очистка
            while (listViewHistory.Items.Count > 0) { listViewHistory.Items.RemoveAt(0); };
            // заполнение
            try
            {


                if (useRest)
                {
                    passages.AddRange(ManRest.getFilteredPassagesFIO_REST(filterName, filterValue, timestampUTC, (int)numericHours.Value));
                }
                else
                {
                    passages.AddRange(ManRest.getFilteredPassagesFIODB(filterName, filterValue, timestampUTC, (int)numericHours.Value));
                }

                foreach (var history_pass in passages)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = "      ";

                    //lvi.Text = "";
                    lvi.SubItems.Add(history_pass.card);

                    if (history_pass.fio != "")
                    {
                        if (history_pass.tabnom != 0) { lvi.SubItems.Add($"{history_pass.tabnom}"); }
                        else { lvi.SubItems.Add($""); }
                        lvi.SubItems.Add($"{history_pass.fio}");
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

                    lvi.SubItems.Add($"{cnt}");

                    if (history_pass.isDelivered == 0)
                    {
                        lvi.SubItems.Add("⌛");
                    }


                    listViewHistory.Items.Insert(0, lvi);
                    cnt++;
                }

            }
            finally
            {
                listViewHistory.Visible = true;
            }
            labelSelectedEventsCount.Text = $"{cnt - 1}";

            #endregion history update

        }

        private void cardTextSelect_Click(object sender, EventArgs e)
        {

        }
    }

}
