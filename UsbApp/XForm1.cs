using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data.SQLite;
using SQLite;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using UsbLibrary;
using HorizontalAlignment = System.Windows.Forms.HorizontalAlignment;
using MessageBox = System.Windows.Forms.MessageBox;
using NLog;
using System.Linq;
using System.Threading;

namespace kppApp
{

    public partial class MainFormKPP : Form
    {
        private SignalRCover signaler;

        private static SQLiteConnectionString memDBOptions = new SQLiteConnectionString(":memory:");
        private static SQLiteConnection memdb = new SQLiteConnection(memDBOptions);
        private static NLog.Logger logger;

        private int BearerExpiredSeconds = 15 * 60;
        private long BearerLoadUTC = 0;
        private string BearerToken = "";
        private SizeF currentScaleFactor = new SizeF(1f, 1f);
        private string RightPart = "[&B950:#$0F3F91210381]";
        private string LeftPart = "";

        private static readonly string SpecialDataFolder = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)).FullName, "PSISoftware", "AppKPP");
        private static readonly string BufferDatabaseFile = Path.Combine(SpecialDataFolder, "bufferkpp.db3");
        private static readonly string InfoDatabaseFile = Path.Combine(SpecialDataFolder, "paradox.db3");
        private static readonly string InfoTickFile = Path.Combine(SpecialDataFolder, "infotick.txt");
        private static readonly string InfoPluginFile = Path.Combine(SpecialDataFolder, "httpsrest.dll");
        private static readonly string OperationsJSONFile = Path.Combine(SpecialDataFolder, "operations.json");
        internal Dictionary<string, int> ParamsIndexes = new Dictionary<string, int>
        {
            { "card", 0 },
            { "tabnom", 1 },
            { "fio", 2 },
            { "operation", 3 },
            { "delivered", 4 },
        };

        internal Dictionary<string, string> SQLFilters = new Dictionary<string, string>
        {
        };
        bool MustClose = false;
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
        private string passageDirection = "";
        private int reader_id = 777;
        private string restServerAddr = "http://localhost:3002";
        private string restapiAuthEnabled = "1";
        //internal string sqlite_connectionstring = "Data Source=c:\\appkpp\\kppbuffer.db;Version=3;New=False";

        private string statusCodeOK = "201";
        private int prev_passageID = -2;
        private bool was_sended = false;
        long send_cnt = 0;
        string[] operSource;
        //WcfServer srv;

        public bool useRest = false;

        #region scaling
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
        #endregion scaling

        public MainFormKPP()
        {

            InitializeComponent();
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console


            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = Path.Combine(SpecialDataFolder, "appkpp-log-${shortdate}.txt"),
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message:withException=true}",
                ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.Sequence,
                ArchiveAboveSize = 5242880,
                MaxArchiveFiles = 30
            };
            //MessageBox.Show(logfile.Layout.ToString());

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;
            NLog.Common.InternalLogger.LogLevel = LogLevel.Debug;
            NLog.Common.InternalLogger.LogFile = Path.Combine(SpecialDataFolder, "internal-log.txt"); // On Linux one can use "/home/nlog-internal.txt"
            logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Запуск приложения");


            if (!SettingsRead())
            {
                MessageBox.Show("Не удалось прочитать настройки из appkpp.ini");
            }
            else
            {
                logger.Info("Настройки успешно прочитаны из appkpp.ini");
            };

            if (!PrepareDataFolder())
            {
                return;
            };


            //ManRest = new LocalRESTManager(sqlite_connectionstring);

            listViewHistory.Columns[1].ImageIndex = 0;
            listViewHistory.Columns[2].ImageIndex = 0;
            listViewHistory.Columns[3].ImageIndex = 0;
            listViewHistory.Columns[4].ImageIndex = 0;
            listViewHistory.Columns[5].ImageIndex = 0;
            listViewHistory.Columns[8].ImageIndex = 0;
            while (listViewHistory.Items.Count > 0) { listViewHistory.Items.RemoveAt(0); };
            //            columnDelivery.ImageIndex = 0;
            tabControl1.ItemSize = new Size(1, 1);
            operCheck.Checked = checkOperations(OperationsJSONFile);
            peopleCheck.Checked = checkPeople();
            if (!operCheck.Checked || !peopleCheck.Checked)
            {
                WaitModeEnable();
            }
            tryUpdateBearer(false);
            fillMemoryDB();
            timerWorkersUpdate_Tick(null, new EventArgs());
        }

        private bool fillMemoryDB()
        {

            var cfm = new CipherManager(InfoPluginFile);
            var doptions = new SQLiteConnectionString(InfoDatabaseFile, true, cfm.getFullPword(RightPart)); ;
            SQLiteConnection diskdb = new SQLiteConnection(doptions);
            try
            {
                logger.Info($"БД {InfoDatabaseFile} -> mem копирование начато");
                //memdb.DropTable<WorkerPerson>();
                memdb.CreateTable<WorkerPersonPure>();
                memdb.InsertAll(diskdb.Table<WorkerPersonPure>());
                logger.Info($"БД {InfoDatabaseFile} -> mem копирование WorkerPersonPure окончено");
                //memdb.DropTable<Position>();
                memdb.CreateTable<Position>();
                memdb.InsertAll(diskdb.Table<Position>());
                logger.Info($"БД {InfoDatabaseFile} -> mem копирование Position окончено");
                //memdb.DropTable<Card>();
                memdb.CreateTable<Card>();
                memdb.InsertAll(diskdb.Table<Card>());
                memdb.CreateCommand(@"create view prettyworker as select w.second_name||'@'||w.first_name||'@'||w.last_name as fio, w.asup_guid as userguid, p.name as job, p.personnel_number as tabnom,d.number as card
                                    from WorkerPersonPure w left
                                    join Position p on p.ownerid = w.id left
                                    join card d on d.ownerid = p.id;").ExecuteNonQuery();
                logger.Info($"БД {InfoDatabaseFile} -> mem копирование Card окончено");
                //MessageBox.Show("Mem done!");
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, $"Ошибка копирования БД {InfoDatabaseFile} -> mem copy");
                return false;
            }
        }

        private bool checkOperations(string fullpathOperations)
        {
            bool myResult = false;
            if (!File.Exists(fullpathOperations)) { return myResult; };
            using (StreamReader file = File.OpenText(fullpathOperations))
            {
                JsonSerializer serializer = new JsonSerializer();
                List<PerimeterOperation> perop = (List<PerimeterOperation>)serializer.Deserialize(file, typeof(List<PerimeterOperation>));
                if (perop.Count > 0)
                {
                    myResult = true;
                }
            }
            return myResult;
        }

        private bool checkPeople()
        {
            bool myResult = false;
            CipherManager cfm = new CipherManager(InfoPluginFile);
            try
            {
                var pword = cfm.getFullPword(RightPart);
                var options = new SQLiteConnectionString(InfoDatabaseFile, true, pword);
                var connection = new SQLiteConnection(options);

                var xcmd = connection.Query<Val>("SELECT name as str FROM sqlite_master;\n").ToArray();
                int tablecounter = 0;
                foreach (Val x in xcmd)
                {
                    if (x.str == "WorkerPersonPure" || x.str == "Position" || x.str == "Card")
                    {
                        tablecounter++;
                    }
                }
                myResult = tablecounter == 3;
                if (myResult)
                {
                    var xcmd2 = connection.Query<ValCnt>("SELECT count(w.id) as cnt FROM WorkerPersonPure w;\n").ToArray();
                    myResult = xcmd2[0].cnt > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "InfoDatabaseFile испорчен");
                myResult = false;
            }

            return myResult;
        }

        private void WaitModeEnable()
        {
            listView1.Enabled = false;
            tabControl1.SelectTab(5);
            timerWaitMode.Enabled = true;
        }

        private void WaitModeDisable()
        {
            listView1.Enabled = true;
            tabControl1.SelectTab(0);
            timerWaitMode.Enabled = false;
        }

        private bool PrepareDataFolder()
        {
            bool myResult = true;

            if (!Directory.Exists(SpecialDataFolder))
            {
                try
                {
                    Directory.CreateDirectory(SpecialDataFolder);
                    //                    logger.Info($"Успех создания папки БД {SpecialDataFolder}");
                }
                catch (Exception ex)
                {
                    //                  logger.Fatal("Ошибка создания папки БД", ex);
                    //reg($"Ошибка создания папки БД: {ex.Message}");
                    MessageBox.Show("Невозможно создать папку БД!\nРабота программы будет прекращена!\nОбратитесь в службу технической поддержки.");
                    //Close();
                }

            }


            try
            {
                using (var fff = File.Create(Path.Combine(SpecialDataFolder, "test.txt")))
                {
                    logger.Info($"Успех создания тестового файла в папке БД {SpecialDataFolder}");
                }

            }
            catch (Exception ex)
            {
                logger.Fatal($"Ошибка записи в папку { SpecialDataFolder}", ex);
                //reg($"Ошибка записи в папку {SpecialDataFolder}: {ex.Message}");
                MessageBox.Show($"Невозможно создать файл в папке {SpecialDataFolder}!\nРабота программы будет прекращена!\nОбратитесь в службу технической поддержки."); ;
                //                Close();
            }
            try
            {
                File.Delete(Path.Combine(SpecialDataFolder, "test.txt"));
                logger.Info($"Успех удаления тестового файла в папке БД {SpecialDataFolder}");
            }
            catch (Exception ex)
            {
                logger.Fatal($"Ошибка удаления в папке { SpecialDataFolder}", ex);
                //reg($"Ошибка записи в папку {SpecialDataFolder}: {ex.Message}");
                MessageBox.Show($"Невозможно удалить файл в папке {SpecialDataFolder}!\nРабота программы будет прекращена!\nОбратитесь в службу технической поддержки."); ;
                //                Close();
            }
            if (!File.Exists(BufferDatabaseFile))
            {
                if (!CreateBufferDatabase())
                {
                    myResult = false;
                    MustClose = true;
                }
            }
            if (!File.Exists(InfoDatabaseFile) || !File.Exists(InfoPluginFile))
            {
                if (!CreateInfoDatabase())
                {
                    myResult = false;
                    MustClose = true;
                }
            }

            return myResult;
        }

        private bool CreateBufferDatabase()
        {
            bool myResult = false;
            try
            {
                var options = new SQLiteConnectionString(BufferDatabaseFile);
                using (var db = new SQLiteConnection(options))
                {
                    db.CreateTable<Passage>();
                    List<Passage> passages = new List<Passage>();
                    db.InsertAll(passages);
                }
                myResult = true;
                logger.Info($"Успех создания буферной БД { BufferDatabaseFile}");
            }
            catch (Exception ex)
            {
                logger.Fatal($"Ошибка создания буферной БД { BufferDatabaseFile}", ex);
                //reg($"Ошибка создания буферной БД {BufferDatabaseFile}: {ex.Message}");
                MessageBox.Show($"Ошибка создания буферной БД {BufferDatabaseFile}: {ex.Message}!\nРабота программы будет прекращена!\nОбратитесь в службу технической поддержки."); ;
            }
            return myResult;
        }

        private bool CreateInfoDatabase()
        {
            Random r = new Random();
            bool myResult = false;
            LeftPart = new string(RightPart.Reverse().ToArray());


            try
            {
                File.Delete(InfoDatabaseFile);
                File.Delete(InfoTickFile);
                logger.Info($"Успех удаления инфоБД");
            }
            catch (Exception ex)
            {
                logger.Info($"Сбой удаления инфоБД", ex);
            }

            var cfm = new CipherManager(InfoPluginFile);
            try
            {
                var pword = LeftPart + $"{r.Next(100000, 999999)}";
                cfm.UpdatePStorage(pword);
                //                File.WriteAllBytes(InfoPluginFile, seedPassword(pword));
                //pword = unseedPassword(File.ReadAllBytes(InfoPluginFile));
                logger.Info($"Успех создания парольного хранилища");
            }
            catch (Exception ex)
            {
                reg($"Невозможно создать хранилище пароля [{ex.Message}]");
            }


            //LeftPart = unseedPassword(File.ReadAllBytes(InfoPluginFile));

            try
            {

                var options = new SQLiteConnectionString(InfoDatabaseFile, true, cfm.getFullPword(RightPart));
                using (var db = new SQLiteConnection(options))
                {
                    db.CreateTable<Card>();
                    db.CreateTable<WorkerPersonPure>();
                    db.CreateTable<Position>();
                }

                logger.Info($"Успех создания справочной БД {InfoDatabaseFile}");
                myResult = true;
            }
            catch (Exception ex)
            {
                logger.Fatal($"Ошибка создания справочной БД {InfoDatabaseFile}", ex);
                File.Delete(InfoPluginFile);
                MessageBox.Show($"Ошибка справочной БД {InfoDatabaseFile}: {ex.Message}!\nРабота программы будет прекращена!\nОбратитесь в службу технической поддержки."); ;
            }

            return myResult;
        }



        private bool SettingsRead()
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
            //bool sqlite_in_settings = INI.KeyExists("sqlite_connectionstring", "settings");
            bool direction_in_settings = INI.KeyExists("passage_direction", "settings");
            bool readerid_in_settings = INI.KeyExists("reader_id", "settings");
            //bool auth_enabled_in_settings = INI.KeyExists("restapi_auth_enabled", "settings");

            if (rest_in_settings & direction_in_settings & readerid_in_settings)
            {
                restServerAddr = INI.Read("restapi_path", "settings");
                passageDirection = INI.Read("passage_direction", "settings");
                try
                {
                    reader_id = int.Parse(INI.Read("reader_id", "settings"));
                }
                catch (Exception ex)
                {
                    reader_id = 777777;
                    logger.Error(ex, $"Ошибка преобразования reader_id из настроек");
                }
                restapi_path_label.Text = restServerAddr;
                //    sqlite_connectionstring = INI.Read("sqlite_connectionstring", "settings");
                logger.Info($"Настройки в appkpp.ini: restapi_path={restServerAddr}, passage_direction={passageDirection}, reader_id={reader_id}");
                result = true;
            }
            else
            {
                logger.Info("Настройки в appkpp.ini не полные, заполнены примерами необходимых ключей");
                // заполняем примерами значений важных ключей
                if (!rest_in_settings) INI.Write("restapi_path", "http://www.google.com/restapi/v1", "settings");
                if (!direction_in_settings) INI.Write("passage_direction", "input", "settings");
                if (!readerid_in_settings) INI.Write("reader_id", "777", "settings");

                restapi_path_label.Text = "Неизвестно";
                result = false;
            }

            //if (!File.Exists(OperationsJSONFile)){
            //    RestLoadOperations(OperationsJSONFile);
            //}

            // read JSON directly from a file
            //            string mypath = AppDomain.CurrentDomain.BaseDirectory  + @"operations.json";
            if (File.Exists(OperationsJSONFile))
            {
                try
                {
                    InitOperationsViews(OperationsJSONFile);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Загрузка операций из файла {OperationsJSONFile}");
                }
            }
            else
            {
                logger.Error($"Загрузка операций из файла {OperationsJSONFile}");
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

        private bool RestLoadOperations(string fullpathOperations)
        {
            bool myResult = true;

            string url = restServerAddr + "control-point?page=";
            AppControlPoints xlist = new AppControlPoints();
            List<PerimeterOperation> polist = new List<PerimeterOperation>();
            var client = new RestClient();
            //var client2 = new RestClient();
            client.Timeout = 5000;

            //client.Execute(request);
            //var response = client.Execute<AppControlPoints>(request);
            var pagecount = 1;
            while (pagecount < 1000)
            {
                var request = new RestRequest(url + $"{pagecount}", Method.GET);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    logger.Error($"Неуспех загрузки {url}");
                    regbb($"Неуспех загрузки {url}");
                    myResult = false;
                }
                else
                {
                    try
                    {
                        xlist = JsonConvert.DeserializeObject<AppControlPoints>(response.Content);
                        //xlist.AddRange(response.Data.data);
                        regbb($"Успех извлечения списка операций, длина={xlist.data.Count}");
                    }
                    catch (Exception ex)
                    {
                        regbb($"Ошибка извлечения списка ControlPoints, длина={xlist.data.Count} [{ex.Message}]");
                        logger.Error(ex, $"Извлечение списка ControlPoints, длина={xlist.data.Count}");
                        myResult = false;
                        return myResult;
                    }
                    if (xlist.data.Count < 1)
                    {
                        break;
                    }
                    foreach (ControlPoint cp in xlist.data)
                    {
                        polist.Add(new PerimeterOperation() { operid = cp.id, operdesc = cp.title, operhide = 0 });
                    }
                }
                pagecount++;
            }
            try
            {
                File.WriteAllText(OperationsJSONFile, JsonConvert.SerializeObject(polist));
                logger.Info($"Успешно сохранен список {polist.Count} операций в {OperationsJSONFile}");
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Сохранение списка ControlPoints в {OperationsJSONFile}");
                myResult = false;
                return myResult;
            }

            myResult = true;


            return myResult;
        }

        private void InitOperationsViews(string fullpathOperations)
        {
            using (StreamReader file = File.OpenText(fullpathOperations))
            {
                JsonSerializer serializer = new JsonSerializer();
                List<PerimeterOperation> perop = (List<PerimeterOperation>)serializer.Deserialize(file, typeof(List<PerimeterOperation>));
                if (perop.Count > 0)
                {
                    //xList<string> xList= new List<string>(); 
                    foreach (PerimeterOperation oper in perop)
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
        }

        private void reg(string mess)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {mess}\r\n");
        }

        private void SettingsOfReaderHandle(bool userest)
        {
            if (!userest)
            {
                usb.OnDataRecieved += usb_OnDataRecieved;
                usb.OnSpecifiedDeviceRemoved += usb_OnSpecifiedDeviceRemoved;
                usb.OnSpecifiedDeviceArrived += usb_OnSpecifiedDeviceArrived;
            }
            else
            {
                signaler = new SignalRCover("http://localhost:5000/endpoint");
                signaler.OnDeviceArrived += usb_OnSpecifiedDeviceArrived;
                signaler.OnDeviceRemoved += usb_OnSpecifiedDeviceRemoved;
                signaler.OnDataRecieved += usb_OnDataRecieved;
                signaler.OnServiceDown += Signaler_OnServiceDown;
                signaler.OnServiceUp += Signaler_OnServiceUp;
                ServiceStateLabel.Visible = true;
                ServiceLabel.Visible = true;
                this.showServiceState(0);
                signaler.Start();
            }
        }

        private void showServiceState(int state)
        {
            ServiceStateLabel.Text = state == 1 ? "Доступна" : "Недоступна";
        }

        private void Signaler_OnServiceUp(object source, MyEventArgs e)
        {
            this.showServiceState(1);
        }

        private void Signaler_OnServiceDown(object source, MyEventArgs e)
        {
            this.showServiceState(0);
        }

        private void usb_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            this.setRFIDFound();
        }


        private void usb_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            this.setRFIDLost();
        }

        private void usb_OnDeviceArrived(object sender, EventArgs e)
        {
            //this.setRFIDFound();
        }

        private void usb_OnDeviceRemoved(object sender, EventArgs e)
        {
            //this.setRFIDLost();
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

        private PrettyWorker getWorkerByGUID(string userguid)
        {
            PrettyWorker myWP = new PrettyWorker();
            myWP.userguid = "";
            myWP.fio = "";
            ManRest.getGUIDOwnerWorker(userguid, ref myWP);
            return myWP;
        }

        private PrettyWorker getWorkerByCard(string card)
        {
            PrettyWorker myWP = new PrettyWorker();
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

        private void usb_OnDataRecieved(object sender, UsbLibrary.DataRecievedEventArgs args)
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new DataRecievedEventHandler(usb_OnDataRecieved), new object[] { sender, args });
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "usb_OnDataRecieved");
                    //Console.WriteLine(ex.ToString());
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
                lastPassage.timestampUTC = TimeLord.UTCNow();
                this.BackColor = Color.DimGray;
                bool bered_flag = false;
                panelSignal2.BackColor = Color.Transparent;
                if (readerBytes.Length > 0)
                {
                    lastPassage.card = readerBytes;
                    clearDetectionView();

                    long goodRest = restToGoodRepeat(lastPassage.card);
                    PrettyWorker myWorkerPerson = getWorkerByCard(readerBytes);
                    string savedGUID = myWorkerPerson.userguid;
                    if (goodRest != 0)
                    {
                        myWorkerPerson.userguid = "";
                        myWorkerPerson.job = $"Ожидайте {InacceptebleInterval - goodRest} сек";
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

                        if (goodRest == 0 || savedGUID == "")
                        {
                            labelEventFamOtc.Text = "";
                            labelEventName.ForeColor = Color.Coral;
                            labelEventUserguid.Text = labelTPL.Text;
                            labelEventUserguid.ForeColor = Color.Coral;
                        }
                        else
                        {
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
                    labelEventJobDescription.Text = myWorkerPerson.job;
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
                    lastPassage.tabnom = myWorkerPerson.tabnom;
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
                double tsnow = TimeLord.UTCNow();
                double tmp = tsnow - tsUTC;
                if (tmp > 0)
                {
                    Result = (long)Math.Ceiling(tmp);
                }
            }
            return (Result >= InacceptebleInterval) ? 0 : Result;
        }

        private void dictionaryWorkersUpdater()
        {
            /*
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
            */
        }

        private void XForm1_Load(object sender, EventArgs e)
        {
            //listView2.DrawColumnHeader += listView2_DrawColumnHeader;
            //listView2.DrawItem += listView2_DrawItem;
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
                if (arguments.Length > 3)
                {
                    if (arguments[3] == "-noauth")
                    {
                        restapiAuthEnabled = "0";
                    }

                }
            }

            if (!useRest)
            {
                try
                {
                    this.usb.ProductId = Int32.Parse(this.tb_product.Text, System.Globalization.NumberStyles.HexNumber);
                    this.usb.VendorId = Int32.Parse(this.tb_vendor.Text, System.Globalization.NumberStyles.HexNumber);
                    this.usb.CheckDevicePresent();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Инициализация считывателя");
                    //MessageBox.Show(ex.ToString());
                }
            }
            else
            {

            }


            ManRest = new LocalRESTManager(memdb, BufferDatabaseFile, useRest, logger);
            SettingsOfReaderHandle(useRest);
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
            timerPassageSender_Tick(this, e);

        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection breakfast = this.listView1.SelectedItems;
            int idx = 0;
            foreach (ListViewItem item in breakfast)
            {
                idx = item.Index;
                tabControl1.SelectTab(idx);

                break;
            }

            if (idx == 1)
            {
                CopyPassageTable2Memory();
            }
        }

        private bool CopyPassageTable2Memory()
        {

            var doptions = new SQLiteConnectionString(BufferDatabaseFile); ;
            SQLiteConnection diskdb = new SQLiteConnection(doptions);
            try
            {
                //logger.Info($"БД {BufferDatabaseFile} -> mem копирование начато");
                memdb.DropTable<Passage>();
                memdb.CreateTable<Passage>();
                memdb.InsertAll(diskdb.Table<Passage>());
                //logger.Info($"БД {BufferDatabaseFile} -> mem копирование Passage окончено");
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, $"Ошибка копирования БД {BufferDatabaseFile} -> mem copy");
                return false;
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
            //dictionaryWorkersUpdater();
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

        private Passage1bit bit1PassageByPassage(Passage firstUndelivered)
        {
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

            try
            {
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
                    else
                    {
                        lvi.SubItems.Add("-");
                    }
                    if (first_pass.fio != "")
                    {
                        if (first_pass.tabnom != "") { lvi.SubItems.Add($"{first_pass.tabnom}"); }
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
            usb_OnDataRecieved(sender, new UsbLibrary.DataRecievedEventArgs(decBytes1));
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
            label36.Text = e.Column.ToString();
            tabSubfilter.SelectedIndex = 0;
            if (e.Column >= 1 && e.Column <= 8)
            {
                listViewHistory.Columns[e.Column].ImageIndex = 1 - listViewHistory.Columns[e.Column].ImageIndex;
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
                        tabSubfilter.SelectTab(5);
                        break;
                    case 5:
                        tabSubfilter.SelectTab(3);
                        break;
                    case 8:
                        tabSubfilter.SelectTab(4);
                        break;
                }
            }
        }

        private void buttonHistoryFilterHide_Click(object sender, EventArgs e)
        {
            // tabSubfilter.Visible = false;
            // panelFilterSelect.Visible = false;
        }
        /*
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
                           // passages.AddRange(ManRest.getFilteredPassagesFIO_REST(filterName, filterValue, timestampUTC, (int)numericHours.Value));
                        }
                        else
                        {

                            var rng = ManRest.getFilteredPassagesFIODB(SQLFilters);

                            passages.AddRange(rng);
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
                */
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
            labelManualTabnomKeeper.Text = "";
        }

        private void buttonCancelRedEvent_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void buttonCancelManualEvent_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            labelManualTabnomKeeper.Text = "";
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
            labelGreenTabnom.Text = "";
            buttonOkGreenEvent.Enabled = false;
            buttonOkGreenEvent.BackColor = false ? Color.Teal : Color.Gainsboro;
            while (lvGreenEventSearch.Items.Count > 0) { lvGreenEventSearch.Items.RemoveAt(0); };
            MainTableReload(sender, e);
        }


        #region hints handling

        private string getWorkerByHint(string entityName, string entityValue)
        {

            List<PrettyWorker> workerPersons = new List<PrettyWorker>();
            workerPersons.AddRange(ManRest.getFilteredWorkersByEntity(entityName, entityValue, useRest));

            string result = "";
            if (workerPersons.Count > 0)
            {
                result = workerPersons[0].card + "@" + workerPersons[0].fio.Replace("@", " ") + "@" + workerPersons[0].userguid +
                        "@" + workerPersons[0].tabnom;
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
            List<PrettyWorker> workerPersons = new List<PrettyWorker>();
            workerPersons.AddRange(ManRest.getFilteredWorkersByEntity(entityName, entityTemplate, useRest));

            foreach (PrettyWorker worker in workerPersons)
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
            labelManualTabnomKeeper.Text = arr2[3];
            HideMyHint(hintsManualEventCard);
        }

        private void hintsManualEventFIO_MouseClick(object sender, MouseEventArgs e)
        {
            string selectString = getWorkerByHint("fio", hintsManualEventFIO.GetItemText(hintsManualEventFIO.SelectedItem).Replace(" ", "@"));
            string[] arr2 = selectString.Split('@');
            editManualEventFIO.Text = arr2[1];
            editManualEventGUID.Text = arr2[2];
            editManualEventCard.Text = arr2[0];
            labelManualTabnomKeeper.Text = arr2[3];
            HideMyHint(hintsManualEventFIO);
        }

        private void hintsManualEventGUID_MouseClick(object sender, MouseEventArgs e)
        {
            string selectString = getWorkerByHint("userguid", hintsManualEventGUID.GetItemText(hintsManualEventGUID.SelectedItem));
            string[] arr2 = selectString.Split('@');
            editManualEventFIO.Text = arr2[1];
            editManualEventGUID.Text = arr2[2];
            editManualEventCard.Text = arr2[0];
            labelManualTabnomKeeper.Text = arr2[3];
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
            p.tabnom = labelManualTabnomKeeper.Text;
            p.userguid = editManualEventGUID.Text;
            p.description = editManualEventComment.Text;
            //object xxx = comboManualEventOperation.SelectedItem;
            p.timestampUTC = TimeLord.UTCNow();
            p.operCode = ((KeyValuePair<int, string>)comboManualEventOperation.SelectedItem).Key;
            p.kppId = Environment.MachineName;
            p.rowID = "";
            write2sqlite(p);
            clearDetectionView();
            PrettyWorker wp = getWorkerByGUID(p.userguid);

            string[] stmp = wp.fio.Split('@');
            if (stmp.Length > 0)
            {
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
            labelEventJobDescription.Text = wp.job;
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
            buttonOkRedEvent.BackColor = editRedEventComment.Text.Length > 0 ? Color.Teal : Color.Gainsboro;
        }

        private void buttonOkRedEvent_Click(object sender, EventArgs e)
        {

            string operCode = $"{((KeyValuePair<int, string>)comboRedEventOperation.SelectedItem).Key}";

            var p = new Passage();
            p.operCode = int.Parse(operCode);
            p.passageID = int.Parse(labelRedEventID.Text);
            p.description = editRedEventComment.Text;

            ManRest.updatePassage("red", p, useRest);

            tabControl1.SelectTab(0);
            editRedEventComment.Text = "";
            MainTableReload(sender, e);
        }

        private void buttonDeleteGreenEvent_Click(object sender, EventArgs e)
        {

            ManRest.deleteManualPassageByID(BufferDatabaseFile, labelGreenEventID.Text);
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
            p.tabnom = labelGreenTabnom.Text;
            p.userguid = editGreenEventGUID.Text;
            p.description = editGreenEventComment.Text;
            //object xxx = comboManualEventOperation.SelectedItem;
            p.timestampUTC = TimeLord.UTCNow();
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
            labelGreenTabnom.Text = arr2[3];
            HideMyHint(hintsGreenEventCard);

        }

        private void hintsGreenEventFIO_MouseClick(object sender, MouseEventArgs e)
        {
            string selectString = getWorkerByHint("fio", hintsGreenEventFIO.GetItemText(hintsGreenEventFIO.SelectedItem).Replace(" ", "@"));
            string[] arr2 = selectString.Split('@');
            editGreenEventFIO.Text = arr2[1];
            editGreenEventGUID.Text = arr2[2];
            editGreenEventCard.Text = arr2[0];
            labelGreenTabnom.Text = arr2[3];
            HideMyHint(hintsGreenEventFIO);

        }

        private void hintsGreenEventGUID_MouseClick(object sender, MouseEventArgs e)
        {
            string selectString = getWorkerByHint("userguid", hintsGreenEventGUID.GetItemText(hintsGreenEventGUID.SelectedItem));
            string[] arr2 = selectString.Split('@');
            editGreenEventFIO.Text = arr2[1];
            editGreenEventGUID.Text = arr2[2];
            editGreenEventCard.Text = arr2[0];
            labelGreenTabnom.Text = arr2[3];
            HideMyHint(hintsGreenEventGUID);

        }

        private void fillWorkersBy(string entityName, string entityValue, ListView LV)
        {
            List<PrettyWorker> workers = new List<PrettyWorker>();

            if (entityValue.Length < 3) return;
            LV.Visible = false;
            // очистка
            while (LV.Items.Count > 0) { LV.Items.RemoveAt(0); };
            // заполнение
            workers.AddRange(ManRest.getFilteredWorkersByEntity(entityName, entityValue, useRest));

            foreach (PrettyWorker worker in workers)
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
                labelManualTabnomKeeper.Text = "";
                labelManualTabnomKeeper.Text = lvManualEventSearch.SelectedItems[0].SubItems[1].Text;
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
                labelGreenTabnom.Text = "";
                labelGreenTabnom.Text = lvGreenEventSearch.SelectedItems[0].SubItems[1].Text;
            }

        }



        private void buttonResetFilter_Click(object sender, EventArgs e)
        {
            listViewHistory.Columns[1].ImageIndex = 0;
            listViewHistory.Columns[2].ImageIndex = 0;
            listViewHistory.Columns[3].ImageIndex = 0;
            listViewHistory.Columns[4].ImageIndex = 0;
            listViewHistory.Columns[5].ImageIndex = 0;
            listViewHistory.Columns[8].ImageIndex = 0;
            begPickerSelect.Value = DateTime.Now.AddHours(-24 * 3);
            //tabSubfilter.Visible = false;
            //            listViewHistory.Items.Clear();
            //buttonHistorySelect_Click(sender, e);
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
                        labelGreenTabnom.Text = "";
                        labelGreenTabnom.Text = myTabnom == "-" ? "" : myTabnom;
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
                if (spl.Length < 2)
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
            PrettyWorker workerPerson = new PrettyWorker();
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
            PrettyWorker workerPerson = new PrettyWorker();
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
            MainTableReload(sender, e);
        }

        private void buttonPOST_Click(object sender, EventArgs e)
        {
            Passage1bitExt bit = new Passage1bitExt();
            int deliveryFlag = 0;
            try
            {
                var db = new SQLiteConnection(BufferDatabaseFile);

                var arr = db.Query<ShortPassage>($"select p.card, p.opercode, p.timestampUTC, p.description, p.userguid, p.isManual, p.toDelete, p.isDelivered from passage p where p.passageID=?", labelGreenEventID.Text).ToArray();

                foreach (ShortPassage sp in arr)
                {
                    string prefix_comment = "";
                    //deliveryFlag = sp.isDelivered;
                    if (sp.isManual == 1)
                    {
                        prefix_comment += "[Manual]";
                    }
                    if (sp.toDelete == 1)
                    {
                        prefix_comment += "[Deleted]";
                    }
                    bit.bit1_id = runningInstanceGuid + $"-{bit.bit1_timestampUTC}";
                    // не
                    bit.bit1_system = "stop-covid";
                    bit.bit1_timestampUTC = sp.timestampUTC;
                    bit.bit1_card_number = sp.card != null ? (sp.card == "" ? "-" : sp.card) : "-";
                    bit.bit1_individual_guid = sp.userguid != null ? sp.userguid : "";
                    bit.bit1_reader_id = this.reader_id;

                    bit.bit1_comment = prefix_comment + sp.description;

                    bit.bit1_opercode = this.passageDirection;
                    bit.bit1_control_point_type_id = sp.operCode;
                    bit.timezone_seconds = TimeLord.timezone_seconds();
                    break;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Подготовка формата отправки");
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
            tryUpdateBearer(false);
            /*
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
                    logger.Error("Неудачная авторизация " + $"{restServerAddr}/auth/login/");
                    MessageBox.Show("Неудачная авторизация!");
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Авторизация " + $"{restServerAddr}/auth/login/");
                MessageBox.Show("Авторизация:\n"+ex.Message);
            }
            */
            //            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "password");

            try
            {
                //var client = new RestClient($"{restServerAddr}/reading-event/");
                var url = $"{restServerAddr}reading-event/";
                var client = new RestClient();
                client.Timeout = 5000;
                var request = new RestRequest(url, deliveryFlag == 2 ? Method.PUT : Method.POST);

                //            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "password");

                //          request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                if (restapiAuthEnabled == "1")
                {
                    request.AddHeader("Authorization", $"Bearer {this.BearerToken}");
                }

                request.AddHeader("Accept", "*" + "/" + "*");
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(bit);

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {

                    string qry_update_mark_id_asdelivered = $"update passage set isDelivered=1, rowID='{bit.bit1_id}' where (isDelivered=0 or isDelivered=2) and passageID={labelGreenEventID.Text}";
                    using (var dbdisk = new SQLiteConnection(new SQLiteConnectionString(BufferDatabaseFile)))
                    {
                        dbdisk.CreateCommand(qry_update_mark_id_asdelivered).ExecuteNonQuery();
                        send_cnt++;
                    }

                    MessageBox.Show("Доставлено успешно!");
                    logger.Info($"Успешно доставлено {send_cnt} событий");
                }
                else
                {
                    logger.Error($"НЕ доставлено на {send_cnt} событии");
                    File.WriteAllText("Error.txt", body + "\n" + response.Content.ToString());
                    MessageBox.Show("Не доставлено!\nСм. Error.txt");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"POST {restServerAddr}/reading-event/", ex);
                MessageBox.Show($"POST {restServerAddr}/reading-event/:\n" + ex.Message);
            }

        }

        private void buttonHistoryReload_Click(object sender, EventArgs e)
        {
            List<PassageFIO> passages = new List<PassageFIO>();
            /*
            listViewHistory.Columns[1].ImageIndex = 0;
            listViewHistory.Columns[2].ImageIndex = 0;
            listViewHistory.Columns[3].ImageIndex = 0;
            listViewHistory.Columns[4].ImageIndex = 0;
            listViewHistory.Columns[5].ImageIndex = 0;
            listViewHistory.Columns[8].ImageIndex = 0;
            */
            #region history view update
            /*
            string from_clause = " FROM buffer_passage p ";
            // готовим фильтрацию
            long tsUTCbeg = (long)begPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            long tsUTCend = (long)begPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds + (long)numericHours.Value*3600;
            string where_clause = $" where p.timestampUTC >= {tsUTCbeg} and p.timestampUTC <= {tsUTCend} ";
            */

            //listViewHistory.Columns[1].ImageIndex = 0; //card
            //listViewHistory.Columns[2].ImageIndex = 0; //tabnom
            //listViewHistory.Columns[3].ImageIndex = 0; //fio
            //listViewHistory.Columns[4].ImageIndex = 0; //operation
            //listViewHistory.Columns[5].ImageIndex = 0; //delivered
            //listViewHistory.Columns[8].ImageIndex = 0; //dates
            SQLFilters.Clear();
            if (listViewHistory.Columns[1].ImageIndex == 1)
            {
                SQLFilters["card"] = cardTextSelect.Text;
            }
            if (listViewHistory.Columns[2].ImageIndex == 1)
            {
                SQLFilters["tabnom"] = tabnomTextSelect.Text;
            }
            if (listViewHistory.Columns[3].ImageIndex == 1)
            {
                SQLFilters["fio"] = fioTextSelect.Text;
            }
            if (listViewHistory.Columns[5].ImageIndex == 1)
            {

                if (comboBoxHistoryOperations.SelectedIndex != -1)
                {
                    object xxx = comboBoxHistoryOperations.SelectedItem;
                    int ch = ((KeyValuePair<int, string>)xxx).Key;
                    SQLFilters["operation"] = $"{ch}";
                };
            }
            if (listViewHistory.Columns[8].ImageIndex == 1)
            {
                SQLFilters["delivered"] = radioDelivered.Checked ? "1" : "0";
            }
            if (listViewHistory.Columns[4].ImageIndex == 1)
            {
                SQLFilters["tsbeg"] = $"{(long)begPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds}";
                SQLFilters["tsend"] = $"{(long)endPickerSelect.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds}";
            }

            listViewHistory.Visible = false;
            int cnt = 1;
            // очистка
            while (listViewHistory.Items.Count > 0) { listViewHistory.Items.RemoveAt(0); };
            // заполнение
            try
            {
                if (useRest)
                {
                    passages.AddRange(ManRest.getFilteredPassagesFIO_REST(SQLFilters));
                }
                else
                {
                    passages.AddRange(ManRest.getFilteredPassagesFIODB(SQLFilters));
                }

                foreach (var history_pass in passages)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = "      ";

                    //lvi.Text = "";
                    lvi.SubItems.Add(history_pass.card);

                    if (history_pass.fio != "" && history_pass.fio != null)
                    {
                        if (history_pass.tabnom != "") { lvi.SubItems.Add($"{history_pass.tabnom}"); }
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

                    if (history_pass.isDelivered == 0 || history_pass.isDelivered == 2)
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

        private void cardTextSelect_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonDaily_CheckedChanged(object sender, EventArgs e)
        {
            MainTableReload(sender, e);
        }

        private void XForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            memdb.Close();
            logger.Info("Завершение приложения");
            NLog.LogManager.Shutdown();
        }



        private void timerWaitMode_Tick(object sender, EventArgs e)
        {
            timerWaitMode.Enabled = false;
            // справочники еще не загружены
            if (operCheck.Checked && peopleCheck.Checked)
            {
                WaitModeDisable();
                return;
            }
            // проверяем необходимость использования авторизации и обновления токена для нее



            try
            {
                blockingBox.Clear();
                if (!tryUpdateBearer(true))
                {
                    return;
                }

                if (!operCheck.Checked)
                {
                    operCheck.Checked = RestLoadOperations(OperationsJSONFile);
                }
                if (!peopleCheck.Checked)
                {
                    var finish = TimeLord.UTCNow();
                    List<WorkerPersonX> xlist = RestLoadPeople(restServerAddr, 0, TimeLord.UTCNow());
                    peopleCheck.Checked = xlist.Count > 0;
                    peopleCheck.Checked = CreateInfoDatabase();
                    peopleCheck.Checked = UpdateInfoFile(xlist,false);
                    if (peopleCheck.Checked)
                    {
                        File.WriteAllText(InfoTickFile, $"{finish}");
                    }


                }
            }
            finally
            {
                timerWaitMode.Enabled = true;
            }
        }

        private bool tryUpdateBearer(bool isBlockMode)
        {
            bool myResult = true;
            if (restapiAuthEnabled == "1")
            {
                if (BearerLoadUTC > -1)
                {
                    if ((TimeLord.UTCNow() - BearerLoadUTC) > BearerExpiredSeconds)
                    {
                        try
                        {
                            if (LoadBearerToken())
                            {
                                logger.Info("Bearer токен получен успешно");
                                if (isBlockMode) regbb("Bearer токен получен успешно");
                            }
                            else
                            {
                                logger.Error("Bearer токен не получен");
                                if (isBlockMode) regbb("Bearer токен НЕ получен");
                                myResult = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Авторизация");
                            myResult = false;
                        }
                    }
                }
            }
            return myResult;
        }

        private bool UpdateInfoFile(List<WorkerPersonX> xlist, bool useOverwrite)
        {
            // пишем в подготовленную БД
            CipherManager cfm = new CipherManager(InfoPluginFile);
            var pword = cfm.getFullPword(RightPart);
            var options = new SQLiteConnectionString(InfoDatabaseFile, true, pword);
            try
            {
                // var options = new SQLiteConnectionString(dbconst, true, key: "0000");
                using (var db = new SQLiteConnection(options))
                {
                    foreach (WorkerPersonX wp in xlist)
                    {
                        #region delete single worker to update
                        if (useOverwrite)
                        {
                            List<WorkerPersonHierarhy> wphlist;
                            wphlist = db.Query<WorkerPersonHierarhy>(@"select w.id as id_person, p.id as id_position, d.id as id_card
                                                        from WorkerPersonPure w 
                                                        left join Position p on p.ownerid = w.id left join card d on d.ownerid = p.id " +
                                                        $"where w.asup_guid='{wp.asup_guid}'");
                            foreach (WorkerPersonHierarhy wph in wphlist)
                            {
                                if (wph.id_card != 0)
                                {
                                    db.CreateCommand($"Delete from Card where id={wph.id_card}").ExecuteNonQuery();
                                }
                                if (wph.id_position != 0)
                                {
                                    db.CreateCommand($"Delete from Position where id={wph.id_position}").ExecuteNonQuery();
                                }
                                db.CreateCommand($"Delete from WorkerPersonPure where id={wph.id_person}").ExecuteNonQuery();
                            }
                        }
                        # endregion delete single worker 

                        WorkerPersonPure wpx = new WorkerPersonPure() { asup_guid = wp.asup_guid, id = wp.id, first_name = wp.first_name, last_name = wp.last_name, second_name = wp.second_name };
                        db.Insert(wpx);
                        foreach (PositionX po in wp.positions)
                        {
                            Position pox = new Position() { ownerid = wp.id, id = po.id, active = po.active, name = po.name, personnel_number = po.personnel_number };
                            db.Insert(pox);

                            foreach (Card rd in po.cards)
                            {
                                rd.ownerid = po.id;
                                db.Insert(rd);
                            }

                        }
                    }
                }
                logger.Info($"Успех при заполнении БД {InfoDatabaseFile} из rest json ");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"БД {InfoDatabaseFile} при заполнении из rest json");
                return false;
            }


        }

        private List<WorkerPersonX> RestLoadPeople(string urlbegin, long start, long finish)
        {
            List<WorkerPersonX> myResult = new List<WorkerPersonX>();

            string operurl = urlbegin + $"individuals?date_from={start}&date_to={finish}&page=";


            var client = new RestClient();
            client.Timeout = 5000;
            int pageNumberValue = 1;
            List<WorkerPersonX> xlist;
            while (pageNumberValue < 10000)
            {
                var request = new RestRequest($"{operurl}{pageNumberValue}", Method.GET);
                var response = client.Execute<AllPersons>(request);
                setVisibleRESTStatus(response.IsSuccessful ? "Доступен" : "Недоступен");
                
                try
                {
                    if (response.Data.data.Count < 1)
                    {
                        break;
                    }

                    xlist = response.Data.data;
                    myResult.AddRange(xlist);
                    logger.Info($"{operurl}{pageNumberValue} IsSuccessful = {response.IsSuccessful}");
                    
                    regbb($"Получено {xlist.Count} человек");
                    logger.Info($"Получено {xlist.Count} человек");
                    logger.Info($"[0] = {response.Data.data[0].last_name} {response.Data.data[0].first_name[0]} {response.Data.data[0].second_name[0]}");
                }
                catch (Exception ex)
                {
                    regbb($"Получено {response.Data.data.Count} человек [{ex.Message}]");
                    logger.Error(ex, $"Ошибка получения человек {operurl}{pageNumberValue}");
                    break;
                }
                pageNumberValue += 1;
            }
            return myResult;

        }

        private void setVisibleRESTStatus(string mess)
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new Action(() =>
                    {

                        labelHostAccess.Text = mess;
                    }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                labelHostAccess.Text = mess;
            }
        }

        private void regbb(string mess)
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new Action(() =>
                    {
                        blockingBox.AppendText($"{DateTime.Now.ToString("HH:mm:ss")} {mess}\r\n");
                    }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                blockingBox.AppendText($"{DateTime.Now.ToString("HH:mm:ss")} {mess}\r\n");
            }
        }

        private bool LoadBearerToken()
        {
            string tokentoken = "";
            bool myResult = false;
            var resourceURL = $"{restServerAddr}auth/login/";
            try
            {
                var client0 = new RestClient();
                client0.Timeout = 8000;
                var request0 = new RestRequest(resourceURL, Method.POST);
                request0.AddHeader("Content-Type", "application/json");
                var body0 = JsonConvert.SerializeObject(new { login = loginbox.Text, password = passwordbox.Text });
                request0.AddParameter("application/json", body0, ParameterType.RequestBody);
                IRestResponse response0 = client0.Execute(request0);

                if (response0.IsSuccessful)
                {
                    logger.Info("Успешная авторизация в " + $"{resourceURL} [login={loginbox.Text},password={passwordbox.Text}]");
                    var zlist = response0.Content.Split(':');
                    if (zlist.Length > 2)
                    {
                        tokentoken = zlist[2].Replace('"', ' ').Replace('}', ' ');
                        myResult = true;
                        BearerToken = tokentoken;
                        BearerLoadUTC = TimeLord.UTCNow();
                    }
                    else
                    {
                        logger.Error($"Проблема при извлечении токена из {response0.Content}");
                    }
                }
                else
                {
                    logger.Error("Неудачная авторизация в " + $"{resourceURL} [login={loginbox.Text},password={passwordbox.Text}]");
                    //        MessageBox.Show("Неудачная авторизация!");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Сбой при авторизации " + $"{resourceURL} [login={loginbox.Text},password={passwordbox.Text}]");
                //MessageBox.Show("Авторизация:\n" + ex.Message);
            }
            return myResult;
        }

        private void buttonCheckEvents_Click(object sender, EventArgs e)
        {
            makeCheck(sender, e);
        }

        private void threadWorkersUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            //if needRestartlabel.Visible needRestartlabel.Visible = false;
            // 1. извлечь из файла послений тик
            string tickstr = "0";
            Int64 tick = 0;
            bool StatusOk = true;
            try
            {
                tickstr = File.ReadAllText(InfoTickFile);
                tick = int.Parse(tickstr);
                infotickLabel.Text = TimeLord.TimestampAsDatetimeString(tick);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Дата последнего изменения справочника недоступна");
            }
            var finish = TimeLord.UTCNow();

            List<WorkerPersonX> xlist = RestLoadPeople(restServerAddr, tick, finish);
            if (xlist.Count > 0)
            {
                logger.Info($"Найдено обновление справочника на {xlist.Count} человек");
                // каждый человек полученный следующим методом удаляется в инфобазе и добавлется с новыми полями 
                StatusOk = UpdateInfoFile(xlist, true);
            }else
            { 
                //logger.Info("Не найдено обновление справочника");
            }

            if (StatusOk && (xlist.Count > 0))
            {
                logger.Info($"Включено предупреждение об обновлении справочника. Новый тик={finish}"); ;
                infotickLabel.Text = TimeLord.TimestampAsDatetimeString(finish);
                File.WriteAllText(InfoTickFile, $"{finish}");
                needRestartlabel.Visible = true;
            }
        }

        private void restapi_path_label_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            threadWorkersUpdater.RunWorkerAsync();
        }

        private void threadWorkersUpdater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerWorkersUpdate.Enabled = true;
        }

        private void timerEraser_Tick(object sender, EventArgs e)
        {
            timerEraser.Enabled = false; 
            threadEraser30.RunWorkerAsync(e);   
        }

        private void threadEraser30_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerEraser.Enabled = true;
        }

        private void threadEraser30_DoWork(object sender, DoWorkEventArgs e)
        {
            using(var dbdisk = new SQLiteConnection(new SQLiteConnectionString(BufferDatabaseFile)))
            {
                string qry = "[]";
                try
                {
                    var xnow = TimeLord.UTCNow();
                    var diff = 30 * 24 * 60 * 60;
                    qry = @"delete from passage " +
                          $" where isdelivered=1 and ischecked=1 and {xnow}-timestamputc>{diff} ";
                    dbdisk.CreateCommand(qry).ExecuteNonQuery();
                    logger.Info("Успех при удалении событий старше 30 дней");
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Ошибка при удалении событий старше 30 дней {qry}");
                }
            }
        }

        private void threadPassageSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerPassageSender.Enabled = true;
        }


        private bool sendOneEvent(Passage1bitExt bit1, int deliveryFlag, int PassageId)
        {
            // restsharp
            // выбираем первый неотправленный passage и если массив непустой - отправляем через rest
            // если успешно отправлось - помечаем passageID отправленным и обновляем главную таблицу
            // получить наименьший локальный проход
            // отправить 
            // оценить результат
            // обновить состояние или не обновлять
            // удаление доставленных - другим методом
            tryUpdateBearer(false);
            bool myResult = false;
            string body = "undefined";
            try
            {
                var url = $"{restServerAddr}reading-event/";
                var client = new RestClient();
                client.Timeout = 5000;
                //var request = new RestRequest(url, deliveryFlag == 2 ? Method.PUT : Method.POST);
                var request = new RestRequest(url, Method.POST);

                if (restapiAuthEnabled == "1")
                {
                    request.AddHeader("Authorization", $"Bearer {this.BearerToken}");
                }

                request.AddHeader("Accept", "*" + "/" + "*");
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request.AddHeader("Content-Type", "application/json");
                body = JsonConvert.SerializeObject(bit1);
                body = body.Replace(",\"individual_guid\":\"-\"","");
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    string qry_update_mark_id_asdelivered = $"update passage set isDelivered=1, rowID='{bit1.bit1_id}' where (isDelivered=0 or isDelivered=2) and passageID={PassageId}";
                    try {
                        var dbdisk = new SQLiteConnection(new SQLiteConnectionString(BufferDatabaseFile));
                        dbdisk.CreateCommand(qry_update_mark_id_asdelivered).ExecuteNonQuery();
                        myResult = true;
                    }catch(Exception ex)
                    {
                        logger.Error(ex, $"Сбой при пометке отправленным {qry_update_mark_id_asdelivered}");
                    }
                }
                else
                {
                    logger.Error($"НЕ доставлено событие {body} StatusCode={response.StatusCode} Error={response.ResponseStatus}");
                    File.WriteAllText("error.html",response.Content);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"POST {restServerAddr}/reading-event/ body:{body}");
            }
            return myResult;
        }

        private void threadPassageSender_DoWork(object sender, DoWorkEventArgs e)
        {
            Passage1bitExt bit = new Passage1bitExt();
            int deliveryFlag = 0;
            int goodCounter = 0;
            try
            {
                var db = new SQLiteConnection(BufferDatabaseFile);

                var arr = db.Query<ShortPassage>(@"select p.rowid, p.passageid, p.card, p.opercode, p.timestampUTC, p.description, p.userguid, p.isManual, p.toDelete, p.isDelivered 
                    from passage p where p.isDelivered<>1").ToArray();
                if (arr.Length > 0)
                {
                    foreach (ShortPassage sp in arr)
                    {
                        string prefix_comment = "";
                        //deliveryFlag = sp.isDelivered;
                        if (sp.isManual == 1)
                        {
                            prefix_comment += "[Manual]";
                        }
                        if (sp.toDelete == 1)
                        {
                            prefix_comment += "[Deleted]";
                        }
                        bit.bit1_timestampUTC = sp.timestampUTC;
                        bit.bit1_id = runningInstanceGuid + $"-{bit.bit1_timestampUTC}";
                        if (sp.rowID != null)
                        {
                            if (sp.rowID != "")
                            {
                                bit.bit1_id = sp.rowID;
                            }
                        }
                        // не
                        bit.bit1_system = "stop-covid";
                        
                        bit.bit1_card_number = sp.card != null ? (sp.card == "" ? "-" : sp.card) : "-";
                        bit.bit1_individual_guid = sp.userguid != null ? (sp.userguid == "" ? "-" : sp.userguid) : "-";
                        bit.bit1_reader_id = this.reader_id;

                        bit.bit1_comment = prefix_comment + sp.description;
                        if (bit.bit1_comment == ""){ bit.bit1_comment = "-"; };
                        bit.bit1_opercode = this.passageDirection;
                        bit.bit1_control_point_type_id = sp.operCode;
                        bit.timezone_seconds = TimeLord.timezone_seconds();
                        if (sendOneEvent(bit, deliveryFlag, sp.PassageId))
                        {
                            goodCounter++;
                        }
                    }
                    logger.Info($"Отправлено успешно {goodCounter} из {arr.Length}");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Подготовка отправки");
                //MessageBox.Show("Подготовка формата отправки:\n" + ex.Message);
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            update_cipher_pword();
        }
        private void update_cipher_pword()
        {
            string pword = Constainer.genPWord();
            var cfm = new CipherManager(InfoPluginFile);
            var old = cfm.getFullPword(RightPart);
            cfm.UpdatePStorage(pword);

            logger.Warn("Локальное п-обновление");
            SQLiteConnectionString options = null;

            options = new SQLiteConnectionString(InfoDatabaseFile, true, key: old);

            var connection = new SQLiteConnection(options);
            var command = connection.CreateCommand($"PRAGMA rekey = '{cfm.getFullPword(RightPart)}'\n");
            try
            {
                command.ExecuteNonQuery();
                logger.Warn("Локальное п-обновление успешно");

            }
            catch (Exception ex)
            {
                if (ex.Message == "not an error")
                {
                    logger.Warn("Локальное п-обновление успешно");

                }
                else
                {
                    logger.Error($"Ошибка, локальное п-обновление [{ex.Message}]");
                }
            };
            logger.Warn("Локальное п-обновление завершено");
            connection.Close();
        }

        private void timerPupdate_Tick(object sender, EventArgs e)
        {
            var localtime = DateTime.Now;
            if (localtime.ToLongTimeString() == "3:03:03")
            {
                timerPupdate.Enabled = true;
                threadPupdate.RunWorkerAsync();
            }
        }

        private void threadPupdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerPupdate.Enabled = true;
        }

        private void threadPupdate_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(2000);
            update_cipher_pword();
        }
    }
}
