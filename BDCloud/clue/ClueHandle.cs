using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BDCloud.common;
using BDCloud.clue;
using User;
namespace BDCloud
{
    public partial class Form1 : Form
    {
        int cluecount_d1 = 0;//线索数量
        int cluecount_now_d1 = 0;//当前页的线索数
        int pageNow_d1 = 1;//当前页标记
        bool isSearch_d1 = false;//判断当前搜索是否是根据搜索框内容搜索
        int pagesNum_d1 = 1;//总页数
        int size_d1 = 9;//每页线索最大数
        List<Maticsoft.Model.evidence> listclue_d1 = new List<Maticsoft.Model.evidence>();

        public void initialze_d1()
        {
            pageNow_d1 = 1;//当前页标记
            pagesNum_d1 = 1;//总页数
            if (cluecount_d1 % size_d1 != 0)
                pagesNum_d1 = cluecount_d1 / size_d1 + 1;
            else
                pagesNum_d1 = cluecount_d1 / size_d1;
         //   pageStyleShow();
            showPages_d1(1);
            
            if (!isSearch_d1)//typeList_d1.Items.Count == 1)
            {
                while (typeList_d1.Items.Count > 0)
                    typeList_d1.Items.RemoveAt(typeList_d1.Items.Count - 1);
                typeList_d1.Items.Add("线索类型");
                typeList_d1.Items.Add("走私普通货物");
                typeList_d1.Items.Add("走私废物");
                typeList_d1.Items.Add("走私枪支");
                typeList_d1.Items.Add("走私红油");
                typeList_d1.Items.Add("走私文物");
                typeList_d1.SelectedIndex = 0;
            }

        }
        public void showClue_d1(int pageIndex)
        {
            //获取数据
            String searchContent=search_d1.Text;
            String sql="1=1";
            if (isSearch_d1 && searchContent != null && !"".Equals(searchContent) && !"请输入要搜索的内容".Equals(searchContent)
                || isSearch_d1 && typeList_d1.Text != null && !"线索类型".Equals(typeList_d1.Text))
            {
                String type = typeList_d1.Text;
                if (type.Equals("线索类型"))
                    type = "";
                //if (type.Equals("电子邮件"))
                //    type = "" + (int)evType.E_Mail;
                //if (type.Equals("综合文档"))
                //    type = "" + (int)evType.Integrated_document;
                //if (type.Equals("图片资料"))
                //    type = "" + (int)evType.PicTure;
                //if (type.Equals("电子话单"))
                //    type = "" + (int)evType.Electronic_statement;//or evType LIKE '%" + type + "%'
                if (searchContent != null && !"".Equals(searchContent) && !"请输入要搜索的内容".Equals(searchContent))
                    sql = "threadType LIKE '%" + type + "%' and caseName LIKE '%" + searchContent + "%' or caseNum LIKE '%" + searchContent +
                            "%' or userName LIKE '%" + searchContent + "%'";
                else
                    sql = "threadType LIKE '%" + type + "%'";
            }

            Maticsoft.BLL.caseinfo caseinfo1 = new Maticsoft.BLL.caseinfo();
            DataSet ds = caseinfo1.GetList(sql + " limit " + (pageIndex - 1) * size_d1 + ",9");
          //  int count = evidence1.GetRecordCount("");
            cluecount_d1 = caseinfo1.GetRecordCount(sql);//ds.Tables[0].Rows.Count;
            List<Maticsoft.Model.caseinfo> listclue_d1 = new List<Maticsoft.Model.caseinfo>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Maticsoft.Model.caseinfo cainfo1 = new Maticsoft.Model.caseinfo();
                cainfo1.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                //evi.evName = ds.Tables[0].Rows[i]["evName"].ToString();
                //evi.evType = Convert.ToInt32(ds.Tables[0].Rows[i]["evType"].ToString());
                //evi.evAdmin = ds.Tables[0].Rows[i]["evAdmin"].ToString();
                Maticsoft.Model.caseinfo cainfo = caseinfo1.GetModel(cainfo1.id);
                listclue_d1.Add(cainfo);
            }
            //获取数据——end
            
