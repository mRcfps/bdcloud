using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maticsoft.BLL;
using Maticsoft.Model;
using Maticsoft.DBUtility;
using System.Threading;
using User;
using FTP;



namespace BDCloud
{


    public partial class Form1 : Form
    {
        //edit by CXY,上传线程相关
        Thread uploadThread = null;
        int cur_upload_eviId;

        private bool ifClose = false;//关闭标志，点击关闭按钮时赋值为true；
        public static Form1 form1;  // 用于在其他窗体中操作此窗体中的控件
        BDCloud.Pagination pagination = new Pagination();
        int tabIndex = 0;//当前tabpage的index
        List<CaseClue> list = new List<CaseClue>();//对象数据

        List<int> historyList = new List<int>();//前进，回退存储信息
        int historyIndex = 1;//前进回退当前位置信息

        System.Resources.ResourceManager rs = new System.Resources.ResourceManager(typeof(Form1));
        public Form1()
        {
            /*
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() != DialogResult.OK)
                this.Close();*/

            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            form1 = this;
            load();

            ThreadStart tf = new ThreadStart(ThreadFunc);
            Thread temp = new Thread(tf);
            //       temp.Start();
            //分页接口
            pagination = new Pagination(pageinformation_d1, btnpage1_d1, btnpage2_d1, btnpage3_d1, btnpage4_d1, btnpage5_d1, texbox_page_jump_d1, panel_page_jump_d1,
            page_jump_d1, btnlastpage_d1, btnnextpage_d1, pagesNum_d1, cluecount_d1);
            Pagination.method = new changePageMethod(showPages_d1);

        }

