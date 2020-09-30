using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using HyperViperGuiDriver;

namespace HyperViperGuiHypercall
{
    public partial class DriverControlComponent : UserControl
    {
        System.Timers.Timer timerRefresh;

        public DriverControlComponent()
        {
            InitializeComponent();

            for (int x = 1; x < 0xe9; x++)
            {
                object[] row = new object[] { "0x" + x.ToString("X2"), 0, 0, 0, false, false, "BREAK", "START", "START", "CLEAR" };
                table.Rows.Add(row);
            }

            refreshStats(null, null);
            timerRefresh = new System.Timers.Timer(Int32.Parse(txtRefresh.Text));
            timerRefresh.Elapsed += refreshStats;
            timerRefresh.AutoReset = true;
            timerRefresh.Enabled = true;
        }

        private void btnHook_Click(object sender, EventArgs e)
        {
            if (btnHook.Text == "Hook")
            {
                if (DriverIO.hook())
                    btnHook.Text = "Unhook";
                else
                    MessageBox.Show("Hooking failed");
            }
            else
            {
                if (DriverIO.unhook())
                    btnHook.Text = "Hook";
                else
                    MessageBox.Show("Unhooking failed");
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (btnRecord.Text == "Start recording")
            {
                if (DriverIO.startLogging(txtRecord.Text))
                    btnRecord.Text = "Stop recording";
                else
                    MessageBox.Show("Starting recording failed");
            }
            else
            {
                if (DriverIO.stopLogging())
                    btnRecord.Text = "Start recording";
                else
                    MessageBox.Show("Stopping recording failed");
            }
        }

        private void refreshStats(Object source, ElapsedEventArgs e)
        {
            HV_HOOKING_HCALL_STATS[] stats = DriverIO.getStats();
            if (stats != null)
            {
                for(int x = 0; x < table.Rows.Count; x++)
                {
                    //int idx = (int)table.Rows[x].Cells[0].Value;
                    int idx = Convert.ToInt32(table.Rows[x].Cells[0].Value.ToString().Substring(2), 16);
                    table.Rows[x].Cells[1].Value = stats[idx].count;
                    table.Rows[x].Cells[2].Value = stats[idx].lastProcessID;
                    table.Rows[x].Cells[3].Value = stats[idx].lastElementCount;
                    table.Rows[x].Cells[4].Value = stats[idx].fast > 0;
                    table.Rows[x].Cells[5].Value = stats[idx].slow > 0;
                }
                applyFilters();
                return;
            }
        }

        private void txtRefresh_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter)
                    return;
                if (Int32.Parse(txtRefresh.Text) < 100)
                    txtRefresh.Text = "100";


                timerRefresh.Interval = Int32.Parse(txtRefresh.Text);

            }
            catch (FormatException)
            {
                txtRefresh.Text = timerRefresh.Interval.ToString();
            }
        }

        private void txtRefresh_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(txtRefresh.Text) < 100)
                    txtRefresh.Text = "100";


                timerRefresh.Interval = Int32.Parse(txtRefresh.Text);

            }
            catch (FormatException)
            {
                txtRefresh.Text = timerRefresh.Interval.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DriverIO.clearStats();
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 5)
            {
                try
                {
                    HV_HOOKING_HCALL_CONF_SET conf;
                    //conf.hypercall = (uint)Int32.Parse(table.Rows[e.RowIndex].Cells[0].Value.ToString());
                    conf.hypercall = Convert.ToUInt32(table.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(2), 16);
                    conf.breakpoint = 0;
                    conf.dbgPrint = 0;
                    conf.log = 0;
                    conf.bufferSize = 0x1000;

                    if (e.ColumnIndex == 6)
                        conf.breakpoint = 1;
                    if (e.ColumnIndex == 7)
                        conf.dbgPrint = 1;
                    if (e.ColumnIndex == 8)
                    {
                        conf.log = 1;
                        conf.bufferSize = 0x1000;
                    }

                    DriverIO.setConf(conf);                    
                }
                catch (Exception) { }
            }
        }

        private void applyFilters()
        {
            for (int x = 0; x < table.Rows.Count; x++)
            {
                if (optHideFast.Checked && (bool)table.Rows[x].Cells[4].Value)
                    table.Rows[x].Visible = false;
                else if (optHideSlow.Checked && (bool)table.Rows[x].Cells[5].Value)
                    table.Rows[x].Visible = false;
                else if (optHideZero.Checked && (uint)table.Rows[x].Cells[1].Value == 0)
                    table.Rows[x].Visible = false;
                else
                    table.Rows[x].Visible = true;
            }
        }

        private void optCheckedChanged(object sender, EventArgs e)
        {
            applyFilters();
        }

        private void btnAllToFile_Click(object sender, EventArgs e)
        {
            HV_HOOKING_HCALL_CONF_SET conf;
            conf.hypercall = 0;
            conf.breakpoint = 0;
            conf.dbgPrint = 0;
            conf.log = 0;
            conf.bufferSize = 0x1000;
            conf.log = 4;
            conf.bufferSize = 0x1000;
            DriverIO.setConf(conf);
        }

        private void btnAllBreak_Click(object sender, EventArgs e)
        {
            HV_HOOKING_HCALL_CONF_SET conf;
            conf.hypercall = 0;
            conf.breakpoint = 1;
            conf.dbgPrint = 0;
            conf.log = 0;
            conf.bufferSize = 0x1000;
            DriverIO.setConf(conf);
        }

        private void btnAllDbg_Click(object sender, EventArgs e)
        {
            HV_HOOKING_HCALL_CONF_SET conf;
            conf.hypercall = 0;
            conf.breakpoint = 0;
            conf.dbgPrint = 1;
            conf.log = 0;
            conf.bufferSize = 0x1000;
            DriverIO.setConf(conf);
        }

        private void txtRecord_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