            //当前页该显示的线索条数
            cluecount_now_d1 = 0;//初始为0
            if (pageIndex * size_d1 <= cluecount_d1)
                cluecount_now_d1 = 9;
            else if ((pageIndex * size_d1 - size_d1) < cluecount_d1)
                cluecount_now_d1 = cluecount_d1 % size_d1;
            //显示每页的线索
            if (cluecount_now_d1 % size_d1 == 0 && cluecount_now_d1 != 0)
            {
                clueitem9_d1.Visible = true;
                setClueControlValue(clueitem9_d1,listclue_d1[8]);
            }
            if (cluecount_now_d1 % size_d1 >= 8 || (cluecount_now_d1 % size_d1 == 0 && cluecount_now_d1 != 0))
            {
                clueitem8_d1.Visible = true;
                setClueControlValue(clueitem8_d1, listclue_d1[7]);
            }
            if (cluecount_now_d1 % size_d1 >= 7 || (cluecount_now_d1 % size_d1 == 0 && cluecount_now_d1 != 0))
            {
                clueitem7_d1.Visible = true;
                setClueControlValue(clueitem7_d1, listclue_d1[6]);
            }
            if (cluecount_now_d1 % size_d1 >= 6 || (cluecount_now_d1 % size_d1 == 0 && cluecount_now_d1 != 0))
            {
                clueitem6_d1.Visible = true;
                setClueControlValue(clueitem6_d1, listclue_d1[5]);
            }
            if (cluecount_now_d1 % size_d1 >= 5 || (cluecount_now_d1 % size_d1 == 0 && cluecount_now_d1 != 0))
            {
                clueitem5_d1.Visible = true;
                setClueControlValue(clueitem5_d1, listclue_d1[4]);
            }
            if (cluecount_now_d1 % size_d1 >= 4 || (cluecount_now_d1 % size_d1 == 0 && cluecount_now_d1 != 0))
            {
                clueitem4_d1.Visible = true;
                setClueControlValue(clueitem4_d1, listclue_d1[3]);
            }
            if (cluecount_now_d1 % size_d1 >= 3 || (cluecount_now_d1 % size_d1 == 0 && cluecount_now_d1 != 0))
            {
                clueitem3_d1.Visible = true;
                setClueControlValue(clueitem3_d1, listclue_d1[2]);
            }
            if (cluecount_now_d1 % size_d1 >= 2 || (cluecount_now_d1 % size_d1 == 0 && cluecount_now_d1 != 0))
            {
                clueitem2_d1.Visible = true;
                setClueControlValue(clueitem2_d1, listclue_d1[1]);
            }
            if (cluecount_now_d1 % size_d1 >= 1 || (cluecount_now_d1 % size_d1 == 0 && cluecount_now_d1 != 0))
            {
                clueitem1_d1.Visible = true;
                setClueControlValue(clueitem1_d1, listclue_d1[0]);
            }          
        }
        /// <summary>
        /// 给自定义控件赋值
        /// </summary>
        /// <param name="usercontrol"></param>
        /// <param name="evi"></param>
        private void setClueControlValue(CluePanel.UserControl1 usercontrol,Maticsoft.Model.caseinfo cainfo1)
        {
            usercontrol.Controls["lblClueNum"].Text = "" + cainfo1.caseNum;
            usercontrol.Controls["label3"].Text = cainfo1.caseName;
            usercontrol.Controls["label4"].Text = "" + cainfo1.threadType;
            Maticsoft.Model.department depart1 = new Maticsoft.Model.department();
            Maticsoft.BLL.department depart2 = new Maticsoft.BLL.department();
            if (cainfo1.userPartment != null && !"".Equals(cainfo1.userPartment))
            {
                depart1 = depart2.GetModel(Convert.ToInt32(cainfo1.userPartment));
            }
            usercontrol.Controls["label5"].Text = depart1.departmentName;
            usercontrol.Controls["label6"].Text = cainfo1.userName;
            usercontrol.Controls["panel1"].Controls["label7"].Text = "" + cainfo1.id;
            
        }
        /**
         * 隐藏控件
         * */
        public void hideControl_d1()
        {
            clueitem1_d1.Visible = false;
            clueitem2_d1.Visible = false;
            clueitem3_d1.Visible = false;
            clueitem4_d1.Visible = false;
            clueitem5_d1.Visible = false;
            clueitem6_d1.Visible = false;
            clueitem7_d1.Visible = false;
            clueitem8_d1.Visible = false;
            clueitem9_d1.Visible = false;
        }
        /**
         * 下方页码显示
         * 分页
         * pageIndex:当前页
         * */
        public void showPages_d1(int pageIndex)
        {
            //隐藏所有线索控件后显示当前页该显示的线索数量
            hideControl_d1();
            showClue_d1(pageIndex);     
        }
 
