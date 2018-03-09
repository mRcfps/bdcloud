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
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            form1 = this;
            load();

            // 分页接口
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
            showClue_d1(1);

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
            ShowSystemStatus();
            ShowSystemStatus();
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

        private void minButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.ifClose = true;

            Application.Exit();
            System.Environment.Exit(0);//终止当前进程并为基础操作系统提供指定的退出代码
        }

        private void right_d1_Click(object sender, EventArgs e)
        {
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

        private void search_d1_Leave(object sender, EventArgs e)
        {
            if (!isSearch_d1)
            {
                search_d1.Text = "请输入要搜索的内容";
                panelTitle_d1.Select();
            }
        }

        // 登出按钮
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
