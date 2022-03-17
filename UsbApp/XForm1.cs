using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace kppApp
{

    public partial class XForm1 : Form
    {
        public static Sniffer mySnifferForm;

        public static Dictionary<string, string> Persons;

        WcfServer srv;
        public XForm1()
        {

            InitializeComponent();
        }

        private void dictionaryWorkersUpdater()
        {

                Persons = new Dictionary<string, string>();
                Persons.Clear();

                using (var connection = new SQLiteConnection("Data Source=c:\\appkpp\\kppbuffer.db;Version=3;New=False;"))
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
                            var card = reader.GetString(0);
                            var tab = reader.GetInt64(1);
                            var fio = reader.GetString(2);
                            var job = reader.GetString(3);
                            var isGuardian = reader.GetInt16(4);
                            if (card != "")
                            {
                                Persons.Add($"{card}", $"Табельный №: {tab}\r\nФИО: {fio}\r\nДолжность: {job}\r\nСотрудникСБ: {isGuardian}");
                            }
                        }
                    }
                }
                if (Persons.Count == 0)
                {
                    MessageBox.Show("Справочник персонала поврежден!\r\nСервис не позволяет идентифицировать персонал!");
                }
                mySnifferForm.UpdatePersons(Persons);
                // Unix timestamp is seconds past epoch

                tsUpdated local_updated = new tsUpdated();
                using (var connection = new SQLiteConnection("Data Source=c:\\appkpp\\kppbuffer.db;Version=3;New=False;"))
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

            threadWorkersUpdater.DoWork += updateWorkers;
            threadWorkersUpdater.RunWorkerCompleted += updateWorkers_ResultHandler;
            threadPassageSender.DoWork += sendPassage;
            threadPassageSender.RunWorkerCompleted += sendPassage_ResultHandler;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection breakfast =this.listView1.SelectedItems;


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
            this.toolStripStatusLabel3.BackColor= Color.Salmon;
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
            // private void updateWorkers(object sender, DoWorkEventArgs e)
            // получаем стамп последнего обновления работников с сервера
            var client = new RestClient("http://localhost:3002/kpp/v1/workers/update_ts");
            client.Timeout = 5000;
            var request = new RestRequest(Method.GET);
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            tsUpdated remote_updated = JsonConvert.DeserializeObject<tsUpdated>(response.Content);
            // получаем стамп последнего обновления работников локальный
            tsUpdated local_updated = new tsUpdated();
            using (var connection = new SQLiteConnection("Data Source=c:\\appkpp\\kppbuffer.db;Version=3;New=False;"))
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
                    var client2 = new RestClient("http://localhost:3002/kpp/v1/workers/");
                    client2.Timeout = 5000;
                    var request2 = new RestRequest(Method.GET);
                    var body2 = @"";
                    request2.AddParameter("text/plain", body2, ParameterType.RequestBody);
                    // получем json array
                    IRestResponse response2 = client2.Execute(request);
                    // заполняем список записей
                    List<WokerPerson> remote_workers = JsonConvert.DeserializeObject<List<WokerPerson>>(response2.Content);

                    // очищаем приемную таблицу
                    var command3 = connection.CreateCommand();
                    command3.CommandText = $"delete from buffer_workers_input";
                    command3.ExecuteNonQuery();

                    if (remote_workers.Count > 0)
                    {
                        // каждую персону из списка вливаем в приемную таблицу
                        foreach (WokerPerson wp in remote_workers)
                        {
                            if (wp.kard != "" & wp.fio != "" & wp.tabnom != 0)
                            {
                                command3.CommandText = $"insert into buffer_workers_input(card,fio,tabnom,job,isGuardian) values('{wp.kard}','{wp.fio}',{wp.tabnom},'{wp.job}',{wp.isGuardian})";
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

        private void label10_DoubleClick(object sender, EventArgs e)
        {
            threadWorkersUpdater.RunWorkerAsync();
        }
    }
}
