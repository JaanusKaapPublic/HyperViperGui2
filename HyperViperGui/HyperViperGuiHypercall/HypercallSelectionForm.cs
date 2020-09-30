using HyperViperGuiDriver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HyperViperGuiHypercall.HVCL;

namespace HyperViperGuiHypercall
{
    public partial class HypercallSelectionForm : Form
    {
        MainWindow parent;
        List<HypercallStruct> calls;

        public HypercallSelectionForm(MainWindow parentIn, List<HypercallStruct> callsIn)
        {
            InitializeComponent();
            cmbFuzzType.SelectedIndex = 0;
            parent = parentIn;
            calls = callsIn;

            table.Rows.Clear();
            int idx = 0;
            int largest = 16;
            foreach (HypercallStruct call in calls)
            {
                String code = "0x" + HypercallConversions.getCallCode(call.code).ToString("X2");
                String count = "0x" + HypercallConversions.getCountOfElements(call.code).ToString("X");
                String start = "0x" + HypercallConversions.getRepStartIndex(call.code).ToString("X");
                bool fast = HypercallConversions.isFast(call.code);
                if (call.input.Length > largest)
                    largest = call.input.Length;
                object[] row = new object[] { idx++, code, fast, count, start, call.input.Length.ToString(), "open" };
                table.Rows.Add(row);
            }
            txtFuzzMaxPos.Text = largest.ToString();
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                try
                {
                    int idx = Int32.Parse(table.Rows[e.RowIndex].Cells[0].Value.ToString());
                    parent.addHypercall(calls[idx].code, calls[idx].input);
                }
                catch(Exception){ }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < table.Rows.Count; x++)
            {
                if (table.Rows[x].Selected)
                {
                    int idx = Int32.Parse(table.Rows[x].Cells[0].Value.ToString());
                    parent.addHypercall(calls[idx].code, calls[idx].input);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int total = 0;
            for (int x = 0; x < table.Rows.Count; x++)
                if (table.Rows[x].Selected)
                    total++;

            for (int x = 0; x < table.Rows.Count; x++)
            {
                if (table.Rows[x].Selected)
                {
                    int idx = Int32.Parse(table.Rows[x].Cells[0].Value.ToString());
                    lblCount.Text = total.ToString() + " left (ID=" + idx + ")";
                    lblCount.Refresh();
                    HV_MUTATION_CONF conf;
                    conf.target = 0;
                    conf.dbgMsg = (byte)(chkFuzzDbg.Checked ? 1 : 0);
                    conf.type = getFuzzType();
                    conf.seed = UInt32.Parse(txtFuzzSeed.Text);
                    conf.minChanges = UInt32.Parse(txtFuzzMin.Text);
                    conf.maxChanges = UInt32.Parse(txtFuzzMax.Text);
                    conf.maxLength = UInt32.Parse(txtFuzzMaxPos.Text);
                    conf.count = getFuzzCount((uint)(calls[idx].input.Length));

                    if (DriverIO.hypercallFuzz((long)calls[idx].code, calls[idx].input, conf.maxLength, conf))
                    {
                        total--;
                    }
                    else
                    {
                        lblCount.Text = "FAILED";
                        return;
                     }
                }
            }
            lblCount.Text = "DONE";
        }


        private uint getFuzzType()
        {
            switch (cmbFuzzType.Text)
            {
                case "Increment single byte":
                    return 1;
                case "Bit flipping single bit":
                    return 2;
                case "Replace with single special value":
                    return 3;
                case "Increment multiple bytes randomly":
                    return 4;
                case "Bit flipping multiple bits randomly":
                    return 5;
            }
            return 0;
        }
        private uint getFuzzCount(uint bufferLen)
        {
            switch (cmbFuzzType.Text)
            {
                case "Increment single byte":
                    return (bufferLen < UInt32.Parse(txtFuzzMaxPos.Text) ? bufferLen : UInt32.Parse(txtFuzzMaxPos.Text)) * 0xFF;
                case "Bit flipping single bit":
                    return (bufferLen < UInt32.Parse(txtFuzzMaxPos.Text) ? bufferLen : UInt32.Parse(txtFuzzMaxPos.Text)) * 8;
                case "Replace with single special value":
                    return (bufferLen < UInt32.Parse(txtFuzzMaxPos.Text) ? bufferLen : UInt32.Parse(txtFuzzMaxPos.Text)) * 33;
                case "Increment multiple bytes randomly":
                case "Bit flipping multiple bits randomly":
                    return UInt32.Parse(txtFuzzCount.Text);
            }
            return 0;
        }

        private void cmbFuzzType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbFuzzType.Text)
            {
                case "Increment single byte":
                    txtFuzzCount.Enabled = false;
                    txtFuzzMax.Enabled = false;
                    txtFuzzMin.Enabled = false;
                    txtFuzzSeed.Enabled = false;
                    txtFuzzCount.Text = "0";
                    txtFuzzMax.Text = "1";
                    txtFuzzMin.Text = "1";
                    txtFuzzSeed.Text = "0";
                    break;
                case "Bit flipping single bit":
                    txtFuzzCount.Enabled = false;
                    txtFuzzMax.Enabled = false;
                    txtFuzzMin.Enabled = false;
                    txtFuzzSeed.Enabled = false;
                    txtFuzzCount.Text = "0";
                    txtFuzzMax.Text = "1";
                    txtFuzzMin.Text = "1";
                    txtFuzzSeed.Text = "0";
                    break;
                case "Replace with single special value":
                    txtFuzzCount.Enabled = false;
                    txtFuzzMax.Enabled = false;
                    txtFuzzMin.Enabled = false;
                    txtFuzzSeed.Enabled = false;
                    txtFuzzCount.Text = "0";
                    txtFuzzMax.Text = "1";
                    txtFuzzMin.Text = "1";
                    txtFuzzSeed.Text = "0";
                    break;
                case "Increment multiple bytes randomly":
                    txtFuzzCount.Enabled = true;
                    txtFuzzMax.Enabled = true;
                    txtFuzzMin.Enabled = true;
                    txtFuzzSeed.Enabled = true;
                    txtFuzzCount.Text = "1000";
                    txtFuzzMax.Text = "4";
                    txtFuzzMin.Text = "2";
                    txtFuzzSeed.Text = "1234";
                    break;
                case "Bit flipping multiple bits randomly":
                    txtFuzzCount.Enabled = true;
                    txtFuzzMax.Enabled = true;
                    txtFuzzMin.Enabled = true;
                    txtFuzzSeed.Enabled = true;
                    txtFuzzCount.Text = "1000";
                    txtFuzzMax.Text = "4";
                    txtFuzzMin.Text = "2";
                    txtFuzzSeed.Text = "1234";
                    break;
            }
        }
    }
}
