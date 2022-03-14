using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UsbLibrary;
using System.IO;
using System.Data.SQLite;

namespace UsbApp
{

    public partial class Sniffer : Form
    {
        Dictionary<string, string> Persons;
        CrossRecord lastCrossing = new CrossRecord();
        public Sniffer()
        {
            InitializeComponent();
        }

        private void usb_OnDeviceArrived(object sender, EventArgs e)
        {
            this.lb_message.Items.Add("Found a Device");

        }

        private void usb_OnDeviceRemoved(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(usb_OnDeviceRemoved), new object[] { sender, e });
            }
            else
            {
                this.lb_message.Items.Add("Device was removed");
            }
        }

        private void usb_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {

            this.lb_message.Items.Add("My device was found");

            //setting string form for sending data
            string text = "";
            for (int i = 0; i < this.usb.SpecifiedDevice.OutputReportLength - 1; i++)
            {
                text += "000 ";
            }
            this.tb_send.Text = text;
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

        private void btn_ok_Click(object sender, EventArgs e)
        {

        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                string text = this.tb_send.Text + " ";
                text.Trim();
                string[] arrText = text.Split(' ');
                byte[] data = new byte[arrText.Length];
                for (int i = 0; i < arrText.Length; i++)
                {
                    if (arrText[i] != "")
                    {
                        int value = Int32.Parse(arrText[i], System.Globalization.NumberStyles.Number);
                        data[i] = (byte)Convert.ToByte(value);
                    }
                }

                if (this.usb.SpecifiedDevice != null)
                {
                    this.usb.SpecifiedDevice.SendData(data);
                }
                else
                {
                    MessageBox.Show("Sorry but your device is not present. Plug it in!! ");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void usb_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(usb_OnSpecifiedDeviceRemoved), new object[] { sender, e });
            }
            else
            {
                this.lb_message.Items.Add("My device was removed");
            }
        }

        private void usb_OnDataRecieved(object sender, DataRecievedEventArgs args)
        {
            lastCrossing.timestampUTC = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
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

                //string rec_data = "Data: ";
                //foreach (byte myData in args.data)
                //{
                //    if (myData.ToString().Length == 1)
                //    {
                //        rec_data += "00";
                //    }

                //    if (myData.ToString().Length == 2)
                //    {
                //        rec_data += "0";
                //    }

                //    rec_data += myData.ToString() + " ";
                //}

                //this.lb_read.Items.Insert(0, rec_data);
                
                byte[] bdata= new byte[100];
                //args.data.CopyTo(bdata, 2);
                Array.Copy(args.data, 1, bdata, 0, 100);

                string xxx = BytesToString(bdata);
                xxx = xxx.TrimEnd('\0');

                if (xxx.Length > 0)
                {
                    lastCrossing.card = xxx;
                    this.lb_read.Items.Insert(0, xxx+" ("+ xxx.Length.ToString()+")");
                    string PersDesc = "Карта не найдена!";
                    if (Persons.ContainsKey(xxx))
                    {
                        PersDesc = Persons[xxx];
                    }

                    textBox1.Text = xxx + "\r\n" + PersDesc;
                    this.Show();
                    buttonInside.Enabled = true;
                    buttonOutside.Enabled = true;

                }
            }
        }

        private void usb_OnDataSend(object sender, EventArgs e)
        {
            this.lb_message.Items.Add("Some data was send");
        }

        private void dbsend(int IsOut)
        {
            // записываем информацию в базу данных
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=c:\\appkpp\\bufferdb.db;Version=3;New=False;"))
            {
                string commandText = "INSERT INTO BorderCrossing ([timestampUTC], [card], [IsOUT], [KPPID]) VALUES(@timestampUTC, @card, @IsOUT, @KPPID)";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);

                Command.Parameters.AddWithValue("@timestampUTC", lastCrossing.timestampUTC);
                Command.Parameters.AddWithValue("@card", lastCrossing.card);
                Command.Parameters.AddWithValue("@IsOut", IsOut);
                Command.Parameters.AddWithValue("@KPPID", Environment.MachineName);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
                MessageBox.Show("Проход записан в базу данных");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonInside.Enabled = false;
            buttonOutside.Enabled = false;
            dbsend(0);
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonInside.Enabled = false;
            buttonOutside.Enabled = false;
            dbsend(1);
            this.Hide();
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

        private void Sniffer_Load(object sender, EventArgs e)
        {
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
        }

        //public static void Main()
        //{
        //    byte[] bytes = Encoding.ASCII.GetBytes("ABC123");
        //    Console.WriteLine("Byte Array is: " + String.Join(" ", bytes));

        //    string str = BytesToString(bytes);
        //    Console.WriteLine("The String is: " + str);
        //}
        public void UpdatePersons(Dictionary<string, string> persons){
            this.Persons = persons;
        }

    }
}