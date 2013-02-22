namespace SandBox.TestConnection
{
    partial class CommandForm
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
            this.btnExit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnEventReport = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTarget = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbObject = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbActId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbModId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEnvId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_1_mac = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_1_ip = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_1_type = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_1_id = new System.Windows.Forms.TextBox();
            this.btnReadyReport = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(699, 305);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExitClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(762, 287);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnReadyReport);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.tb_1_mac);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.tb_1_ip);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.tb_1_type);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.tb_1_id);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(754, 261);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Отчет о готовности";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnEventReport);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.tbTarget);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tbObject);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.tbActId);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.tbModId);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.tbEnvId);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(754, 261);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Отчет о событии";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnEventReport
            // 
            this.btnEventReport.Location = new System.Drawing.Point(92, 143);
            this.btnEventReport.Name = "btnEventReport";
            this.btnEventReport.Size = new System.Drawing.Size(75, 23);
            this.btnEventReport.TabIndex = 10;
            this.btnEventReport.Text = "Send";
            this.btnEventReport.UseVisualStyleBackColor = true;
            this.btnEventReport.Click += new System.EventHandler(this.BtnEventReportClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "target:";
            // 
            // tbTarget
            // 
            this.tbTarget.Location = new System.Drawing.Point(50, 117);
            this.tbTarget.Name = "tbTarget";
            this.tbTarget.Size = new System.Drawing.Size(117, 20);
            this.tbTarget.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "object:";
            // 
            // tbObject
            // 
            this.tbObject.Location = new System.Drawing.Point(50, 91);
            this.tbObject.Name = "tbObject";
            this.tbObject.Size = new System.Drawing.Size(117, 20);
            this.tbObject.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "actId:";
            // 
            // tbActId
            // 
            this.tbActId.Location = new System.Drawing.Point(50, 65);
            this.tbActId.Name = "tbActId";
            this.tbActId.Size = new System.Drawing.Size(117, 20);
            this.tbActId.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "modId:";
            // 
            // tbModId
            // 
            this.tbModId.Location = new System.Drawing.Point(50, 39);
            this.tbModId.Name = "tbModId";
            this.tbModId.Size = new System.Drawing.Size(117, 20);
            this.tbModId.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "envId:";
            // 
            // tbEnvId
            // 
            this.tbEnvId.Location = new System.Drawing.Point(50, 13);
            this.tbEnvId.Name = "tbEnvId";
            this.tbEnvId.Size = new System.Drawing.Size(117, 20);
            this.tbEnvId.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "mac:";
            // 
            // tb_1_mac
            // 
            this.tb_1_mac.Location = new System.Drawing.Point(49, 88);
            this.tb_1_mac.Name = "tb_1_mac";
            this.tb_1_mac.Size = new System.Drawing.Size(117, 20);
            this.tb_1_mac.TabIndex = 14;
            this.tb_1_mac.Text = "09-08-07-06-05-04";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "ip:";
            // 
            // tb_1_ip
            // 
            this.tb_1_ip.Location = new System.Drawing.Point(49, 62);
            this.tb_1_ip.Name = "tb_1_ip";
            this.tb_1_ip.Size = new System.Drawing.Size(117, 20);
            this.tb_1_ip.TabIndex = 12;
            this.tb_1_ip.Text = "10.10.22.64";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "type:";
            // 
            // tb_1_type
            // 
            this.tb_1_type.Location = new System.Drawing.Point(49, 36);
            this.tb_1_type.Name = "tb_1_type";
            this.tb_1_type.Size = new System.Drawing.Size(117, 20);
            this.tb_1_type.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "id:";
            // 
            // tb_1_id
            // 
            this.tb_1_id.Location = new System.Drawing.Point(49, 10);
            this.tb_1_id.Name = "tb_1_id";
            this.tb_1_id.Size = new System.Drawing.Size(117, 20);
            this.tb_1_id.TabIndex = 8;
            // 
            // btnReadyReport
            // 
            this.btnReadyReport.Location = new System.Drawing.Point(91, 114);
            this.btnReadyReport.Name = "btnReadyReport";
            this.btnReadyReport.Size = new System.Drawing.Size(75, 23);
            this.btnReadyReport.TabIndex = 16;
            this.btnReadyReport.Text = "Send";
            this.btnReadyReport.UseVisualStyleBackColor = true;
            this.btnReadyReport.Click += new System.EventHandler(this.BtnReadyReportClick);
            // 
            // CommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 336);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CommandForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Комманды клиенту";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbEnvId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbObject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbActId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbModId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEventReport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTarget;
        private System.Windows.Forms.Button btnReadyReport;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_1_mac;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_1_ip;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_1_type;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_1_id;
    }
}