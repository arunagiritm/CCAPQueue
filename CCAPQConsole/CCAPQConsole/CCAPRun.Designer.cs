namespace CCAPQConsole
{
    partial class CCAPRun
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
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listBoxInQueue = new System.Windows.Forms.ListBox();
            this.listBoxInProgress = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelExecutedJobName = new System.Windows.Forms.Label();
            this.labelExecutedLogName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 262);
            this.richTextBoxLog.MaxLength = 0;
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(501, 157);
            this.richTextBoxLog.TabIndex = 8;
            this.richTextBoxLog.Text = "";
            // 
            // listBoxInQueue
            // 
            this.listBoxInQueue.FormattingEnabled = true;
            this.listBoxInQueue.Location = new System.Drawing.Point(12, 18);
            this.listBoxInQueue.Name = "listBoxInQueue";
            this.listBoxInQueue.Size = new System.Drawing.Size(161, 199);
            this.listBoxInQueue.TabIndex = 9;
            this.listBoxInQueue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listBoxInQueue_MouseMove);
            // 
            // listBoxInProgress
            // 
            this.listBoxInProgress.FormattingEnabled = true;
            this.listBoxInProgress.Location = new System.Drawing.Point(314, 18);
            this.listBoxInProgress.Name = "listBoxInProgress";
            this.listBoxInProgress.Size = new System.Drawing.Size(161, 199);
            this.listBoxInProgress.TabIndex = 10;
            this.listBoxInProgress.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listBoxInProgress_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Jobs in Queue";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Jobs in Progress";
            // 
            // labelExecutedJobName
            // 
            this.labelExecutedJobName.AutoSize = true;
            this.labelExecutedJobName.Location = new System.Drawing.Point(12, 224);
            this.labelExecutedJobName.Name = "labelExecutedJobName";
            this.labelExecutedJobName.Size = new System.Drawing.Size(55, 13);
            this.labelExecutedJobName.TabIndex = 13;
            this.labelExecutedJobName.Text = "Job Name";
            // 
            // labelExecutedLogName
            // 
            this.labelExecutedLogName.AutoSize = true;
            this.labelExecutedLogName.Location = new System.Drawing.Point(12, 244);
            this.labelExecutedLogName.Name = "labelExecutedLogName";
            this.labelExecutedLogName.Size = new System.Drawing.Size(56, 13);
            this.labelExecutedLogName.TabIndex = 14;
            this.labelExecutedLogName.Text = "Log Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(179, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Job Queue Location";
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.Items.AddRange(new object[] {
            "Local",
            "Server"});
            this.comboBoxLocation.Location = new System.Drawing.Point(182, 18);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(115, 21);
            this.comboBoxLocation.TabIndex = 18;
            this.comboBoxLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxLocation_SelectedIndexChanged);
            // 
            // CCAPRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 425);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxLocation);
            this.Controls.Add(this.labelExecutedLogName);
            this.Controls.Add(this.labelExecutedJobName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxInProgress);
            this.Controls.Add(this.listBoxInQueue);
            this.Controls.Add(this.richTextBoxLog);
            this.MaximizeBox = false;
            this.Name = "CCAPRun";
            this.Text = "CCAPRun";
            this.Load += new System.EventHandler(this.CCAPRun_Load);
            this.Shown += new System.EventHandler(this.CCAPRun_Shown);
            this.Resize += new System.EventHandler(this.CCAPRun_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListBox listBoxInQueue;
        private System.Windows.Forms.ListBox listBoxInProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelExecutedJobName;
        private System.Windows.Forms.Label labelExecutedLogName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxLocation;

    }
}