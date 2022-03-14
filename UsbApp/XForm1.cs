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


namespace UsbApp
{

    public partial class XForm1 : Form
    {
        public static Sniffer mySnifferForm;

        public static Dictionary<string, string> Persons;

        public XForm1()
        {
            InitializeComponent();
        }

        private void XForm1_Load(object sender, EventArgs e)
        {
            Persons = new Dictionary<string, string>();
            Persons.Clear();

            using (var connection = new SQLiteConnection("Data Source=c:\\appkpp\\bufferdb.db;Version=3;New=False;"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT card, tabnom, fio, jobposition FROM persons
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
                        Persons.Add($"{card}", $"Табельный №: {tab}\r\nФИО: {fio}\r\n Должность: {job}");
                        //Console.WriteLine($"Hello, {name}!");
                    }
                }
            }
            //        using (var connection = new SqliteConnection("Data Source=c:\\appkpp\\buddb.db,"))
            //        {
            //            connection.Open();

            //            var command = connection.CreateCommand();
            //            command.CommandText =
            //            @"
            //    SELECT name
            //    FROM user
            //    WHERE id = $id
            //";
            //            command.Parameters.AddWithValue("$id", id);

            //            using (var reader = command.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    var name = reader.GetString(0);

            //                    Console.WriteLine($"Hello, {name}!");
            //                }
            //            }
            //        }


            //Persons.Add("47 3354", $"Пикаленко\r\nЖайло\r\nКутепович\r\n51426\r\nГлавный конструктор");
           // Persons.Add("74 4669", $"Ласкус\r\nВилен\r\nТранторович\r\n15111\r\nИнженер");

            mySnifferForm = new Sniffer();
            mySnifferForm.Left = this.Width - mySnifferForm.Width;
            mySnifferForm.Top = this.Height - mySnifferForm.Height;
            mySnifferForm.UpdatePersons(Persons);
            mySnifferForm.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mySnifferForm.Show();
        }
    }
}
