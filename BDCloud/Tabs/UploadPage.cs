using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using User;

namespace BDCloud
{
    public partial class Form1 : Form
    {
        private void dropFile_DragDrop(object sender, DragEventArgs e)
        {
            this.uploadIcon.Visible = false;
            this.uploadMessage.Visible = false;
            this.FilePath.Text = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }

        private void dropFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dropFile_Click(object sender, EventArgs e)
        {
            var dlg1 = new FolderBrowserDialogEx();
            dlg1.Description = "请选择想要上传的文件或文件夹";
            dlg1.ShowNewFolderButton = true;
            dlg1.ShowEditBox = true;
            dlg1.NewStyle = true;
            dlg1.ShowFullPathInEditBox = true;
            dlg1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            dlg1.ShowBothFilesAndFolders = true;

            // 判断用户是否正确的选择了文件
            if (dlg1.ShowDialog() == DialogResult.OK)
            {
                this.FilePath.Text = dlg1.SelectedPath;
                this.uploadIcon.Visible = false;
                this.uploadMessage.Visible = false;
            }
        }

        private void FilePath_Click(object sender, EventArgs e)
        {
            this.dropFile_Click(sender, e);
        }

        private void uploadMessage_Click(object sender, EventArgs e)
        {
            this.dropFile_Click(sender, e);
        }

        private void uploadIcon_Click(object sender, EventArgs e)
        {
            this.dropFile_Click(sender, e);
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            int caseId = BDCloud.common.ClueInfo.caseId;

            if (String.IsNullOrWhiteSpace(FilePath.Text))
            {
                MessageBox.Show("请选择数据上传");
                return;
            }

            if (String.IsNullOrWhiteSpace(txtEvName.Text))
            {
                MessageBox.Show("请输入数据名称");
                return;
            }

            if (uploadThread == null || uploadThread.ThreadState == ThreadState.Stopped || uploadThread.ThreadState == ThreadState.Aborted)
            {
                ShowSystemStatus();
                uploadButton.Text = "暂停";
                uploadThread = new Thread(() =>
                {
                    if (!insertDataToDB()) //将上传行为插入到数据库中
                    {
                        uploadButton.Text = "上传";
                        return;
                    }
                    Maticsoft.BLL.evidence evidence_update = new Maticsoft.BLL.evidence();

                    DataSet ds = evidence_update.GetList("1=1 ORDER BY id desc LIMIT 1");
                    int eviId = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
                    cur_upload_eviId = eviId;
                    Ftp.Uploader.uploadPath(FilePath.Text);
                });
                uploadThread.Name = "uploadThread";
                uploadThread.Start();
            }
            else if (uploadThread.ThreadState == ThreadState.Suspended)
            {
                ShowSystemStatus();
                userControl12.isPause = true;
                userControl12.resetPauseButton();

                uploadButton.Text = "暂停";
                uploadThread.Resume();
            }
            else if (uploadThread.ThreadState == ThreadState.Running)
            {
                ShowSystemStatus();
                userControl12.isPause = false;
                userControl12.resetPauseButton();

                uploadButton.Text = "继续上传";
                uploadThread.Suspend();
            }
            else
            {
                Console.Out.WriteLine(uploadThread.ThreadState);
            }
        }
    }
}
