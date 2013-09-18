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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nzo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pause = new System.Windows.Forms.DataGridViewButtonColumn();
            this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataRefresher
            // 
            this.dataRefresher.Enabled = true;
            this.dataRefresher.Interval = 1000;
            this.dataRefresher.Tick += new System.EventHandler(this.dataRefresher_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nzo_id,
            this.pause,
            this.filename,
            this.size,
            this.timeleft,
            this.category,
            this.priority,
            this.script,
            this.delete});
            this.dataGridView1.Location = new System.Drawing.Point(2, 58);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(630, 228);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
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
            this.currentProgressBar.Location = new System.Drawing.Point(12, 24);
            this.currentProgressBar.Name = "currentProgressBar";
            this.currentProgressBar.Size = new System.Drawing.Size(367, 23);
            this.currentProgressBar.TabIndex = 1;
            // 
            // btnPauseMain
            // 
            this.btnPauseMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPauseMain.Location = new System.Drawing.Point(547, 31);
            this.btnPauseMain.Name = "btnPauseMain";
            this.btnPauseMain.Size = new System.Drawing.Size(75, 23);
            this.btnPauseMain.TabIndex = 2;
            this.btnPauseMain.Text = "Pause";
            this.btnPauseMain.UseVisualStyleBackColor = true;
            this.btnPauseMain.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Location = new System.Drawing.Point(385, 29);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(27, 13);
            this.lblPercentage.TabIndex = 3;
            this.lblPercentage.Text = "00%";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 289);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(634, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // versionLink
            // 
            this.versionLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.versionLink.AutoSize = true;
            this.versionLink.Location = new System.Drawing.Point(587, 9);
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
            this.numericUpDown1.Location = new System.Drawing.Point(476, 32);
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
            this.lblSpeedLimit.AutoSize = true;
            this.lblSpeedLimit.Location = new System.Drawing.Point(453, 16);
            this.lblSpeedLimit.Name = "lblSpeedLimit";
            this.lblSpeedLimit.Size = new System.Drawing.Size(88, 13);
            this.lblSpeedLimit.TabIndex = 7;
            this.lblSpeedLimit.Text = "Speed limit: none";
            // 
            // lblLastError
            // 
            this.lblLastError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLastError.AutoSize = true;
            this.lblLastError.Location = new System.Drawing.Point(2, 295);
            this.lblLastError.Name = "lblLastError";
            this.lblLastError.Size = new System.Drawing.Size(0, 13);
            this.lblLastError.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 311);
            this.Controls.Add(this.lblLastError);
            this.Controls.Add(this.lblSpeedLimit);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.versionLink);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblPercentage);
            this.Controls.Add(this.btnPauseMain);
            this.Controls.Add(this.currentProgressBar);
            this.Controls.Add(this.dataGridView1);
            this.MinimumSize = new System.Drawing.Size(650, 350);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Icon = Resources.green_down_arrow_hi;

        }

        #endregion

        private System.Windows.Forms.Timer dataRefresher;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar currentProgressBar;
        private System.Windows.Forms.Button btnPauseMain;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nzo_id;
        private System.Windows.Forms.DataGridViewButtonColumn pause;
        private System.Windows.Forms.DataGridViewTextBoxColumn filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn size;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeleft;
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn priority;
        private System.Windows.Forms.DataGridViewTextBoxColumn script;
        private System.Windows.Forms.DataGridViewButtonColumn delete;
        private System.Windows.Forms.LinkLabel versionLink;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label lblSpeedLimit;
        private System.Windows.Forms.Label lblLastError;
    }
}

