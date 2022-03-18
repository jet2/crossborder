using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cardSender
{
    public partial class Form1 : Form
    {
        WcfClient xcli;
        public Form1()
        {
            xcli = new WcfClient();
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                label1.Text = $"Пустое не послать";
                return;
            }
            label1.Text = $"Пытаемся послать";
            try
            {
                string buf = Convert.ToChar(3)  + comboBox1.Text.PadRight(200, '\0');
                xcli.Send(buf); 
            }
            catch(System.ServiceModel.EndpointNotFoundException ex){
                label1.Text = $"Не послалось {ex.ToString()}";
            }
            if (xcli.State== System.ServiceModel.CommunicationState.Faulted)
            {
                label1.Text = $"Не послалось по ошибке";
            }
            else
            {
                label1.Text = $"OK послалось!";
            }

        }
    }
}