        public void load()
        {
            //combobox控件使用自定义绘制 -lyk-2018115
            typeList_d1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            evTypes_d3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            dataTypes_d3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;

            //更新数据库finished字段 -lyk-2018122
            uploadDatabaseFinished();

            //隐藏最大化按钮 -lyk-2018122
            //max_d1.Visible = false;

            //            ThreadPool.SetMaxThreads(20, 20);
            //DbHelperMySQL.connectionString = "Database=bdcloud;Data Source=172.16.103.250;User Id=root;Password=123456;pooling=false;CharSet=utf8;port=3306";
            //UpdateSpaceProgress();
            //bindDataTransFinish();
            showClue_d1(1);

            //showClueList();
            //searchDataList("");//数据列表

            initialze_d1();
            // test();


            LoadHeader();
            //线索界面自定义控件中点击事件绑定
            clueitem1_d1.ButtonClick += new CluePanel.UserControl1.ClikcButton(clueUpload_d1);
            clueitem1_d1.picClick += new CluePanel.UserControl1.ClikcPicBox(clueContent_d1);
            clueitem1_d1.panelClick += new CluePanel.UserControl1.ClikcPanel(clueList_d1);
            clueitem1_d1.mouseEnterPanel += new CluePanel.UserControl1.MouseEnterPanel(clueMouseEnter_d1);
            clueitem1_d1.mouseHoverButton += new CluePanel.UserControl1.MouseHoverButton(clueMouseHover_d1);

            clueitem2_d1.ButtonClick += new CluePanel.UserControl1.ClikcButton(clueUpload_d1);
            clueitem2_d1.picClick += new CluePanel.UserControl1.ClikcPicBox(clueContent_d1);
            clueitem2_d1.panelClick += new CluePanel.UserControl1.ClikcPanel(clueList_d1);
            clueitem2_d1.mouseEnterPanel += new CluePanel.UserControl1.MouseEnterPanel(clueMouseEnter_d1);
            clueitem2_d1.mouseHoverButton += new CluePanel.UserControl1.MouseHoverButton(clueMouseHover_d1);

            clueitem3_d1.ButtonClick += new CluePanel.UserControl1.ClikcButton(clueUpload_d1);
            clueitem3_d1.picClick += new CluePanel.UserControl1.ClikcPicBox(clueContent_d1);
            clueitem3_d1.panelClick += new CluePanel.UserControl1.ClikcPanel(clueList_d1);
            clueitem3_d1.mouseEnterPanel += new CluePanel.UserControl1.MouseEnterPanel(clueMouseEnter_d1);
            clueitem3_d1.mouseHoverButton += new CluePanel.UserControl1.MouseHoverButton(clueMouseHover_d1);

            clueitem4_d1.ButtonClick += new CluePanel.UserControl1.ClikcButton(clueUpload_d1);
            clueitem4_d1.picClick += new CluePanel.UserControl1.ClikcPicBox(clueContent_d1);
            clueitem4_d1.panelClick += new CluePanel.UserControl1.ClikcPanel(clueList_d1);
            clueitem4_d1.mouseEnterPanel += new CluePanel.UserControl1.MouseEnterPanel(clueMouseEnter_d1);
            clueitem4_d1.mouseHoverButton += new CluePanel.UserControl1.MouseHoverButton(clueMouseHover_d1);

            clueitem5_d1.ButtonClick += new CluePanel.UserControl1.ClikcButton(clueUpload_d1);
            clueitem5_d1.picClick += new CluePanel.UserControl1.ClikcPicBox(clueContent_d1);
            clueitem5_d1.panelClick += new CluePanel.UserControl1.ClikcPanel(clueList_d1);
            clueitem5_d1.mouseEnterPanel += new CluePanel.UserControl1.MouseEnterPanel(clueMouseEnter_d1);
            clueitem5_d1.mouseHoverButton += new CluePanel.UserControl1.MouseHoverButton(clueMouseHover_d1);

            clueitem6_d1.ButtonClick += new CluePanel.UserControl1.ClikcButton(clueUpload_d1);
            clueitem6_d1.picClick += new CluePanel.UserControl1.ClikcPicBox(clueContent_d1);
            clueitem6_d1.panelClick += new CluePanel.UserControl1.ClikcPanel(clueList_d1);
            clueitem6_d1.mouseEnterPanel += new CluePanel.UserControl1.MouseEnterPanel(clueMouseEnter_d1);
            clueitem6_d1.mouseHoverButton += new CluePanel.UserControl1.MouseHoverButton(clueMouseHover_d1);

            clueitem7_d1.ButtonClick += new CluePanel.UserControl1.ClikcButton(clueUpload_d1);
            clueitem7_d1.picClick += new CluePanel.UserControl1.ClikcPicBox(clueContent_d1);
            clueitem7_d1.panelClick += new CluePanel.UserControl1.ClikcPanel(clueList_d1);
            clueitem7_d1.mouseEnterPanel += new CluePanel.UserControl1.MouseEnterPanel(clueMouseEnter_d1);
            clueitem7_d1.mouseHoverButton += new CluePanel.UserControl1.MouseHoverButton(clueMouseHover_d1);

            clueitem8_d1.ButtonClick += new CluePanel.UserControl1.ClikcButton(clueUpload_d1);
            clueitem8_d1.picClick += new CluePanel.UserControl1.ClikcPicBox(clueContent_d1);
            clueitem8_d1.panelClick += new CluePanel.UserControl1.ClikcPanel(clueList_d1);
            clueitem8_d1.mouseEnterPanel += new CluePanel.UserControl1.MouseEnterPanel(clueMouseEnter_d1);
            clueitem8_d1.mouseHoverButton += new CluePanel.UserControl1.MouseHoverButton(clueMouseHover_d1);

            clueitem9_d1.ButtonClick += new CluePanel.UserControl1.ClikcButton(clueUpload_d1);
            clueitem9_d1.picClick += new CluePanel.UserControl1.ClikcPicBox(clueContent_d1);
            clueitem9_d1.panelClick += new CluePanel.UserControl1.ClikcPanel(clueList_d1);
            clueitem9_d1.mouseEnterPanel += new CluePanel.UserControl1.MouseEnterPanel(clueMouseEnter_d1);
            clueitem9_d1.mouseHoverButton += new CluePanel.UserControl1.MouseHoverButton(clueMouseHover_d1);

            //数据列表暂停
            userControl12.pause += new DataLineA.UserControl1.ClikcPicBox(pauseUpload);
            userControl13.pause += new DataLineA.UserControl1.ClikcPicBox(pauseUpload);
            userControl14.pause += new DataLineA.UserControl1.ClikcPicBox(pauseUpload);
            userControl15.pause += new DataLineA.UserControl1.ClikcPicBox(pauseUpload);
            userControl16.pause += new DataLineA.UserControl1.ClikcPicBox(pauseUpload);
            userControl17.pause += new DataLineA.UserControl1.ClikcPicBox(pauseUpload);
            userControl18.pause += new DataLineA.UserControl1.ClikcPicBox(pauseUpload);
            userControl19.pause += new DataLineA.UserControl1.ClikcPicBox(pauseUpload);
            userControl110.pause += new DataLineA.UserControl1.ClikcPicBox(pauseUpload);
            userControl111.pause += new DataLineA.UserControl1.ClikcPicBox(pauseUpload);

            //数据列表删除
            userControl12.delete += new DataLineA.UserControl1.ClikcPicBox(deleteUpload);
            userControl13.delete += new DataLineA.UserControl1.ClikcPicBox(deleteUpload1);
            userControl14.delete += new DataLineA.UserControl1.ClikcPicBox(deleteUpload2);
            userControl15.delete += new DataLineA.UserControl1.ClikcPicBox(deleteUpload3);
            userControl16.delete += new DataLineA.UserControl1.ClikcPicBox(deleteUpload4);
            userControl17.delete += new DataLineA.UserControl1.ClikcPicBox(deleteUpload5);
            userControl18.delete += new DataLineA.UserControl1.ClikcPicBox(deleteUpload6);
            userControl19.delete += new DataLineA.UserControl1.ClikcPicBox(deleteUpload7);
            userControl110.delete += new DataLineA.UserControl1.ClikcPicBox(deleteUpload8);
            userControl111.delete += new DataLineA.UserControl1.ClikcPicBox(deleteUpload9);

            //前进后退信息初始化
            historyList.Add(0);
            historyIndex = 0;
            //显示系统状态
            showSystemStaus();
            showSystemStaus();
        }

        public void LoadHeader()
        {
            Maticsoft.DAL.user userDAL = new Maticsoft.DAL.user();
            Maticsoft.Model.user userModel = userDAL.GetModel(UserInfo.id);

            // 如果用户设置过头像
            if (userModel.header != null)
            {
                Image header = Image.FromStream(new MemoryStream(userModel.header));
                this.picImage.BackgroundImage = header;
            }
        }

