using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _2FADesktopAuthenticator
{
    public partial class AddAuthenticator : Form
    {
        public const string tmp = "/config/";
        public AddAuthenticator()
        {
            InitializeComponent();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory(),
                    temp = path + tmp;
            string name = nameAcc.Text;
            string secret = secretCode.Text;
            string secretUp = secret.Replace(" ", "");
            if (name=="")
            {
                MessageBox.Show("Enter name of account");
            }
            else if (secret=="")
            {
                MessageBox.Show("Enter secret key");
            }
            else if (secret == "" && name == "")
            {
                MessageBox.Show("Enter secret key and username");
            }
            else
            {
                File.AppendAllText(temp + name + ".json", secretUp);
                Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
            
        }

        private void AddAuthenticator_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
