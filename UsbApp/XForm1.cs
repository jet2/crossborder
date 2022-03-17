using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

        private void XForm1_Load(object sender, EventArgs e)
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
                        var card= reader.GetString(0);
                        var tab = reader.GetInt16(1);
                        var fio = reader.GetString(2);
                        var job = reader.GetString(3);
                        var isGuardian = reader.GetInt16(1);
                        Persons.Add($"{card}", $"Табельный №: {tab}\r\nФИО: {fio}\r\nДолжность: {job}\r\nСотрудникСБ: {isGuardian}");
                        //Console.WriteLine($"Hello, {name}!");
                    }
                }
            }
            mySnifferForm = new Sniffer(this);
            mySnifferForm.Left = this.Width - mySnifferForm.Width;
            mySnifferForm.Top = this.Height - mySnifferForm.Height;
            mySnifferForm.UpdatePersons(Persons);
            mySnifferForm.Show();
            mySnifferForm.Hide();

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
            threadWorkersUpdater.DoWork += updateWorkers;
            threadWorkersUpdater.RunWorkerCompleted += updateWorkers_ResultHandler;
            threadWorkersUpdater.RunWorkerAsync(1000);
        }

        private void updateWorkers()
        {

            // restsharp
            // DB Загрузка timestampUTC из sqlite updated
            // REST Загрузка timestampUTC из updated
            // сравнение -> разное -> вызываем REST Workers и загружаем json в sqlite
            // сравнение -> одинаковое -> выход 
            // результат всегда успех
        }

        private void updateWorkers_ResultHandler()
        {
            timerWorkersUpdate.Enabled = true;
        }

        private void sendPassage()
        {
            // restsharp
            // выбираем первый неотправленный passage и если массив непустой - отправялем через rest
            // если успешно отправлось - помечаем passageID отправленным и обновляем главную таблицу
        }

        private void sendPassage_ResultHandler()
        {
            timerPassageSender.Enabled = true;

        }

        private void timerPassageSender_Tick(object sender, EventArgs e)
        {
            timerPassageSender.Enabled = false;
            threadPassageSender.DoWork += sendPassage;
            threadPassageSender.RunWorkerCompleted += sendPassage_ResultHandler;
            threadPassageSender.RunWorkerAsync(1000);
        }
    }
}