        public void test()
        {
            //DbHelperMySQL.connectionString = "";
            Maticsoft.BLL.user user1 = new Maticsoft.BLL.user();
            Maticsoft.Model.user user2 = new Maticsoft.Model.user();


            int count = user1.GetRecordCount("");
            DataSet ds = user1.GetList("username='王利明'");

            user2.id = 264;
            //   bool b=user1.Exists(2622);
            user2.username = "hhh1aaa";

            //  user2.hea = ds.Tables[0].Rows[0]["header"].ToString();
            user1.Update(user2);


            MessageBox.Show("" + ds.Tables[0].Rows[0]["username"].ToString());


            Console.ReadLine();
        }

        private void UpdateSpaceProgress()
        {
            string strText = "49GB/200GB";
            Font font = new Font("微软雅黑", (float)10, FontStyle.Regular);
            //PointF pointF = new PointF(this.spaceProgress.Width / 2 - 10, this.spaceProgress.Height / 2 - 10);
            //this.spaceProgress.CreateGraphics().DrawString(strText, font, Brushes.Black, pointF);
        }

        private void button3_d1_Click(object sender, EventArgs e)
        {

        }

        private void button7_d1_Click(object sender, EventArgs e)
        {

        }

        private void btnjump_d1_Click(object sender, EventArgs e)
        {
        }

        private void btnhomepage_d1_Click(object sender, EventArgs e)
        {
        }

        private void btnpage1_d1_Click(object sender, EventArgs e)
        {
        }

        private void btnpage2_d1_Click(object sender, EventArgs e)
        {
        }

        private void btnpage3_d1_Click(object sender, EventArgs e)
        {
        }

        private void btnendpage_d1_Click(object sender, EventArgs e)
        {
        }

        private void lastpage_d1_Click(object sender, EventArgs e)
        {
        }

        private void nextpage_d1_Click(object sender, EventArgs e)
        {
        }

        private void tabClueList_Click(object sender, EventArgs e)
        {

        }

        private void panel7_d1_Paint(object sender, PaintEventArgs e)
        {

        }
        private Point mousePoint = new Point();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //  base.OnMouseDown(e);
            this.mousePoint.X = e.X;
            this.mousePoint.Y = e.Y;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Control.MousePosition.Y - mousePoint.Y;
                this.Left = Control.MousePosition.X - mousePoint.X;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.ifClose = true;

            Application.Exit();
            System.Environment.Exit(0);//终止当前进程并为基础操作系统提供指定的退出代码
        }

        private void bindDataTransFinish()
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewImageCell fileType = new DataGridViewImageCell();
            String str = Environment.CurrentDirectory;
            //"E:\\CSharp\\BDCloud2\\BDCloud\\bin\\Debug"
            // String imgpath = str.Substring(0, str.Length - "bin\\Debug".Length) + "img\\pdf.jpg";
            // String imgpath = str.Substring(0, str.Length - "bin\\Release".Length) + "img\\pdf.jpg";
            String imgpath = str + "\\pdf.jpg";
            Image img = Image.FromFile(imgpath);// ("E:\\MultipleSpace\\VS2010\\BDCloud\\BDCloud\\img\\pdf.jpg");//(System.Drawing.Image)rs.GetObject("pdf");
            fileType.Value = img;
            row.Cells.Add(fileType);
            DataGridViewTextBoxCell fileName = new DataGridViewTextBoxCell();
            fileName.Value = "潮州地税局自然人涉税地理信息智能系统建设方案书.pdf";
            row.Cells.Add(fileName);
            DataGridViewTextBoxCell leftTime = new DataGridViewTextBoxCell();
            leftTime.Value = "00:05:30";
            row.Cells.Add(leftTime);
            DataGridViewTextBoxCell progress = new DataGridViewTextBoxCell();
            progress.Value = "90%";
            row.Cells.Add(progress);
            DataGridViewImageCell pause = new DataGridViewImageCell();
            pause.Value = img;
            row.Cells.Add(pause);
            DataGridViewImageCell remove = new DataGridViewImageCell();
            remove.Value = img;
            row.Cells.Add(remove);
            DataGridViewImageCell open = new DataGridViewImageCell();
            open.Value = img;
            row.Cells.Add(open);

            //textboxcell.Value = "aaa";
            //row.Cells.Add(textboxcell);
            //DataGridViewComboBoxCell comboxcell = new DataGridViewComboBoxCell();
            //row.Cells.Add(fileType);
            dataTransFinish.Rows.Add(row);
            //dataTransFinish.Rows.Add(row);
            //dataTransFinish.Rows.Add(row);
            //dataTransFinish.Rows.Add(row);
            //dataTransFinish.Rows.Add(row);

            //int index = this.dataTransFinish.Rows.Add();

            //this.dataTransFinish.Rows[index].Cells[0].Value = "1";

            //this.dataTransFinish.Rows[index].Cells[1].Value = "2";

            //this.dataTransFinish.Rows[index].Cells[2].Value = "监听";


