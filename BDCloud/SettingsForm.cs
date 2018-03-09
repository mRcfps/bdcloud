using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;


namespace BDCloud
{
    public partial class SettingsForm : Form
    {
        private Point mousePoint = new Point();

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists("settings.ini"))
            {
                using (StreamReader sr = new StreamReader("settings.ini"))
                {
                    string line = "";
                    Console.WriteLine(line);
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "[ClientConfig]")
                        {
                            continue;
                        }
                        else if (line.StartsWith("FTP_IP"))
                        {
                            this.clusterIPInput.ForeColor = System.Drawing.Color.Black;
                            this.clusterIPInput.Text = line.Split('=')[1];
                        }
                        else if (line.StartsWith("DBHostIP"))
                        {
                            this.dbIPInput.ForeColor = System.Drawing.Color.Black;
                            this.dbIPInput.Text = line.Split('=')[1];
                        }
                        else
                        {
                            MessageBox.Show("配置文件格式错误！");
                        }
                    }
                }
            }
        }

        private void clusterIPInput_Click(object sender, EventArgs e)
        {
            // 如果尚无IP地址
            if (this.clusterIPInput.ForeColor == System.Drawing.Color.DarkGray)
            {
                this.clusterIPInput.Text = "";
                this.clusterIPInput.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Control.MousePosition.Y - mousePoint.Y;
                this.Left = Control.MousePosition.X - mousePoint.X;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.mousePoint.X = e.X;
            this.mousePoint.Y = e.Y;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            string dbIPInput = this.dbIPInput.Text.Trim();
            string clusterIPInput = this.clusterIPInput.Text.Trim();
            Regex ipRegex = new Regex(@"^(?:(?:\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.){3}(?:\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])$");  

            if (!ipRegex.IsMatch(dbIPInput))
            {
                this.warningMessage.Text = "数据库IP格式错误！";
            }
            else if (!ipRegex.IsMatch(clusterIPInput))
            {
                this.warningMessage.Text = "集群IP格式错误！";
            }
            else
            {
                using (StreamWriter sw = new StreamWriter("settings.ini"))
                {
                    sw.WriteLine("[ClientConfig]");
                    sw.WriteLine("DBHostIP=" + dbIPInput);
                    sw.WriteLine("FTP_IP=" + clusterIPInput);
                }
                this.Close();
            }
        }

        private void dbIPInput_Click(object sender, EventArgs e)
        {
            // 如果尚无IP地址
            if (this.dbIPInput.ForeColor == System.Drawing.Color.DarkGray)
            {
                this.dbIPInput.Text = "";
                this.dbIPInput.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
}
