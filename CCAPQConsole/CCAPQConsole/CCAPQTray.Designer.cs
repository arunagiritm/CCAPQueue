namespace CCAPQConsole
{
    partial class CCAPQTray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCAPQTray));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.QueueStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.localQueueToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemInQueue = new System.Windows.Forms.ToolStripMenuItem();
            this.InProgressMenuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.CompletedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QueueStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Right click to check the Queue status";
            this.notifyIcon1.BalloonTipTitle = "CCAP Queue Status";
            this.notifyIcon1.ContextMenuStrip = this.QueueStatus;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "CCAPQueueStatus";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // QueueStatus
            // 
            this.QueueStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localQueueToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItemInQueue,
            this.InProgressMenuitem,
            this.CompletedMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.QueueStatus.Name = "QueueStatus";
            this.QueueStatus.Size = new System.Drawing.Size(213, 129);
            // 
            // localQueueToolStripMenuItem
            // 
            this.localQueueToolStripMenuItem.Items.AddRange(new object[] {
            "Local Queue",
            "Server Queue"});
            this.localQueueToolStripMenuItem.Name = "localQueueToolStripMenuItem";
            this.localQueueToolStripMenuItem.Size = new System.Drawing.Size(152, 21);
            this.localQueueToolStripMenuItem.Text = "Local Queue";
            this.localQueueToolStripMenuItem.SelectedIndexChanged += new System.EventHandler(this.localQueueToolStripMenuItem_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(209, 6);
            // 
            // toolStripMenuItemInQueue
            // 
            this.toolStripMenuItemInQueue.Name = "toolStripMenuItemInQueue";
            this.toolStripMenuItemInQueue.Size = new System.Drawing.Size(212, 22);
            this.toolStripMenuItemInQueue.Text = "In Queue";
            this.toolStripMenuItemInQueue.Click += new System.EventHandler(this.toolStripMenuItemInQueue_Click);
            // 
            // InProgressMenuitem
            // 
            this.InProgressMenuitem.Name = "InProgressMenuitem";
            this.InProgressMenuitem.Size = new System.Drawing.Size(212, 22);
            this.InProgressMenuitem.Text = "In Progress";
            this.InProgressMenuitem.Click += new System.EventHandler(this.InProgressMenuitem_Click);
            // 
            // CompletedMenuItem
            // 
            this.CompletedMenuItem.Name = "CompletedMenuItem";
            this.CompletedMenuItem.Size = new System.Drawing.Size(212, 22);
            this.CompletedMenuItem.Text = "Processed";
            this.CompletedMenuItem.Click += new System.EventHandler(this.CompletedMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(209, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // CCAPQTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "CCAPQTray";
            this.Text = "CCAPQTray";
            this.Load += new System.EventHandler(this.CCAPQTray_Load);
            this.Shown += new System.EventHandler(this.CCAPQTray_Shown);
            this.QueueStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip QueueStatus;
        private System.Windows.Forms.ToolStripMenuItem InProgressMenuitem;
        private System.Windows.Forms.ToolStripMenuItem CompletedMenuItem;
        private System.Windows.Forms.ToolStripComboBox localQueueToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInQueue;
    }
}