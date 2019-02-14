using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwoStepsAuthenticator;
using System.Diagnostics;

namespace _2FADesktopAuthenticator
{
    public partial class Form1 : Form
    {
        public const string tmp = "/config/";
        private readonly Func<DateTime> NowFunc;
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory(),
                    temp = path + tmp;
            string select_acc = accList.GetItemText(accList.SelectedItem);
            File.Delete(temp + select_acc + ".json");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            AddAuthenticator addAuthenticator = new AddAuthenticator();
            addAuthenticator.ShowDialog();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory(),
                    temp = path + tmp;
            String[] files = Directory.GetFiles(temp).OrderBy(d => new FileInfo(d).CreationTime).ToArray();

            foreach (string path1 in files)
            {
                accList.Items.Add(Path.GetFileNameWithoutExtension(path1));
            }


        }

        private void searchBox_TextChanged_1(object sender, EventArgs e)
        {
            string search = searchBox.Text;
            int index = accList.FindString(search, -1);
            if (index != -1)
            {
                // Select the found item:
                accList.SetSelected(index, true);
                // Send a success message:

            }
        }

        private void accList_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Start();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            string copy = codeBox.Text;
            Clipboard.SetText(copy);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory(),
                    temp = path + tmp;
            string select_acc = accList.GetItemText(accList.SelectedItem);
            var secret = File.ReadAllText(temp + select_acc + ".json");
            try
            {
                var authenticator = new TwoStepsAuthenticator.TimeAuthenticator();
                var code = authenticator.GetCode(secret);
                codeBox.Text = code;
                int IntervalSeconds = 30;
                DateTime dateTime = DateTime.Now;
                TimeSpan ts = (dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
                double st = ts.TotalSeconds % IntervalSeconds;

                progressBar1.Value =  Convert.ToInt32(100-(st / 30 * 100));

            }

            catch (Exception)
            {
                MessageBox.Show("Authenticator is failed");
                return;
            }
        }
        private long GetInterval(DateTime dateTime)
        {
            int IntervalSeconds = 30;
            TimeSpan ts = (dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (long)ts.TotalSeconds / IntervalSeconds;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void openConfigFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory(),
                    temp = path + tmp;
            Process.Start(temp);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void instrucToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Instruction instruction = new Instruction();
            instruction.ShowDialog();
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://vk.com/alexkoloshko");
        }
    }
}
