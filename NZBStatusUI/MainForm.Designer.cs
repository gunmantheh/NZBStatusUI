using NZBStatusUI.DataGridViewProgress;
using NZBStatusUI.Properties;

namespace NZBStatusUI
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
            this.dataRefresher = new System.Windows.Forms.Timer(this.components);
            this.dgvCurrent = new System.Windows.Forms.DataGridView();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nzo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pause = new System.Windows.Forms.DataGridViewButtonColumn();
            this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progress = new NZBStatusUI.DataGridViewProgress.DataGridViewProgressColumn();
            this.timeleft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.script = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.currentProgressBar = new System.Windows.Forms.ProgressBar();
            this.btnPauseMain = new System.Windows.Forms.Button();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.versionLink = new System.Windows.Forms.LinkLabel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lblSpeedLimit = new System.Windows.Forms.Label();
            this.lblLastError = new System.Windows.Forms.Label();
            this.dataGridViewProgressColumn1 = new NZBStatusUI.DataGridViewProgress.DataGridViewProgressColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCurrent = new System.Windows.Forms.TabPage();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.historyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historynzo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historyname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historysize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historyscript = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historycategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historydownloadtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historydownloaded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabCurrent.SuspendLayout();
            this.tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dataRefresher
            // 
            this.dataRefresher.Enabled = true;
            this.dataRefresher.Interval = 1000;
            this.dataRefresher.Tick += new System.EventHandler(this.dataRefresher_Tick);
            // 
            // dgvCurrent
            // 
            this.dgvCurrent.AllowUserToAddRows = false;
            this.dgvCurrent.AllowUserToDeleteRows = false;
            this.dgvCurrent.AllowUserToResizeRows = false;
            this.dgvCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCurrent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCurrent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.nzo_id,
            this.pause,
            this.filename,
            this.size,
            this.progress,
            this.timeleft,
            this.category,
            this.priority,
            this.script,
            this.delete});
            this.dgvCurrent.Location = new System.Drawing.Point(0, 0);
            this.dgvCurrent.MultiSelect = false;
            this.dgvCurrent.Name = "dgvCurrent";
            this.dgvCurrent.ReadOnly = true;
            this.dgvCurrent.RowHeadersVisible = false;
            this.dgvCurrent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCurrent.Size = new System.Drawing.Size(729, 209);
            this.dgvCurrent.TabIndex = 0;
            this.dgvCurrent.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // index
            // 
            this.index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.index.HeaderText = "Index";
            this.index.Name = "index";
            this.index.ReadOnly = true;
            this.index.Width = 40;
            // 
            // nzo_id
            // 
            this.nzo_id.HeaderText = "nzo_id";
            this.nzo_id.Name = "nzo_id";
            this.nzo_id.ReadOnly = true;
            this.nzo_id.Visible = false;
            // 
            // pause
            // 
            this.pause.HeaderText = "Pause";
            this.pause.Name = "pause";
            this.pause.ReadOnly = true;
            // 
            // filename
            // 
            this.filename.HeaderText = "Filename";
            this.filename.Name = "filename";
            this.filename.ReadOnly = true;
            // 
            // size
            // 
            this.size.HeaderText = "Size";
            this.size.Name = "size";
            this.size.ReadOnly = true;
            // 
            // progress
            // 
            this.progress.HeaderText = "Progress [%]";
            this.progress.Name = "progress";
            this.progress.ProgressBarColor = System.Drawing.Color.Empty;
            this.progress.ReadOnly = true;
            // 
            // timeleft
            // 
            this.timeleft.HeaderText = "Time left";
            this.timeleft.Name = "timeleft";
            this.timeleft.ReadOnly = true;
            // 
            // category
            // 
            this.category.HeaderText = "Category";
            this.category.Name = "category";
            this.category.ReadOnly = true;
            this.category.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // priority
            // 
            this.priority.HeaderText = "Priority";
            this.priority.Name = "priority";
            this.priority.ReadOnly = true;
            // 
            // script
            // 
            this.script.HeaderText = "Script";
            this.script.Name = "script";
            this.script.ReadOnly = true;
            // 
            // delete
            // 
            this.delete.HeaderText = "Delete";
            this.delete.Name = "delete";
            this.delete.ReadOnly = true;
            // 
            // currentProgressBar
            // 
            this.currentProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentProgressBar.Location = new System.Drawing.Point(12, 24);
            this.currentProgressBar.Name = "currentProgressBar";
            this.currentProgressBar.Size = new System.Drawing.Size(367, 23);
            this.currentProgressBar.TabIndex = 1;
            // 
            // btnPauseMain
            // 
            this.btnPauseMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPauseMain.Location = new System.Drawing.Point(648, 31);
            this.btnPauseMain.Name = "btnPauseMain";
            this.btnPauseMain.Size = new System.Drawing.Size(75, 23);
            this.btnPauseMain.TabIndex = 2;
            this.btnPauseMain.Text = "Pause";
            this.btnPauseMain.UseVisualStyleBackColor = true;
            this.btnPauseMain.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // lblPercentage
            // 
            this.lblPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Location = new System.Drawing.Point(385, 29);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(21, 13);
            this.lblPercentage.TabIndex = 3;
            this.lblPercentage.Text = "0%";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 290);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(735, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // versionLink
            // 
            this.versionLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.versionLink.AutoSize = true;
            this.versionLink.Location = new System.Drawing.Point(688, 9);
            this.versionLink.Name = "versionLink";
            this.versionLink.Size = new System.Drawing.Size(35, 13);
            this.versionLink.TabIndex = 5;
            this.versionLink.TabStop = true;
            this.versionLink.Text = "Home";
            this.versionLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.versionLink_LinkClicked);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.InterceptArrowKeys = false;
            this.numericUpDown1.Location = new System.Drawing.Point(577, 32);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            102400,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown1_KeyPress);
            // 
            // lblSpeedLimit
            // 
            this.lblSpeedLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpeedLimit.AutoSize = true;
            this.lblSpeedLimit.Location = new System.Drawing.Point(554, 16);
            this.lblSpeedLimit.Name = "lblSpeedLimit";
            this.lblSpeedLimit.Size = new System.Drawing.Size(88, 13);
            this.lblSpeedLimit.TabIndex = 7;
            this.lblSpeedLimit.Text = "Speed limit: none";
            // 
            // lblLastError
            // 
            this.lblLastError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLastError.AutoSize = true;
            this.lblLastError.Location = new System.Drawing.Point(2, 296);
            this.lblLastError.Name = "lblLastError";
            this.lblLastError.Size = new System.Drawing.Size(0, 13);
            this.lblLastError.TabIndex = 8;
            // 
            // dataGridViewProgressColumn1
            // 
            this.dataGridViewProgressColumn1.HeaderText = "Progress [%]";
            this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
            this.dataGridViewProgressColumn1.ProgressBarColor = System.Drawing.Color.Empty;
            this.dataGridViewProgressColumn1.Width = 81;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabCurrent);
            this.tabControl1.Controls.Add(this.tabHistory);
            this.tabControl1.Location = new System.Drawing.Point(2, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(733, 235);
            this.tabControl1.TabIndex = 9;
            // 
            // tabCurrent
            // 
            this.tabCurrent.Controls.Add(this.dgvCurrent);
            this.tabCurrent.Location = new System.Drawing.Point(4, 22);
            this.tabCurrent.Name = "tabCurrent";
            this.tabCurrent.Padding = new System.Windows.Forms.Padding(3);
            this.tabCurrent.Size = new System.Drawing.Size(725, 209);
            this.tabCurrent.TabIndex = 0;
            this.tabCurrent.Text = "Current";
            this.tabCurrent.UseVisualStyleBackColor = true;
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.dgvHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(725, 209);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AllowUserToResizeRows = false;
            this.dgvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.historyID,
            this.historynzo_id,
            this.historyname,
            this.historysize,
            this.historyscript,
            this.historycategory,
            this.historydownloadtime,
            this.historydownloaded});
            this.dgvHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(729, 209);
            this.dgvHistory.TabIndex = 1;
            // 
            // historyID
            // 
            this.historyID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.historyID.HeaderText = "ID";
            this.historyID.Name = "historyID";
            this.historyID.ReadOnly = true;
            this.historyID.Width = 40;
            // 
            // historynzo_id
            // 
            this.historynzo_id.HeaderText = "nzo_id";
            this.historynzo_id.Name = "historynzo_id";
            this.historynzo_id.ReadOnly = true;
            this.historynzo_id.Visible = false;
            // 
            // historyname
            // 
            this.historyname.HeaderText = "Name";
            this.historyname.Name = "historyname";
            this.historyname.ReadOnly = true;
            // 
            // historysize
            // 
            this.historysize.HeaderText = "Size";
            this.historysize.Name = "historysize";
            this.historysize.ReadOnly = true;
            // 
            // historyscript
            // 
            this.historyscript.HeaderText = "Script";
            this.historyscript.Name = "historyscript";
            this.historyscript.ReadOnly = true;
            // 
            // historycategory
            // 
            this.historycategory.HeaderText = "Category";
            this.historycategory.Name = "historycategory";
            this.historycategory.ReadOnly = true;
            this.historycategory.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // historydownloadtime
            // 
            this.historydownloadtime.HeaderText = "Download Time";
            this.historydownloadtime.Name = "historydownloadtime";
            this.historydownloadtime.ReadOnly = true;
            // 
            // historydownloaded
            // 
            this.historydownloaded.HeaderText = "Downloaded";
            this.historydownloaded.Name = "historydownloaded";
            this.historydownloaded.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 312);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblLastError);
            this.Controls.Add(this.lblSpeedLimit);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.versionLink);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblPercentage);
            this.Controls.Add(this.btnPauseMain);
            this.Controls.Add(this.currentProgressBar);
            this.Icon = global::NZBStatusUI.Properties.Resources.green_down_arrow_hi;
            this.MinimumSize = new System.Drawing.Size(650, 350);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabCurrent.ResumeLayout(false);
            this.tabHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer dataRefresher;
        private System.Windows.Forms.DataGridView dgvCurrent;
        private System.Windows.Forms.ProgressBar currentProgressBar;
        private System.Windows.Forms.Button btnPauseMain;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.LinkLabel versionLink;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label lblSpeedLimit;
        private System.Windows.Forms.Label lblLastError;
        private DataGridViewProgressColumn dataGridViewProgressColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn nzo_id;
        private System.Windows.Forms.DataGridViewButtonColumn pause;
        private System.Windows.Forms.DataGridViewTextBoxColumn filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn size;
        private DataGridViewProgressColumn progress;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeleft;
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn priority;
        private System.Windows.Forms.DataGridViewTextBoxColumn script;
        private System.Windows.Forms.DataGridViewButtonColumn delete;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCurrent;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn historyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn historynzo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn historyname;
        private System.Windows.Forms.DataGridViewTextBoxColumn historysize;
        private System.Windows.Forms.DataGridViewTextBoxColumn historyscript;
        private System.Windows.Forms.DataGridViewTextBoxColumn historycategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn historydownloadtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn historydownloaded;
    }
}

