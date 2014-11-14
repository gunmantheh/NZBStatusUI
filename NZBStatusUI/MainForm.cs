using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Enums;
using JsonDataManipulator.Enums;
using JsonDataManipulator.Helpers;
using JsonDataManipulator.DTOs;
using Microsoft.WindowsAPICodePack.Taskbar;
using NZBStatusUI.DataGridViewProgress;
using NZBStatusUI.Helpers;
using NZBStatusUI.Properties;

namespace NZBStatusUI
{
    public partial class MainForm : Form
    {
        private const string NzoID = "nzo_id";
        private IEnumerable<Slot> _queue;
        private IEnumerable<History> _history;
        // private Slot _currentSlot;
        private readonly JsonDataManipulator _jsr;
        private readonly bool _noApiKey;
        private readonly bool _noServerFile;
        private HashSet<string> _currentList;
        private HashSet<string> _historyList;
        public MainForm()
        {

            InitializeComponent();
            _noApiKey = false;
            _noServerFile = false;
            _currentList = new HashSet<string>();
            _historyList = new HashSet<string>();
            var apikey = "";
            try
            {
                apikey = File.ReadAllText("apikey");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(Resources.MainForm_MainForm_Couldn_t_find__apikey__file_in_application_folder, Resources.MainForm_MainForm_No_Api_key, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _noApiKey = true;
            }
            var server = "";
            try
            {
                server = File.ReadAllText("server");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(Resources.MainForm_MainForm_Server_file_couldn_t_be_found, Resources.MainForm_MainForm_No_server_file, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _noServerFile = true;
            }
            string[] args = { server, apikey };
            _jsr = Initialize(args);
            _queue = new List<Slot>();
            _history = new List<History>();
        }

        private JsonDataManipulator Initialize(string[] args)
        {
            var jsr = new JsonDataManipulator(args[0],
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
                _queue = _jsr.GetAllSlots();
                _history = _jsr.GetHistory(0);
            }
        }

        private void RefreshUI()
        {
            Text = (!_jsr.IsDownloading && !_jsr.IsPaused) ? "[IDLE]" :
                String.Format("[{0}{1} | {2} | {3}]",
                _jsr.ConnectionStatus() != ConnectionStatus.Ok ? _jsr.ConnectionStatus() + " - " : string.Empty,
                _jsr.IsDownloading
                    ? _jsr.Speed.SpeedToString()
                    : _jsr.IsPaused ? Resources.MainForm_RefreshUI_PAUSED : Resources.MainForm_RefreshUI_IDLE,
                _jsr.TotalMBLeft.SizeToString(),
                _jsr.ETA + " left");

            if (_noApiKey)
                Text = Resources.MainForm_RefreshUI_No_API_key_has_been_found;
            if (_noServerFile)
                Text = Resources.MainForm_RefreshUI_No_server_file_has_been_found;
            if (_noApiKey && _noApiKey)
                Text = string.Format("{0} {1}", Resources.MainForm_RefreshUI_No_API_key_has_been_found,
                    Resources.MainForm_RefreshUI_No_server_file_has_been_found);


            lblPercentage.Text = string.Format("{0}%", _jsr.TotalPercentage);
            currentProgressBar.Value = _jsr.CurrentPercentage;

            try
            {
                if (_jsr.CurrentPercentage > 0)
                {
                    TaskbarManager.Instance.SetProgressState(_jsr.IsPaused ? TaskbarProgressBarState.Paused : TaskbarProgressBarState.Normal);
                    TaskbarManager.Instance.SetProgressValue(_jsr.CurrentPercentage, 100);
                }
                else
                {
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                }
            }
            catch (InvalidOperationException)
            {
            }

            lblLastError.Text = _jsr.GetLastError();

            versionLink.Text = _jsr.Version;
            lblSpeedLimit.Text = string.Format("Speed limit: {0}", _jsr.SpeedLimit == 0 ? "none" : Convert.ToDecimal(_jsr.SpeedLimit).SpeedToString());

            btnPauseMain.Text = _jsr.IsPaused ? Resources.MainForm_RefreshUI_Resume : Resources.MainForm_RefreshUI_Pause;
            _currentList.Clear();
            _historyList.Clear();

            FillCurrentTable();
            FillHistoryTable();
        }

        private void FillCurrentTable()
        {
            if (_queue.Any())
            {
                foreach (var slot in _queue)
                {
                    var row = (DataGridViewRow)dgvCurrent.RowTemplate.Clone();

                    // prgProgress.SetProgressBarColor(Color.FromKnownColor(KnownColor.Control));
                    if (row != null)
                    {
                        CreateRow(row, slot);
                    }
                    if (!dgvCurrent.ContainsValue(NzoID, slot.nzo_id))
                    {
                        if (row != null) dgvCurrent.Rows.Add(row);
                    }
                    else
                    {
                        int rowID = dgvCurrent.GetRowID(NzoID, slot.nzo_id);

                        //var kvCol = dgv1.Columns["category"] as DataGridViewComboBoxColumn;
                        //if (kvCol != null)
                        //{
                        //    var existingCategories = new List<string>(kvCol.Items.Cast<string>().ToList());
                        //    kvCol.
                        //    if (existingCategories.Except(_jsr.Categories()).Any())
                        //    {
                        //        kvCol.DataSource = _jsr.Categories();
                        //    }
                        //}

                        UpdateValue(dgvCurrent, rowID, "index", slot.index);
                        UpdateValue(dgvCurrent, rowID, "pause", GetStatus(slot));
                        UpdateValue(dgvCurrent, rowID, "filename", slot.filename);
                        UpdateValue(dgvCurrent, rowID, "size", slot.size);
                        UpdateValue(dgvCurrent, rowID, "progress", slot.percentage);
                        UpdateValue(dgvCurrent, rowID, "timeleft", slot.timeleft);
                        UpdateValue(dgvCurrent, rowID, "priority", slot.priority);
                        UpdateValue(dgvCurrent, rowID, "script", slot.script);
                        if ((string)dgvCurrent.Rows[rowID].Cells["category"].Value != slot.cat)
                        {
                            dgvCurrent.Rows[rowID].Cells["category"].Value = slot.cat;
                            dgvCurrent.Rows[rowID].Cells["category"].Style.BackColor = GetColorByCategory(slot.cat);
                        }
                    }
                    _currentList.Add(slot.nzo_id);
                }
                RemoveMissingRows(dgvCurrent, _currentList);
                SortList(dgvCurrent, _queue);
            }
            else
            {
                if (dgvCurrent.Rows.Count > 0) dgvCurrent.Rows.Clear();
            }
        }

        private void FillHistoryTable()
        {
            if (_history.Any())
            {
                foreach (var slot in _history)
                {
                    var row = (DataGridViewRow)dgvHistory.RowTemplate.Clone();

                    // prgProgress.SetProgressBarColor(Color.FromKnownColor(KnownColor.Control));
                    if (row != null)
                    {
                        CreateRow(row, slot);
                    }
                    if (!dgvHistory.ContainsValue("history" + NzoID, slot.nzo_id))
                    {
                        if (row != null) dgvHistory.Rows.Add(row);
                    }
                    else
                    {
                        int rowID = dgvHistory.GetRowID("history" + NzoID, slot.nzo_id);

                        //var kvCol = dgv1.Columns["category"] as DataGridViewComboBoxColumn;
                        //if (kvCol != null)
                        //{
                        //    var existingCategories = new List<string>(kvCol.Items.Cast<string>().ToList());
                        //    kvCol.
                        //    if (existingCategories.Except(_jsr.Categories()).Any())
                        //    {
                        //        kvCol.DataSource = _jsr.Categories();
                        //    }
                        //}

                        UpdateValue(dgvHistory, rowID, "history" + "id", slot.id);
                        UpdateValue(dgvHistory, rowID, "history" + "size", slot.size);
                        UpdateValue(dgvHistory, rowID, "history" + "script", slot.script);
                        UpdateValue(dgvHistory, rowID, "history" + "name", slot.name);
                        if ((string)dgvHistory.Rows[rowID].Cells["history" + "category"].Value != slot.category)
                        {
                            dgvHistory.Rows[rowID].Cells["history" + "category"].Value = slot.category;
                            dgvHistory.Rows[rowID].Cells["history" + "category"].Style.BackColor = GetColorByCategory(slot.category);
                        }
                    }
                    _historyList.Add(slot.nzo_id);
                }
                RemoveMissingRows(dgvHistory, _historyList, true);
                SortList(dgvHistory, _history);
            }
            else
            {
                if (dgvHistory.Rows.Count > 0) dgvHistory.Rows.Clear();
            }
        }

        /// <summary>
        /// Removes rows from DataGridView that are no longer present
        /// </summary>
        private void RemoveMissingRows(DataGridView dgv, HashSet<string> list, bool isHistory = false)
        {
            string rowID = isHistory ? "history" + NzoID : NzoID;
            HashSet<string> listOfValues = dgv.GetHashSet(rowID);
            foreach (var missing in listOfValues.Except(list))
            {
                dgv.Rows.RemoveAt(dgv.GetRowID(rowID, missing));
            }
        }

        private void UpdateValue(DataGridView dgv, int rowID, string key, int value)
        {
            if ((int)dgv.Rows[rowID].Cells[key].Value != value)
            {
                dgv.Rows[rowID].Cells[key].Value = value;
            }
        }

        private void UpdateValue(DataGridView dgv, int rowID, string key, string value)
        {
            if ((string)dgv.Rows[rowID].Cells[key].Value != value)
            {
                dgv.Rows[rowID].Cells[key].Value = value;
            }
        }

        private void SortList(DataGridView dgv, IEnumerable<Slot> list)
        {
            var column = dgv.Columns["index"];
            if (column != null)
            {
                dgv.Sort(column, ListSortDirection.Ascending);
            }
        }

        private void SortList(DataGridView dgv, IEnumerable<History> list)
        {

            var column = dgv.Columns["historyid"];
            if (column != null)
            {
                dgv.Sort(column, ListSortDirection.Descending);
            }
        }

        private static string GetStatus(Slot slot)
        {
            return slot.status == "Paused" ? "Resume" : "Pause";
        }


        private void CreateRow(DataGridViewRow row, Slot slot)
        {
            var btnDelete = new DataGridViewButtonCell { Value = "Delete" };
            var btnPause = new DataGridViewButtonCell { Value = GetStatus(slot) };
            var prgProgress = new DataGridViewProgressCell { Value = slot.percentage };
            //var cmbCategories = new DataGridViewComboBoxCell() { Value = slot.cat };

            //cmbCategories.DataSource = _jsr.Categories();
            //cmbCategories.ValueType = typeof (string);
            //cmbCategories.ReadOnly = false;

            prgProgress.SetProgressBarColor(Color.LimeGreen);

            row.CreateCells(dgvCurrent, slot.index, slot.nzo_id, Resources.MainForm_RefreshUI_Pause, slot.filename, slot.size, "progress",
                slot.timeleft, slot.cat, slot.priority, slot.script, "delete");
            var dataGridViewColumn = dgvCurrent.Columns[Resources.MainForm_RefreshUI_delete];
            if (dataGridViewColumn != null)
            {
                row.Cells[dataGridViewColumn.Index] = btnDelete;
                dataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            }

            dataGridViewColumn = dgvCurrent.Columns[Resources.MainForm_RefreshUI_Pause];
            if (dataGridViewColumn != null)
            {
                row.Cells[dataGridViewColumn.Index] = btnPause;
                dataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            dataGridViewColumn = dgvCurrent.Columns["progress"];
            if (dataGridViewColumn != null)
            {
                row.Cells[dataGridViewColumn.Index] = prgProgress;
                dataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            dataGridViewColumn = dgvCurrent.Columns["Category"];
            if (dataGridViewColumn != null)
            {
                //row.Cells[dataGridViewColumn.Index] = cmbCategories;
                row.Cells[dataGridViewColumn.Index].Style.BackColor = GetColorByCategory(slot.cat);
            }

            if (slot.status == "Paused")
                row.DefaultCellStyle.BackColor = Color.LightGray;
        }

        private void CreateRow(DataGridViewRow row, History slot)
        {
            row.CreateCells(dgvHistory, slot.id, slot.nzo_id, slot.name, slot.size, slot.script, slot.category, DateTime.Today.AddSeconds(slot.download_time).ToString("HH:mm:ss"), (slot.downloaded / 1024 / 1024).ToString() + " MB");

            var dataGridViewColumn = dgvHistory.Columns["historycategory"];
            if (dataGridViewColumn != null)
            {
                //row.Cells[dataGridViewColumn.Index] = cmbCategories;
                row.Cells[dataGridViewColumn.Index].Style.BackColor = GetColorByCategory(slot.category);
            }

            switch (slot.status)
            {
                case "Completed":
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    break;
                case "Failed":
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    break;
            }
        }

        private Color GetColorByCategory(string cat)
        {
            // TODO: this has to be loaded from file so that colors and categories can be customized
            switch (cat)
            {
                case "anime":
                    return Color.FromArgb(255, 255, 185);
                default:
                    return Color.White;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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
                    case "delete":
                        if (e.RowIndex >= 0)
                        {
                            _jsr.Delete(dataGridView.Rows[e.RowIndex].Cells[NzoID].Value as string);
                        }
                        break;
                    case "pause":
                        if (e.RowIndex >= 0)
                        {
                            if (dataGridView.Rows[e.RowIndex].Cells["pause"].Value as string == "Resume")
                            {
                                _jsr.Resume(dataGridView.Rows[e.RowIndex].Cells[NzoID].Value as string);
                            }
                            else
                            {
                                _jsr.Pause(dataGridView.Rows[e.RowIndex].Cells[NzoID].Value as string);
                            }
                        }
                        break;
                }
            /*
            dgv1.BeginEdit(false);
            var dataGridViewColumn = dgv1.Columns["category"];
            if (dataGridViewColumn != null && e.ColumnIndex == dataGridViewColumn.Index)// the combobox column index
            {
                if (this.dgv1.EditingControl != null
                    && this.dgv1.EditingControl is ComboBox)
                {
                    ComboBox cmb = this.dgv1.EditingControl as ComboBox;
                    cmb.DroppedDown = true;
                }
            }
             */
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

        /*
        private void dgv1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var dgvc = dgv1.Columns["category"];
            if (dgvc != null && (dgv1.CurrentCell.ColumnIndex == dgvc.Index && e.Control is ComboBox))
            {
                ComboBox cb = e.Control as ComboBox;
                if (cb != null)
                {
                    // first remove event handler to keep from attaching multiple:
                    cb.SelectedIndexChanged -= new
                    EventHandler(cb_SelectedIndexChanged);

                    // now attach the event handler
                    cb.SelectedIndexChanged += new
                    EventHandler(cb_SelectedIndexChanged);
                }
           }
        }
        private void dgv1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv1.IsCurrentCellDirty)
            {
                dgv1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Selected index changed");
        }
        */
    }
}