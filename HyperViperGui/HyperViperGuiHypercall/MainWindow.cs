using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HyperViperGuiHypercall.HVCL;

namespace HyperViperGuiHypercall
{
    public partial class MainWindow : Form
    {
        private MainMenu mainMenu;
        private MenuItem menuSave;
        private MenuItem menuClose;

        public MainWindow()
        {
            InitializeComponent();
            tabControl.Controls.Add(new DriverControlComponent());

            mainMenu = new MainMenu();
            MenuItem File = mainMenu.MenuItems.Add("&File");
            File.MenuItems.Add(new MenuItem("&New hypercall", MenuItemNew));
            menuSave = new MenuItem("&Save", MenuItemSave);
            menuSave.Enabled = false;
            File.MenuItems.Add(menuSave);
            File.MenuItems.Add(new MenuItem("&Open", MenuItemOpen));
            menuClose = new MenuItem("&Close", MenuItemClose);
            menuClose.Enabled = false;
            File.MenuItems.Add(menuClose);
            File.MenuItems.Add(new MenuItem("&Exit"));
            this.Menu = mainMenu;
            MenuItem About = mainMenu.MenuItems.Add("&About");
            About.MenuItems.Add(new MenuItem("&About"));
            this.Menu = mainMenu;
        }

        public void addHypercall(UInt64 code, UInt64 param1, UInt64 param2)
        {
            TabPage newPage = new TabPage("Hypercall " + HypercallConversions.getCallCode(code));
            newPage.Controls.Add(new HypercallComponent(code, param1, param2));
            tabs.TabPages.Add(newPage);
        }
        
        public void addHypercall(UInt64 code, byte[] input)
        {
            TabPage newPage = new TabPage("Hypercall " + HypercallConversions.getCallCode(code));
            newPage.Controls.Add(new HypercallComponent(code, input));
            tabs.TabPages.Add(newPage);
        }

        private void MenuItemNew(Object sender, System.EventArgs e)
        {
            addHypercall(0x10001, 0x1122334455667788, 0x99aabbccddeeff00);
        }
        private void MenuItemOpen(Object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "HyperViper Call List (*.hvcl)|*.hvcl|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<HypercallStruct> calls = HVCL.open(openFileDialog.FileName);
                if(calls == null)
                {
                    MessageBox.Show("Opening file failed");
                    return;
                }

                if (calls.Count > 1)
                {
                    HypercallSelectionForm selectionWindow = new HypercallSelectionForm(this, calls);
                    selectionWindow.Show();
                }
                else
                {
                    if (calls.Count == 1)
                        addHypercall(calls[0].code, calls[0].input);
                    else
                        MessageBox.Show("This file contained no hypercalls");
                }
            }
        }

        private void MenuItemSave(Object sender, System.EventArgs e)
        {
            if (tabs.SelectedTab.Controls[0] is HypercallComponent o)
            {
                o.save();
            }
        }

        private void MenuItemClose(Object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Sure you want to close?", "Closing tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                tabs.TabPages.Remove(tabs.SelectedTab);
            }
        }

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            menuSave.Enabled = (tabs.SelectedTab != tabControl);
            menuClose.Enabled = (tabs.SelectedTab != tabControl);
        }
    }
}
