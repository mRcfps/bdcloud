using System;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maticsoft.BLL;
using Maticsoft.Model;
using Maticsoft.DBUtility;
using User;

namespace BDCloud
{
    public partial class LoginForm : Form
    {
        private Point mousePoint = new Point();
        public static LoginForm loginForm;

        public LoginForm()
        {
            InitializeComponent();

 
            loginForm = this;
            this.username.Text += "nj2003";
            this.password.Text += "123456";
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            // 判断是否已进行初始化设置
            if (!File.Exists("settings.ini"))
            {
                this.warningMessage.Text = "IP尚未配置";
                return;
            }

            // 判断用户名正确
            int userId = -1;
           // DbHelperMySQL.connectionString = "Database=bdcloud;Data Source=172.16.103.250;User Id=root;Password=123456;pooling=false;CharSet=utf8;port=3306";
            Maticsoft.BLL.user user1 = new Maticsoft.BLL.user();
            DataSet ds = user1.GetList("1=1");
            DataTable userTable = ds.Tables[0];

            for (int i = 0; i < userTable.Rows.Count; i++)
            {
                if (userTable.Rows[i]["policeNo"].ToString().Equals(this.username.Text))
                {
                    userId = i;
                }
            }
            if (userId == -1)
            {
                this.warningMessage.Text = "用户名错误！";
                return;
            }

            // 判断密码是否正确
            MD5 md5Hash = MD5.Create();
            string hash = GetMd5Hash(md5Hash, "xl_" + this.password.Text);
            if (!hash.Equals(userTable.Rows[userId]["password"].ToString()))
            {
                this.warningMessage.Text = "密码错误！";
                return;
            }

            // 将用户信息存入UserInfo类
            UserInfo.id = (int) userTable.Rows[userId]["id"];
            UserInfo.username = userTable.Rows[userId]["username"].ToString();
            UserInfo.policeNO = userTable.Rows[userId]["policeNO"].ToString();
            UserInfo.userRolle = userTable.Rows[userId]["userrole"].ToString();

            // 进入主界面
            
            Form1 form = new Form1();
            this.Hide();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
            Application.ExitThread();
            //this.DialogResult = DialogResult.OK;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.mousePoint.X = e.X;
            this.mousePoint.Y = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Control.MousePosition.Y - mousePoint.Y;
                this.Left = Control.MousePosition.X - mousePoint.X;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.mousePoint.X = e.X;
            this.mousePoint.Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Control.MousePosition.Y - mousePoint.Y;
                this.Left = Control.MousePosition.X - mousePoint.X;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
            System.Environment.Exit(0);
        }

        private void min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] byteArray = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
                byteArray[i] = (byte)input[i];

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(byteArray);

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                int val = ((int)data[i]) & 0xff;
                if (val < 16)
                    sBuilder.Append("0");
                sBuilder.Append(data[i].ToString("x"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // 判断是否已进行初始化设置
            if (File.Exists("settings.ini"))
            {
                this.warningMessage.Text = "";
            }
        }

        private void username_Leave(object sender, EventArgs e)
        {
            // 用户未输入账号，恢复占位符
            if (this.username.Text == "")
            {
                this.usernameLabel.Visible = true;
            }
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (this.password.Text == "")
            {
                this.passwordLabel.Visible = true;
            }
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            this.usernameLabel.Visible = false;
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            this.passwordLabel.Visible = false;
        }

        public void ClearWarning()
        {
            this.warningMessage.Text = "";
        }

        private void usernameLabel_Click(object sender, EventArgs e)
        {
            this.usernameLabel.Visible = false;
            this.username.Focus();
        }

        private void passwordLabel_Click(object sender, EventArgs e)
        {
            this.passwordLabel.Visible = false;
            this.password.Focus();
        }
    }
}
