using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BDCloud
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 更新上传页面上传进度条
        /// </summary>
        public void UpdateProgress(double progress)
        {
            uploadProgressBar.Value = (int)(progress * 100);
            progressLabel.Text = ((int)(progress * 100.0)).ToString() + "%";
        }

        public void UpdateSpeed(double speed)
        {
            if (speed < (double)1024)
            {
                speedLabel.Text = Math.Round(speed, 1).ToString() + "B/s";
            }
            else if (speed < Math.Pow(1024, 2))
            {
                speedLabel.Text = Math.Round((speed / 1024.0), 1).ToString() + "KB/s";
            }
            else if (speed < Math.Pow(1024, 3))
            {
                speedLabel.Text = Math.Round((speed / Math.Pow(1024, 2)), 1).ToString() + "MB/s";
            }
            else
            {
                speedLabel.Text = Math.Round((speed / Math.Pow(1024, 3)), 1).ToString() + "GB/s";
            }
        }

        public void UpdateNum(int uploadedNum, int totalNum)
        {
            uploadNumLabel.Text = "已上传数 " + uploadedNum.ToString() + "/" + totalNum.ToString();
        }

        public void UpdateLog(string log)
        {
            logBox.AppendText(log + "\r\n");
            logBox.SelectionStart = logBox.Text.Length;

            logBox.ScrollToCaret();
        }

        public void UpdateTime(DateTime remainTime)
        {
            timeLabel.Text = "剩余时间 " + remainTime.ToString("HH:mm:ss");
        }

        public void CompleteUpload()
        {
            uploadButton.Text = "上传";
            updateFilesSuccStatus();      // 上传完成，修改finished字段为true    
            insertOnLineNumberDataToDB(); // 将onlinenumber 表中字段进行更新

            startAnalysis(Ftp.Uploader.FtpPathHash, filesBeans[index].getId().ToString());
            Console.WriteLine("--------------  " + Ftp.Uploader.FtpPathHash + "   " + filesBeans[index].getId().ToString());
            filesBeans[index].setInit(false);
            evTypes_d3.TextChanged -= new EventHandler(evTypes_d3_TextChanged);
            evTypes_d3.Text = "电子邮件";
            evTypes_d3.TextChanged += new EventHandler(evTypes_d3_TextChanged);

            FilePath.TextChanged -= new EventHandler(FilePath_TextChanged);
            FilePath.Text = "";
            FilePath.TextChanged += new EventHandler(FilePath_TextChanged);

            uploadIcon.Visible = true;
            uploadMessage.Visible = true;

            txtEvName.TextChanged -= new EventHandler(txtEvName_TextChanged);
            txtEvName.Text = "";
            txtEvName.TextChanged += new EventHandler(txtEvName_TextChanged);

            txtComment.TextChanged -= new EventHandler(txtComment_TextChanged);
            txtComment.Text = "";
            txtComment.TextChanged += new EventHandler(txtComment_TextChanged);

            dataTypes_d3.TextChanged -= new EventHandler(dataTypes_d3_TextChanged);
            dataTypes_d3.Text = "";
            dataTypes_d3.TextChanged += new EventHandler(dataTypes_d3_TextChanged);

            progressLabel.Text = 0 + "%";
            uploadProgressBar.Value = 0;
            ShowSystemStatus();
        }
    }
}
