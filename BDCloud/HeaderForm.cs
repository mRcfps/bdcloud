using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using User;

namespace BDCloud
{
    public partial class HeaderForm : Form
    {
        private Point mousePoint = new Point();
        private Image pickedImage;

        public HeaderForm()
        {
            InitializeComponent();
        }

        public static byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                    image.Save(ms, ImageFormat.Jpeg);
                else if (format.Equals(ImageFormat.Png))
                    image.Save(ms, ImageFormat.Png);
                else if (format.Equals(ImageFormat.Bmp))
                    image.Save(ms, ImageFormat.Bmp);
                byte[] buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        private void headerBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp";

            //判断用户是否正确的选择了文件
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.pickedImage = Image.FromFile(fileDialog.FileName);
                this.headerBox.BackgroundImage = this.pickedImage;
                this.hint.Text = "";
            }
        }

        private void hint_Click(object sender, EventArgs e)
        {
            headerBox_Click(sender, e);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (this.pickedImage != null)
            {
                // 将头像信息存储到UserInfo类中
                UserInfo.header = this.pickedImage;

                // 将头像图片存入数据库
                Maticsoft.DAL.user userDAL = new Maticsoft.DAL.user();
                Maticsoft.Model.user userModel = userDAL.GetModel(UserInfo.id);
                userModel.header = ImageToBytes(this.pickedImage);
                userDAL.Update(userModel);

                // 在主界面中更新头像
                Form1.form1.LoadHeader();

                this.Close();
            }
            else
            {
                MessageBox.Show("请先选择头像！");
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
    }
}
