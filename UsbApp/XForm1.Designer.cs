namespace kppApp
{
    partial class MainFormKPP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormKPP));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Контроль регистрации", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("История считываний", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "123", System.Drawing.SystemColors.WindowText, System.Drawing.Color.Gainsboro, new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "111-111", System.Drawing.Color.Gray, System.Drawing.Color.Gainsboro, new System.Drawing.Font("Roboto", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "#12356"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Цыпленко Гав Мяуевич"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "20.20.2020 1:01"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Въезд  КПП16  ОГОК"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "🖉   💬", System.Drawing.Color.Teal, System.Drawing.Color.Gainsboro, new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "55555"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "123", System.Drawing.Color.Gray, System.Drawing.Color.Gainsboro, new System.Drawing.Font("Roboto", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))))}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "      💡", System.Drawing.Color.Red, System.Drawing.SystemColors.Window, new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "111-111"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "#12356"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Цыпленко Гав Мяуевич"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "20.20.2020 1:01"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Въезд  КПП16  ОГОК"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "🖉   💬", System.Drawing.Color.Teal, System.Drawing.Color.White, new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "55555"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "123")}, -1);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.infotickLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel10 = new System.Windows.Forms.ToolStripStatusLabel();
            this.restapi_path_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelHostAccess = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel12 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ServiceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ServiceStateLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.needRestartlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.threadWorkersUpdater = new System.ComponentModel.BackgroundWorker();
            this.threadPassageSender = new System.ComponentModel.BackgroundWorker();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.timerWorkersUpdate = new System.Windows.Forms.Timer(this.components);
            this.timerWaitMode = new System.Windows.Forms.Timer(this.components);
            this.timerPassageSender = new System.Windows.Forms.Timer(this.components);
            this.timerCol = new System.Windows.Forms.Timer(this.components);
            this.timerEraser = new System.Windows.Forms.Timer(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.buttonBeRed = new System.Windows.Forms.Button();
            this.buttonBeWhite = new System.Windows.Forms.Button();
            this.startBtnSelect = new System.Windows.Forms.Button();
            this.buttonBeGreen = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.listViewHotBuffer = new System.Windows.Forms.ListView();
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEventID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnToDelete = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label32 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButtonDaily = new System.Windows.Forms.RadioButton();
            this.labelShomItem = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.buttonMakeManual = new System.Windows.Forms.Button();
            this.buttonMarkToDelete = new System.Windows.Forms.Button();
            this.comboBoxOperationsMain = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelEventCounter = new System.Windows.Forms.Label();
            this.buttonCheckEvents = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.panelSignal2 = new System.Windows.Forms.Panel();
            this.LayPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelEventName = new System.Windows.Forms.Label();
            this.labelEventDate = new System.Windows.Forms.Label();
            this.labelEventCard = new System.Windows.Forms.Label();
            this.labelEventJobDescription = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelEventUserguid = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelEventFamOtc = new System.Windows.Forms.Label();
            this.panelSignal = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelTPL = new System.Windows.Forms.Label();
            this.tb_vendor = new System.Windows.Forms.TextBox();
            this.tb_product = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel18 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.listViewHistory = new System.Windows.Forms.ListView();
            this.columnFailure = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCard = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTabnom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFIO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnOperation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnManual = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDelivery = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label45 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.labelSelectedEventsCount = new System.Windows.Forms.Label();
            this.panelFilterSelect = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.tabSubfilter = new System.Windows.Forms.TabControl();
            this.tabCard = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.cardTextSelect = new System.Windows.Forms.TextBox();
            this.tabTabnom = new System.Windows.Forms.TabPage();
            this.label38 = new System.Windows.Forms.Label();
            this.tabnomTextSelect = new System.Windows.Forms.TextBox();
            this.tabFIO = new System.Windows.Forms.TabPage();
            this.label39 = new System.Windows.Forms.Label();
            this.fioTextSelect = new System.Windows.Forms.TextBox();
            this.tabOperation = new System.Windows.Forms.TabPage();
            this.label40 = new System.Windows.Forms.Label();
            this.comboBoxHistoryOperations = new System.Windows.Forms.ComboBox();
            this.tabDelivery = new System.Windows.Forms.TabPage();
            this.radioWait = new System.Windows.Forms.RadioButton();
            this.radioDelivered = new System.Windows.Forms.RadioButton();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.endPickerSelect = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.begPickerSelect = new System.Windows.Forms.DateTimePicker();
            this.buttonResetFilter = new System.Windows.Forms.Button();
            this.buttonHistoryReload = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label31 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelManualTabnomKeeper = new System.Windows.Forms.Label();
            this.labelManualEventOperation = new System.Windows.Forms.Label();
            this.labelManualEventCard = new System.Windows.Forms.Label();
            this.hintsManualEventCard = new System.Windows.Forms.ListBox();
            this.hintsManualEventFIO = new System.Windows.Forms.ListBox();
            this.hintsManualEventGUID = new System.Windows.Forms.ListBox();
            this.lvManualEventSearch = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboManualEventOperation = new System.Windows.Forms.ComboBox();
            this.buttonManualEventSearchByCard = new System.Windows.Forms.Button();
            this.buttonManualEventSearchByGUID = new System.Windows.Forms.Button();
            this.buttonManualEventSearchByFIO = new System.Windows.Forms.Button();
            this.editManualEventComment = new System.Windows.Forms.TextBox();
            this.editManualEventCard = new System.Windows.Forms.TextBox();
            this.editManualEventFIO = new System.Windows.Forms.TextBox();
            this.editManualEventGUID = new System.Windows.Forms.TextBox();
            this.buttonCancelManualEvent = new System.Windows.Forms.Button();
            this.buttonOKManualEvent = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.labelRedOperation = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.labelRedEventID = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.comboRedEventOperation = new System.Windows.Forms.ComboBox();
            this.editRedEventComment = new System.Windows.Forms.TextBox();
            this.editRedEventCard = new System.Windows.Forms.TextBox();
            this.editRedEventFIO = new System.Windows.Forms.TextBox();
            this.editRedEventGUID = new System.Windows.Forms.TextBox();
            this.buttonCancelRedEvent = new System.Windows.Forms.Button();
            this.buttonOkRedEvent = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.boxmethod2 = new System.Windows.Forms.TextBox();
            this.methodbox = new System.Windows.Forms.TextBox();
            this.hostbox = new System.Windows.Forms.TextBox();
            this.passwordbox = new System.Windows.Forms.TextBox();
            this.loginbox = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.buttonPOST = new System.Windows.Forms.Button();
            this.editGreenEventTabnom = new System.Windows.Forms.NumericUpDown();
            this.labelGreenOperation = new System.Windows.Forms.Label();
            this.labelGreenTabnom = new System.Windows.Forms.Label();
            this.labelGreenEventID = new System.Windows.Forms.Label();
            this.hintsGreenEventCard = new System.Windows.Forms.ListBox();
            this.hintsGreenEventFIO = new System.Windows.Forms.ListBox();
            this.hintsGreenEventGUID = new System.Windows.Forms.ListBox();
            this.comboGreenEventOperation = new System.Windows.Forms.ComboBox();
            this.buttonGreenEventSearchByCard = new System.Windows.Forms.Button();
            this.buttonGreenEventSearchByGUID = new System.Windows.Forms.Button();
            this.buttonGreenEventSearchByFIO = new System.Windows.Forms.Button();
            this.editGreenEventComment = new System.Windows.Forms.TextBox();
            this.editGreenEventCard = new System.Windows.Forms.TextBox();
            this.editGreenEventFIO = new System.Windows.Forms.TextBox();
            this.editGreenEventGUID = new System.Windows.Forms.TextBox();
            this.buttonCancelGreenEvent = new System.Windows.Forms.Button();
            this.buttonOkGreenEvent = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lvGreenEventSearch = new System.Windows.Forms.ListView();
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonDeleteGreenEvent = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.panel20 = new System.Windows.Forms.Panel();
            this.operCheck = new System.Windows.Forms.CheckBox();
            this.peopleCheck = new System.Windows.Forms.CheckBox();
            this.blockingBox = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.usb = new UsbLibrary.UsbHidPort(this.components);
            this.threadEraser30 = new System.ComponentModel.BackgroundWorker();
            this.timerPupdate = new System.Windows.Forms.Timer(this.components);
            this.threadPupdate = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelSignal2.SuspendLayout();
            this.LayPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelSignal.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panelFilterSelect.SuspendLayout();
            this.panel17.SuspendLayout();
            this.tabSubfilter.SuspendLayout();
            this.tabCard.SuspendLayout();
            this.tabTabnom.SuspendLayout();
            this.tabFIO.SuspendLayout();
            this.tabOperation.SuspendLayout();
            this.tabDelivery.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editGreenEventTabnom)).BeginInit();
            this.tabPage7.SuspendLayout();
            this.panel20.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.statusStrip1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel7,
            this.infotickLabel,
            this.toolStripStatusLabel8,
            this.toolStripStatusLabel9,
            this.toolStripStatusLabel10,
            this.restapi_path_label,
            this.labelHostAccess,
            this.toolStripStatusLabel12,
            this.ServiceLabel,
            this.ServiceStateLabel,
            this.needRestartlabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 803);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 7, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1524, 23);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(84, 18);
            this.toolStripStatusLabel1.Text = "Считыватель";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 18);
            this.toolStripStatusLabel2.Text = ":";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.Color.Salmon;
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(65, 18);
            this.toolStripStatusLabel3.Text = "Отключен";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(14, 18);
            this.toolStripStatusLabel4.Text = " ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(46, 18);
            this.toolStripStatusLabel5.Text = "Кадры";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(10, 18);
            this.toolStripStatusLabel7.Text = ":";
            // 
            // infotickLabel
            // 
            this.infotickLabel.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infotickLabel.Name = "infotickLabel";
            this.infotickLabel.Size = new System.Drawing.Size(124, 18);
            this.infotickLabel.Text = "1970-01-01 00:00:01";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(0, 18);
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(10, 18);
            this.toolStripStatusLabel9.Text = " ";
            // 
            // toolStripStatusLabel10
            // 
            this.toolStripStatusLabel10.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel10.Name = "toolStripStatusLabel10";
            this.toolStripStatusLabel10.Size = new System.Drawing.Size(48, 18);
            this.toolStripStatusLabel10.Text = "Сервер";
            // 
            // restapi_path_label
            // 
            this.restapi_path_label.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.restapi_path_label.Name = "restapi_path_label";
            this.restapi_path_label.Size = new System.Drawing.Size(92, 18);
            this.restapi_path_label.Text = "http://localhost";
            this.restapi_path_label.DoubleClick += new System.EventHandler(this.restapi_path_label_DoubleClick);
            // 
            // labelHostAccess
            // 
            this.labelHostAccess.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHostAccess.Name = "labelHostAccess";
            this.labelHostAccess.Size = new System.Drawing.Size(74, 18);
            this.labelHostAccess.Text = "Недоступен";
            // 
            // toolStripStatusLabel12
            // 
            this.toolStripStatusLabel12.Name = "toolStripStatusLabel12";
            this.toolStripStatusLabel12.Size = new System.Drawing.Size(10, 18);
            this.toolStripStatusLabel12.Text = " ";
            // 
            // ServiceLabel
            // 
            this.ServiceLabel.Name = "ServiceLabel";
            this.ServiceLabel.Size = new System.Drawing.Size(151, 18);
            this.ServiceLabel.Text = "Локальная служба REST:";
            this.ServiceLabel.Visible = false;
            // 
            // ServiceStateLabel
            // 
            this.ServiceStateLabel.Name = "ServiceStateLabel";
            this.ServiceStateLabel.Size = new System.Drawing.Size(72, 18);
            this.ServiceStateLabel.Text = "Исключена";
            this.ServiceStateLabel.Visible = false;
            // 
            // needRestartlabel
            // 
            this.needRestartlabel.ForeColor = System.Drawing.Color.Red;
            this.needRestartlabel.Name = "needRestartlabel";
            this.needRestartlabel.Size = new System.Drawing.Size(207, 18);
            this.needRestartlabel.Text = "Требуется перезапуск приложения";
            this.needRestartlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.needRestartlabel.Visible = false;
            // 
            // threadWorkersUpdater
            // 
            this.threadWorkersUpdater.DoWork += new System.ComponentModel.DoWorkEventHandler(this.threadWorkersUpdater_DoWork);
            this.threadWorkersUpdater.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.threadWorkersUpdater_RunWorkerCompleted);
            // 
            // threadPassageSender
            // 
            this.threadPassageSender.DoWork += new System.ComponentModel.DoWorkEventHandler(this.threadPassageSender_DoWork);
            this.threadPassageSender.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.threadPassageSender_RunWorkerCompleted);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "qr.png");
            this.imageList1.Images.SetKeyName(1, "story.png");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "clock3.png");
            this.imageList2.Images.SetKeyName(1, "done3.png");
            this.imageList2.Images.SetKeyName(2, "red3.png");
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.imageList3.Images.SetKeyName(0, "filter.png");
            this.imageList3.Images.SetKeyName(1, "filter1.png");
            // 
            // timerWorkersUpdate
            // 
            this.timerWorkersUpdate.Interval = 30000;
            this.timerWorkersUpdate.Tick += new System.EventHandler(this.timerWorkersUpdate_Tick);
            // 
            // timerWaitMode
            // 
            this.timerWaitMode.Interval = 2500;
            this.timerWaitMode.Tick += new System.EventHandler(this.timerWaitMode_Tick);
            // 
            // timerPassageSender
            // 
            this.timerPassageSender.Interval = 3000;
            this.timerPassageSender.Tick += new System.EventHandler(this.timerPassageSender_Tick);
            // 
            // timerCol
            // 
            this.timerCol.Interval = 700;
            this.timerCol.Tick += new System.EventHandler(this.timerCol_Tick);
            // 
            // timerEraser
            // 
            this.timerEraser.Enabled = true;
            this.timerEraser.Interval = 1800000;
            this.timerEraser.Tick += new System.EventHandler(this.timerEraser_Tick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Font = new System.Drawing.Font("Roboto Lt", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.splitContainer2.Panel1.Controls.Add(this.buttonBeRed);
            this.splitContainer2.Panel1.Controls.Add(this.buttonBeWhite);
            this.splitContainer2.Panel1.Controls.Add(this.startBtnSelect);
            this.splitContainer2.Panel1.Controls.Add(this.buttonBeGreen);
            this.splitContainer2.Panel1.Controls.Add(this.listView1);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer2.Panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Panel2.Font = new System.Drawing.Font("Roboto Lt", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.splitContainer2.Size = new System.Drawing.Size(1524, 803);
            this.splitContainer2.SplitterDistance = 220;
            this.splitContainer2.TabIndex = 4;
            // 
            // buttonBeRed
            // 
            this.buttonBeRed.BackColor = System.Drawing.Color.OrangeRed;
            this.buttonBeRed.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBeRed.Location = new System.Drawing.Point(54, 240);
            this.buttonBeRed.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBeRed.Name = "buttonBeRed";
            this.buttonBeRed.Size = new System.Drawing.Size(125, 48);
            this.buttonBeRed.TabIndex = 16;
            this.buttonBeRed.Text = "red";
            this.buttonBeRed.UseVisualStyleBackColor = false;
            this.buttonBeRed.Visible = false;
            this.buttonBeRed.Click += new System.EventHandler(this.buttonBeRed_Click);
            // 
            // buttonBeWhite
            // 
            this.buttonBeWhite.BackColor = System.Drawing.Color.White;
            this.buttonBeWhite.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBeWhite.Location = new System.Drawing.Point(51, 368);
            this.buttonBeWhite.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBeWhite.Name = "buttonBeWhite";
            this.buttonBeWhite.Size = new System.Drawing.Size(125, 48);
            this.buttonBeWhite.TabIndex = 19;
            this.buttonBeWhite.Text = "white";
            this.buttonBeWhite.UseVisualStyleBackColor = false;
            this.buttonBeWhite.Visible = false;
            this.buttonBeWhite.Click += new System.EventHandler(this.buttonBeWhite_Click);
            // 
            // startBtnSelect
            // 
            this.startBtnSelect.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startBtnSelect.Location = new System.Drawing.Point(43, 443);
            this.startBtnSelect.Margin = new System.Windows.Forms.Padding(2);
            this.startBtnSelect.Name = "startBtnSelect";
            this.startBtnSelect.Size = new System.Drawing.Size(134, 23);
            this.startBtnSelect.TabIndex = 17;
            this.startBtnSelect.Text = "Select";
            this.startBtnSelect.UseVisualStyleBackColor = true;
            this.startBtnSelect.Visible = false;
            // 
            // buttonBeGreen
            // 
            this.buttonBeGreen.BackColor = System.Drawing.Color.LightGreen;
            this.buttonBeGreen.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBeGreen.Location = new System.Drawing.Point(51, 306);
            this.buttonBeGreen.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBeGreen.Name = "buttonBeGreen";
            this.buttonBeGreen.Size = new System.Drawing.Size(125, 48);
            this.buttonBeGreen.TabIndex = 18;
            this.buttonBeGreen.Text = "green";
            this.buttonBeGreen.UseVisualStyleBackColor = false;
            this.buttonBeGreen.Visible = false;
            this.buttonBeGreen.Click += new System.EventHandler(this.buttonBeGreen_Click);
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.AutoArrange = false;
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Font = new System.Drawing.Font("Roboto", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.ForeColor = System.Drawing.SystemColors.Window;
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            listViewItem1.IndentCount = 5;
            listViewItem2.IndentCount = 5;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listView1.LabelWrap = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(0, 104);
            this.listView1.Margin = new System.Windows.Forms.Padding(1);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Scrollable = false;
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(218, 93);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 15;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::kppApp.Properties.Resources.polus1;
            this.pictureBox1.Location = new System.Drawing.Point(9, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(170, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 100);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1300, 803);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 2;
            this.tabControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvManualEventSearch_MouseDoubleClick);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Controls.Add(this.panelSignal);
            this.tabPage1.Controls.Add(this.tb_vendor);
            this.tabPage1.Controls.Add(this.tb_product);
            this.tabPage1.Location = new System.Drawing.Point(4, 104);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(1);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(1);
            this.tabPage1.Size = new System.Drawing.Size(1292, 695);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Главное";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 29);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.5102F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.4898F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1290, 665);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.panel10, 3);
            this.panel10.Controls.Add(this.panel13);
            this.panel10.Controls.Add(this.splitter1);
            this.panel10.Controls.Add(this.panel12);
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Location = new System.Drawing.Point(15, 169);
            this.panel10.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1266, 496);
            this.panel10.TabIndex = 16;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.listViewHotBuffer);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(0, 24);
            this.panel13.Margin = new System.Windows.Forms.Padding(1);
            this.panel13.Name = "panel13";
            this.panel13.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panel13.Size = new System.Drawing.Size(1266, 452);
            this.panel13.TabIndex = 6;
            // 
            // listViewHotBuffer
            // 
            this.listViewHotBuffer.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewHotBuffer.BackColor = System.Drawing.Color.White;
            this.listViewHotBuffer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewHotBuffer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader16,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnEventID,
            this.columnToDelete});
            this.listViewHotBuffer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHotBuffer.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listViewHotBuffer.ForeColor = System.Drawing.Color.Gray;
            this.listViewHotBuffer.FullRowSelect = true;
            this.listViewHotBuffer.GridLines = true;
            this.listViewHotBuffer.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewHotBuffer.HideSelection = false;
            listViewItem3.UseItemStyleForSubItems = false;
            this.listViewHotBuffer.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3});
            this.listViewHotBuffer.Location = new System.Drawing.Point(5, 5);
            this.listViewHotBuffer.Margin = new System.Windows.Forms.Padding(1);
            this.listViewHotBuffer.MultiSelect = false;
            this.listViewHotBuffer.Name = "listViewHotBuffer";
            this.listViewHotBuffer.ShowGroups = false;
            this.listViewHotBuffer.Size = new System.Drawing.Size(1256, 442);
            this.listViewHotBuffer.SmallImageList = this.imageList2;
            this.listViewHotBuffer.TabIndex = 5;
            this.listViewHotBuffer.UseCompatibleStateImageBehavior = false;
            this.listViewHotBuffer.View = System.Windows.Forms.View.Details;
            this.listViewHotBuffer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewHotBuffer_MouseDoubleClick);
            this.listViewHotBuffer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewHotBuffer_MouseUp);
            // 
            // columnHeader16
            // 
            this.columnHeader16.DisplayIndex = 6;
            this.columnHeader16.Text = "Статус";
            this.columnHeader16.Width = 89;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 0;
            this.columnHeader2.Text = "Номер карты";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 144;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 1;
            this.columnHeader3.Text = "Таб.номер";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 115;
            // 
            // columnHeader4
            // 
            this.columnHeader4.DisplayIndex = 2;
            this.columnHeader4.Text = "ФИО";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 289;
            // 
            // columnHeader5
            // 
            this.columnHeader5.DisplayIndex = 3;
            this.columnHeader5.Text = "Дата регистрации";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 192;
            // 
            // columnHeader6
            // 
            this.columnHeader6.DisplayIndex = 4;
            this.columnHeader6.Text = "Событие";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 240;
            // 
            // columnHeader7
            // 
            this.columnHeader7.DisplayIndex = 5;
            this.columnHeader7.Text = "Вручную";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 99;
            // 
            // columnEventID
            // 
            this.columnEventID.Text = "";
            this.columnEventID.Width = 0;
            // 
            // columnToDelete
            // 
            this.columnToDelete.Text = "⌫";
            this.columnToDelete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnToDelete.Width = 58;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 23);
            this.splitter1.Margin = new System.Windows.Forms.Padding(1);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1266, 1);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            this.splitter1.Visible = false;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.White;
            this.panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel12.Controls.Add(this.label32);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel12.Location = new System.Drawing.Point(0, 476);
            this.panel12.Margin = new System.Windows.Forms.Padding(1);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1266, 20);
            this.panel12.TabIndex = 4;
            // 
            // label32
            // 
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Image = global::kppApp.Properties.Resources.bottom1;
            this.label32.Location = new System.Drawing.Point(0, 0);
            this.label32.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1266, 20);
            this.label32.TabIndex = 2;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.White;
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel11.Controls.Add(this.flowLayoutPanel2);
            this.panel11.Controls.Add(this.labelShomItem);
            this.panel11.Controls.Add(this.label33);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Margin = new System.Windows.Forms.Padding(1);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1266, 23);
            this.panel11.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.radioButton1);
            this.flowLayoutPanel2.Controls.Add(this.radioButtonDaily);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1013, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(254, 23);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.radioButton1.Location = new System.Drawing.Point(1, 1);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(1);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(163, 21);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "С последнего сброса";
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButtonDaily
            // 
            this.radioButtonDaily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonDaily.AutoSize = true;
            this.radioButtonDaily.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.radioButtonDaily.Location = new System.Drawing.Point(166, 1);
            this.radioButtonDaily.Margin = new System.Windows.Forms.Padding(1);
            this.radioButtonDaily.Name = "radioButtonDaily";
            this.radioButtonDaily.Size = new System.Drawing.Size(83, 21);
            this.radioButtonDaily.TabIndex = 8;
            this.radioButtonDaily.Text = "За сутки";
            this.radioButtonDaily.UseVisualStyleBackColor = true;
            this.radioButtonDaily.Click += new System.EventHandler(this.radioButtonDaily_Click);
            // 
            // labelShomItem
            // 
            this.labelShomItem.AutoSize = true;
            this.labelShomItem.Location = new System.Drawing.Point(690, 4);
            this.labelShomItem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelShomItem.Name = "labelShomItem";
            this.labelShomItem.Size = new System.Drawing.Size(102, 15);
            this.labelShomItem.TabIndex = 9;
            this.labelShomItem.Text = "DO NOT DELETE";
            this.labelShomItem.Visible = false;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label33.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(-3, -3);
            this.label33.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(181, 33);
            this.label33.TabIndex = 5;
            this.label33.Text = "История считываний:";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel14);
            this.flowLayoutPanel1.Controls.Add(this.panel15);
            this.flowLayoutPanel1.Controls.Add(this.panelSignal2);
            this.flowLayoutPanel1.ForeColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(164, 2);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(968, 148);
            this.flowLayoutPanel1.TabIndex = 15;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.Transparent;
            this.panel14.Controls.Add(this.buttonMakeManual);
            this.panel14.Controls.Add(this.buttonMarkToDelete);
            this.panel14.Controls.Add(this.comboBoxOperationsMain);
            this.panel14.Controls.Add(this.label9);
            this.panel14.Location = new System.Drawing.Point(3, 3);
            this.panel14.Margin = new System.Windows.Forms.Padding(0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(306, 144);
            this.panel14.TabIndex = 5;
            // 
            // buttonMakeManual
            // 
            this.buttonMakeManual.BackColor = System.Drawing.Color.Teal;
            this.buttonMakeManual.FlatAppearance.BorderSize = 0;
            this.buttonMakeManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMakeManual.Font = new System.Drawing.Font("Roboto", 10F);
            this.buttonMakeManual.ForeColor = System.Drawing.Color.White;
            this.buttonMakeManual.Location = new System.Drawing.Point(14, 80);
            this.buttonMakeManual.Margin = new System.Windows.Forms.Padding(1);
            this.buttonMakeManual.Name = "buttonMakeManual";
            this.buttonMakeManual.Size = new System.Drawing.Size(140, 33);
            this.buttonMakeManual.TabIndex = 13;
            this.buttonMakeManual.Text = "Создать событие";
            this.buttonMakeManual.UseVisualStyleBackColor = false;
            this.buttonMakeManual.Click += new System.EventHandler(this.buttonMakeManual_Click);
            // 
            // buttonMarkToDelete
            // 
            this.buttonMarkToDelete.BackColor = System.Drawing.Color.White;
            this.buttonMarkToDelete.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.buttonMarkToDelete.FlatAppearance.BorderSize = 0;
            this.buttonMarkToDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonMarkToDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonMarkToDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMarkToDelete.Font = new System.Drawing.Font("Roboto", 10F);
            this.buttonMarkToDelete.ForeColor = System.Drawing.Color.DarkRed;
            this.buttonMarkToDelete.Location = new System.Drawing.Point(194, 80);
            this.buttonMarkToDelete.Margin = new System.Windows.Forms.Padding(1);
            this.buttonMarkToDelete.Name = "buttonMarkToDelete";
            this.buttonMarkToDelete.Size = new System.Drawing.Size(98, 33);
            this.buttonMarkToDelete.TabIndex = 14;
            this.buttonMarkToDelete.Text = "К удалению";
            this.buttonMarkToDelete.UseVisualStyleBackColor = false;
            this.buttonMarkToDelete.Click += new System.EventHandler(this.buttonMarkToDelete_Click);
            // 
            // comboBoxOperationsMain
            // 
            this.comboBoxOperationsMain.BackColor = System.Drawing.Color.White;
            this.comboBoxOperationsMain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOperationsMain.DropDownWidth = 450;
            this.comboBoxOperationsMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxOperationsMain.Font = new System.Drawing.Font("Roboto", 10F);
            this.comboBoxOperationsMain.IntegralHeight = false;
            this.comboBoxOperationsMain.ItemHeight = 15;
            this.comboBoxOperationsMain.Location = new System.Drawing.Point(14, 30);
            this.comboBoxOperationsMain.MaxDropDownItems = 30;
            this.comboBoxOperationsMain.Name = "comboBoxOperationsMain";
            this.comboBoxOperationsMain.Size = new System.Drawing.Size(278, 23);
            this.comboBoxOperationsMain.TabIndex = 12;
            this.comboBoxOperationsMain.TabStop = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(11, 3);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 25);
            this.label9.TabIndex = 14;
            this.label9.Text = "Выбрать событие";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Transparent;
            this.panel15.Controls.Add(this.panel3);
            this.panel15.Location = new System.Drawing.Point(309, 3);
            this.panel15.Margin = new System.Windows.Forms.Padding(0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(307, 152);
            this.panel15.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.labelEventCounter);
            this.panel3.Controls.Add(this.buttonCheckEvents);
            this.panel3.Controls.Add(this.label10);
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Margin = new System.Windows.Forms.Padding(1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 141);
            this.panel3.TabIndex = 11;
            // 
            // labelEventCounter
            // 
            this.labelEventCounter.BackColor = System.Drawing.Color.White;
            this.labelEventCounter.Font = new System.Drawing.Font("Roboto", 18F);
            this.labelEventCounter.ForeColor = System.Drawing.Color.Black;
            this.labelEventCounter.Location = new System.Drawing.Point(214, 11);
            this.labelEventCounter.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelEventCounter.Name = "labelEventCounter";
            this.labelEventCounter.Size = new System.Drawing.Size(55, 42);
            this.labelEventCounter.TabIndex = 6;
            this.labelEventCounter.Text = "0";
            this.labelEventCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCheckEvents
            // 
            this.buttonCheckEvents.BackColor = System.Drawing.Color.Transparent;
            this.buttonCheckEvents.FlatAppearance.BorderSize = 0;
            this.buttonCheckEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCheckEvents.Font = new System.Drawing.Font("Roboto", 10F);
            this.buttonCheckEvents.ForeColor = System.Drawing.Color.Teal;
            this.buttonCheckEvents.Image = global::kppApp.Properties.Resources.rarrows;
            this.buttonCheckEvents.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCheckEvents.Location = new System.Drawing.Point(25, 76);
            this.buttonCheckEvents.Margin = new System.Windows.Forms.Padding(1);
            this.buttonCheckEvents.Name = "buttonCheckEvents";
            this.buttonCheckEvents.Size = new System.Drawing.Size(111, 33);
            this.buttonCheckEvents.TabIndex = 5;
            this.buttonCheckEvents.Text = "Сбросить";
            this.buttonCheckEvents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCheckEvents.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCheckEvents.UseVisualStyleBackColor = false;
            this.buttonCheckEvents.Click += new System.EventHandler(this.buttonCheckEvents_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(1, 12);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(199, 41);
            this.label10.TabIndex = 4;
            this.label10.Text = "Счетчик считываний:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelSignal2
            // 
            this.panelSignal2.BackColor = System.Drawing.Color.Transparent;
            this.panelSignal2.Controls.Add(this.LayPanel);
            this.panelSignal2.Location = new System.Drawing.Point(616, 3);
            this.panelSignal2.Margin = new System.Windows.Forms.Padding(0);
            this.panelSignal2.Name = "panelSignal2";
            this.panelSignal2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.panelSignal2.Size = new System.Drawing.Size(352, 145);
            this.panelSignal2.TabIndex = 7;
            // 
            // LayPanel
            // 
            this.LayPanel.BackColor = System.Drawing.Color.White;
            this.LayPanel.ColumnCount = 2;
            this.LayPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.LayPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.LayPanel.Controls.Add(this.panel2, 0, 0);
            this.LayPanel.Controls.Add(this.labelEventCard, 0, 4);
            this.LayPanel.Controls.Add(this.labelEventJobDescription, 0, 2);
            this.LayPanel.Controls.Add(this.label6, 1, 3);
            this.LayPanel.Controls.Add(this.labelEventUserguid, 1, 4);
            this.LayPanel.Controls.Add(this.label8, 0, 3);
            this.LayPanel.Controls.Add(this.labelEventFamOtc, 0, 1);
            this.LayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayPanel.ForeColor = System.Drawing.Color.Black;
            this.LayPanel.Location = new System.Drawing.Point(3, 3);
            this.LayPanel.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.LayPanel.Name = "LayPanel";
            this.LayPanel.RowCount = 5;
            this.LayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.46798F));
            this.LayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.46798F));
            this.LayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.80103F));
            this.LayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.73399F));
            this.LayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.52901F));
            this.LayPanel.Size = new System.Drawing.Size(346, 139);
            this.LayPanel.TabIndex = 1;
            // 
            // panel2
            // 
            this.LayPanel.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.labelEventName);
            this.panel2.Controls.Add(this.labelEventDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(346, 29);
            this.panel2.TabIndex = 16;
            // 
            // labelEventName
            // 
            this.labelEventName.AutoSize = true;
            this.labelEventName.BackColor = System.Drawing.Color.Transparent;
            this.labelEventName.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelEventName.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEventName.ForeColor = System.Drawing.Color.Black;
            this.labelEventName.Location = new System.Drawing.Point(0, 0);
            this.labelEventName.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelEventName.Name = "labelEventName";
            this.labelEventName.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelEventName.Size = new System.Drawing.Size(24, 23);
            this.labelEventName.TabIndex = 14;
            this.labelEventName.Text = "-";
            this.labelEventName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // labelEventDate
            // 
            this.labelEventDate.AutoSize = true;
            this.labelEventDate.BackColor = System.Drawing.Color.Transparent;
            this.labelEventDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelEventDate.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEventDate.Location = new System.Drawing.Point(322, 0);
            this.labelEventDate.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.labelEventDate.Name = "labelEventDate";
            this.labelEventDate.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.labelEventDate.Size = new System.Drawing.Size(24, 23);
            this.labelEventDate.TabIndex = 13;
            this.labelEventDate.Text = "-";
            this.labelEventDate.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // labelEventCard
            // 
            this.labelEventCard.BackColor = System.Drawing.Color.Transparent;
            this.labelEventCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEventCard.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEventCard.ForeColor = System.Drawing.Color.Black;
            this.labelEventCard.Location = new System.Drawing.Point(1, 110);
            this.labelEventCard.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelEventCard.Name = "labelEventCard";
            this.labelEventCard.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelEventCard.Size = new System.Drawing.Size(151, 29);
            this.labelEventCard.TabIndex = 15;
            this.labelEventCard.Text = "-";
            this.labelEventCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEventJobDescription
            // 
            this.labelEventJobDescription.BackColor = System.Drawing.Color.Transparent;
            this.LayPanel.SetColumnSpan(this.labelEventJobDescription, 2);
            this.labelEventJobDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEventJobDescription.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEventJobDescription.ForeColor = System.Drawing.Color.Black;
            this.labelEventJobDescription.Location = new System.Drawing.Point(1, 58);
            this.labelEventJobDescription.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelEventJobDescription.Name = "labelEventJobDescription";
            this.labelEventJobDescription.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelEventJobDescription.Size = new System.Drawing.Size(344, 38);
            this.labelEventJobDescription.TabIndex = 14;
            this.labelEventJobDescription.Text = "-";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Roboto", 9F);
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(154, 96);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.label6.Size = new System.Drawing.Size(191, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "Уникальный ИД";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // labelEventUserguid
            // 
            this.labelEventUserguid.BackColor = System.Drawing.Color.Transparent;
            this.labelEventUserguid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEventUserguid.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEventUserguid.ForeColor = System.Drawing.Color.Black;
            this.labelEventUserguid.Location = new System.Drawing.Point(154, 110);
            this.labelEventUserguid.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelEventUserguid.Name = "labelEventUserguid";
            this.labelEventUserguid.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.labelEventUserguid.Size = new System.Drawing.Size(191, 29);
            this.labelEventUserguid.TabIndex = 9;
            this.labelEventUserguid.Text = "-";
            this.labelEventUserguid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Roboto", 9F);
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(1, 96);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label8.Size = new System.Drawing.Size(151, 14);
            this.label8.TabIndex = 6;
            this.label8.Text = "Номер карты";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // labelEventFamOtc
            // 
            this.labelEventFamOtc.BackColor = System.Drawing.Color.Transparent;
            this.LayPanel.SetColumnSpan(this.labelEventFamOtc, 2);
            this.labelEventFamOtc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEventFamOtc.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEventFamOtc.Location = new System.Drawing.Point(1, 29);
            this.labelEventFamOtc.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelEventFamOtc.Name = "labelEventFamOtc";
            this.labelEventFamOtc.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelEventFamOtc.Size = new System.Drawing.Size(344, 29);
            this.labelEventFamOtc.TabIndex = 2;
            this.labelEventFamOtc.Text = "-";
            this.labelEventFamOtc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelSignal
            // 
            this.panelSignal.BackColor = System.Drawing.Color.Transparent;
            this.panelSignal.Controls.Add(this.label47);
            this.panelSignal.Controls.Add(this.label34);
            this.panelSignal.Controls.Add(this.label11);
            this.panelSignal.Controls.Add(this.labelTPL);
            this.panelSignal.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSignal.Location = new System.Drawing.Point(1, 1);
            this.panelSignal.Margin = new System.Windows.Forms.Padding(1);
            this.panelSignal.MaximumSize = new System.Drawing.Size(0, 41);
            this.panelSignal.Name = "panelSignal";
            this.panelSignal.Size = new System.Drawing.Size(1290, 28);
            this.panelSignal.TabIndex = 6;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.ForeColor = System.Drawing.Color.Red;
            this.label47.Location = new System.Drawing.Point(546, 6);
            this.label47.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(309, 15);
            this.label47.TabIndex = 16;
            this.label47.Text = "64 бита  не работают!!!!!!!!!!!!!!!!!!!!!!! предпочитать 32";
            this.label47.Visible = false;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(139)))), ((int)(((byte)(137)))));
            this.label34.Location = new System.Drawing.Point(335, 3);
            this.label34.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(207, 28);
            this.label34.TabIndex = 14;
            this.label34.Text = "Cервис контроля доступа";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(82, 1);
            this.label11.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(283, 27);
            this.label11.TabIndex = 0;
            this.label11.Text = "Контроль регистрации";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.DoubleClick += new System.EventHandler(this.label11_DoubleClick);
            // 
            // labelTPL
            // 
            this.labelTPL.BackColor = System.Drawing.Color.Yellow;
            this.labelTPL.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTPL.ForeColor = System.Drawing.Color.Red;
            this.labelTPL.Location = new System.Drawing.Point(667, 0);
            this.labelTPL.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelTPL.Name = "labelTPL";
            this.labelTPL.Size = new System.Drawing.Size(22, 28);
            this.labelTPL.TabIndex = 13;
            this.labelTPL.Text = "⚠";
            this.labelTPL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelTPL.Visible = false;
            // 
            // tb_vendor
            // 
            this.tb_vendor.Location = new System.Drawing.Point(1066, 6);
            this.tb_vendor.Name = "tb_vendor";
            this.tb_vendor.Size = new System.Drawing.Size(33, 23);
            this.tb_vendor.TabIndex = 5;
            this.tb_vendor.Text = "046E";
            this.tb_vendor.Visible = false;
            // 
            // tb_product
            // 
            this.tb_product.Location = new System.Drawing.Point(1066, 33);
            this.tb_product.Name = "tb_product";
            this.tb_product.Size = new System.Drawing.Size(33, 23);
            this.tb_product.TabIndex = 6;
            this.tb_product.Text = "52C3";
            this.tb_product.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabPage2.CausesValidation = false;
            this.tabPage2.Controls.Add(this.panel18);
            this.tabPage2.Location = new System.Drawing.Point(4, 104);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(1);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(1);
            this.tabPage2.Size = new System.Drawing.Size(1292, 695);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "История";
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.panel19);
            this.panel18.Controls.Add(this.panel16);
            this.panel18.Controls.Add(this.panelFilterSelect);
            this.panel18.Controls.Add(this.panel1);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel18.Location = new System.Drawing.Point(1, 1);
            this.panel18.Margin = new System.Windows.Forms.Padding(1);
            this.panel18.Name = "panel18";
            this.panel18.Padding = new System.Windows.Forms.Padding(15, 15, 15, 15);
            this.panel18.Size = new System.Drawing.Size(1290, 693);
            this.panel18.TabIndex = 18;
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel19.Controls.Add(this.listViewHistory);
            this.panel19.Controls.Add(this.label45);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Location = new System.Drawing.Point(15, 99);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(1260, 556);
            this.panel19.TabIndex = 27;
            // 
            // listViewHistory
            // 
            this.listViewHistory.BackColor = System.Drawing.Color.White;
            this.listViewHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFailure,
            this.columnCard,
            this.columnTabnom,
            this.columnFIO,
            this.columnDate,
            this.columnOperation,
            this.columnManual,
            this.columnID,
            this.columnDelivery});
            this.listViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHistory.Font = new System.Drawing.Font("Roboto", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listViewHistory.GridLines = true;
            this.listViewHistory.HideSelection = false;
            listViewItem4.StateImageIndex = 0;
            listViewItem4.UseItemStyleForSubItems = false;
            this.listViewHistory.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4});
            this.listViewHistory.Location = new System.Drawing.Point(0, 0);
            this.listViewHistory.Margin = new System.Windows.Forms.Padding(1);
            this.listViewHistory.MultiSelect = false;
            this.listViewHistory.Name = "listViewHistory";
            this.listViewHistory.ShowGroups = false;
            this.listViewHistory.Size = new System.Drawing.Size(1260, 556);
            this.listViewHistory.SmallImageList = this.imageList3;
            this.listViewHistory.TabIndex = 20;
            this.listViewHistory.UseCompatibleStateImageBehavior = false;
            this.listViewHistory.View = System.Windows.Forms.View.Details;
            this.listViewHistory.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView3_ColumnClick);
            // 
            // columnFailure
            // 
            this.columnFailure.DisplayIndex = 7;
            this.columnFailure.Text = "Не найдена";
            this.columnFailure.Width = 81;
            // 
            // columnCard
            // 
            this.columnCard.Text = "Номер карты";
            this.columnCard.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnCard.Width = 121;
            // 
            // columnTabnom
            // 
            this.columnTabnom.Text = "Таб.номер";
            this.columnTabnom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnTabnom.Width = 101;
            // 
            // columnFIO
            // 
            this.columnFIO.Text = "ФИО";
            this.columnFIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnFIO.Width = 215;
            // 
            // columnDate
            // 
            this.columnDate.Text = "Дата регистрации";
            this.columnDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnDate.Width = 122;
            // 
            // columnOperation
            // 
            this.columnOperation.Text = "Событие";
            this.columnOperation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnOperation.Width = 171;
            // 
            // columnManual
            // 
            this.columnManual.Text = "Вручную";
            this.columnManual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnManual.Width = 94;
            // 
            // columnID
            // 
            this.columnID.DisplayIndex = 0;
            this.columnID.Text = "ID";
            this.columnID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnDelivery
            // 
            this.columnDelivery.Text = "Ожид.";
            this.columnDelivery.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnDelivery.Width = 91;
            // 
            // label45
            // 
            this.label45.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label45.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label45.Location = new System.Drawing.Point(3, 537);
            this.label45.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(18, 19);
            this.label45.TabIndex = 19;
            this.label45.Text = "0";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.Transparent;
            this.panel16.Controls.Add(this.labelSelectedEventsCount);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel16.Location = new System.Drawing.Point(15, 655);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(1260, 23);
            this.panel16.TabIndex = 20;
            // 
            // labelSelectedEventsCount
            // 
            this.labelSelectedEventsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectedEventsCount.AutoSize = true;
            this.labelSelectedEventsCount.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelSelectedEventsCount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelSelectedEventsCount.Location = new System.Drawing.Point(3, 4);
            this.labelSelectedEventsCount.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelSelectedEventsCount.Name = "labelSelectedEventsCount";
            this.labelSelectedEventsCount.Size = new System.Drawing.Size(18, 19);
            this.labelSelectedEventsCount.TabIndex = 19;
            this.labelSelectedEventsCount.Text = "0";
            this.labelSelectedEventsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFilterSelect
            // 
            this.panelFilterSelect.Controls.Add(this.label36);
            this.panelFilterSelect.Controls.Add(this.panel17);
            this.panelFilterSelect.Controls.Add(this.buttonResetFilter);
            this.panelFilterSelect.Controls.Add(this.buttonHistoryReload);
            this.panelFilterSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilterSelect.Location = new System.Drawing.Point(15, 41);
            this.panelFilterSelect.Margin = new System.Windows.Forms.Padding(2);
            this.panelFilterSelect.Name = "panelFilterSelect";
            this.panelFilterSelect.Size = new System.Drawing.Size(1260, 58);
            this.panelFilterSelect.TabIndex = 18;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.label36.Location = new System.Drawing.Point(207, 22);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(48, 15);
            this.label36.TabIndex = 23;
            this.label36.Text = "label36";
            this.label36.Visible = false;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.panel17.Controls.Add(this.tabSubfilter);
            this.panel17.Location = new System.Drawing.Point(259, 5);
            this.panel17.Margin = new System.Windows.Forms.Padding(2);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(570, 50);
            this.panel17.TabIndex = 26;
            // 
            // tabSubfilter
            // 
            this.tabSubfilter.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabSubfilter.Controls.Add(this.tabCard);
            this.tabSubfilter.Controls.Add(this.tabTabnom);
            this.tabSubfilter.Controls.Add(this.tabFIO);
            this.tabSubfilter.Controls.Add(this.tabOperation);
            this.tabSubfilter.Controls.Add(this.tabDelivery);
            this.tabSubfilter.Controls.Add(this.tabPage6);
            this.tabSubfilter.ItemSize = new System.Drawing.Size(1, 1);
            this.tabSubfilter.Location = new System.Drawing.Point(14, -1);
            this.tabSubfilter.Margin = new System.Windows.Forms.Padding(0);
            this.tabSubfilter.Multiline = true;
            this.tabSubfilter.Name = "tabSubfilter";
            this.tabSubfilter.Padding = new System.Drawing.Point(0, 0);
            this.tabSubfilter.SelectedIndex = 0;
            this.tabSubfilter.Size = new System.Drawing.Size(541, 89);
            this.tabSubfilter.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabSubfilter.TabIndex = 32;
            // 
            // tabCard
            // 
            this.tabCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabCard.Controls.Add(this.label15);
            this.tabCard.Controls.Add(this.cardTextSelect);
            this.tabCard.ForeColor = System.Drawing.Color.Black;
            this.tabCard.Location = new System.Drawing.Point(4, 5);
            this.tabCard.Margin = new System.Windows.Forms.Padding(0);
            this.tabCard.Name = "tabCard";
            this.tabCard.Size = new System.Drawing.Size(533, 80);
            this.tabCard.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Roboto", 11F);
            this.label15.Location = new System.Drawing.Point(4, 10);
            this.label15.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 18);
            this.label15.TabIndex = 15;
            this.label15.Text = "Карта";
            // 
            // cardTextSelect
            // 
            this.cardTextSelect.Font = new System.Drawing.Font("Roboto", 11F);
            this.cardTextSelect.Location = new System.Drawing.Point(59, 9);
            this.cardTextSelect.Margin = new System.Windows.Forms.Padding(1);
            this.cardTextSelect.Name = "cardTextSelect";
            this.cardTextSelect.Size = new System.Drawing.Size(152, 25);
            this.cardTextSelect.TabIndex = 14;
            // 
            // tabTabnom
            // 
            this.tabTabnom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabTabnom.Controls.Add(this.label38);
            this.tabTabnom.Controls.Add(this.tabnomTextSelect);
            this.tabTabnom.Location = new System.Drawing.Point(4, 5);
            this.tabTabnom.Margin = new System.Windows.Forms.Padding(2);
            this.tabTabnom.Name = "tabTabnom";
            this.tabTabnom.Padding = new System.Windows.Forms.Padding(2);
            this.tabTabnom.Size = new System.Drawing.Size(533, 80);
            this.tabTabnom.TabIndex = 1;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Roboto", 11F);
            this.label38.Location = new System.Drawing.Point(6, 12);
            this.label38.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(53, 18);
            this.label38.TabIndex = 15;
            this.label38.Text = "Таб №";
            // 
            // tabnomTextSelect
            // 
            this.tabnomTextSelect.Font = new System.Drawing.Font("Roboto", 11F);
            this.tabnomTextSelect.Location = new System.Drawing.Point(67, 9);
            this.tabnomTextSelect.Margin = new System.Windows.Forms.Padding(1);
            this.tabnomTextSelect.Name = "tabnomTextSelect";
            this.tabnomTextSelect.Size = new System.Drawing.Size(141, 25);
            this.tabnomTextSelect.TabIndex = 14;
            // 
            // tabFIO
            // 
            this.tabFIO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabFIO.Controls.Add(this.label39);
            this.tabFIO.Controls.Add(this.fioTextSelect);
            this.tabFIO.Location = new System.Drawing.Point(4, 5);
            this.tabFIO.Margin = new System.Windows.Forms.Padding(2);
            this.tabFIO.Name = "tabFIO";
            this.tabFIO.Padding = new System.Windows.Forms.Padding(2);
            this.tabFIO.Size = new System.Drawing.Size(533, 80);
            this.tabFIO.TabIndex = 2;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Roboto", 9F);
            this.label39.Location = new System.Drawing.Point(6, 12);
            this.label39.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(31, 14);
            this.label39.TabIndex = 15;
            this.label39.Text = "ФИО";
            // 
            // fioTextSelect
            // 
            this.fioTextSelect.Font = new System.Drawing.Font("Roboto", 9F);
            this.fioTextSelect.Location = new System.Drawing.Point(51, 10);
            this.fioTextSelect.Margin = new System.Windows.Forms.Padding(1);
            this.fioTextSelect.Name = "fioTextSelect";
            this.fioTextSelect.Size = new System.Drawing.Size(252, 22);
            this.fioTextSelect.TabIndex = 14;
            // 
            // tabOperation
            // 
            this.tabOperation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabOperation.Controls.Add(this.label40);
            this.tabOperation.Controls.Add(this.comboBoxHistoryOperations);
            this.tabOperation.Location = new System.Drawing.Point(4, 5);
            this.tabOperation.Margin = new System.Windows.Forms.Padding(2);
            this.tabOperation.Name = "tabOperation";
            this.tabOperation.Padding = new System.Windows.Forms.Padding(2);
            this.tabOperation.Size = new System.Drawing.Size(533, 80);
            this.tabOperation.TabIndex = 3;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label40.Location = new System.Drawing.Point(15, 8);
            this.label40.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(71, 18);
            this.label40.TabIndex = 16;
            this.label40.Text = "Событие";
            // 
            // comboBoxHistoryOperations
            // 
            this.comboBoxHistoryOperations.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxHistoryOperations.DropDownWidth = 450;
            this.comboBoxHistoryOperations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxHistoryOperations.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxHistoryOperations.IntegralHeight = false;
            this.comboBoxHistoryOperations.ItemHeight = 18;
            this.comboBoxHistoryOperations.Location = new System.Drawing.Point(90, 5);
            this.comboBoxHistoryOperations.MaxDropDownItems = 30;
            this.comboBoxHistoryOperations.Name = "comboBoxHistoryOperations";
            this.comboBoxHistoryOperations.Size = new System.Drawing.Size(301, 26);
            this.comboBoxHistoryOperations.TabIndex = 13;
            this.comboBoxHistoryOperations.TabStop = false;
            // 
            // tabDelivery
            // 
            this.tabDelivery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabDelivery.Controls.Add(this.radioWait);
            this.tabDelivery.Controls.Add(this.radioDelivered);
            this.tabDelivery.Location = new System.Drawing.Point(4, 5);
            this.tabDelivery.Margin = new System.Windows.Forms.Padding(2);
            this.tabDelivery.Name = "tabDelivery";
            this.tabDelivery.Padding = new System.Windows.Forms.Padding(2);
            this.tabDelivery.Size = new System.Drawing.Size(533, 80);
            this.tabDelivery.TabIndex = 4;
            // 
            // radioWait
            // 
            this.radioWait.AutoSize = true;
            this.radioWait.Font = new System.Drawing.Font("Roboto", 11F);
            this.radioWait.Location = new System.Drawing.Point(152, 10);
            this.radioWait.Margin = new System.Windows.Forms.Padding(1);
            this.radioWait.Name = "radioWait";
            this.radioWait.Size = new System.Drawing.Size(115, 22);
            this.radioWait.TabIndex = 9;
            this.radioWait.Text = "Ожидающие";
            this.radioWait.UseVisualStyleBackColor = true;
            // 
            // radioDelivered
            // 
            this.radioDelivered.AutoSize = true;
            this.radioDelivered.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioDelivered.Checked = true;
            this.radioDelivered.Font = new System.Drawing.Font("Roboto", 11F);
            this.radioDelivered.Location = new System.Drawing.Point(3, 10);
            this.radioDelivered.Margin = new System.Windows.Forms.Padding(1);
            this.radioDelivered.Name = "radioDelivered";
            this.radioDelivered.Size = new System.Drawing.Size(134, 22);
            this.radioDelivered.TabIndex = 8;
            this.radioDelivered.TabStop = true;
            this.radioDelivered.Text = "Доставленные";
            this.radioDelivered.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioDelivered.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabPage6.Controls.Add(this.label14);
            this.tabPage6.Controls.Add(this.endPickerSelect);
            this.tabPage6.Controls.Add(this.label13);
            this.tabPage6.Controls.Add(this.begPickerSelect);
            this.tabPage6.Location = new System.Drawing.Point(4, 5);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage6.Size = new System.Drawing.Size(533, 80);
            this.tabPage6.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(216, 17);
            this.label14.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 15);
            this.label14.TabIndex = 21;
            this.label14.Text = "Окончание";
            // 
            // endPickerSelect
            // 
            this.endPickerSelect.CustomFormat = "dd.MM.yy HH:mm";
            this.endPickerSelect.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.endPickerSelect.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endPickerSelect.Location = new System.Drawing.Point(309, 14);
            this.endPickerSelect.Margin = new System.Windows.Forms.Padding(1);
            this.endPickerSelect.Name = "endPickerSelect";
            this.endPickerSelect.Size = new System.Drawing.Size(140, 23);
            this.endPickerSelect.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(6, 17);
            this.label13.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 15);
            this.label13.TabIndex = 19;
            this.label13.Text = "Начало";
            // 
            // begPickerSelect
            // 
            this.begPickerSelect.CustomFormat = "dd.MM.yy HH:mm";
            this.begPickerSelect.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.begPickerSelect.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.begPickerSelect.Location = new System.Drawing.Point(61, 14);
            this.begPickerSelect.Margin = new System.Windows.Forms.Padding(1);
            this.begPickerSelect.Name = "begPickerSelect";
            this.begPickerSelect.Size = new System.Drawing.Size(140, 23);
            this.begPickerSelect.TabIndex = 18;
            // 
            // buttonResetFilter
            // 
            this.buttonResetFilter.BackColor = System.Drawing.Color.White;
            this.buttonResetFilter.FlatAppearance.BorderSize = 0;
            this.buttonResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonResetFilter.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonResetFilter.ForeColor = System.Drawing.Color.Black;
            this.buttonResetFilter.Location = new System.Drawing.Point(103, 20);
            this.buttonResetFilter.Margin = new System.Windows.Forms.Padding(1);
            this.buttonResetFilter.Name = "buttonResetFilter";
            this.buttonResetFilter.Size = new System.Drawing.Size(70, 24);
            this.buttonResetFilter.TabIndex = 22;
            this.buttonResetFilter.Text = "Сброс";
            this.buttonResetFilter.UseVisualStyleBackColor = false;
            this.buttonResetFilter.Click += new System.EventHandler(this.buttonResetFilter_Click);
            // 
            // buttonHistoryReload
            // 
            this.buttonHistoryReload.BackColor = System.Drawing.Color.Teal;
            this.buttonHistoryReload.FlatAppearance.BorderSize = 0;
            this.buttonHistoryReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHistoryReload.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonHistoryReload.ForeColor = System.Drawing.Color.White;
            this.buttonHistoryReload.Location = new System.Drawing.Point(10, 20);
            this.buttonHistoryReload.Margin = new System.Windows.Forms.Padding(1);
            this.buttonHistoryReload.Name = "buttonHistoryReload";
            this.buttonHistoryReload.Size = new System.Drawing.Size(84, 24);
            this.buttonHistoryReload.TabIndex = 20;
            this.buttonHistoryReload.Text = "Загрузить";
            this.buttonHistoryReload.UseVisualStyleBackColor = false;
            this.buttonHistoryReload.Click += new System.EventHandler(this.buttonHistoryReload_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label35);
            this.panel1.Controls.Add(this.label37);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(15, 15);
            this.panel1.MaximumSize = new System.Drawing.Size(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1260, 26);
            this.panel1.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(139)))), ((int)(((byte)(137)))));
            this.label1.Location = new System.Drawing.Point(288, -5);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 28);
            this.label1.TabIndex = 16;
            this.label1.Text = "Список событий";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label35.Location = new System.Drawing.Point(3, -4);
            this.label35.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(283, 27);
            this.label35.TabIndex = 15;
            this.label35.Text = "История считываний";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.Yellow;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label37.ForeColor = System.Drawing.Color.Red;
            this.label37.Location = new System.Drawing.Point(667, 0);
            this.label37.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(22, 28);
            this.label37.TabIndex = 13;
            this.label37.Text = "⚠";
            this.label37.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label37.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabPage3.Controls.Add(this.label31);
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Controls.Add(this.panel5);
            this.tabPage3.Location = new System.Drawing.Point(4, 104);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(1);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(1);
            this.tabPage3.Size = new System.Drawing.Size(1292, 695);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Создание";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(92, 501);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(48, 15);
            this.label31.TabIndex = 31;
            this.label31.Text = "label31";
            this.label31.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(1, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(1);
            this.panel4.MaximumSize = new System.Drawing.Size(0, 41);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1290, 25);
            this.panel4.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(139)))), ((int)(((byte)(137)))));
            this.label12.Location = new System.Drawing.Point(248, 1);
            this.label12.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(207, 28);
            this.label12.TabIndex = 14;
            this.label12.Text = "Cервис контроля доступа";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(59, 0);
            this.label16.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(214, 27);
            this.label16.TabIndex = 0;
            this.label16.Text = "Создание события";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Yellow;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(667, 0);
            this.label17.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(22, 28);
            this.label17.TabIndex = 13;
            this.label17.Text = "⚠";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label17.Visible = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.labelManualTabnomKeeper);
            this.panel5.Controls.Add(this.labelManualEventOperation);
            this.panel5.Controls.Add(this.labelManualEventCard);
            this.panel5.Controls.Add(this.hintsManualEventCard);
            this.panel5.Controls.Add(this.hintsManualEventFIO);
            this.panel5.Controls.Add(this.hintsManualEventGUID);
            this.panel5.Controls.Add(this.lvManualEventSearch);
            this.panel5.Controls.Add(this.comboManualEventOperation);
            this.panel5.Controls.Add(this.buttonManualEventSearchByCard);
            this.panel5.Controls.Add(this.buttonManualEventSearchByGUID);
            this.panel5.Controls.Add(this.buttonManualEventSearchByFIO);
            this.panel5.Controls.Add(this.editManualEventComment);
            this.panel5.Controls.Add(this.editManualEventCard);
            this.panel5.Controls.Add(this.editManualEventFIO);
            this.panel5.Controls.Add(this.editManualEventGUID);
            this.panel5.Controls.Add(this.buttonCancelManualEvent);
            this.panel5.Controls.Add(this.buttonOKManualEvent);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label44);
            this.panel5.Controls.Add(this.label43);
            this.panel5.Controls.Add(this.label42);
            this.panel5.Controls.Add(this.label41);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Location = new System.Drawing.Point(20, 45);
            this.panel5.Margin = new System.Windows.Forms.Padding(1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1044, 421);
            this.panel5.TabIndex = 29;
            // 
            // labelManualTabnomKeeper
            // 
            this.labelManualTabnomKeeper.AutoSize = true;
            this.labelManualTabnomKeeper.Location = new System.Drawing.Point(19, 298);
            this.labelManualTabnomKeeper.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelManualTabnomKeeper.Name = "labelManualTabnomKeeper";
            this.labelManualTabnomKeeper.Size = new System.Drawing.Size(164, 15);
            this.labelManualTabnomKeeper.TabIndex = 82;
            this.labelManualTabnomKeeper.Text = "labelManualTabnomKeeper";
            this.labelManualTabnomKeeper.Visible = false;
            // 
            // labelManualEventOperation
            // 
            this.labelManualEventOperation.AutoSize = true;
            this.labelManualEventOperation.Location = new System.Drawing.Point(317, 407);
            this.labelManualEventOperation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelManualEventOperation.Name = "labelManualEventOperation";
            this.labelManualEventOperation.Size = new System.Drawing.Size(164, 15);
            this.labelManualEventOperation.TabIndex = 81;
            this.labelManualEventOperation.Text = "labelManualEventOperation";
            this.labelManualEventOperation.Visible = false;
            // 
            // labelManualEventCard
            // 
            this.labelManualEventCard.AutoSize = true;
            this.labelManualEventCard.Location = new System.Drawing.Point(15, 406);
            this.labelManualEventCard.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelManualEventCard.Name = "labelManualEventCard";
            this.labelManualEventCard.Size = new System.Drawing.Size(135, 15);
            this.labelManualEventCard.TabIndex = 79;
            this.labelManualEventCard.Text = "labelManualEventCard";
            this.labelManualEventCard.Visible = false;
            // 
            // hintsManualEventCard
            // 
            this.hintsManualEventCard.BackColor = System.Drawing.Color.White;
            this.hintsManualEventCard.Font = new System.Drawing.Font("Roboto", 10F);
            this.hintsManualEventCard.ForeColor = System.Drawing.Color.DimGray;
            this.hintsManualEventCard.FormattingEnabled = true;
            this.hintsManualEventCard.ItemHeight = 15;
            this.hintsManualEventCard.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "9",
            "0",
            "0",
            "11",
            "--"});
            this.hintsManualEventCard.Location = new System.Drawing.Point(32, 63);
            this.hintsManualEventCard.Margin = new System.Windows.Forms.Padding(2);
            this.hintsManualEventCard.MinimumSize = new System.Drawing.Size(4, 94);
            this.hintsManualEventCard.Name = "hintsManualEventCard";
            this.hintsManualEventCard.ScrollAlwaysVisible = true;
            this.hintsManualEventCard.Size = new System.Drawing.Size(251, 94);
            this.hintsManualEventCard.TabIndex = 77;
            this.hintsManualEventCard.Visible = false;
            this.hintsManualEventCard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hintsManualEventCard_MouseClick);
            this.hintsManualEventCard.MouseLeave += new System.EventHandler(this.hintsManualEventCard_MouseLeave);
            this.hintsManualEventCard.MouseHover += new System.EventHandler(this.hintsManualEventCard_MouseHover);
            // 
            // hintsManualEventFIO
            // 
            this.hintsManualEventFIO.Font = new System.Drawing.Font("Roboto", 10F);
            this.hintsManualEventFIO.ForeColor = System.Drawing.Color.DimGray;
            this.hintsManualEventFIO.FormattingEnabled = true;
            this.hintsManualEventFIO.ItemHeight = 15;
            this.hintsManualEventFIO.Location = new System.Drawing.Point(32, 117);
            this.hintsManualEventFIO.Margin = new System.Windows.Forms.Padding(2);
            this.hintsManualEventFIO.MinimumSize = new System.Drawing.Size(4, 94);
            this.hintsManualEventFIO.Name = "hintsManualEventFIO";
            this.hintsManualEventFIO.ScrollAlwaysVisible = true;
            this.hintsManualEventFIO.Size = new System.Drawing.Size(251, 94);
            this.hintsManualEventFIO.TabIndex = 77;
            this.hintsManualEventFIO.Visible = false;
            this.hintsManualEventFIO.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hintsManualEventFIO_MouseClick);
            this.hintsManualEventFIO.MouseLeave += new System.EventHandler(this.hintsManualEventFIO_MouseLeave);
            this.hintsManualEventFIO.MouseHover += new System.EventHandler(this.hintsManualEventFIO_MouseHover);
            // 
            // hintsManualEventGUID
            // 
            this.hintsManualEventGUID.Font = new System.Drawing.Font("Roboto", 10F);
            this.hintsManualEventGUID.ForeColor = System.Drawing.Color.DimGray;
            this.hintsManualEventGUID.FormattingEnabled = true;
            this.hintsManualEventGUID.ItemHeight = 15;
            this.hintsManualEventGUID.Location = new System.Drawing.Point(32, 170);
            this.hintsManualEventGUID.Margin = new System.Windows.Forms.Padding(2);
            this.hintsManualEventGUID.MinimumSize = new System.Drawing.Size(4, 94);
            this.hintsManualEventGUID.Name = "hintsManualEventGUID";
            this.hintsManualEventGUID.ScrollAlwaysVisible = true;
            this.hintsManualEventGUID.Size = new System.Drawing.Size(251, 94);
            this.hintsManualEventGUID.TabIndex = 78;
            this.hintsManualEventGUID.Visible = false;
            this.hintsManualEventGUID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hintsManualEventGUID_MouseClick);
            this.hintsManualEventGUID.MouseLeave += new System.EventHandler(this.hintsManualEventGUID_MouseLeave);
            this.hintsManualEventGUID.MouseHover += new System.EventHandler(this.hintsManualEventGUID_MouseHover);
            // 
            // lvManualEventSearch
            // 
            this.lvManualEventSearch.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvManualEventSearch.BackColor = System.Drawing.SystemColors.Window;
            this.lvManualEventSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvManualEventSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader15,
            this.columnHeader17,
            this.columnHeader18});
            this.lvManualEventSearch.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvManualEventSearch.FullRowSelect = true;
            this.lvManualEventSearch.GridLines = true;
            this.lvManualEventSearch.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvManualEventSearch.HideSelection = false;
            this.lvManualEventSearch.Location = new System.Drawing.Point(313, 21);
            this.lvManualEventSearch.Margin = new System.Windows.Forms.Padding(1);
            this.lvManualEventSearch.MultiSelect = false;
            this.lvManualEventSearch.Name = "lvManualEventSearch";
            this.lvManualEventSearch.ShowGroups = false;
            this.lvManualEventSearch.Size = new System.Drawing.Size(717, 372);
            this.lvManualEventSearch.SmallImageList = this.imageList2;
            this.lvManualEventSearch.TabIndex = 64;
            this.lvManualEventSearch.UseCompatibleStateImageBehavior = false;
            this.lvManualEventSearch.View = System.Windows.Forms.View.Details;
            this.lvManualEventSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvManualEventSearch_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "№ карты";
            this.columnHeader1.Width = 157;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Таб.номер";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 108;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "GUID";
            this.columnHeader17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader17.Width = 132;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "ФИО";
            this.columnHeader18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader18.Width = 304;
            // 
            // comboManualEventOperation
            // 
            this.comboManualEventOperation.BackColor = System.Drawing.Color.White;
            this.comboManualEventOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboManualEventOperation.DropDownWidth = 450;
            this.comboManualEventOperation.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboManualEventOperation.IntegralHeight = false;
            this.comboManualEventOperation.ItemHeight = 15;
            this.comboManualEventOperation.Location = new System.Drawing.Point(20, 200);
            this.comboManualEventOperation.MaxDropDownItems = 30;
            this.comboManualEventOperation.Name = "comboManualEventOperation";
            this.comboManualEventOperation.Size = new System.Drawing.Size(275, 23);
            this.comboManualEventOperation.TabIndex = 63;
            this.comboManualEventOperation.TabStop = false;
            // 
            // buttonManualEventSearchByCard
            // 
            this.buttonManualEventSearchByCard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonManualEventSearchByCard.FlatAppearance.BorderSize = 2;
            this.buttonManualEventSearchByCard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonManualEventSearchByCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonManualEventSearchByCard.ForeColor = System.Drawing.Color.Transparent;
            this.buttonManualEventSearchByCard.Image = global::kppApp.Properties.Resources.zoomer;
            this.buttonManualEventSearchByCard.Location = new System.Drawing.Point(275, 40);
            this.buttonManualEventSearchByCard.Margin = new System.Windows.Forms.Padding(1);
            this.buttonManualEventSearchByCard.Name = "buttonManualEventSearchByCard";
            this.buttonManualEventSearchByCard.Size = new System.Drawing.Size(25, 25);
            this.buttonManualEventSearchByCard.TabIndex = 61;
            this.buttonManualEventSearchByCard.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonManualEventSearchByCard.UseVisualStyleBackColor = true;
            this.buttonManualEventSearchByCard.Click += new System.EventHandler(this.buttonManualEventSearchByCard_Click);
            // 
            // buttonManualEventSearchByGUID
            // 
            this.buttonManualEventSearchByGUID.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonManualEventSearchByGUID.FlatAppearance.BorderSize = 2;
            this.buttonManualEventSearchByGUID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonManualEventSearchByGUID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonManualEventSearchByGUID.ForeColor = System.Drawing.Color.Transparent;
            this.buttonManualEventSearchByGUID.Image = global::kppApp.Properties.Resources.zoomer;
            this.buttonManualEventSearchByGUID.Location = new System.Drawing.Point(275, 147);
            this.buttonManualEventSearchByGUID.Margin = new System.Windows.Forms.Padding(1);
            this.buttonManualEventSearchByGUID.Name = "buttonManualEventSearchByGUID";
            this.buttonManualEventSearchByGUID.Size = new System.Drawing.Size(25, 25);
            this.buttonManualEventSearchByGUID.TabIndex = 61;
            this.buttonManualEventSearchByGUID.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonManualEventSearchByGUID.UseVisualStyleBackColor = true;
            this.buttonManualEventSearchByGUID.Click += new System.EventHandler(this.buttonManualEventSearchByGUID_Click);
            // 
            // buttonManualEventSearchByFIO
            // 
            this.buttonManualEventSearchByFIO.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonManualEventSearchByFIO.FlatAppearance.BorderSize = 2;
            this.buttonManualEventSearchByFIO.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonManualEventSearchByFIO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonManualEventSearchByFIO.ForeColor = System.Drawing.Color.Transparent;
            this.buttonManualEventSearchByFIO.Image = global::kppApp.Properties.Resources.zoomer;
            this.buttonManualEventSearchByFIO.Location = new System.Drawing.Point(275, 93);
            this.buttonManualEventSearchByFIO.Margin = new System.Windows.Forms.Padding(1);
            this.buttonManualEventSearchByFIO.Name = "buttonManualEventSearchByFIO";
            this.buttonManualEventSearchByFIO.Size = new System.Drawing.Size(25, 25);
            this.buttonManualEventSearchByFIO.TabIndex = 61;
            this.buttonManualEventSearchByFIO.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonManualEventSearchByFIO.UseVisualStyleBackColor = true;
            this.buttonManualEventSearchByFIO.Click += new System.EventHandler(this.buttonManualEventSearchByFIO_Click);
            // 
            // editManualEventComment
            // 
            this.editManualEventComment.BackColor = System.Drawing.Color.White;
            this.editManualEventComment.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editManualEventComment.ForeColor = System.Drawing.Color.Black;
            this.editManualEventComment.Location = new System.Drawing.Point(20, 253);
            this.editManualEventComment.Margin = new System.Windows.Forms.Padding(1);
            this.editManualEventComment.Name = "editManualEventComment";
            this.editManualEventComment.Size = new System.Drawing.Size(275, 24);
            this.editManualEventComment.TabIndex = 57;
            // 
            // editManualEventCard
            // 
            this.editManualEventCard.BackColor = System.Drawing.Color.White;
            this.editManualEventCard.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editManualEventCard.Location = new System.Drawing.Point(20, 40);
            this.editManualEventCard.Margin = new System.Windows.Forms.Padding(1);
            this.editManualEventCard.Name = "editManualEventCard";
            this.editManualEventCard.Size = new System.Drawing.Size(251, 24);
            this.editManualEventCard.TabIndex = 28;
            this.editManualEventCard.TextChanged += new System.EventHandler(this.editManualEventCard_TextChanged);
            this.editManualEventCard.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editManualEventCard_KeyUp);
            this.editManualEventCard.Leave += new System.EventHandler(this.editManualEventCard_Leave);
            // 
            // editManualEventFIO
            // 
            this.editManualEventFIO.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editManualEventFIO.Location = new System.Drawing.Point(20, 93);
            this.editManualEventFIO.Margin = new System.Windows.Forms.Padding(1);
            this.editManualEventFIO.Name = "editManualEventFIO";
            this.editManualEventFIO.Size = new System.Drawing.Size(251, 24);
            this.editManualEventFIO.TabIndex = 33;
            this.editManualEventFIO.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editManualEventFIO_KeyUp);
            this.editManualEventFIO.Leave += new System.EventHandler(this.editManualEventFIO_Leave);
            // 
            // editManualEventGUID
            // 
            this.editManualEventGUID.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editManualEventGUID.Location = new System.Drawing.Point(20, 147);
            this.editManualEventGUID.Margin = new System.Windows.Forms.Padding(1);
            this.editManualEventGUID.Name = "editManualEventGUID";
            this.editManualEventGUID.Size = new System.Drawing.Size(251, 24);
            this.editManualEventGUID.TabIndex = 31;
            this.editManualEventGUID.TextChanged += new System.EventHandler(this.editManualEventGUID_TextChanged);
            this.editManualEventGUID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editManualEventGUID_KeyUp);
            this.editManualEventGUID.Leave += new System.EventHandler(this.editManualEventGUID_Leave);
            // 
            // buttonCancelManualEvent
            // 
            this.buttonCancelManualEvent.BackColor = System.Drawing.Color.White;
            this.buttonCancelManualEvent.FlatAppearance.BorderSize = 0;
            this.buttonCancelManualEvent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancelManualEvent.Font = new System.Drawing.Font("Roboto", 10F);
            this.buttonCancelManualEvent.ForeColor = System.Drawing.Color.Black;
            this.buttonCancelManualEvent.Location = new System.Drawing.Point(22, 363);
            this.buttonCancelManualEvent.Margin = new System.Windows.Forms.Padding(1);
            this.buttonCancelManualEvent.Name = "buttonCancelManualEvent";
            this.buttonCancelManualEvent.Size = new System.Drawing.Size(127, 28);
            this.buttonCancelManualEvent.TabIndex = 47;
            this.buttonCancelManualEvent.Text = "Отмена";
            this.buttonCancelManualEvent.UseVisualStyleBackColor = false;
            this.buttonCancelManualEvent.Click += new System.EventHandler(this.buttonCancelManualEvent_Click);
            // 
            // buttonOKManualEvent
            // 
            this.buttonOKManualEvent.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonOKManualEvent.Enabled = false;
            this.buttonOKManualEvent.FlatAppearance.BorderSize = 0;
            this.buttonOKManualEvent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOKManualEvent.Font = new System.Drawing.Font("Roboto", 10F);
            this.buttonOKManualEvent.ForeColor = System.Drawing.Color.White;
            this.buttonOKManualEvent.Location = new System.Drawing.Point(168, 363);
            this.buttonOKManualEvent.Margin = new System.Windows.Forms.Padding(1);
            this.buttonOKManualEvent.Name = "buttonOKManualEvent";
            this.buttonOKManualEvent.Size = new System.Drawing.Size(127, 28);
            this.buttonOKManualEvent.TabIndex = 48;
            this.buttonOKManualEvent.Text = "Создать";
            this.buttonOKManualEvent.UseVisualStyleBackColor = false;
            this.buttonOKManualEvent.Click += new System.EventHandler(this.buttonOKManualEvent_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(176, 255);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 14);
            this.label4.TabIndex = 34;
            this.label4.Text = "ФИО";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label44.Location = new System.Drawing.Point(17, 21);
            this.label44.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(103, 18);
            this.label44.TabIndex = 58;
            this.label44.Text = "Номер карты";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label43.Location = new System.Drawing.Point(17, 75);
            this.label43.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(39, 18);
            this.label43.TabIndex = 58;
            this.label43.Text = "ФИО";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label42.Location = new System.Drawing.Point(17, 129);
            this.label42.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(42, 18);
            this.label42.TabIndex = 58;
            this.label42.Text = "GUID";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label41.Location = new System.Drawing.Point(17, 181);
            this.label41.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(97, 18);
            this.label41.TabIndex = 58;
            this.label41.Text = "Тип события";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(17, 234);
            this.label7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 18);
            this.label7.TabIndex = 58;
            this.label7.Text = "Комментарий";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabPage5.Controls.Add(this.labelRedOperation);
            this.tabPage5.Controls.Add(this.panel9);
            this.tabPage5.Controls.Add(this.labelRedEventID);
            this.tabPage5.Controls.Add(this.panel8);
            this.tabPage5.Location = new System.Drawing.Point(4, 104);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(1);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(1);
            this.tabPage5.Size = new System.Drawing.Size(1292, 695);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Красная";
            // 
            // labelRedOperation
            // 
            this.labelRedOperation.AutoSize = true;
            this.labelRedOperation.Location = new System.Drawing.Point(266, 446);
            this.labelRedOperation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRedOperation.Name = "labelRedOperation";
            this.labelRedOperation.Size = new System.Drawing.Size(112, 15);
            this.labelRedOperation.TabIndex = 83;
            this.labelRedOperation.Text = "labelRedOperation";
            this.labelRedOperation.Visible = false;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.Controls.Add(this.label28);
            this.panel9.Controls.Add(this.label29);
            this.panel9.Controls.Add(this.label30);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(1, 1);
            this.panel9.Margin = new System.Windows.Forms.Padding(1);
            this.panel9.MaximumSize = new System.Drawing.Size(0, 41);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1290, 25);
            this.panel9.TabIndex = 35;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(139)))), ((int)(((byte)(137)))));
            this.label28.Location = new System.Drawing.Point(320, 1);
            this.label28.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(207, 28);
            this.label28.TabIndex = 14;
            this.label28.Text = "Cервис контроля доступа";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.Location = new System.Drawing.Point(59, 0);
            this.label29.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(300, 27);
            this.label29.TabIndex = 0;
            this.label29.Text = "Редактирование события";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.Yellow;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(667, 0);
            this.label30.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(22, 28);
            this.label30.TabIndex = 13;
            this.label30.Text = "⚠";
            this.label30.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label30.Visible = false;
            // 
            // labelRedEventID
            // 
            this.labelRedEventID.AutoSize = true;
            this.labelRedEventID.Location = new System.Drawing.Point(180, 445);
            this.labelRedEventID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRedEventID.Name = "labelRedEventID";
            this.labelRedEventID.Size = new System.Drawing.Size(101, 15);
            this.labelRedEventID.TabIndex = 82;
            this.labelRedEventID.Text = "labelRedEventID";
            this.labelRedEventID.Visible = false;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.comboRedEventOperation);
            this.panel8.Controls.Add(this.editRedEventComment);
            this.panel8.Controls.Add(this.editRedEventCard);
            this.panel8.Controls.Add(this.editRedEventFIO);
            this.panel8.Controls.Add(this.editRedEventGUID);
            this.panel8.Controls.Add(this.buttonCancelRedEvent);
            this.panel8.Controls.Add(this.buttonOkRedEvent);
            this.panel8.Controls.Add(this.label20);
            this.panel8.Controls.Add(this.label21);
            this.panel8.Controls.Add(this.label22);
            this.panel8.Controls.Add(this.label25);
            this.panel8.Controls.Add(this.label26);
            this.panel8.Location = new System.Drawing.Point(65, 45);
            this.panel8.Margin = new System.Windows.Forms.Padding(1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(325, 413);
            this.panel8.TabIndex = 33;
            // 
            // comboRedEventOperation
            // 
            this.comboRedEventOperation.BackColor = System.Drawing.Color.White;
            this.comboRedEventOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRedEventOperation.DropDownWidth = 450;
            this.comboRedEventOperation.Font = new System.Drawing.Font("Roboto", 10F);
            this.comboRedEventOperation.IntegralHeight = false;
            this.comboRedEventOperation.ItemHeight = 15;
            this.comboRedEventOperation.Location = new System.Drawing.Point(20, 199);
            this.comboRedEventOperation.MaxDropDownItems = 30;
            this.comboRedEventOperation.Name = "comboRedEventOperation";
            this.comboRedEventOperation.Size = new System.Drawing.Size(275, 23);
            this.comboRedEventOperation.TabIndex = 75;
            this.comboRedEventOperation.TabStop = false;
            // 
            // editRedEventComment
            // 
            this.editRedEventComment.BackColor = System.Drawing.Color.White;
            this.editRedEventComment.Font = new System.Drawing.Font("Roboto", 10F);
            this.editRedEventComment.ForeColor = System.Drawing.Color.Black;
            this.editRedEventComment.Location = new System.Drawing.Point(20, 252);
            this.editRedEventComment.Margin = new System.Windows.Forms.Padding(1);
            this.editRedEventComment.Name = "editRedEventComment";
            this.editRedEventComment.Size = new System.Drawing.Size(275, 24);
            this.editRedEventComment.TabIndex = 69;
            this.editRedEventComment.TextChanged += new System.EventHandler(this.editRedEventComment_TextChanged);
            // 
            // editRedEventCard
            // 
            this.editRedEventCard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.editRedEventCard.Enabled = false;
            this.editRedEventCard.Font = new System.Drawing.Font("Roboto", 10F);
            this.editRedEventCard.Location = new System.Drawing.Point(20, 40);
            this.editRedEventCard.Margin = new System.Windows.Forms.Padding(1);
            this.editRedEventCard.Name = "editRedEventCard";
            this.editRedEventCard.Size = new System.Drawing.Size(275, 24);
            this.editRedEventCard.TabIndex = 64;
            // 
            // editRedEventFIO
            // 
            this.editRedEventFIO.BackColor = System.Drawing.Color.WhiteSmoke;
            this.editRedEventFIO.Enabled = false;
            this.editRedEventFIO.Font = new System.Drawing.Font("Roboto", 10F);
            this.editRedEventFIO.Location = new System.Drawing.Point(20, 92);
            this.editRedEventFIO.Margin = new System.Windows.Forms.Padding(1);
            this.editRedEventFIO.Name = "editRedEventFIO";
            this.editRedEventFIO.Size = new System.Drawing.Size(274, 24);
            this.editRedEventFIO.TabIndex = 66;
            // 
            // editRedEventGUID
            // 
            this.editRedEventGUID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.editRedEventGUID.Enabled = false;
            this.editRedEventGUID.Font = new System.Drawing.Font("Roboto", 10F);
            this.editRedEventGUID.Location = new System.Drawing.Point(20, 147);
            this.editRedEventGUID.Margin = new System.Windows.Forms.Padding(1);
            this.editRedEventGUID.Name = "editRedEventGUID";
            this.editRedEventGUID.Size = new System.Drawing.Size(274, 24);
            this.editRedEventGUID.TabIndex = 65;
            // 
            // buttonCancelRedEvent
            // 
            this.buttonCancelRedEvent.BackColor = System.Drawing.Color.White;
            this.buttonCancelRedEvent.FlatAppearance.BorderSize = 0;
            this.buttonCancelRedEvent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancelRedEvent.Font = new System.Drawing.Font("Roboto", 10F);
            this.buttonCancelRedEvent.ForeColor = System.Drawing.Color.Black;
            this.buttonCancelRedEvent.Location = new System.Drawing.Point(22, 363);
            this.buttonCancelRedEvent.Margin = new System.Windows.Forms.Padding(1);
            this.buttonCancelRedEvent.Name = "buttonCancelRedEvent";
            this.buttonCancelRedEvent.Size = new System.Drawing.Size(127, 28);
            this.buttonCancelRedEvent.TabIndex = 67;
            this.buttonCancelRedEvent.Text = "Отмена";
            this.buttonCancelRedEvent.UseVisualStyleBackColor = false;
            this.buttonCancelRedEvent.Click += new System.EventHandler(this.buttonCancelRedEvent_Click);
            // 
            // buttonOkRedEvent
            // 
            this.buttonOkRedEvent.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonOkRedEvent.FlatAppearance.BorderSize = 0;
            this.buttonOkRedEvent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOkRedEvent.Font = new System.Drawing.Font("Roboto", 10F);
            this.buttonOkRedEvent.ForeColor = System.Drawing.Color.White;
            this.buttonOkRedEvent.Location = new System.Drawing.Point(168, 363);
            this.buttonOkRedEvent.Margin = new System.Windows.Forms.Padding(1);
            this.buttonOkRedEvent.Name = "buttonOkRedEvent";
            this.buttonOkRedEvent.Size = new System.Drawing.Size(127, 28);
            this.buttonOkRedEvent.TabIndex = 68;
            this.buttonOkRedEvent.Text = "Сохранить";
            this.buttonOkRedEvent.UseVisualStyleBackColor = false;
            this.buttonOkRedEvent.Click += new System.EventHandler(this.buttonOkRedEvent_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label20.Location = new System.Drawing.Point(17, 21);
            this.label20.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(103, 18);
            this.label20.TabIndex = 70;
            this.label20.Text = "Номер карты";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label21.Location = new System.Drawing.Point(17, 75);
            this.label21.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 18);
            this.label21.TabIndex = 71;
            this.label21.Text = "ФИО";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label22.Location = new System.Drawing.Point(17, 128);
            this.label22.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(42, 18);
            this.label22.TabIndex = 72;
            this.label22.Text = "GUID";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label25.Location = new System.Drawing.Point(17, 181);
            this.label25.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(97, 18);
            this.label25.TabIndex = 73;
            this.label25.Text = "Тип события";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label26.Location = new System.Drawing.Point(17, 234);
            this.label26.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(104, 18);
            this.label26.TabIndex = 74;
            this.label26.Text = "Комментарий";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.tabPage4.Controls.Add(this.boxmethod2);
            this.tabPage4.Controls.Add(this.methodbox);
            this.tabPage4.Controls.Add(this.hostbox);
            this.tabPage4.Controls.Add(this.passwordbox);
            this.tabPage4.Controls.Add(this.loginbox);
            this.tabPage4.Controls.Add(this.panel7);
            this.tabPage4.Controls.Add(this.panel6);
            this.tabPage4.Location = new System.Drawing.Point(4, 104);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(1);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(1);
            this.tabPage4.Size = new System.Drawing.Size(1292, 695);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "Зеленая";
            // 
            // boxmethod2
            // 
            this.boxmethod2.Location = new System.Drawing.Point(453, 563);
            this.boxmethod2.Margin = new System.Windows.Forms.Padding(2);
            this.boxmethod2.Name = "boxmethod2";
            this.boxmethod2.Size = new System.Drawing.Size(187, 23);
            this.boxmethod2.TabIndex = 39;
            this.boxmethod2.Text = "/perco/v1/reading-event";
            this.boxmethod2.Visible = false;
            // 
            // methodbox
            // 
            this.methodbox.Location = new System.Drawing.Point(453, 525);
            this.methodbox.Margin = new System.Windows.Forms.Padding(2);
            this.methodbox.Name = "methodbox";
            this.methodbox.Size = new System.Drawing.Size(187, 23);
            this.methodbox.TabIndex = 38;
            this.methodbox.Text = "/perco/v1/auth/login/";
            this.methodbox.Visible = false;
            // 
            // hostbox
            // 
            this.hostbox.Location = new System.Drawing.Point(453, 479);
            this.hostbox.Margin = new System.Windows.Forms.Padding(2);
            this.hostbox.Name = "hostbox";
            this.hostbox.Size = new System.Drawing.Size(187, 23);
            this.hostbox.TabIndex = 37;
            this.hostbox.Text = "https://api.dev.checkin.polyus.com";
            this.hostbox.Visible = false;
            // 
            // passwordbox
            // 
            this.passwordbox.Location = new System.Drawing.Point(11, 549);
            this.passwordbox.Margin = new System.Windows.Forms.Padding(2);
            this.passwordbox.Name = "passwordbox";
            this.passwordbox.Size = new System.Drawing.Size(187, 23);
            this.passwordbox.TabIndex = 36;
            this.passwordbox.Text = "fxtaS2gVu";
            this.passwordbox.Visible = false;
            // 
            // loginbox
            // 
            this.loginbox.Location = new System.Drawing.Point(11, 503);
            this.loginbox.Margin = new System.Windows.Forms.Padding(2);
            this.loginbox.Name = "loginbox";
            this.loginbox.Size = new System.Drawing.Size(187, 23);
            this.loginbox.TabIndex = 35;
            this.loginbox.Text = "ALL-AS-CHIN-Perco";
            this.loginbox.Visible = false;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Controls.Add(this.label23);
            this.panel7.Controls.Add(this.label24);
            this.panel7.Controls.Add(this.label27);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(1, 1);
            this.panel7.Margin = new System.Windows.Forms.Padding(1);
            this.panel7.MaximumSize = new System.Drawing.Size(0, 41);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1290, 25);
            this.panel7.TabIndex = 34;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(139)))), ((int)(((byte)(137)))));
            this.label23.Location = new System.Drawing.Point(305, 2);
            this.label23.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(207, 28);
            this.label23.TabIndex = 14;
            this.label23.Text = "Cервис контроля доступа";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label24.Location = new System.Drawing.Point(59, 0);
            this.label24.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(254, 27);
            this.label24.TabIndex = 0;
            this.label24.Text = "Редактирование события";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Yellow;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(667, 0);
            this.label27.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(22, 28);
            this.label27.TabIndex = 13;
            this.label27.Text = "⚠";
            this.label27.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label27.Visible = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.buttonPOST);
            this.panel6.Controls.Add(this.editGreenEventTabnom);
            this.panel6.Controls.Add(this.labelGreenOperation);
            this.panel6.Controls.Add(this.labelGreenTabnom);
            this.panel6.Controls.Add(this.labelGreenEventID);
            this.panel6.Controls.Add(this.hintsGreenEventCard);
            this.panel6.Controls.Add(this.hintsGreenEventFIO);
            this.panel6.Controls.Add(this.hintsGreenEventGUID);
            this.panel6.Controls.Add(this.comboGreenEventOperation);
            this.panel6.Controls.Add(this.buttonGreenEventSearchByCard);
            this.panel6.Controls.Add(this.buttonGreenEventSearchByGUID);
            this.panel6.Controls.Add(this.buttonGreenEventSearchByFIO);
            this.panel6.Controls.Add(this.editGreenEventComment);
            this.panel6.Controls.Add(this.editGreenEventCard);
            this.panel6.Controls.Add(this.editGreenEventFIO);
            this.panel6.Controls.Add(this.editGreenEventGUID);
            this.panel6.Controls.Add(this.buttonCancelGreenEvent);
            this.panel6.Controls.Add(this.buttonOkGreenEvent);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label18);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.lvGreenEventSearch);
            this.panel6.Controls.Add(this.buttonDeleteGreenEvent);
            this.panel6.Location = new System.Drawing.Point(21, 45);
            this.panel6.Margin = new System.Windows.Forms.Padding(1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1046, 413);
            this.panel6.TabIndex = 33;
            // 
            // buttonPOST
            // 
            this.buttonPOST.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPOST.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPOST.Location = new System.Drawing.Point(23, 288);
            this.buttonPOST.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPOST.Name = "buttonPOST";
            this.buttonPOST.Size = new System.Drawing.Size(273, 25);
            this.buttonPOST.TabIndex = 88;
            this.buttonPOST.Text = "Отправить принудительно";
            this.buttonPOST.UseVisualStyleBackColor = true;
            this.buttonPOST.Visible = false;
            this.buttonPOST.Click += new System.EventHandler(this.buttonPOST_Click);
            // 
            // editGreenEventTabnom
            // 
            this.editGreenEventTabnom.AccessibleName = "";
            this.editGreenEventTabnom.Location = new System.Drawing.Point(145, 396);
            this.editGreenEventTabnom.Margin = new System.Windows.Forms.Padding(2);
            this.editGreenEventTabnom.Maximum = new decimal(new int[] {
            276447231,
            23283,
            0,
            0});
            this.editGreenEventTabnom.Name = "editGreenEventTabnom";
            this.editGreenEventTabnom.Size = new System.Drawing.Size(35, 23);
            this.editGreenEventTabnom.TabIndex = 87;
            this.editGreenEventTabnom.Visible = false;
            // 
            // labelGreenOperation
            // 
            this.labelGreenOperation.AutoSize = true;
            this.labelGreenOperation.Location = new System.Drawing.Point(241, 400);
            this.labelGreenOperation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelGreenOperation.Name = "labelGreenOperation";
            this.labelGreenOperation.Size = new System.Drawing.Size(125, 15);
            this.labelGreenOperation.TabIndex = 86;
            this.labelGreenOperation.Text = "labelGreenOperation";
            this.labelGreenOperation.Visible = false;
            // 
            // labelGreenTabnom
            // 
            this.labelGreenTabnom.AutoSize = true;
            this.labelGreenTabnom.Location = new System.Drawing.Point(20, 347);
            this.labelGreenTabnom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelGreenTabnom.Name = "labelGreenTabnom";
            this.labelGreenTabnom.Size = new System.Drawing.Size(116, 15);
            this.labelGreenTabnom.TabIndex = 85;
            this.labelGreenTabnom.Text = "labelGreenTabnom";
            this.labelGreenTabnom.Visible = false;
            // 
            // labelGreenEventID
            // 
            this.labelGreenEventID.AutoSize = true;
            this.labelGreenEventID.Location = new System.Drawing.Point(20, 400);
            this.labelGreenEventID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelGreenEventID.Name = "labelGreenEventID";
            this.labelGreenEventID.Size = new System.Drawing.Size(114, 15);
            this.labelGreenEventID.TabIndex = 84;
            this.labelGreenEventID.Text = "labelGreenEventID";
            this.labelGreenEventID.Visible = false;
            // 
            // hintsGreenEventCard
            // 
            this.hintsGreenEventCard.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hintsGreenEventCard.FormattingEnabled = true;
            this.hintsGreenEventCard.ItemHeight = 15;
            this.hintsGreenEventCard.Location = new System.Drawing.Point(31, 62);
            this.hintsGreenEventCard.Margin = new System.Windows.Forms.Padding(2);
            this.hintsGreenEventCard.MinimumSize = new System.Drawing.Size(4, 94);
            this.hintsGreenEventCard.Name = "hintsGreenEventCard";
            this.hintsGreenEventCard.Size = new System.Drawing.Size(251, 94);
            this.hintsGreenEventCard.TabIndex = 79;
            this.hintsGreenEventCard.Visible = false;
            this.hintsGreenEventCard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hintsGreenEventCard_MouseClick);
            this.hintsGreenEventCard.MouseLeave += new System.EventHandler(this.hintsGreenEventCard_MouseLeave);
            this.hintsGreenEventCard.MouseHover += new System.EventHandler(this.hintsGreenEventCard_MouseHover);
            // 
            // hintsGreenEventFIO
            // 
            this.hintsGreenEventFIO.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hintsGreenEventFIO.FormattingEnabled = true;
            this.hintsGreenEventFIO.ItemHeight = 15;
            this.hintsGreenEventFIO.Location = new System.Drawing.Point(31, 115);
            this.hintsGreenEventFIO.Margin = new System.Windows.Forms.Padding(2);
            this.hintsGreenEventFIO.MinimumSize = new System.Drawing.Size(4, 94);
            this.hintsGreenEventFIO.Name = "hintsGreenEventFIO";
            this.hintsGreenEventFIO.Size = new System.Drawing.Size(251, 94);
            this.hintsGreenEventFIO.TabIndex = 80;
            this.hintsGreenEventFIO.Visible = false;
            this.hintsGreenEventFIO.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hintsGreenEventFIO_MouseClick);
            this.hintsGreenEventFIO.MouseLeave += new System.EventHandler(this.hintsGreenEventFIO_MouseLeave);
            this.hintsGreenEventFIO.MouseHover += new System.EventHandler(this.hintsGreenEventFIO_MouseHover);
            // 
            // hintsGreenEventGUID
            // 
            this.hintsGreenEventGUID.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hintsGreenEventGUID.FormattingEnabled = true;
            this.hintsGreenEventGUID.ItemHeight = 15;
            this.hintsGreenEventGUID.Location = new System.Drawing.Point(31, 169);
            this.hintsGreenEventGUID.Margin = new System.Windows.Forms.Padding(2);
            this.hintsGreenEventGUID.MinimumSize = new System.Drawing.Size(4, 94);
            this.hintsGreenEventGUID.Name = "hintsGreenEventGUID";
            this.hintsGreenEventGUID.Size = new System.Drawing.Size(251, 94);
            this.hintsGreenEventGUID.TabIndex = 81;
            this.hintsGreenEventGUID.Visible = false;
            this.hintsGreenEventGUID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hintsGreenEventGUID_MouseClick);
            this.hintsGreenEventGUID.MouseLeave += new System.EventHandler(this.hintsGreenEventGUID_MouseLeave);
            this.hintsGreenEventGUID.MouseHover += new System.EventHandler(this.hintsGreenEventGUID_MouseHover);
            // 
            // comboGreenEventOperation
            // 
            this.comboGreenEventOperation.BackColor = System.Drawing.Color.White;
            this.comboGreenEventOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGreenEventOperation.DropDownWidth = 450;
            this.comboGreenEventOperation.Font = new System.Drawing.Font("Roboto", 10F);
            this.comboGreenEventOperation.IntegralHeight = false;
            this.comboGreenEventOperation.ItemHeight = 15;
            this.comboGreenEventOperation.Location = new System.Drawing.Point(20, 198);
            this.comboGreenEventOperation.MaxDropDownItems = 30;
            this.comboGreenEventOperation.Name = "comboGreenEventOperation";
            this.comboGreenEventOperation.Size = new System.Drawing.Size(275, 23);
            this.comboGreenEventOperation.TabIndex = 78;
            this.comboGreenEventOperation.TabStop = false;
            // 
            // buttonGreenEventSearchByCard
            // 
            this.buttonGreenEventSearchByCard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonGreenEventSearchByCard.FlatAppearance.BorderSize = 2;
            this.buttonGreenEventSearchByCard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonGreenEventSearchByCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGreenEventSearchByCard.ForeColor = System.Drawing.Color.Transparent;
            this.buttonGreenEventSearchByCard.Image = global::kppApp.Properties.Resources.zoomer;
            this.buttonGreenEventSearchByCard.Location = new System.Drawing.Point(275, 39);
            this.buttonGreenEventSearchByCard.Margin = new System.Windows.Forms.Padding(1);
            this.buttonGreenEventSearchByCard.Name = "buttonGreenEventSearchByCard";
            this.buttonGreenEventSearchByCard.Size = new System.Drawing.Size(25, 25);
            this.buttonGreenEventSearchByCard.TabIndex = 75;
            this.buttonGreenEventSearchByCard.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonGreenEventSearchByCard.UseVisualStyleBackColor = true;
            this.buttonGreenEventSearchByCard.Click += new System.EventHandler(this.buttonGreenEventSearchByCard_Click);
            // 
            // buttonGreenEventSearchByGUID
            // 
            this.buttonGreenEventSearchByGUID.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonGreenEventSearchByGUID.FlatAppearance.BorderSize = 2;
            this.buttonGreenEventSearchByGUID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonGreenEventSearchByGUID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGreenEventSearchByGUID.ForeColor = System.Drawing.Color.Transparent;
            this.buttonGreenEventSearchByGUID.Image = global::kppApp.Properties.Resources.zoomer;
            this.buttonGreenEventSearchByGUID.Location = new System.Drawing.Point(275, 144);
            this.buttonGreenEventSearchByGUID.Margin = new System.Windows.Forms.Padding(1);
            this.buttonGreenEventSearchByGUID.Name = "buttonGreenEventSearchByGUID";
            this.buttonGreenEventSearchByGUID.Size = new System.Drawing.Size(25, 25);
            this.buttonGreenEventSearchByGUID.TabIndex = 76;
            this.buttonGreenEventSearchByGUID.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonGreenEventSearchByGUID.UseVisualStyleBackColor = true;
            this.buttonGreenEventSearchByGUID.Click += new System.EventHandler(this.buttonGreenEventSearchByGUID_Click);
            // 
            // buttonGreenEventSearchByFIO
            // 
            this.buttonGreenEventSearchByFIO.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonGreenEventSearchByFIO.FlatAppearance.BorderSize = 2;
            this.buttonGreenEventSearchByFIO.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonGreenEventSearchByFIO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGreenEventSearchByFIO.ForeColor = System.Drawing.Color.Transparent;
            this.buttonGreenEventSearchByFIO.Image = global::kppApp.Properties.Resources.zoomer;
            this.buttonGreenEventSearchByFIO.Location = new System.Drawing.Point(275, 89);
            this.buttonGreenEventSearchByFIO.Margin = new System.Windows.Forms.Padding(1);
            this.buttonGreenEventSearchByFIO.Name = "buttonGreenEventSearchByFIO";
            this.buttonGreenEventSearchByFIO.Size = new System.Drawing.Size(25, 25);
            this.buttonGreenEventSearchByFIO.TabIndex = 77;
            this.buttonGreenEventSearchByFIO.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonGreenEventSearchByFIO.UseVisualStyleBackColor = true;
            this.buttonGreenEventSearchByFIO.Click += new System.EventHandler(this.buttonGreenEventSearchByFIO_Click);
            // 
            // editGreenEventComment
            // 
            this.editGreenEventComment.BackColor = System.Drawing.Color.White;
            this.editGreenEventComment.Font = new System.Drawing.Font("Roboto", 10F);
            this.editGreenEventComment.ForeColor = System.Drawing.Color.Black;
            this.editGreenEventComment.Location = new System.Drawing.Point(20, 251);
            this.editGreenEventComment.Margin = new System.Windows.Forms.Padding(1);
            this.editGreenEventComment.Name = "editGreenEventComment";
            this.editGreenEventComment.Size = new System.Drawing.Size(275, 24);
            this.editGreenEventComment.TabIndex = 69;
            // 
            // editGreenEventCard
            // 
            this.editGreenEventCard.BackColor = System.Drawing.Color.White;
            this.editGreenEventCard.Font = new System.Drawing.Font("Roboto", 10F);
            this.editGreenEventCard.Location = new System.Drawing.Point(20, 39);
            this.editGreenEventCard.Margin = new System.Windows.Forms.Padding(1);
            this.editGreenEventCard.Name = "editGreenEventCard";
            this.editGreenEventCard.Size = new System.Drawing.Size(251, 24);
            this.editGreenEventCard.TabIndex = 64;
            this.editGreenEventCard.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editGreenEventCard_KeyUp);
            this.editGreenEventCard.Leave += new System.EventHandler(this.editGreenEventCard_Leave);
            // 
            // editGreenEventFIO
            // 
            this.editGreenEventFIO.Font = new System.Drawing.Font("Roboto", 10F);
            this.editGreenEventFIO.Location = new System.Drawing.Point(20, 89);
            this.editGreenEventFIO.Margin = new System.Windows.Forms.Padding(1);
            this.editGreenEventFIO.Name = "editGreenEventFIO";
            this.editGreenEventFIO.Size = new System.Drawing.Size(251, 24);
            this.editGreenEventFIO.TabIndex = 66;
            this.editGreenEventFIO.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editGreenEventFIO_KeyUp);
            this.editGreenEventFIO.Leave += new System.EventHandler(this.editGreenEventFIO_Leave);
            // 
            // editGreenEventGUID
            // 
            this.editGreenEventGUID.Font = new System.Drawing.Font("Roboto", 10F);
            this.editGreenEventGUID.Location = new System.Drawing.Point(20, 144);
            this.editGreenEventGUID.Margin = new System.Windows.Forms.Padding(1);
            this.editGreenEventGUID.Name = "editGreenEventGUID";
            this.editGreenEventGUID.Size = new System.Drawing.Size(251, 24);
            this.editGreenEventGUID.TabIndex = 65;
            this.editGreenEventGUID.TextChanged += new System.EventHandler(this.editGreenEventGUID_TextChanged);
            this.editGreenEventGUID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editGreenEventGUID_KeyUp);
            this.editGreenEventGUID.Leave += new System.EventHandler(this.editGreenEventGUID_Leave);
            // 
            // buttonCancelGreenEvent
            // 
            this.buttonCancelGreenEvent.BackColor = System.Drawing.Color.White;
            this.buttonCancelGreenEvent.FlatAppearance.BorderSize = 0;
            this.buttonCancelGreenEvent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancelGreenEvent.Font = new System.Drawing.Font("Roboto", 10F);
            this.buttonCancelGreenEvent.ForeColor = System.Drawing.Color.Black;
            this.buttonCancelGreenEvent.Location = new System.Drawing.Point(22, 363);
            this.buttonCancelGreenEvent.Margin = new System.Windows.Forms.Padding(1);
            this.buttonCancelGreenEvent.Name = "buttonCancelGreenEvent";
            this.buttonCancelGreenEvent.Size = new System.Drawing.Size(83, 28);
            this.buttonCancelGreenEvent.TabIndex = 67;
            this.buttonCancelGreenEvent.Text = "Отмена";
            this.buttonCancelGreenEvent.UseVisualStyleBackColor = false;
            this.buttonCancelGreenEvent.Click += new System.EventHandler(this.buttonCancelGreenEvent_Click);
            // 
            // buttonOkGreenEvent
            // 
            this.buttonOkGreenEvent.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonOkGreenEvent.Enabled = false;
            this.buttonOkGreenEvent.FlatAppearance.BorderSize = 0;
            this.buttonOkGreenEvent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOkGreenEvent.Font = new System.Drawing.Font("Roboto", 10F);
            this.buttonOkGreenEvent.ForeColor = System.Drawing.Color.White;
            this.buttonOkGreenEvent.Location = new System.Drawing.Point(121, 363);
            this.buttonOkGreenEvent.Margin = new System.Windows.Forms.Padding(1);
            this.buttonOkGreenEvent.Name = "buttonOkGreenEvent";
            this.buttonOkGreenEvent.Size = new System.Drawing.Size(173, 28);
            this.buttonOkGreenEvent.TabIndex = 68;
            this.buttonOkGreenEvent.Text = "Сохранить изменения";
            this.buttonOkGreenEvent.UseVisualStyleBackColor = false;
            this.buttonOkGreenEvent.Click += new System.EventHandler(this.buttonOkGreenEvent_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(17, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 18);
            this.label2.TabIndex = 70;
            this.label2.Text = "Номер карты";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(17, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 18);
            this.label3.TabIndex = 71;
            this.label3.Text = "ФИО";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(17, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 18);
            this.label5.TabIndex = 72;
            this.label5.Text = "GUID";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label18.Location = new System.Drawing.Point(17, 181);
            this.label18.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(97, 18);
            this.label18.TabIndex = 73;
            this.label18.Text = "Тип события";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label19.Location = new System.Drawing.Point(17, 233);
            this.label19.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(104, 18);
            this.label19.TabIndex = 74;
            this.label19.Text = "Комментарий";
            // 
            // lvGreenEventSearch
            // 
            this.lvGreenEventSearch.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvGreenEventSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvGreenEventSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader23});
            this.lvGreenEventSearch.Font = new System.Drawing.Font("Roboto", 11F);
            this.lvGreenEventSearch.FullRowSelect = true;
            this.lvGreenEventSearch.GridLines = true;
            this.lvGreenEventSearch.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGreenEventSearch.HideSelection = false;
            this.lvGreenEventSearch.Location = new System.Drawing.Point(313, 21);
            this.lvGreenEventSearch.Margin = new System.Windows.Forms.Padding(1);
            this.lvGreenEventSearch.MultiSelect = false;
            this.lvGreenEventSearch.Name = "lvGreenEventSearch";
            this.lvGreenEventSearch.ShowGroups = false;
            this.lvGreenEventSearch.Size = new System.Drawing.Size(710, 372);
            this.lvGreenEventSearch.SmallImageList = this.imageList2;
            this.lvGreenEventSearch.TabIndex = 50;
            this.lvGreenEventSearch.UseCompatibleStateImageBehavior = false;
            this.lvGreenEventSearch.View = System.Windows.Forms.View.Details;
            this.lvGreenEventSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvGreenEventSearch_MouseDoubleClick);
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "№ карты";
            this.columnHeader19.Width = 128;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "Таб.номер";
            this.columnHeader20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader20.Width = 127;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "GUID";
            this.columnHeader21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader21.Width = 130;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "ФИО";
            this.columnHeader23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader23.Width = 322;
            // 
            // buttonDeleteGreenEvent
            // 
            this.buttonDeleteGreenEvent.BackColor = System.Drawing.Color.White;
            this.buttonDeleteGreenEvent.FlatAppearance.BorderSize = 0;
            this.buttonDeleteGreenEvent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDeleteGreenEvent.Font = new System.Drawing.Font("Roboto", 10F);
            this.buttonDeleteGreenEvent.ForeColor = System.Drawing.Color.Black;
            this.buttonDeleteGreenEvent.Location = new System.Drawing.Point(23, 319);
            this.buttonDeleteGreenEvent.Margin = new System.Windows.Forms.Padding(1);
            this.buttonDeleteGreenEvent.Name = "buttonDeleteGreenEvent";
            this.buttonDeleteGreenEvent.Size = new System.Drawing.Size(273, 28);
            this.buttonDeleteGreenEvent.TabIndex = 45;
            this.buttonDeleteGreenEvent.Text = "Удалить запись";
            this.buttonDeleteGreenEvent.UseVisualStyleBackColor = false;
            this.buttonDeleteGreenEvent.Click += new System.EventHandler(this.buttonDeleteGreenEvent_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.panel20);
            this.tabPage7.Location = new System.Drawing.Point(4, 104);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage7.Size = new System.Drawing.Size(1292, 695);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Загр справ";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.operCheck);
            this.panel20.Controls.Add(this.peopleCheck);
            this.panel20.Controls.Add(this.blockingBox);
            this.panel20.Controls.Add(this.label46);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel20.Location = new System.Drawing.Point(2, 2);
            this.panel20.MinimumSize = new System.Drawing.Size(0, 300);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(1288, 300);
            this.panel20.TabIndex = 7;
            // 
            // operCheck
            // 
            this.operCheck.AutoSize = true;
            this.operCheck.Enabled = false;
            this.operCheck.Location = new System.Drawing.Point(321, 16);
            this.operCheck.Name = "operCheck";
            this.operCheck.Size = new System.Drawing.Size(85, 19);
            this.operCheck.TabIndex = 20;
            this.operCheck.Text = "Операции";
            this.operCheck.UseVisualStyleBackColor = true;
            // 
            // peopleCheck
            // 
            this.peopleCheck.AutoSize = true;
            this.peopleCheck.Enabled = false;
            this.peopleCheck.Location = new System.Drawing.Point(237, 16);
            this.peopleCheck.Name = "peopleCheck";
            this.peopleCheck.Size = new System.Drawing.Size(78, 19);
            this.peopleCheck.TabIndex = 19;
            this.peopleCheck.Text = "Физлица";
            this.peopleCheck.UseVisualStyleBackColor = true;
            // 
            // blockingBox
            // 
            this.blockingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockingBox.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.blockingBox.Location = new System.Drawing.Point(0, 45);
            this.blockingBox.Margin = new System.Windows.Forms.Padding(2);
            this.blockingBox.Multiline = true;
            this.blockingBox.Name = "blockingBox";
            this.blockingBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.blockingBox.Size = new System.Drawing.Size(1288, 255);
            this.blockingBox.TabIndex = 18;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.Transparent;
            this.label46.Dock = System.Windows.Forms.DockStyle.Top;
            this.label46.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label46.Location = new System.Drawing.Point(0, 0);
            this.label46.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1288, 45);
            this.label46.TabIndex = 17;
            this.label46.Text = "Загрузка справочников";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // usb
            // 
            this.usb.ProductId = 21172;
            this.usb.VendorId = 0;
            this.usb.OnSpecifiedDeviceArrived += new System.EventHandler(this.usb_OnSpecifiedDeviceArrived);
            this.usb.OnSpecifiedDeviceRemoved += new System.EventHandler(this.usb_OnSpecifiedDeviceRemoved);
            this.usb.OnDeviceArrived += new System.EventHandler(this.usb_OnDeviceArrived);
            this.usb.OnDeviceRemoved += new System.EventHandler(this.usb_OnDeviceRemoved);
            this.usb.OnDataRecieved += new UsbLibrary.DataRecievedEventHandler(this.usb_OnDataRecieved);
            // 
            // threadEraser30
            // 
            this.threadEraser30.DoWork += new System.ComponentModel.DoWorkEventHandler(this.threadEraser30_DoWork);
            this.threadEraser30.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.threadEraser30_RunWorkerCompleted);
            // 
            // timerPupdate
            // 
            this.timerPupdate.Enabled = true;
            this.timerPupdate.Interval = 900;
            this.timerPupdate.Tick += new System.EventHandler(this.timerPupdate_Tick);
            // 
            // threadPupdate
            // 
            this.threadPupdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.threadPupdate_DoWork);
            this.threadPupdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.threadPupdate_RunWorkerCompleted);
            // 
            // MainFormKPP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1524, 826);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "MainFormKPP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сервис контроля доступа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XForm1_FormClosing);
            this.Load += new System.EventHandler(this.XForm1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelSignal2.ResumeLayout(false);
            this.LayPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelSignal.ResumeLayout(false);
            this.panelSignal.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panelFilterSelect.ResumeLayout(false);
            this.panelFilterSelect.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.tabSubfilter.ResumeLayout(false);
            this.tabCard.ResumeLayout(false);
            this.tabCard.PerformLayout();
            this.tabTabnom.ResumeLayout(false);
            this.tabTabnom.PerformLayout();
            this.tabFIO.ResumeLayout(false);
            this.tabFIO.PerformLayout();
            this.tabOperation.ResumeLayout(false);
            this.tabOperation.PerformLayout();
            this.tabDelivery.ResumeLayout(false);
            this.tabDelivery.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editGreenEventTabnom)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.ComponentModel.BackgroundWorker threadWorkersUpdater;
        private System.ComponentModel.BackgroundWorker threadPassageSender;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel infotickLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel10;
        private System.Windows.Forms.ToolStripStatusLabel restapi_path_label;
        private System.Windows.Forms.ToolStripStatusLabel labelHostAccess;
        private System.Windows.Forms.Timer timerWorkersUpdate;
        private System.Windows.Forms.Timer timerWaitMode;
        private System.Windows.Forms.Timer timerPassageSender;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.Timer timerCol;
        private System.Windows.Forms.Timer timerEraser;

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button buttonBeRed;
        private System.Windows.Forms.Button buttonBeWhite;
        private System.Windows.Forms.Button startBtnSelect;
        private System.Windows.Forms.Button buttonBeGreen;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panelSignal;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelTPL;
        private System.Windows.Forms.TextBox tb_vendor;
        private System.Windows.Forms.TextBox tb_product;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelManualEventOperation;
        private System.Windows.Forms.Label labelManualEventCard;
        private System.Windows.Forms.ListBox hintsManualEventCard;
        private System.Windows.Forms.ListBox hintsManualEventFIO;
        private System.Windows.Forms.ListBox hintsManualEventGUID;
        private System.Windows.Forms.ListView lvManualEventSearch;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ComboBox comboManualEventOperation;
        private System.Windows.Forms.Button buttonManualEventSearchByCard;
        private System.Windows.Forms.Button buttonManualEventSearchByGUID;
        private System.Windows.Forms.Button buttonManualEventSearchByFIO;
        private System.Windows.Forms.TextBox editManualEventComment;
        private System.Windows.Forms.TextBox editManualEventCard;
        private System.Windows.Forms.TextBox editManualEventFIO;
        private System.Windows.Forms.TextBox editManualEventGUID;
        private System.Windows.Forms.Button buttonCancelManualEvent;
        private System.Windows.Forms.Button buttonOKManualEvent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label labelRedOperation;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label labelRedEventID;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ComboBox comboRedEventOperation;
        private System.Windows.Forms.TextBox editRedEventComment;
        private System.Windows.Forms.TextBox editRedEventCard;
        private System.Windows.Forms.TextBox editRedEventFIO;
        private System.Windows.Forms.TextBox editRedEventGUID;
        private System.Windows.Forms.Button buttonCancelRedEvent;
        private System.Windows.Forms.Button buttonOkRedEvent;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox boxmethod2;
        private System.Windows.Forms.TextBox methodbox;
        private System.Windows.Forms.TextBox hostbox;
        private System.Windows.Forms.TextBox passwordbox;
        private System.Windows.Forms.TextBox loginbox;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button buttonPOST;
        private System.Windows.Forms.NumericUpDown editGreenEventTabnom;
        private System.Windows.Forms.Label labelGreenOperation;
        private System.Windows.Forms.Label labelGreenTabnom;
        private System.Windows.Forms.Label labelGreenEventID;
        private System.Windows.Forms.ListBox hintsGreenEventCard;
        private System.Windows.Forms.ListBox hintsGreenEventFIO;
        private System.Windows.Forms.ListBox hintsGreenEventGUID;
        private System.Windows.Forms.ComboBox comboGreenEventOperation;
        private System.Windows.Forms.Button buttonGreenEventSearchByCard;
        private System.Windows.Forms.Button buttonGreenEventSearchByGUID;
        private System.Windows.Forms.Button buttonGreenEventSearchByFIO;
        private System.Windows.Forms.TextBox editGreenEventComment;
        private System.Windows.Forms.TextBox editGreenEventCard;
        private System.Windows.Forms.TextBox editGreenEventFIO;
        private System.Windows.Forms.TextBox editGreenEventGUID;
        private System.Windows.Forms.Button buttonCancelGreenEvent;
        private System.Windows.Forms.Button buttonOkGreenEvent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ListView lvGreenEventSearch;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader23;
        private System.Windows.Forms.Button buttonDeleteGreenEvent;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Panel panelFilterSelect;
        private System.Windows.Forms.Button buttonResetFilter;
        private System.Windows.Forms.Button buttonHistoryReload;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label labelSelectedEventsCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel12;
        private System.Windows.Forms.ToolStripStatusLabel ServiceLabel;
        private System.Windows.Forms.ToolStripStatusLabel ServiceStateLabel;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.TabControl tabSubfilter;
        private System.Windows.Forms.TabPage tabCard;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox cardTextSelect;
        private System.Windows.Forms.TabPage tabTabnom;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox tabnomTextSelect;
        private System.Windows.Forms.TabPage tabFIO;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox fioTextSelect;
        private System.Windows.Forms.TabPage tabOperation;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ComboBox comboBoxHistoryOperations;
        private System.Windows.Forms.TabPage tabDelivery;
        private System.Windows.Forms.RadioButton radioWait;
        private System.Windows.Forms.RadioButton radioDelivered;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker endPickerSelect;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker begPickerSelect;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.ListView listViewHistory;
        private System.Windows.Forms.ColumnHeader columnFailure;
        private System.Windows.Forms.ColumnHeader columnCard;
        private System.Windows.Forms.ColumnHeader columnTabnom;
        private System.Windows.Forms.ColumnHeader columnFIO;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.ColumnHeader columnOperation;
        private System.Windows.Forms.ColumnHeader columnManual;
        private System.Windows.Forms.ColumnHeader columnID;
        private System.Windows.Forms.ColumnHeader columnDelivery;
        private System.Windows.Forms.Label label45;
        private UsbLibrary.UsbHidPort usb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.ListView listViewHotBuffer;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnEventID;
        private System.Windows.Forms.ColumnHeader columnToDelete;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButtonDaily;
        private System.Windows.Forms.Label labelShomItem;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Button buttonMakeManual;
        private System.Windows.Forms.Button buttonMarkToDelete;
        private System.Windows.Forms.ComboBox comboBoxOperationsMain;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelEventCounter;
        private System.Windows.Forms.Button buttonCheckEvents;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panelSignal2;
        private System.Windows.Forms.TableLayoutPanel LayPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelEventName;
        private System.Windows.Forms.Label labelEventDate;
        private System.Windows.Forms.Label labelEventCard;
        private System.Windows.Forms.Label labelEventJobDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelEventUserguid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelEventFamOtc;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.CheckBox operCheck;
        private System.Windows.Forms.CheckBox peopleCheck;
        private System.Windows.Forms.TextBox blockingBox;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.ToolStripStatusLabel needRestartlabel;
        private System.Windows.Forms.Label labelManualTabnomKeeper;
        private System.Windows.Forms.Label label47;
        private System.ComponentModel.BackgroundWorker threadEraser30;
        private System.Windows.Forms.Timer timerPupdate;
        private System.ComponentModel.BackgroundWorker threadPupdate;
    }
}