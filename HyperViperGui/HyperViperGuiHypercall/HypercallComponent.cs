using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Be.Windows.Forms;
using HyperViperGuiDriver;

namespace HyperViperGuiHypercall
{
    public partial class HypercallComponent : UserControl
    {
        private HexBox hexBoxIn;
        private HexBox hexBoxOut;

        public HypercallComponent(UInt64 code, UInt64 param1, UInt64 param2)
        {
            InitializeComponent();
            createHexBox();
            cmbFuzzType.SelectedIndex = 0;

            txtCallnr.Text = HypercallConversions.getCallCode(code).ToString();
            txtCount.Text = HypercallConversions.getCountOfElements(code).ToString();
            txtStart.Text = HypercallConversions.getRepStartIndex(code).ToString();
            optFast.Checked = HypercallConversions.isFast(code);

            byte[] byteArr = new byte[16];
            for (int x = 0; x < 8; x++)
            {
                byteArr[x] = (byte)(param1 & 0xFF);
                param1 = param1 >> 8;
            }
            for (int x = 0; x < 8; x++)
            {
                byteArr[x + 8] = (byte)(param2 & 0xFF);
                param2 = param2 >> 8;
            }
            hexBoxIn.ByteProvider = new DynamicByteProvider(byteArr);
            updateEnabled();
        }

        public HypercallComponent(UInt64 code, byte[] input)
        {
            InitializeComponent();
            createHexBox();
            cmbFuzzType.SelectedIndex = 0;

            txtCallnr.Text = HypercallConversions.getCallCode(code).ToString();
            txtCount.Text = HypercallConversions.getCountOfElements(code).ToString();
            txtStart.Text = HypercallConversions.getRepStartIndex(code).ToString();
            optFast.Checked = HypercallConversions.isFast(code);

            hexBoxIn.ByteProvider = new DynamicByteProvider(input);
            updateEnabled();
        }

        private void createHexBox()
        {
            hexBoxIn = new HexBox()
            {
                Top = 0,
                Width = pnlHexIn.Width,
                Height = pnlHexIn.Height,
                Left = 0,
                Visible = true,
                UseFixedBytesPerLine = true,
                BytesPerLine = 16,
                ColumnInfoVisible = true,
                LineInfoVisible = true,
                StringViewVisible = true,
                VScrollBarVisible = true
            };
            pnlHexIn.Controls.Add(hexBoxIn);


            hexBoxOut = new HexBox()
            {
                Top = 0,
                Width = pnlHexOut.Width,
                Height = pnlHexOut.Height,
                Left = 0,
                Visible = true,
                UseFixedBytesPerLine = true,
                BytesPerLine = 16,
                ColumnInfoVisible = true,
                LineInfoVisible = true,
                StringViewVisible = true,
                VScrollBarVisible = true,
                ReadOnly = true
            };
            pnlHexOut.Controls.Add(hexBoxOut);
        }

        private void updateEnabled()
        {
            if(optFast.Checked)
            {
                txtOutSize.Enabled = false;
                hexBoxOut.Visible = false;
            }
            else
            {
                txtOutSize.Enabled = true;
                hexBoxOut.Visible = true;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                uint callnr = UInt32.Parse(txtCallnr.Text);
                uint count = UInt32.Parse(txtCount.Text);
                uint start = UInt32.Parse(txtStart.Text);
                uint outSize = UInt32.Parse(txtOutSize.Text);
                long inputInt = HypercallConversions.hypercallInput(callnr, optFast.Checked, count, start);
                long output;
                byte[] outputBuffer = new byte[outSize];
                                
                byte[] inputBuffer = new byte[hexBoxIn.ByteProvider.Length];
                for (int x = 0; x < inputBuffer.Length; x++)
                    inputBuffer[x] = hexBoxIn.ByteProvider.ReadByte(x);
                
                if (optFast.Checked && inputBuffer.Length != 0x10)
                {
                    MessageBox.Show("Fast hypercall input has to be 16 bytes (two 8 byte registers");
                    return;
                }

                if(DriverIO.hypercall(inputInt, inputBuffer, (uint)inputBuffer.Length, out output, out outputBuffer, outSize))
                {
                    txtResultStatus.Text = (output & 0xFFFF).ToString();
                    if ((output & 0xFFFF) > 0 || optFast.Checked)
                    {
                        hexBoxOut.ByteProvider = new DynamicByteProvider(new byte[0]);
                        hexBoxOut.Visible = false;
                    }
                    else
                    {
                        hexBoxOut.ByteProvider = new DynamicByteProvider(outputBuffer);
                        hexBoxOut.Visible = true;
                    }
                }
                else
                {
                    hexBoxOut.ByteProvider = new DynamicByteProvider(new byte[0]);
                    hexBoxOut.Visible = false;
                    txtResultStatus.Text = "";
                    MessageBox.Show("Making hypercall failed!");
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void optFast_CheckedChanged(object sender, EventArgs e)
        {
            updateEnabled();
        }

        public void save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "HyperViper Call List (*.hvcl)|*.hvcl|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                uint callnr = UInt32.Parse(txtCallnr.Text);
                uint count = UInt32.Parse(txtCount.Text);
                uint start = UInt32.Parse(txtStart.Text);
                uint outSize = UInt32.Parse(txtOutSize.Text);
                ulong inputInt = (ulong)HypercallConversions.hypercallInput(callnr, optFast.Checked, count, start);

                byte[] inputBuffer = new byte[hexBoxIn.ByteProvider.Length];
                for (int x = 0; x < inputBuffer.Length; x++)
                    inputBuffer[x] = hexBoxIn.ByteProvider.ReadByte(x);

                HVCL.save(saveFileDialog.FileName, inputInt, inputBuffer);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uint callnr = UInt32.Parse(txtCallnr.Text);
            uint count = UInt32.Parse(txtCount.Text);
            uint start = UInt32.Parse(txtStart.Text);
            long inputInt = HypercallConversions.hypercallInput(callnr, optFast.Checked, count, start);

            byte[] inputBuffer = new byte[hexBoxIn.ByteProvider.Length];
            for (int x = 0; x < inputBuffer.Length; x++)
                inputBuffer[x] = hexBoxIn.ByteProvider.ReadByte(x);

            HV_MUTATION_CONF conf;
            conf.target = 0;
            conf.dbgMsg = (byte)(chkFuzzDbg.Checked ? 1 : 0);
            conf.type = getFuzzType();
            conf.seed = UInt32.Parse(txtFuzzSeed.Text);
            conf.minChanges = UInt32.Parse(txtFuzzMin.Text);
            conf.maxChanges = UInt32.Parse(txtFuzzMax.Text);
            conf.maxLength = (uint)inputBuffer.Length;
            conf.count = getFuzzCount((uint)inputBuffer.Length);

            if (optFast.Checked && inputBuffer.Length != 0x10)
            {
                MessageBox.Show("Fast hypercall input has to be 16 bytes (two 8 byte registers");
                return;
            }

            if (DriverIO.hypercallFuzz(inputInt, inputBuffer, (uint)inputBuffer.Length, conf))
            {
                MessageBox.Show("DONE");
            }
            else
            {
                MessageBox.Show("FAILED");
            }
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
                    return bufferLen * 0xFF;
                case "Bit flipping single bit":
                    return bufferLen * 8;
                case "Replace with single special value":
                    return bufferLen * 33;
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
