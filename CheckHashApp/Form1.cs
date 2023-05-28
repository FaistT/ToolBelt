using System;
using System.Drawing;
using System.Linq;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace WFA_NET_Framework_1
{
    public partial class Form1 : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.BackColor = Color.Silver;
            GenerateSHA256(textBox2.Text, textBox1.Text, comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
            }
        }

        public void GenerateSHA256(string filePath, string importedHash, string algorithm)
        {
            using (PowerShell ps = PowerShell.Create())
            {
                byte[] script = CheckHashApp.Properties.Resources.checkFileHash_script;
                var scriptStr = System.Text.Encoding.UTF8.GetString(CheckHashApp.Properties.Resources.checkFileHash_script); 
                ps.AddScript(scriptStr);
                ps.AddParameter("importedHash", importedHash);
                ps.AddParameter("filePath", filePath);
                ps.AddParameter("algorithm", algorithm);
                System.Collections.ObjectModel.Collection<string> collection = ps.Invoke<string>();
                string v = collection.FirstOrDefault();
                label2.BackColor = v == "True" ? Color.Green : Color.Red;
                label2.Text = v;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void label1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var InfoPopup = new AboutBox1();
            InfoPopup.ShowDialog(this);
        }
    }
}