        /// <summary>
        /// 线索控件点击上传事件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void clueUpload_d1(object sender, EventArgs e)
        {
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
            //将线索id传入上传页面

            ClueInfo.id = Convert.ToInt32(((Button)sender).Parent.Parent.Parent.Controls["panel1"].Controls["label7"].Text);
            while (historyList.Count > 1)
            {
                historyList.RemoveAt(historyList.Count - 1);
            }
            historyIndex = 1;
            historyList.Add(2);
            changeForwardAndBackStyle();


            //获取数据
            Maticsoft.DAL.caseinfo caseinfo1 = new Maticsoft.DAL.caseinfo();
            DataSet ds = caseinfo1.GetList("id=" + ClueInfo.id);
            ClueInfo.comment = ds.Tables[0].Rows[0]["susItem"].ToString();
            ClueInfo.caseId = ClueInfo.id;// Convert.ToInt32(ds.Tables[0].Rows[0]["caseID"].ToString());
            initUploadHandlePage();
        }
        /// <summary>
        /// 线索控件线索详情点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void clueContent_d1(object sender, EventArgs e)
        {
            //获取数据
            Maticsoft.DAL.caseinfo caseinfo1 = new Maticsoft.DAL.caseinfo();
            int id = Convert.ToInt32(((PictureBox)sender).Parent.Controls["panel1"].Controls["label7"].Text);
            DataSet ds = caseinfo1.GetList("id=" + id);
            ClueInfo.comment = ds.Tables[0].Rows[0]["susItem"].ToString();
            ClueInfo.caseId = id;
            //显示窗口
            ClueDetailsForm clueDetailForm = new ClueDetailsForm();
            clueDetailForm.StartPosition = FormStartPosition.CenterParent;
            clueDetailForm.Controls["textBox1"].Text = ClueInfo.comment;
            clueDetailForm.ShowDialog();
        }
        //点击自定义控件的事件，进入数据列表页面
        public void clueList_d1(object sender, EventArgs e)
        {
            search_d1.Text = "请输入要搜索的内容";
            btnupload_d2.Visible = true;
            pic_upload_d2.Visible = true;
            panel_upload_d2.Visible = true;
            tabPages.SelectedIndex = 1;
            tabIndex = 1;
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

            //将线索id传入数据列表页面
            Maticsoft.DAL.caseinfo caseinfo1 = new Maticsoft.DAL.caseinfo();
            int id = Convert.ToInt32(((Panel)sender).Parent.Controls["panel1"].Controls["label7"].Text);
            DataSet ds = caseinfo1.GetList("id=" + id);
            ClueInfo.comment = ds.Tables[0].Rows[0]["susItem"].ToString();
            ClueInfo.caseId = id;
            ClueInfo.id = id;           
            while (historyList.Count > 1)
            {
                historyList.RemoveAt(historyList.Count-1);
            }
            historyIndex = 1;
            historyList.Add(1);
            changeForwardAndBackStyle();

            searchDataList("");
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
        }
        //点击自定义控件的事件，鼠标进入自定义控件显示上传按钮
        public void clueMouseEnter_d1(object sender, EventArgs e)
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
            ((Panel)sender).Controls["panel2"].Visible=true;//显示上传按钮
            clueitem1_d1.Controls["panel1"].BackgroundImage = null;
            clueitem2_d1.Controls["panel1"].BackgroundImage = null;
            clueitem3_d1.Controls["panel1"].BackgroundImage = null;
            clueitem4_d1.Controls["panel1"].BackgroundImage = null;
            clueitem5_d1.Controls["panel1"].BackgroundImage = null;
            clueitem6_d1.Controls["panel1"].BackgroundImage = null;
            clueitem7_d1.Controls["panel1"].BackgroundImage = null;
            clueitem8_d1.Controls["panel1"].BackgroundImage = null;
            clueitem9_d1.Controls["panel1"].BackgroundImage = null;
            ((Panel)sender).BackgroundImage = global::BDCloud.Properties.Resources.clueItemBackground;//增加阴影效果
            clueitem1_d1.BackColor = System.Drawing.Color.White;
            clueitem2_d1.BackColor = System.Drawing.Color.White;
            clueitem3_d1.BackColor = System.Drawing.Color.White;
            clueitem4_d1.BackColor = System.Drawing.Color.White;
            clueitem5_d1.BackColor = System.Drawing.Color.White;
            clueitem6_d1.BackColor = System.Drawing.Color.White;
            clueitem7_d1.BackColor = System.Drawing.Color.White;
            clueitem8_d1.BackColor = System.Drawing.Color.White;
            clueitem9_d1.BackColor = System.Drawing.Color.White;
            ((Panel)sender).Parent.BackColor = System.Drawing.Color.Transparent;//背景变为透明

        }
        /// <summary>
        /// 改变前进后退按钮样式
        /// </summary>
        public void changeForwardAndBackStyle()
        {
            if (historyIndex > 0)
                left_d1.Image = BDCloud.Properties.Resources.left_selected;
            else
                left_d1.Image = BDCloud.Properties.Resources.left2;
            if (historyIndex < historyList.Count - 1)
                right_d1.Image = BDCloud.Properties.Resources.right_selected;
            else
                right_d1.Image = BDCloud.Properties.Resources.right2;

        }
        /// <summary>
        /// 系统的存储状态，当前上传人数,用户姓名和等级显示
        /// </summary>
        public void showSystemStaus()
        {
            //上传人数显示
            Maticsoft.BLL.onlinenumber onlines = new Maticsoft.BLL.onlinenumber();
            List<Maticsoft.Model.onlinenumber> onlineList1 = onlines.GetModelList("onlinestatus=1");
            //username去重
            for (int i = 0; i < onlineList1.Count; i++)
            {
                for (int j = onlineList1.Count - 1; j > i; j--)
                {
                    if (onlineList1[i].username.Equals(onlineList1[j].username))
                    {
                        onlineList1.RemoveAt(j);
                        //break;
                    }
                }
            }
            labelUpload.Text = "当前有" + onlineList1.Count + "人正在上传数据";
            //系统存储状态显示
            Maticsoft.BLL.clutercapacity bCluter1 = new Maticsoft.BLL.clutercapacity();
            Maticsoft.Model.clutercapacity mCluter1 = bCluter1.GetModelList("1=1 group by id desc limit 1")[0];
            String total = mCluter1.totalCapacity;
            String used = mCluter1.usedCapacity;
            float totalNum = 1;
            float usedNum = 0;
            //将系统总量转换为GB表示，用于计算进度条
            if (total.Contains("TB"))
            {
                String temp = total.Split("TB".ToArray())[0];
                totalNum = float.Parse(temp) * 1024;
            }
            else if(total.Contains("GB"))
            {
                String temp = total.Split("GB".ToArray())[0];
                totalNum = float.Parse(temp);
            }
            //将已使用量转换为GB显示，用于计算进度条比例
            if (used.Contains("TB"))
            {
                String temp = used.Split("TB".ToArray())[0];
                usedNum = float.Parse(temp) * 1024;
            }
            else if (used.Contains("GB"))
            {
                String temp = used.Split("GB".ToArray())[0];
                usedNum = float.Parse(temp);
            }
            processBar_SystemStaus1.Text = used + "/" + total;
            processBar_SystemStaus1.Value =(usedNum / totalNum) * 100;

            //用户姓名和等级显示
            labelUser.Text=UserInfo.username;
            String userRolle = UserInfo.userRolle;
            Maticsoft.BLL.role roleB = new Maticsoft.BLL.role();
            Maticsoft.Model.role roleM = roleB.GetModel(Convert.ToInt32(UserInfo.userRolle));
            labelPost.Text = roleM.roleName;
        }
        /// <summary>
        /// 当鼠标移动到线索详情上时鼠标换成手
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void clueMouseHover_d1(object sender, EventArgs e)
        {
            ((PictureBox)sender).Cursor = System.Windows.Forms.Cursors.Hand;
            // this.clueitem2_d1.Cursor = System.Windows.Forms.Cursors.Hand;
        }
    }
}
