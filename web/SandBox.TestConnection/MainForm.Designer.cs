namespace SandBox.TestConnection
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSendReport = new System.Windows.Forms.Button();
            this.tbServerIncoming = new System.Windows.Forms.TextBox();
            this.tbServerOutgoing = new System.Windows.Forms.TextBox();
            this.btnServerClear = new System.Windows.Forms.Button();
            this.lbServerLog = new System.Windows.Forms.ListBox();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbServerPort = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbClientIncoming = new System.Windows.Forms.TextBox();
            this.tbClientOutgoing = new System.Windows.Forms.TextBox();
            this.btnClientClear = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClientSend = new System.Windows.Forms.Button();
            this.tbClientMessage = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbClientPort = new System.Windows.Forms.NumericUpDown();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbClientLog = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.btnCheckTime = new System.Windows.Forms.Button();
            this.tbTime_2 = new System.Windows.Forms.TextBox();
            this.tbTime_1 = new System.Windows.Forms.TextBox();
            this.tbDb = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lLastStopRsch = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbServerPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbClientPort)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSendReport);
            this.groupBox1.Controls.Add(this.tbServerIncoming);
            this.groupBox1.Controls.Add(this.tbServerOutgoing);
            this.groupBox1.Controls.Add(this.btnServerClear);
            this.groupBox1.Controls.Add(this.lbServerLog);
            this.groupBox1.Controls.Add(this.btnStopServer);
            this.groupBox1.Controls.Add(this.btnStartServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbServerPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 411);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сервер";
            // 
            // btnSendReport
            // 
            this.btnSendReport.Location = new System.Drawing.Point(421, 100);
            this.btnSendReport.Name = "btnSendReport";
            this.btnSendReport.Size = new System.Drawing.Size(96, 23);
            this.btnSendReport.TabIndex = 8;
            this.btnSendReport.Text = "SendReport";
            this.btnSendReport.UseVisualStyleBackColor = true;
            this.btnSendReport.Click += new System.EventHandler(this.btnSendReport_Click);
            // 
            // tbServerIncoming
            // 
            this.tbServerIncoming.Enabled = false;
            this.tbServerIncoming.Location = new System.Drawing.Point(122, 74);
            this.tbServerIncoming.Name = "tbServerIncoming";
            this.tbServerIncoming.Size = new System.Drawing.Size(100, 20);
            this.tbServerIncoming.TabIndex = 7;
            // 
            // tbServerOutgoing
            // 
            this.tbServerOutgoing.Enabled = false;
            this.tbServerOutgoing.Location = new System.Drawing.Point(19, 74);
            this.tbServerOutgoing.Name = "tbServerOutgoing";
            this.tbServerOutgoing.Size = new System.Drawing.Size(100, 20);
            this.tbServerOutgoing.TabIndex = 6;
            // 
            // btnServerClear
            // 
            this.btnServerClear.Location = new System.Drawing.Point(330, 382);
            this.btnServerClear.Name = "btnServerClear";
            this.btnServerClear.Size = new System.Drawing.Size(85, 23);
            this.btnServerClear.TabIndex = 5;
            this.btnServerClear.Text = "Очистить";
            this.btnServerClear.UseVisualStyleBackColor = true;
            this.btnServerClear.Click += new System.EventHandler(this.BtnServerClearClick);
            // 
            // lbServerLog
            // 
            this.lbServerLog.FormattingEnabled = true;
            this.lbServerLog.HorizontalScrollbar = true;
            this.lbServerLog.Location = new System.Drawing.Point(19, 100);
            this.lbServerLog.Name = "lbServerLog";
            this.lbServerLog.Size = new System.Drawing.Size(396, 277);
            this.lbServerLog.TabIndex = 4;
            // 
            // btnStopServer
            // 
            this.btnStopServer.Location = new System.Drawing.Point(203, 19);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(75, 23);
            this.btnStopServer.TabIndex = 3;
            this.btnStopServer.Text = "Стоп";
            this.btnStopServer.UseVisualStyleBackColor = true;
            this.btnStopServer.Click += new System.EventHandler(this.BtnStopServerClick);
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(122, 19);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(75, 23);
            this.btnStartServer.TabIndex = 2;
            this.btnStartServer.Text = "Старт";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.BtnStartServerClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Порт:";
            // 
            // tbServerPort
            // 
            this.tbServerPort.Location = new System.Drawing.Point(57, 20);
            this.tbServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(59, 20);
            this.tbServerPort.TabIndex = 0;
            this.tbServerPort.Value = new decimal(new int[] {
            8001,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbClientIncoming);
            this.groupBox2.Controls.Add(this.tbClientOutgoing);
            this.groupBox2.Controls.Add(this.btnClientClear);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnClientSend);
            this.groupBox2.Controls.Add(this.tbClientMessage);
            this.groupBox2.Controls.Add(this.btnDisconnect);
            this.groupBox2.Controls.Add(this.btnConnect);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbClientPort);
            this.groupBox2.Controls.Add(this.tbHost);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lbClientLog);
            this.groupBox2.Location = new System.Drawing.Point(541, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 411);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Клиент";
            // 
            // tbClientIncoming
            // 
            this.tbClientIncoming.Enabled = false;
            this.tbClientIncoming.Location = new System.Drawing.Point(121, 74);
            this.tbClientIncoming.Name = "tbClientIncoming";
            this.tbClientIncoming.Size = new System.Drawing.Size(100, 20);
            this.tbClientIncoming.TabIndex = 17;
            // 
            // tbClientOutgoing
            // 
            this.tbClientOutgoing.Enabled = false;
            this.tbClientOutgoing.Location = new System.Drawing.Point(18, 74);
            this.tbClientOutgoing.Name = "tbClientOutgoing";
            this.tbClientOutgoing.Size = new System.Drawing.Size(100, 20);
            this.tbClientOutgoing.TabIndex = 16;
            // 
            // btnClientClear
            // 
            this.btnClientClear.Location = new System.Drawing.Point(430, 383);
            this.btnClientClear.Name = "btnClientClear";
            this.btnClientClear.Size = new System.Drawing.Size(75, 23);
            this.btnClientClear.TabIndex = 15;
            this.btnClientClear.Text = "Очистить";
            this.btnClientClear.UseVisualStyleBackColor = true;
            this.btnClientClear.Click += new System.EventHandler(this.BtnClientClearClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Сообщение:";
            // 
            // btnClientSend
            // 
            this.btnClientSend.Location = new System.Drawing.Point(430, 46);
            this.btnClientSend.Name = "btnClientSend";
            this.btnClientSend.Size = new System.Drawing.Size(75, 23);
            this.btnClientSend.TabIndex = 13;
            this.btnClientSend.Text = "Отправить";
            this.btnClientSend.UseVisualStyleBackColor = true;
            this.btnClientSend.Click += new System.EventHandler(this.BtnClientSendClick);
            // 
            // tbClientMessage
            // 
            this.tbClientMessage.Location = new System.Drawing.Point(92, 48);
            this.tbClientMessage.Name = "tbClientMessage";
            this.tbClientMessage.Size = new System.Drawing.Size(332, 20);
            this.tbClientMessage.TabIndex = 12;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(430, 17);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 11;
            this.btnDisconnect.Text = "Разорвать";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.BtnDisconnectClick);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(349, 17);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Соединить";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnectClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Порт:";
            // 
            // tbClientPort
            // 
            this.tbClientPort.Location = new System.Drawing.Point(284, 20);
            this.tbClientPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.tbClientPort.Name = "tbClientPort";
            this.tbClientPort.Size = new System.Drawing.Size(59, 20);
            this.tbClientPort.TabIndex = 8;
            this.tbClientPort.Value = new decimal(new int[] {
            8001,
            0,
            0,
            0});
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(92, 19);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(145, 20);
            this.tbHost.TabIndex = 7;
            this.tbHost.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Сервер:";
            // 
            // lbClientLog
            // 
            this.lbClientLog.FormattingEnabled = true;
            this.lbClientLog.HorizontalScrollbar = true;
            this.lbClientLog.Location = new System.Drawing.Point(18, 100);
            this.lbClientLog.Name = "lbClientLog";
            this.lbClientLog.Size = new System.Drawing.Size(487, 277);
            this.lbClientLog.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.labelTime);
            this.groupBox3.Controls.Add(this.btnCheckTime);
            this.groupBox3.Controls.Add(this.tbTime_2);
            this.groupBox3.Controls.Add(this.tbTime_1);
            this.groupBox3.Controls.Add(this.tbDb);
            this.groupBox3.Location = new System.Drawing.Point(1070, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(143, 411);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "БД";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(7, 182);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(50, 13);
            this.labelTime.TabIndex = 4;
            this.labelTime.Text = "Прошло:";
            // 
            // btnCheckTime
            // 
            this.btnCheckTime.Location = new System.Drawing.Point(7, 205);
            this.btnCheckTime.Name = "btnCheckTime";
            this.btnCheckTime.Size = new System.Drawing.Size(130, 23);
            this.btnCheckTime.TabIndex = 3;
            this.btnCheckTime.Text = "Время";
            this.btnCheckTime.UseVisualStyleBackColor = true;
            this.btnCheckTime.Click += new System.EventHandler(this.BtnCheckTimeClick);
            // 
            // tbTime_2
            // 
            this.tbTime_2.Enabled = false;
            this.tbTime_2.Location = new System.Drawing.Point(7, 155);
            this.tbTime_2.Name = "tbTime_2";
            this.tbTime_2.Size = new System.Drawing.Size(130, 20);
            this.tbTime_2.TabIndex = 2;
            // 
            // tbTime_1
            // 
            this.tbTime_1.Enabled = false;
            this.tbTime_1.Location = new System.Drawing.Point(7, 128);
            this.tbTime_1.Name = "tbTime_1";
            this.tbTime_1.Size = new System.Drawing.Size(130, 20);
            this.tbTime_1.TabIndex = 1;
            // 
            // tbDb
            // 
            this.tbDb.Enabled = false;
            this.tbDb.Location = new System.Drawing.Point(7, 14);
            this.tbDb.Name = "tbDb";
            this.tbDb.Size = new System.Drawing.Size(130, 20);
            this.tbDb.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lLastStopRsch);
            this.groupBox4.Location = new System.Drawing.Point(10, 234);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(127, 171);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Монитиор остановки";
            // 
            // lLastStopRsch
            // 
            this.lLastStopRsch.Location = new System.Drawing.Point(6, 19);
            this.lLastStopRsch.Name = "lLastStopRsch";
            this.lLastStopRsch.Size = new System.Drawing.Size(0, 13);
            this.lLastStopRsch.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 435);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "TestConnection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbServerPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbClientPort)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbServerLog;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown tbServerPort;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown tbClientPort;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbClientLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClientSend;
        private System.Windows.Forms.TextBox tbClientMessage;
        private System.Windows.Forms.Button btnServerClear;
        private System.Windows.Forms.Button btnClientClear;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbDb;
        private System.Windows.Forms.TextBox tbServerIncoming;
        private System.Windows.Forms.TextBox tbServerOutgoing;
        private System.Windows.Forms.TextBox tbClientIncoming;
        private System.Windows.Forms.TextBox tbClientOutgoing;
        private System.Windows.Forms.Button btnSendReport;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Button btnCheckTime;
        private System.Windows.Forms.TextBox tbTime_2;
        private System.Windows.Forms.TextBox tbTime_1;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.LabelControl lLastStopRsch;
        private System.Windows.Forms.Timer timer1;
    }
}

