namespace kppApp
{
    partial class Sniffer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lb_recieved = new System.Windows.Forms.Label();
            this.btn_send = new System.Windows.Forms.Button();
            this.tb_send = new System.Windows.Forms.TextBox();
            this.lb_vendor = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.lb_product = new System.Windows.Forms.Label();
            this.lb_senddata = new System.Windows.Forms.Label();
            this.lb_messages = new System.Windows.Forms.Label();
            this.tb_vendor = new System.Windows.Forms.TextBox();
            this.tb_product = new System.Windows.Forms.TextBox();
            this.gb_filter = new System.Windows.Forms.GroupBox();
            this.buttonOutside = new System.Windows.Forms.Button();
            this.buttonInside = new System.Windows.Forms.Button();
            this.lb_message = new System.Windows.Forms.ListBox();
            this.gb_details = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lb_read = new System.Windows.Forms.ListBox();
            this.usb = new UsbLibrary.UsbHidPort(this.components);
            this.gb_filter.SuspendLayout();
            this.gb_details.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_recieved
            // 
            this.lb_recieved.AutoSize = true;
            this.lb_recieved.Location = new System.Drawing.Point(458, 31);
            this.lb_recieved.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_recieved.Name = "lb_recieved";
            this.lb_recieved.Size = new System.Drawing.Size(159, 25);
            this.lb_recieved.TabIndex = 4;
            this.lb_recieved.Text = "Recieved Data:";
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(521, 121);
            this.btn_send.Margin = new System.Windows.Forms.Padding(6);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(96, 44);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // tb_send
            // 
            this.tb_send.Location = new System.Drawing.Point(265, 157);
            this.tb_send.Margin = new System.Windows.Forms.Padding(6);
            this.tb_send.Name = "tb_send";
            this.tb_send.Size = new System.Drawing.Size(260, 31);
            this.tb_send.TabIndex = 2;
            // 
            // lb_vendor
            // 
            this.lb_vendor.AutoSize = true;
            this.lb_vendor.Location = new System.Drawing.Point(18, 42);
            this.lb_vendor.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_vendor.Name = "lb_vendor";
            this.lb_vendor.Size = new System.Drawing.Size(185, 25);
            this.lb_vendor.TabIndex = 5;
            this.lb_vendor.Text = "Device Vendor ID:";
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(644, 83);
            this.btn_ok.Margin = new System.Windows.Forms.Padding(6);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(150, 44);
            this.btn_ok.TabIndex = 7;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // lb_product
            // 
            this.lb_product.AutoSize = true;
            this.lb_product.Location = new System.Drawing.Point(18, 92);
            this.lb_product.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_product.Name = "lb_product";
            this.lb_product.Size = new System.Drawing.Size(190, 25);
            this.lb_product.TabIndex = 6;
            this.lb_product.Text = "Device Product ID:";
            // 
            // lb_senddata
            // 
            this.lb_senddata.AutoSize = true;
            this.lb_senddata.Location = new System.Drawing.Point(108, 157);
            this.lb_senddata.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_senddata.Name = "lb_senddata";
            this.lb_senddata.Size = new System.Drawing.Size(119, 25);
            this.lb_senddata.TabIndex = 5;
            this.lb_senddata.Text = "Send Data:";
            // 
            // lb_messages
            // 
            this.lb_messages.AutoSize = true;
            this.lb_messages.Location = new System.Drawing.Point(16, 31);
            this.lb_messages.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_messages.Name = "lb_messages";
            this.lb_messages.Size = new System.Drawing.Size(161, 25);
            this.lb_messages.TabIndex = 7;
            this.lb_messages.Text = "Usb Messages:";
            // 
            // tb_vendor
            // 
            this.tb_vendor.Location = new System.Drawing.Point(228, 37);
            this.tb_vendor.Margin = new System.Windows.Forms.Padding(6);
            this.tb_vendor.Name = "tb_vendor";
            this.tb_vendor.Size = new System.Drawing.Size(374, 31);
            this.tb_vendor.TabIndex = 1;
            this.tb_vendor.Text = "046E";
            // 
            // tb_product
            // 
            this.tb_product.Location = new System.Drawing.Point(228, 87);
            this.tb_product.Margin = new System.Windows.Forms.Padding(6);
            this.tb_product.Name = "tb_product";
            this.tb_product.Size = new System.Drawing.Size(374, 31);
            this.tb_product.TabIndex = 2;
            this.tb_product.Text = "52C3";
            // 
            // gb_filter
            // 
            this.gb_filter.Controls.Add(this.btn_ok);
            this.gb_filter.Controls.Add(this.lb_product);
            this.gb_filter.Controls.Add(this.lb_vendor);
            this.gb_filter.Controls.Add(this.btn_send);
            this.gb_filter.Controls.Add(this.tb_vendor);
            this.gb_filter.Controls.Add(this.tb_product);
            this.gb_filter.ForeColor = System.Drawing.Color.White;
            this.gb_filter.Location = new System.Drawing.Point(24, 23);
            this.gb_filter.Margin = new System.Windows.Forms.Padding(6);
            this.gb_filter.Name = "gb_filter";
            this.gb_filter.Padding = new System.Windows.Forms.Padding(6);
            this.gb_filter.Size = new System.Drawing.Size(856, 154);
            this.gb_filter.TabIndex = 5;
            this.gb_filter.TabStop = false;
            this.gb_filter.Text = "Filter for Device:";
            // 
            // buttonOutside
            // 
            this.buttonOutside.Enabled = false;
            this.buttonOutside.ForeColor = System.Drawing.Color.Black;
            this.buttonOutside.Location = new System.Drawing.Point(444, 59);
            this.buttonOutside.Name = "buttonOutside";
            this.buttonOutside.Size = new System.Drawing.Size(373, 85);
            this.buttonOutside.TabIndex = 9;
            this.buttonOutside.Text = "Наружу";
            this.buttonOutside.UseVisualStyleBackColor = true;
            this.buttonOutside.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonInside
            // 
            this.buttonInside.Enabled = false;
            this.buttonInside.ForeColor = System.Drawing.Color.Black;
            this.buttonInside.Location = new System.Drawing.Point(23, 59);
            this.buttonInside.Name = "buttonInside";
            this.buttonInside.Size = new System.Drawing.Size(407, 85);
            this.buttonInside.TabIndex = 8;
            this.buttonInside.Text = "Внутрь";
            this.buttonInside.UseVisualStyleBackColor = true;
            this.buttonInside.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_message
            // 
            this.lb_message.FormattingEnabled = true;
            this.lb_message.ItemHeight = 25;
            this.lb_message.Location = new System.Drawing.Point(189, 27);
            this.lb_message.Margin = new System.Windows.Forms.Padding(6);
            this.lb_message.Name = "lb_message";
            this.lb_message.Size = new System.Drawing.Size(155, 29);
            this.lb_message.TabIndex = 6;
            // 
            // gb_details
            // 
            this.gb_details.Controls.Add(this.buttonOutside);
            this.gb_details.Controls.Add(this.textBox1);
            this.gb_details.Controls.Add(this.buttonInside);
            this.gb_details.Controls.Add(this.lb_read);
            this.gb_details.Controls.Add(this.lb_messages);
            this.gb_details.Controls.Add(this.lb_message);
            this.gb_details.Controls.Add(this.lb_recieved);
            this.gb_details.ForeColor = System.Drawing.Color.White;
            this.gb_details.Location = new System.Drawing.Point(24, 188);
            this.gb_details.Margin = new System.Windows.Forms.Padding(6);
            this.gb_details.Name = "gb_details";
            this.gb_details.Padding = new System.Windows.Forms.Padding(6);
            this.gb_details.Size = new System.Drawing.Size(856, 628);
            this.gb_details.TabIndex = 6;
            this.gb_details.TabStop = false;
            this.gb_details.Text = "Device Details:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(23, 150);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(794, 451);
            this.textBox1.TabIndex = 9;
            // 
            // lb_read
            // 
            this.lb_read.FormattingEnabled = true;
            this.lb_read.ItemHeight = 25;
            this.lb_read.Location = new System.Drawing.Point(629, 27);
            this.lb_read.Margin = new System.Windows.Forms.Padding(6);
            this.lb_read.Name = "lb_read";
            this.lb_read.Size = new System.Drawing.Size(107, 29);
            this.lb_read.TabIndex = 8;
            // 
            // usb
            // 
            this.usb.ProductId = 81;
            this.usb.VendorId = 1105;
            this.usb.OnSpecifiedDeviceArrived += new System.EventHandler(this.usb_OnSpecifiedDeviceArrived);
            this.usb.OnSpecifiedDeviceRemoved += new System.EventHandler(this.usb_OnSpecifiedDeviceRemoved);
            this.usb.OnDeviceArrived += new System.EventHandler(this.usb_OnDeviceArrived);
            this.usb.OnDeviceRemoved += new System.EventHandler(this.usb_OnDeviceRemoved);
            this.usb.OnDataRecieved += new UsbLibrary.DataRecievedEventHandler(this.usb_OnDataRecieved);
            this.usb.OnDataSend += new System.EventHandler(this.usb_OnDataSend);
            // 
            // Sniffer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(906, 841);
            this.Controls.Add(this.gb_filter);
            this.Controls.Add(this.gb_details);
            this.Controls.Add(this.lb_senddata);
            this.Controls.Add(this.tb_send);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(3740, 1900);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "Sniffer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Sniffer";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Sniffer_Load);
            this.gb_filter.ResumeLayout(false);
            this.gb_filter.PerformLayout();
            this.gb_details.ResumeLayout(false);
            this.gb_details.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_recieved;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox tb_send;
        private System.Windows.Forms.Label lb_vendor;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label lb_product;
        private System.Windows.Forms.Label lb_senddata;
        private System.Windows.Forms.Label lb_messages;
        private System.Windows.Forms.TextBox tb_vendor;
        private System.Windows.Forms.TextBox tb_product;
        private System.Windows.Forms.GroupBox gb_filter;
        private System.Windows.Forms.ListBox lb_message;
        private System.Windows.Forms.GroupBox gb_details;
        private UsbLibrary.UsbHidPort usb;
        private System.Windows.Forms.ListBox lb_read;
        private System.Windows.Forms.Button buttonOutside;
        private System.Windows.Forms.Button buttonInside;
        private System.Windows.Forms.TextBox textBox1;
    }
}

