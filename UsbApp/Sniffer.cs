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

namespace kppApp
{

    public partial class Sniffer : Form
    {
        //Dictionary<string, string> Persons;
        Dictionary<string, WorkerPerson> PersonsStructs;
        Passage lastCrossing = new Passage();
        XForm1 mainInstance;
        
        WcfServer srv;

        public Sniffer(XForm1 mainInstance)
        {
            this.mainInstance = mainInstance;
            InitializeComponent();
        }

        private void usb_OnDeviceArrived(object sender, EventArgs e)
        {
            //this.lb_message.Items.Add("Found a Device");
            mainInstance.setRFIDFound();

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
                mainInstance.setRFIDLost();
            }
        }

        private void usb_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            mainInstance.setRFIDFound();
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
                //this.lb_message.Items.Add("My device was removed");
                mainInstance.setRFIDLost();
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
                
                byte[] bdata= new byte[100];
                //args.data.CopyTo(bdata, 2);
                Array.Copy(args.data, 1, bdata, 0, 100);

                string xxx = BytesToString(bdata);
                xxx = xxx.TrimEnd('\0');
                this.BackColor = Color.DimGray;
                if (xxx.Length > 0)
                {
                    buttonGuardo.Enabled = false;
                    lastCrossing.card = xxx;
                   // this.lb_read.Items.Insert(0, xxx+" ("+ xxx.Length.ToString()+")");
                    string PersDesc = "Карта не найдена!";
                    WorkerPerson myWP; 
                    if (PersonsStructs.ContainsKey(xxx))
                    {
                        myWP = PersonsStructs[xxx];
                        var guardo = myWP.isGuardian == 1 ? "Да" : "Нет";
                        PersDesc = $"Табельный №: {myWP.tabnom}\r\nФИО: {myWP.fio}\r\nДолжность: {myWP.job}\r\nБезопасник: {guardo}";

                        lastCrossing.tabnom = myWP.tabnom;
                        if (myWP.isGuardian == 1) buttonGuardo.Enabled = true;
                    }
                    else
                    {
                        this.BackColor = Color.Coral;
                    }

                    textBox1.Text = "Карта №"+ xxx + "\r\n" + PersDesc;
                    this.Show();
                    buttonInside.Enabled = true;
                    buttonOutside.Enabled = true;

                }
            }
        }

        private void usb_OnDataSend(object sender, EventArgs e)
        {
            //this.lb_message.Items.Add("Some data was send");
        }

        private void write2sqlite(Constants.CardScanResult IsOut, int isManual)
        {
            // записываем информацию в базу данных
            using (SQLiteConnection Connect = new SQLiteConnection(mainInstance.sqlite_connectionstring))
            {
                string commandText = @"INSERT INTO buffer_passage ([timestampUTC], [card], [IsOUT], [KPPID],[tabnom]) 
                                       VALUES(@timestampUTC, @card, @IsOUT, @KPPID,@tabnom)";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Command.Parameters.AddWithValue("@timestampUTC", lastCrossing.timestampUTC);
                Command.Parameters.AddWithValue("@card", lastCrossing.card);
                Command.Parameters.AddWithValue("@tabnom", lastCrossing.tabnom);
                Command.Parameters.AddWithValue("@IsOut", IsOut);
                Command.Parameters.AddWithValue("@KPPID", Environment.MachineName);
                Command.Parameters.AddWithValue("@isManual", isManual);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
               // MessageBox.Show("Проход записан в базу данных");
            }
            mainInstance.label10_DoubleClick(this, new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonInside.Enabled = false;
            buttonOutside.Enabled = false;
            write2sqlite(Constants.CardScanResult.In, 0);
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonInside.Enabled = false;
            buttonOutside.Enabled = false;
            write2sqlite(Constants.CardScanResult.Out, 0);
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
            
            srv = new WcfServer();
            srv.Start();
            srv.Received += OnWCFReceived;
            
        }

        private void OnWCFReceived(object sender, DataReceivedEventArgs args)
        {
            byte[] decBytes1 = Encoding.ASCII.GetBytes(args.Data);
            UsbLibrary.DataRecievedEventArgs argz = new UsbLibrary.DataRecievedEventArgs(decBytes1);
            usb_OnDataRecieved(sender, argz);
        }

        public void UpdatePersons( Dictionary<string, WorkerPerson> personsstructs)
        {
            //this.Persons = persons;
            this.PersonsStructs = personsstructs;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            buttonInside.Enabled = false;
            buttonOutside.Enabled = false;
            write2sqlite(Constants.CardScanResult.Fail, 0);
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            DialogResult r8 = MessageBox.Show(this, "Передать смену\r\n"+$"{PersonsStructs[lastCrossing.card].fio}",
                                   "Подтверждение!", MessageBoxButtons.YesNo);            
            if (r8 == DialogResult.Yes)
            {
                buttonInside.Enabled = false;
                buttonOutside.Enabled = false;
                write2sqlite(Constants.CardScanResult.Guard, 0);
                this.Hide();
                mainInstance.setDuty(PersonsStructs[lastCrossing.card].fio); 
            }
        }
    }
}