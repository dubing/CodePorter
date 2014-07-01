namespace CodePorter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnStartServer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClearLog = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.txtDBConnection = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.treeViewDB = new System.Windows.Forms.TreeView();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.statusStripDB = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelDB = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.txtDANameSpace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            this.statusStripDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStartServer,
            this.toolStripSeparator2,
            this.btnClearLog,
            this.toolStripSeparator1,
            this.btnHelp});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(608, 25);
            this.toolStrip.TabIndex = 11;
            this.toolStrip.Text = "ToolStrip";
            // 
            // btnStartServer
            // 
            this.btnStartServer.Image = ((System.Drawing.Image)(resources.GetObject("btnStartServer.Image")));
            this.btnStartServer.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(52, 22);
            this.btnStartServer.Text = "启动";
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Image = ((System.Drawing.Image)(resources.GetObject("btnClearLog.Image")));
            this.btnClearLog.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(52, 22);
            this.btnClearLog.Text = "清屏";
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnHelp
            // 
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(52, 22);
            this.btnHelp.Text = "帮助";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(503, 53);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 52);
            this.btnLoad.TabIndex = 26;
            this.btnLoad.Text = "加载";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(88, 111);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(199, 21);
            this.txtNameSpace.TabIndex = 25;
            this.txtNameSpace.Text = "Model.Service";
            // 
            // txtDBConnection
            // 
            this.txtDBConnection.Location = new System.Drawing.Point(88, 55);
            this.txtDBConnection.Multiline = true;
            this.txtDBConnection.Name = "txtDBConnection";
            this.txtDBConnection.Size = new System.Drawing.Size(392, 50);
            this.txtDBConnection.TabIndex = 24;
            this.txtDBConnection.Text = "Server=devdb.dev.sh.ctriptravel.com,28747;UID=uws_W_GrapeTravelMoney_dev;password" +
    "=Dyu3UR0bJBun;database=TravelMoneyDB;";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "实体命名空间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "数据库连接";
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(86, 32);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(657, 12);
            this.lblMessage.TabIndex = 21;
            this.lblMessage.Text = "NoMessage";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "提示信息";
            // 
            // treeViewDB
            // 
            this.treeViewDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewDB.CheckBoxes = true;
            this.treeViewDB.Location = new System.Drawing.Point(9, 172);
            this.treeViewDB.Name = "treeViewDB";
            this.treeViewDB.Size = new System.Drawing.Size(471, 343);
            this.treeViewDB.TabIndex = 19;
            this.treeViewDB.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDB_AfterCheck);
            // 
            // txtLog
            // 
            this.txtLog.AcceptsReturn = true;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Location = new System.Drawing.Point(0, 521);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(608, 78);
            this.txtLog.TabIndex = 27;
            // 
            // statusStripDB
            // 
            this.statusStripDB.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelDB});
            this.statusStripDB.Location = new System.Drawing.Point(0, 599);
            this.statusStripDB.Name = "statusStripDB";
            this.statusStripDB.Size = new System.Drawing.Size(608, 22);
            this.statusStripDB.TabIndex = 30;
            this.statusStripDB.Text = "statusStripDB";
            // 
            // toolStripStatusLabelDB
            // 
            this.toolStripStatusLabelDB.Name = "toolStripStatusLabelDB";
            this.toolStripStatusLabelDB.Size = new System.Drawing.Size(87, 17);
            this.toolStripStatusLabelDB.Text = "Please reload";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(503, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 60);
            this.button1.TabIndex = 31;
            this.button1.Text = " 启动";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(503, 257);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(75, 67);
            this.btnOpenFolder.TabIndex = 32;
            this.btnOpenFolder.Text = "打开目录";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // txtDANameSpace
            // 
            this.txtDANameSpace.Location = new System.Drawing.Point(88, 138);
            this.txtDANameSpace.Name = "txtDANameSpace";
            this.txtDANameSpace.Size = new System.Drawing.Size(199, 21);
            this.txtDANameSpace.TabIndex = 34;
            this.txtDANameSpace.Text = "DataAccess.Service";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "DA命名空间";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 621);
            this.Controls.Add(this.txtDANameSpace);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtNameSpace);
            this.Controls.Add(this.txtDBConnection);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.treeViewDB);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStripDB);
            this.Name = "MainForm";
            this.Text = "CodePorter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStripDB.ResumeLayout(false);
            this.statusStripDB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnStartServer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnClearLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.TextBox txtDBConnection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView treeViewDB;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.StatusStrip statusStripDB;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDB;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.TextBox txtDANameSpace;
        private System.Windows.Forms.Label label1;
    }
}