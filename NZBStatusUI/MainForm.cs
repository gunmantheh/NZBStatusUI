using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NZBStatus.DTOs;
using NZBStatusUI.Enums;
using NZBStatusUI.Properties;

namespace NZBStatusUI
{
    public partial class MainForm : Form
    {
        private IEnumerable<Slot> _queue;
        // private Slot _currentSlot;
        private readonly JsonReader _jsr;
        private readonly bool _noApiKey;
        public MainForm()
        {

            InitializeComponent();
            _noApiKey = false;
            var apikey = "";
            try
            {
                apikey = File.ReadAllText("apikey");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("couldn't find 'apikey' file in application folder", "No Api key", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _noApiKey = true;
            }
            finally
            {
                string[] args = { "http://localhost:8080", apikey };
                _jsr = Initialize(args);
                _queue = new List<Slot>();
            }

        }

        private JsonReader Initialize(string[] args)
        {
            var jsr = new JsonReader(args[0],
                                     args[1]);

            var culture = (CultureInfo)CultureInfo.CurrentUICulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            return jsr;
        }

        private void dataRefresher_Tick(object sender, EventArgs e)
        {
            if (_jsr != null)
            {
                GetAndFillData();
                RefreshUI();
            }
        }

        private void GetAndFillData()
        {
            if (_jsr.RefreshData())
            {
                // _currentSlot = _jsr.GetCurrentSlot();
                _queue = _jsr.GetAllSlots(); // .Where(x => x.index != 0);
            }
        }

        private void RefreshUI()
        {
            this.Text = String.Format("{0}{1} | {2} | {3}",
                _jsr.ConnectionStatus() != ConnectionStatus.Ok ? _jsr.ConnectionStatus() + " - " : string.Empty,
                _jsr.IsDownloading ? _jsr.Speed.SpeedToString() : _jsr.IsPaused ? Resources.MainForm_RefreshUI_PAUSED : Resources.MainForm_RefreshUI_IDLE,
                                          _jsr.MBLeft.SizeToString(),
                                          _jsr.ETA + " left");

            if (_noApiKey)
                this.Text = "No API key has been found";

            string percentageString = string.Format("{0}%", _jsr.TotalPercentage.ToString("D2"));

            statMainLabel.Text = this.Text;
            lblPercentage.Text = percentageString;
            statPercentage.Text = percentageString;
            statProgressBar.Value = _jsr.TotalPercentage;
            currentProgressBar.Value = _jsr.TotalPercentage;

            versionLink.Text = _jsr.Version;
            lblSpeedLimit.Text = string.Format("Speed limit: {0}", _jsr.SpeedLimit == 0 ? "none" : Convert.ToDecimal(_jsr.SpeedLimit).SpeedToString());

            btnPauseMain.Text = _jsr.IsPaused ? Resources.MainForm_RefreshUI_Resume : Resources.MainForm_RefreshUI_Pause;
            dataGridView1.Rows.Clear();
            if (_queue.Any())
            {
                foreach (var slot in _queue)
                {
                    var row = (DataGridViewRow)dataGridView1.RowTemplate.Clone();
                    var btnDelete = new DataGridViewButtonCell { Value = "Delete" };
                    var btnPause = new DataGridViewButtonCell { Value = slot.status == "Paused" ? "Resume" : "Pause" };
                    if (row != null)
                    {
                        row.CreateCells(dataGridView1, slot.nzo_id, Resources.MainForm_RefreshUI_Pause, slot.filename, slot.size, slot.timeleft, slot.cat, slot.priority, slot.script, "delete");
                        var dataGridViewColumn = dataGridView1.Columns[Resources.MainForm_RefreshUI_delete];
                        if (dataGridViewColumn != null)
                        {
                            row.Cells[dataGridViewColumn.Index] = btnDelete;
                            dataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        }

                        dataGridViewColumn = dataGridView1.Columns[Resources.MainForm_RefreshUI_Pause];
                        if (dataGridViewColumn != null)
                        {
                            row.Cells[dataGridViewColumn.Index] = btnPause;
                            dataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                        if (slot.status == "Paused")
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                        dataGridView1.Rows.Add(row);
                    }
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.green_down_arrow_hi;
            dataRefresher_Tick(sender, e);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            var status = _jsr.IsPaused ? _jsr.ResumeMain() : _jsr.PauseMain();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView != null)
                switch (dataGridView.Columns[e.ColumnIndex].Name)
                {
                    case "status":
                        MessageBox.Show("you just clicked status");
                        break;
                    case "delete":
                        if (e.RowIndex >= 0)
                        {
                            _jsr.Delete(dataGridView.Rows[e.RowIndex].Cells["nzo_id"].Value as string);
                        }
                        break;
                    case "pause":
                        if (e.RowIndex >= 0)
                        {
                            if (dataGridView.Rows[e.RowIndex].Cells["pause"].Value as string == "Resume")
                            {
                                _jsr.Resume(dataGridView.Rows[e.RowIndex].Cells["nzo_id"].Value as string);
                            }
                            else
                            {
                                _jsr.Pause(dataGridView.Rows[e.RowIndex].Cells["nzo_id"].Value as string);
                            }
                        }
                        break;
                }
        }

        private void versionLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://sabnzbd.org/download/");
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                var numericUpDown = sender as NumericUpDown;
                if (numericUpDown != null)
                    _jsr.SetSpeedLimit(Convert.ToInt32(string.IsNullOrEmpty(numericUpDown.Text) ? "0" : numericUpDown.Text));
            }
        }
    }
}