            //DataTable dt = new DataTable();
            ////新建列
            //DataColumn col1 = new DataColumn("文件名称", typeof(string));
            //DataColumn col2 = new DataColumn("剩余时间", typeof(string));
            //DataColumn col3 = new DataColumn("上传进度", typeof(string));
            //DataColumn col4 = new DataColumn("暂停任务", typeof(string));
            //DataColumn col5 = new DataColumn("删除任务", typeof(string));
            //DataColumn col6 = new DataColumn("文件夹打开", typeof(string));
            ////添加列
            //dt.Columns.Add(col1);
            //dt.Columns.Add(col2);
            //dt.Columns.Add(col3);
            //dt.Columns.Add(col4);
            //dt.Columns.Add(col5);
            //dt.Columns.Add(col6);
            ////新建行
            //DataRow row1 = dt.NewRow();
            ////行赋值
            //row1["文件名称"] = "打印机";
            //row1["剩余时间"] = "李居明";
            //row1["上传进度"] = "JFKSJFKSDFJK151";
            //row1["暂停任务"] = "普通用户";
            //row1["删除任务"] = "在库";
            //row1["文件夹打开"] = "2012-03-20";
            ////row1[6] = "2012-03-27";
            ////添加行
            //dt.Rows.Add(row1);
            ////数据绑定
            //this.dataTransFinish.DataSource = dt;
            //设置属性
            //DataGridTableStyle tablestyle = new DataGridTableStyle();
            //this.dataTransFinish..TableStyles.Add(tablestyle);
            //dataGrid1.TableStyles[0].GridColumnStyles[0].Width = 75;
            //dataGrid1.TableStyles[0].GridColumnStyles[1].Width = 75;
            //dataGrid1.TableStyles[0].GridColumnStyles[2].Width = 75;
            //dataGrid1.TableStyles[0].GridColumnStyles[3].Width = 75;
            //dataGrid1.TableStyles[0].GridColumnStyles[4].Width = 75;
            //dataGrid1.TableStyles[0].GridColumnStyles[5].Width = 120;
            //dataGrid1.TableStyles[0].GridColumnStyles[6].Width = 120;
            //dataGrid1.TableStyles[0].GridColumnStyles[6].Width = 120;


        }

        private void panel1_d1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void userControl14_Load(object sender, EventArgs e)
        {

        }

        private void labelUser_Click(object sender, EventArgs e)
        {

        }

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

            //判断用户是否正确的选择了文件
            if (dlg1.ShowDialog() == DialogResult.OK)
            {
                this.FilePath.Text = dlg1.SelectedPath;
                this.uploadIcon.Visible = false;
                this.uploadMessage.Visible = false;
            }

        }

        private void right_d1_Click(object sender, EventArgs e)
        {
            //if(tabIndex<4)
            //    tabPages.SelectedIndex = ++tabIndex;
            if (historyList.Count > historyIndex + 1)
            {
                int index = historyList[++historyIndex];
                tabPages.SelectedIndex = index;
                tabIndex = index;
            }
            changeForwardAndBackStyle();


            // 判断是否应该显示上传按钮——数据列表界面
            if (tabIndex == 1)
            {
                this.btnupload_d2.Visible = true;
                this.pic_upload_d2.Visible = true;
                this.panel_upload_d2.Visible = true;
                while (typeList_d1.Items.Count > 0)
                    typeList_d1.Items.RemoveAt(typeList_d1.Items.Count - 1);
                typeList_d1.Items.Add("处理状态");
                typeList_d1.Items.Add("上传中");
                typeList_d1.Items.Add("上传完成");
                typeList_d1.Items.Add("上传失败");
                typeList_d1.Items.Add("解析中");
                typeList_d1.Items.Add("解析完成");
                typeList_d1.Items.Add("解析失败");
                typeList_d1.SelectedIndex = 0;

                searchDataList("");//更新数据
            }
            else
            {
                this.btnupload_d2.Visible = false;
                this.pic_upload_d2.Visible = false;
                this.panel_upload_d2.Visible = false;
            }

            if (tabIndex == 2)
            {
                // 是上传页面
                this.panel3_clue_d1.Visible = false;
                this.panel2_search_d1.Visible = false;
            }
            else
            {
                this.panel3_clue_d1.Visible = true;
                this.panel2_search_d1.Visible = true;
            }
        }

        private void left_d1_Click(object sender, EventArgs e)
        {
            //if (tabIndex >0)
            //    tabPages.SelectedIndex = --tabIndex;
            if (historyIndex != 0)
            {
                int index = historyList[--historyIndex];
                tabPages.SelectedIndex = index;
                tabIndex = index;
            }
            changeForwardAndBackStyle();

            //当后退到第一页时
            if (tabIndex == 0 && historyList.Count != 1)
            {
                search_d1.Text = "请输入要搜索的内容";
                isSearch_d1 = false;
                showClue_d1(1);
                initialze_d1();

                //初始化分页类，内部自动处理分页逻辑；搭载函数为Pagination.method = new changePageMethod( /*yourMethod*/ );
                pagination = new Pagination(pageinformation_d1, btnpage1_d1, btnpage2_d1, btnpage3_d1, btnpage4_d1, btnpage5_d1, texbox_page_jump_d1, panel_page_jump_d1,
      page_jump_d1, btnlastpage_d1, btnnextpage_d1, pagesNum_d1, cluecount_d1);
            }
            // 判断是否应该显示上传按钮——数据列表页面
            if (tabIndex == 1)
            {
                this.btnupload_d2.Visible = true;
                this.pic_upload_d2.Visible = true;
                this.panel_upload_d2.Visible = true;
                while (typeList_d1.Items.Count > 0)
                    typeList_d1.Items.RemoveAt(typeList_d1.Items.Count - 1);
                typeList_d1.Items.Add("处理状态");
                typeList_d1.Items.Add("上传中");
                typeList_d1.Items.Add("上传完成");
                typeList_d1.Items.Add("上传失败");
                typeList_d1.Items.Add("解析中");
                typeList_d1.Items.Add("解析完成");
                typeList_d1.Items.Add("解析失败");
                typeList_d1.SelectedIndex = 0;

                searchDataList("");//更新页面
            }
            else
            {
                this.btnupload_d2.Visible = false;
                this.pic_upload_d2.Visible = false;
                this.panel_upload_d2.Visible = false;
            }

            if (tabIndex == 2)
            {
                // 是上传页面
                this.panel3_clue_d1.Visible = false;
                this.panel2_search_d1.Visible = false;
            }
            else
            {
                this.panel3_clue_d1.Visible = true;
                this.panel2_search_d1.Visible = true;
            }
        }

        private void picSetting_d1_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            // this.Hide();
            settingsForm.StartPosition = FormStartPosition.CenterParent;
            settingsForm.ShowDialog();
            //Application.ExitThread();
        }

        private void picSetting_d3_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            // this.Hide();
            settingsForm.StartPosition = FormStartPosition.CenterParent;
            settingsForm.ShowDialog();
            //Application.ExitThread();
        }

        private void picSetting_d2_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            // this.Hide();
            settingsForm.StartPosition = FormStartPosition.CenterParent;
            settingsForm.ShowDialog();
            //Application.ExitThread();
        }

        private void search_d1_Click(object sender, EventArgs e)
        {
            this.search_d1.Text = "";
        }

        #region 最小化到托盘相关
        private void form_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)  //判断是否最小化
            {
                this.ShowInTaskbar = false;  //不显示在系统任务栏（此处会触发关闭事件formclosing\未解决）
                notifyIcon1.Visible = true;  //托盘图标可见
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ShowInTaskbar = true;  //显示在系统任务栏
            this.WindowState = FormWindowState.Normal;  //还原窗体
            notifyIcon1.Visible = false;  //托盘图标隐藏
        }

        private void formClose_icon(object sender, FormClosingEventArgs e)
        {
            if (this.ifClose)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;//点击最小化时会莫名触发关闭事件、以此为暂时解决方案
            }

        }
        #endregion

        private void btnpage1_d1_Click_1(object sender, EventArgs e)
        {
        }

        private void btnpage2_d1_Click_1(object sender, EventArgs e)
        {
        }

        private void btnpage3_d1_Click_1(object sender, EventArgs e)
        {
        }

        private void btnpage4_d1_Click(object sender, EventArgs e)
        {
        }

        private void btnpage5_d1_Click(object sender, EventArgs e)
        {
        }

        private void btnlastpage_d1_Click(object sender, EventArgs e)
        {
        }

        private void btnnextpage_d1_Click(object sender, EventArgs e)
        {
        }

        private void page_jump_d1_Click(object sender, EventArgs e)
        {
        }

        private void picImage_Click(object sender, EventArgs e)
        {
            HeaderForm form = new HeaderForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            // 设置弹窗关闭后更新头像
            if (form.IsDisposed)
            {
                LoadHeader();
            }
        }

        private void btnupload_d2_Click(object sender, EventArgs e)
        {
            //while (historyList.Count > 2)
            //{
            //    historyList.RemoveAt(historyList.Count - 1);
            //}

            historyIndex++;
            historyList.Add(2);

            tabPages.SelectedIndex = 2;
            tabIndex = 2;
            // 判断是否应该显示上传按钮
            if (tabIndex == 1)
            {
                this.btnupload_d2.Visible = true;
                this.pic_upload_d2.Visible = true;
                this.panel_upload_d2.Visible = true;
            }
            else
            {
                this.btnupload_d2.Visible = false;
                this.pic_upload_d2.Visible = false;
                this.panel_upload_d2.Visible = false;
            }

            if (tabIndex == 2)
            {
                // 是上传页面
                this.panel3_clue_d1.Visible = false;
                this.panel2_search_d1.Visible = false;
            }
            else
            {
                this.panel3_clue_d1.Visible = true;
                this.panel2_search_d1.Visible = true;
            }
            initUploadHandlePage();
        }

        #region TabdataLine PageButton
        private void btnlastpage_d2_Click(object sender, EventArgs e)//上一页
        {
            nowDataPage = (nowDataPage - 1) > 0 ? (nowDataPage - 1) : 1;
            setDataListByPage(nowDataPage);

            if (nowDataPage > 2 && (totalPage - nowDataPage > 1))//改变按钮对应值
            {
                pageButtonValue1_d2 = nowDataPage - 2;
                pageButtonValue2_d2 = nowDataPage - 1;
                pageButtonValue3_d2 = nowDataPage;
                pageButtonValue4_d2 = nowDataPage + 1;
                pageButtonValue5_d2 = nowDataPage + 2;

            }
            reSetPage();//重设按钮文字颜色
        }

        private void btnnextpage_d2_Click(object sender, EventArgs e)//下一页
        {
            nowDataPage = nowDataPage + 1;
            if (nowDataPage > totalPage)
            {
                nowDataPage = totalPage;
            }

            if (nowDataPage > 2 && (totalPage - nowDataPage > 1))
            {
                pageButtonValue1_d2 = nowDataPage - 2;
                pageButtonValue2_d2 = nowDataPage - 1;
                pageButtonValue3_d2 = nowDataPage;
                pageButtonValue4_d2 = nowDataPage + 1;
                pageButtonValue5_d2 = nowDataPage + 2;

            }
            setDataListByPage(nowDataPage);
            reSetPage();
        }

        private void btnpage1_d2_Click(object sender, EventArgs e)//第一页
        {
            nowDataPage = pageButtonValue1_d2;

            changePageValue();
            reSetPage();

            setDataListByPage(nowDataPage);
        }

        private void btnpage2_d2_Click(object sender, EventArgs e)//第二页
        {
            nowDataPage = pageButtonValue2_d2;
            changePageValue();
            reSetPage();

            setDataListByPage(nowDataPage);
        }

        private void btnpage3_d2_Click(object sender, EventArgs e)//第三页
        {
            nowDataPage = pageButtonValue3_d2;

            reSetPage();

            setDataListByPage(nowDataPage);
        }

        private void btnpage4_d2_Click(object sender, EventArgs e)//第四页
        {
            nowDataPage = pageButtonValue4_d2;
            changePageValue();
            reSetPage();

            setDataListByPage(nowDataPage);
        }

        private void btnpage5_d2_Click(object sender, EventArgs e)//第五页
        {
            nowDataPage = pageButtonValue5_d2;
            changePageValue();
            reSetPage();

            setDataListByPage(nowDataPage);
        }

        private void page_jump_d2_Click(object sender, EventArgs e)//跳转按钮
        {
            string temp = texbox_page_jump_d2.Text;
            if (temp != null && temp != "")
            {
                int page = Convert.ToInt32(temp);
                if (page > totalPage)
                {
                    page = totalPage;
                }
                else if (page <= 0)
                {
                    page = 1;
                }
                nowDataPage = page;
                changePageValue();
                reSetPage();
                setDataListByPage(nowDataPage);
            }
            texbox_page_jump_d2.Text = "";
        }

        private void texbox_page_jump_d2_KeyPress(object sender, KeyPressEventArgs e)//跳转输入框输入限制
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符
                }
            }
        }
        #endregion

        private void search2_d1_Click(object sender, EventArgs e)
        {
            //
            if (search_d1.Text.Equals(""))
            {
                search_d1.Text = "请输入要搜索的内容";
                panelTitle_d1.Select();
            }

            if (tabIndex == 0)
            {
                isSearch_d1 = true;
                showClue_d1(1);
                initialze_d1();
                pagination = new Pagination(pageinformation_d1, btnpage1_d1, btnpage2_d1, btnpage3_d1, btnpage4_d1, btnpage5_d1, texbox_page_jump_d1, panel_page_jump_d1,
                page_jump_d1, btnlastpage_d1, btnnextpage_d1, pagesNum_d1, cluecount_d1);
            }
            else if (tabIndex == 1)
            {
                //数据列表的处理
                isSearch_d1 = true;
                searchDataList(search_d1.Text);

            }

            uploadButton.PerformClick();
        }

        private void home_d1_Click(object sender, EventArgs e)
        {
            while (historyList.Count > 1)
            {
                historyList.RemoveAt(historyList.Count - 1);
            }
            this.btnupload_d2.Visible = false;
            this.pic_upload_d2.Visible = false;
            this.panel_upload_d2.Visible = false;
            historyIndex = 0;
            changeForwardAndBackStyle();

            tabIndex = 0;
            tabPages.SelectedIndex = tabIndex;
            // 判断是否应该显示上传按钮
            if (tabIndex == 1)
            {
                this.btnupload_d2.Visible = true;
                this.pic_upload_d2.Visible = true;
                this.panel_upload_d2.Visible = true;
            }
            else
            {
                this.btnupload_d2.Visible = false;
                this.pic_upload_d2.Visible = false;
                this.panel_upload_d2.Visible = false;
            }
            if (tabIndex == 2)
            {
                // 是上传页面
                this.panel3_clue_d1.Visible = false;
                this.panel2_search_d1.Visible = false;
            }
            else
            {
                this.panel3_clue_d1.Visible = true;
                this.panel2_search_d1.Visible = true;
            }
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
            isSearch_d1 = false;
            showClue_d1(1);
            initialze_d1();
            pagination = new Pagination(pageinformation_d1, btnpage1_d1, btnpage2_d1, btnpage3_d1, btnpage4_d1, btnpage5_d1, texbox_page_jump_d1, panel_page_jump_d1,
      page_jump_d1, btnlastpage_d1, btnnextpage_d1, pagesNum_d1, cluecount_d1);
        }

        private void HomeClue_d1_Click(object sender, EventArgs e)
        {
            while (historyList.Count > 1)
            {
                historyList.RemoveAt(historyList.Count - 1);
            }
            this.btnupload_d2.Visible = false;
            this.pic_upload_d2.Visible = false;
            this.panel_upload_d2.Visible = false;
            historyIndex = 0;
            changeForwardAndBackStyle();

            tabIndex = 0;
            tabPages.SelectedIndex = tabIndex;
            // 判断是否应该显示上传按钮
            if (tabIndex == 1)
            {
                this.btnupload_d2.Visible = true;
                this.pic_upload_d2.Visible = true;
                this.panel_upload_d2.Visible = true;
            }
            else
            {
                this.btnupload_d2.Visible = false;
                this.pic_upload_d2.Visible = false;
                this.panel_upload_d2.Visible = false;
            }

            if (tabIndex == 2)
            {
                // 是上传页面
                this.panel3_clue_d1.Visible = false;
                this.panel2_search_d1.Visible = false;
            }
            else
            {
                this.panel3_clue_d1.Visible = true;
                this.panel2_search_d1.Visible = true;
            }
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
            isSearch_d1 = false;
            showClue_d1(1);
            initialze_d1();
            pagination = new Pagination(pageinformation_d1, btnpage1_d1, btnpage2_d1, btnpage3_d1, btnpage4_d1, btnpage5_d1, texbox_page_jump_d1, panel_page_jump_d1,
      page_jump_d1, btnlastpage_d1, btnnextpage_d1, pagesNum_d1, cluecount_d1);
        }

        private void homepic_d1_Click(object sender, EventArgs e)
        {
            while (historyList.Count > 1)
            {
                historyList.RemoveAt(historyList.Count - 1);
            }
            this.btnupload_d2.Visible = false;
            this.pic_upload_d2.Visible = false;
            this.panel_upload_d2.Visible = false;
            historyIndex = 0;
            changeForwardAndBackStyle();

            tabIndex = 0;
            tabPages.SelectedIndex = tabIndex;
            // 判断是否应该显示上传按钮
            if (tabIndex == 1)
            {
                this.btnupload_d2.Visible = true;
                this.pic_upload_d2.Visible = true;
                this.panel_upload_d2.Visible = true;
            }
            else
            {
                this.btnupload_d2.Visible = false;
                this.pic_upload_d2.Visible = false;
                this.panel_upload_d2.Visible = false;
            }

            if (tabIndex == 2)
            {
                // 是上传页面
                this.panel3_clue_d1.Visible = false;
                this.panel2_search_d1.Visible = false;
            }
            else
            {
                this.panel3_clue_d1.Visible = true;
                this.panel2_search_d1.Visible = true;
            }
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
            isSearch_d1 = false;
            showClue_d1(1);
            initialze_d1();
            pagination = new Pagination(pageinformation_d1, btnpage1_d1, btnpage2_d1, btnpage3_d1, btnpage4_d1, btnpage5_d1, texbox_page_jump_d1, panel_page_jump_d1,
      page_jump_d1, btnlastpage_d1, btnnextpage_d1, pagesNum_d1, cluecount_d1);

        }

        private void search_d1_TextChanged(object sender, EventArgs e)
        {
            isSearch_d1 = false;
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            int caseId=BDCloud.common.ClueInfo.caseId;
            /*
            if (judgeAnalysisFiles() >= 3)
            {
                MessageBox.Show("同步解析数不得大于4");
                uploadButton.BackColor = System.Drawing.Color.Red;
                uploadButton.Text = "禁止上传";
                return;
            }
             */
            // Console.WriteLine("----------------aaaaaaaaa"+judgeAnalysisFiles());


            if (String.IsNullOrWhiteSpace(FilePath.Text))
            {
                MessageBox.Show("请选择数据上传");
                return;
            }
                /*
            else if(DataTypeUtils.isMatch(FilePath.Text)){
                MessageBox.Show("请上传邮件，压缩包或者仅含有邮件或者压缩包的文件夹");
                return;
            }
            */
            if (String.IsNullOrWhiteSpace(txtEvName.Text))
            {
                MessageBox.Show("请输入数据名称");
                return;
            }




           #region Action
            Action<double> speedAction = delegate(double speed)
            {
                //Console.Out.WriteLine("Speed: " + speed);
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
            };



            Action<int, int> numAction = delegate(int uploadedNum, int totalNum)
            {
                uploadNumLabel.Text = "已上传数 " + uploadedNum.ToString() + "/" + totalNum.ToString();
            };

            Action<string> logAction = delegate(string log)
            {
                logBox.AppendText(log + "\r\n");
                logBox.SelectionStart = logBox.Text.Length;

                logBox.ScrollToCaret();
            };

            Action<DateTime> timeAction = delegate(DateTime remainTime)
            {
                timeLabel.Text = "剩余时间 " + remainTime.ToString("HH:mm:ss");
            };

            // 读数据库操作  读取本地路径和文件大小
       //      FilePath.Text



            Action<bool> workCompleted = delegate(bool isDone)
            {
                if (isDone)
                {
                    //    //跳转到数据列表页面,当前页为上传页面时执行
                    //    if (tabIndex == 2)
                    //    {

                    //        historyList.Add(1);
                    //        historyIndex++;
                    //        int index = 1;
                    //        tabPages.SelectedIndex = index;
                    //        tabIndex = index;
                    //        changeForwardAndBackStyle();
                    //        this.btnupload_d2.Visible = true;
                    //        this.pic_upload_d2.Visible = true;
                    //        this.panel_upload_d2.Visible = true;
                    //        while (typeList_d1.Items.Count > 0)
                    //            typeList_d1.Items.RemoveAt(typeList_d1.Items.Count - 1);
                    //        typeList_d1.Items.Add("处理状态");
                    //        typeList_d1.Items.Add("上传中");
                    //        typeList_d1.Items.Add("上传完成");
                    //        typeList_d1.Items.Add("解析中");
                    //        typeList_d1.Items.Add("解析完成");
                    //        typeList_d1.SelectedIndex = 0;
                    //        searchDataList("");
                    //    }

                    uploadButton.Text = "上传";
                    updateFilesSuccStatus();      //上传完成，修改finished字段为true
                    insertOnLineNumberDataToDB();//将onlinenumber 表中字段进行更新
                    //  dataTransimit();
                    startAnalysis(FTP.UploaderEx.FtpPathHash, filesBeans[index].getId().ToString());
                    Console.WriteLine("--------------  " + FTP.UploaderEx.FtpPathHash + "   " + filesBeans[index].getId().ToString());
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
                    showSystemStaus();
                }
            };
            #endregion

            if (uploadThread == null || uploadThread.ThreadState == ThreadState.Stopped || uploadThread.ThreadState == ThreadState.Aborted)
            {
                showSystemStaus();
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
                    Action<double> progressAction = delegate(double progress)
                    {
                            uploadProgressBar.Value = (int)(progress * 100);
                            progressLabel.Text = ((int)(progress * 100.0)).ToString() + "%";

                        ProgressCache.add_update_Item(eviId, (int)(progress * 100));
                    };
                    FTP.UploaderEx.uploadPath(FilePath.Text,
                        progressAction,
                        speedAction, timeAction
                        , numAction, logAction, workCompleted);
                });
                uploadThread.Name = "uploadThread";
                uploadThread.Start();
            }
            else if (uploadThread.ThreadState == ThreadState.Suspended)
            {
                showSystemStaus();
                userControl12.isPause = true;
                userControl12.resetPauseButton();

                uploadButton.Text = "暂停";
                uploadThread.Resume();
            }
            else if (uploadThread.ThreadState == ThreadState.Running)
            {
                showSystemStaus();
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

        private void panelClue_d1_MouseEnter(object sender, EventArgs e)
        {
            clueitem1_d1.Controls["panel1"].Controls["panel2"].Visible = false;
            clueitem2_d1.Controls["panel1"].Controls["panel2"].Visible = false;
            clueitem3_d1.Controls["panel1"].Controls["panel2"].Visible = false;
            clueitem4_d1.Controls["panel1"].Controls["panel2"].Visible = false;
            clueitem5_d1.Controls["panel1"].Controls["panel2"].Visible = false;
            clueitem6_d1.Controls["panel1"].Controls["panel2"].Visible = false;
            clueitem7_d1.Controls["panel1"].Controls["panel2"].Visible = false;
            clueitem8_d1.Controls["panel1"].Controls["panel2"].Visible = false;
            clueitem9_d1.Controls["panel1"].Controls["panel2"].Visible = false;
        }



        public void ThreadFunc()
        {
            // 线程停止运行的标志位.
            Boolean done = false;
            // 计数器
            int count = 0;
            while (!done)
            {
                // 休眠1秒.
                Thread.Sleep(1000);
                // 计数器递增
                count++;
                // 输出.
                Console.WriteLine("-------[静态]执行次数：{0}", count, "    dropText's Content:  ", this.FilePath.Text);
            }
        }

        #region combobox DrawItem事件 重新绘制控件 对应蓝底bug -lyk-2018115
        private void typeList_d1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            //e.Graphics.DrawString(typeList_d1.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);

            if (e.Index >= 0)
            {
                string s = typeList_d1.Items[e.Index].ToString();

                Color c = Color.White;
                Color b = Color.DarkGray;

                Font font = new Font("微软雅黑", 9);

                e.DrawBackground();
                e.Graphics.FillRectangle(new SolidBrush(c), e.Bounds);
                e.Graphics.DrawString(s, e.Font, new SolidBrush(b), new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));

                e.DrawFocusRectangle();
            }

        }

        private void dataTypes_d3_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                string s = dataTypes_d3.Items[e.Index].ToString();

                Color c = Color.White;
                Color b = Color.Black;

                Font font = new Font("微软雅黑", 9);

                e.DrawBackground();
                e.Graphics.FillRectangle(new SolidBrush(c), e.Bounds);
                e.Graphics.DrawString(s, e.Font, new SolidBrush(b), new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));

                e.DrawFocusRectangle();
            }
        }

        private void evTypes_d3_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                string s = evTypes_d3.Items[e.Index].ToString();

                Color c = Color.White;
                Color b = Color.Black;

                Font font = new Font("微软雅黑", 9);

                e.DrawBackground();
                e.Graphics.FillRectangle(new SolidBrush(c), e.Bounds);
                e.Graphics.DrawString(s, e.Font, new SolidBrush(b), new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));

                e.DrawFocusRectangle();
            }
        }
        #endregion

        //输入时让搜索框清空 -lyk-2018115
        private void search_d1_KeyDown(object sender, KeyEventArgs e)
        {
            if (search_d1.Text == "请输入要搜索的内容")
            {
                this.search_d1.Text = "";
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

        private void panelTitle_d1_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void panel1_list_d1_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void panelClue_d1_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl11_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl12_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl13_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl14_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl15_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl16_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl17_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl18_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl19_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl110_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void userControl111_MouseClick(object sender, MouseEventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            panelTitle_d1.Select();
        }

        private void search_d1_Leave(object sender, EventArgs e)
        {
            if (!isSearch_d1)
            {
                search_d1.Text = "请输入要搜索的内容";
                panelTitle_d1.Select();
            }
        }

        //登出按钮
        private void max_d1_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            this.Hide();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
            //Application.Exit();
            Application.ExitThread();
        }

    }
}
