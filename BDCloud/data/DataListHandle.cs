using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using BDCloud.common;

namespace BDCloud
{
    public partial class Form1 : Form
    {
        //UI线程
        public Thread UIthread;

        private int nowDataPage; //当前页数。默认为1
        private int totalPage;   //总页数
        private int numPerPage; //每页条数
        private int totalDataNum;   //总条数
        public static bool initDataList = true;
        //原始位置
        public const int originalBtn = 900;
        public const int originalPpj = 932;
        public const int originalPj = 979;

        //初始化页码按钮对应页数
        private int pageButtonValue1_d2 = 1;
        private int pageButtonValue2_d2 = 2;
        private int pageButtonValue3_d2 = 3;
        private int pageButtonValue4_d2 = 4;
        private int pageButtonValue5_d2 = 5;

        // 线程停止运行的标志位.
        //private static Boolean done = false;

        //存放进度条空间和对应id
        public List<string> aaaa = new List<string>();
        public List<DataLineA.UserControl1> bbbb = new List<DataLineA.UserControl1>();

        //存放每个控件对应的内容
        public List<Maticsoft.Model.evidence> DataList = new List<Maticsoft.Model.evidence>();

        /// <summary>  
        /// 根据条件查询数据
        /// </summary>
        /// <param name="searchQuery">查询条件</param> 
        /// <returns></returns>
        public void searchDataList(string searchQuery)
        {
            string type = "";
            if (typeList_d1.Text != "处理状态") 
            {
                switch (typeList_d1.Text) 
                {
                    case ("上传中"):
                        type = " and finished = ''";
                        break;
                    case("上传完成"):
                        type = " and finished = true";
                        break;
                    case("解析中"):
                        type = " and (finished = 1 or finished = 2 or finished = 3 or finished = 4)";
                        break;
                    case("解析完成"):
                        type = " and finished = 5";
                        break;
                    case ("解析失败"):
                        type = " and finished = 0";
                        break;
                    case ("上传失败"):
                        type = " and finished = -1";
                        break;
                }
            }

            string sql = "caseID = " + ClueInfo.caseId.ToString() + type;

            if (isSearch_d1 && searchQuery != null && !"".Equals(searchQuery) && !"请输入要搜索的内容".Equals(searchQuery))
                sql = sql + " and (evName LIKE '%" + searchQuery + "%' or evAdmin LIKE '%" + searchQuery + "%')";

            sql = sql + " order by id desc";

            init(sql);

            setDataListByPage(nowDataPage);
        }

        /// <summary>  
        /// 初始化
        /// </summary>
        /// <param name="initQuery">查询语句</param> 
        /// <returns></returns> 
        private void init(string initQuery)
        {
            nowDataPage = 1;
            numPerPage = 10;
            Maticsoft.BLL.evidence evidence1 = new Maticsoft.BLL.evidence();

            string searchQuery = initQuery;//初始化条件（查询条件）
            DataSet ds = evidence1.GetList(searchQuery);

            totalDataNum = ds.Tables[0].Rows.Count;
            if (totalDataNum == 0 || totalDataNum == numPerPage)
            {
                totalPage = 1;
                //MessageBox.Show("无相应记录。");
            }
            else
            {
                if (totalDataNum % numPerPage == 0)
                {
                    totalPage = ds.Tables[0].Rows.Count / numPerPage;
                }
                else 
                {
                    totalPage = ds.Tables[0].Rows.Count / numPerPage + 1;
                }               
            }
            //设置文本内容
            string pageText = "总共" + totalDataNum + "条，当前" + nowDataPage + "/" + totalPage + "页";
            pageinformation_d2.Text = pageText;

            //根据页数设置按钮位置
            switch (totalPage)
            {
                case 1:
                    btnpage2_d2.Visible = false;
                    btnpage3_d2.Visible = false;
                    btnpage4_d2.Visible = false;
                    btnpage5_d2.Visible = false;

                    btnnextpage_d2.Left = originalBtn - 4 * 25;
                    panel_page_jump_d2.Left = originalPpj - 4 * 25;
                    page_jump_d2.Left = originalPj - 4 * 25;

                    break;
                case 2:
                    btnpage2_d2.Visible = true;
                    btnpage3_d2.Visible = false;
                    btnpage4_d2.Visible = false;
                    btnpage5_d2.Visible = false;

                    btnnextpage_d2.Left = originalBtn - 3 * 25;
                    panel_page_jump_d2.Left = originalPpj - 3 * 25;
                    page_jump_d2.Left = originalPj - 3 * 25;

                    break;
                case 3:
                    btnpage2_d2.Visible = true;
                    btnpage3_d2.Visible = true;
                    btnpage4_d2.Visible = false;
                    btnpage5_d2.Visible = false;

                    btnnextpage_d2.Left = originalBtn - 2 * 25;
                    panel_page_jump_d2.Left = originalPpj - 2 * 25;
                    page_jump_d2.Left = originalPj - 2 * 25;
                    break;
                case 4:
                    btnpage2_d2.Visible = true;
                    btnpage3_d2.Visible = true;
                    btnpage4_d2.Visible = true;
                    btnpage5_d2.Visible = false;

                    btnnextpage_d2.Left = originalBtn - 25;
                    panel_page_jump_d2.Left = originalPpj - 25;
                    page_jump_d2.Left = originalPj - 25;
                    break;
                default:
                    btnpage2_d2.Visible = true;
                    btnpage3_d2.Visible = true;
                    btnpage4_d2.Visible = true;
                    btnpage5_d2.Visible = true;

                    btnnextpage_d2.Left = originalBtn;
                    panel_page_jump_d2.Left = originalPpj;
                    page_jump_d2.Left = originalPj;
                    break;
            }
            changePageValue();
            reSetPage();

        }

        /// <summary>  
        /// 数据绑定方法
        /// </summary>  
        /// <returns></returns>
        private void setDataListValue(DataLineA.UserControl1 ctl, Maticsoft.Model.evidence evi) //封装数据绑定(绑定一条)方法
        {
            //初始化文本数据
            ctl.Controls["label1"].Text = evi.addTime;
            ctl.Controls["label2"].Text = evi.evAdmin;
            ctl.Controls["label3"].Text = evi.evSize + "KB";

            switch (evi.evType)
            {
                case 1:
                    ctl.Controls["label4"].Text = "电子邮件";
                    break;
                case 2:
                    ctl.Controls["label4"].Text = "综合文档";
                    break;
                case 3:
                    ctl.Controls["label4"].Text = "电子话单";
                    break;
                case 4:
                    ctl.Controls["label4"].Text = "手机取证";
                    break;
                case 5:
                    ctl.Controls["label4"].Text = "黑客数据";
                    break;
                case 6:
                    ctl.Controls["label4"].Text = "图片资料";
                    break;
                default :
                    ctl.Controls["label4"].Text = "未知类型";
                    break;

            }
            if (evi.evName.Length>14)
            {
                evi.evName = evi.evName.Substring(0,14) + "..";
            }
            ctl.Controls["label5"].Text = evi.evName;
            ctl.Controls["label6"].Text = evi.uploadNum;
            ctl.Controls["label_localpath"].Text = evi.localPath;
            ctl.Controls["label_id"].Text = evi.id.ToString();
            aaaa.Add(evi.id.ToString());
            bbbb.Add(ctl);

            //控件可见
            ctl.Visible = true;

            ctl.resetDeleteButton();
        }

        /// <summary>  
        /// 根据页数设置数据（翻页按钮专用）
        /// </summary>  
        /// <returns></returns>
        private void setDataListByPage(int page)
        {
            //清空控件list
            aaaa.Clear();
            bbbb.Clear();
            if (!initDataList && null != UIthread)//如果不是首次加载页面，则杀死ui线程
            {
                UIthread.Abort();
                Console.WriteLine("-------[UI更新进度线程杀死]  UIthread Alive：{0}--------");
            }
            else
            {
                initDataList = false;
            }

            string startP = ((page - 1) * numPerPage).ToString();//开始条数

            string id = ClueInfo.caseId.ToString();

            string type = "";
            if (typeList_d1.Text != "处理状态")
            {
                switch (typeList_d1.Text)
                {
                    case ("上传中"):
                        type = " and finished = ''";
                        break;
                    case ("上传完成"):
                        type = " and finished = true";
                        break;
                    case ("解析中"):
                        type = " and (finished = 1 or finished = 2 or finished = 3 or finished = 4)";
                        break;
                    case ("解析完成"):
                        type = " and finished = 5";
                        break;
                    case ("解析失败"):
                        type = " and finished = 0";
                        break;
                    case ("上传失败"):
                        type = " and finished = -1";
                        break;
                }
            }

            string serchQuery = "caseID=" + id + type;

            if (isSearch_d1 && search_d1.Text != null && !"".Equals(search_d1.Text) && !"请输入要搜索的内容".Equals(search_d1.Text))//如果有搜索条件
            {
                serchQuery = serchQuery + " and (evName LIKE '%" + search_d1.Text + "%' or evAdmin LIKE '%" + search_d1.Text + "%') order by id desc" + " limit " + startP + "," + numPerPage;
            }
            else
            {
                serchQuery = serchQuery + " order by id desc" + " limit " + startP + "," + numPerPage;
            }

            Maticsoft.BLL.evidence evidence1 = new Maticsoft.BLL.evidence();
            DataSet ds = evidence1.GetList(serchQuery);

            //由于下foreach遍历获取到的Control控件是逆序的，采用栈的方式重新反向排序
            Stack<Control> Cons = new Stack<Control>();
            foreach (Control ctl in this.Controls["tabPages"].Controls["tabDataList"].Controls)//遍历相应容器中的所有控件
            {
                if (ctl as DataLineA.UserControl1 != null)//如果是数据列
                {
                    
                    ctl.Visible = false;
                    Cons.Push(ctl);
                }
            }

            //遍历栈
            int i = 0;
            foreach (DataLineA.UserControl1 c in Cons)
            {
                Maticsoft.Model.evidence resEvi = new Maticsoft.Model.evidence();

                if (i < ds.Tables[0].Rows.Count)
                {
                    resEvi.addTime = ds.Tables[0].Rows[i]["addTime"].ToString().Split(' ')[0];
                    resEvi.evAdmin = ds.Tables[0].Rows[i]["evAdmin"].ToString();
                    resEvi.evSize = ds.Tables[0].Rows[i]["evSize"].ToString();
                    resEvi.evType = Convert.ToInt32(ds.Tables[0].Rows[i]["evType"].ToString());
                    resEvi.evName = ds.Tables[0].Rows[i]["evName"].ToString();
                    resEvi.uploadNum = ds.Tables[0].Rows[i]["uploadNum"].ToString();
                    resEvi.finished = ds.Tables[0].Rows[i]["finished"].ToString();
                    resEvi.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                    resEvi.localPath = ds.Tables[0].Rows[i]["localpath"].ToString();
                    setDataListValue(c, resEvi);
                }
                else
                {
                    Cons.Clear();
                    break;
                }

                if (i < DataList.Count())
                {
                    DataList[i] = resEvi;
                }
                else 
                {
                    DataList.Add(resEvi);
                }
                
                i++;
            }
        
            //重新开启新的线程
            UIthread = new Thread(new ThreadStart(UIthreadRefresh));
            UIthread.Start();

            //设置文本内容
            string pageTextNew = "总共" + totalDataNum + "条，当前" + nowDataPage + "/" + totalPage + "页";
            pageinformation_d2.Text = pageTextNew;
        }

        /// <summary>  
        /// 改变页码按钮value值
        /// </summary>  
        /// <returns></returns>
        public void changePageValue()
        {
            if (nowDataPage > 2 && (totalPage - nowDataPage > 1))//当当前页面值在范围内，居中赋值
            {
                pageButtonValue1_d2 = nowDataPage - 2;
                pageButtonValue2_d2 = nowDataPage - 1;
                pageButtonValue3_d2 = nowDataPage;
                pageButtonValue4_d2 = nowDataPage + 1;
                pageButtonValue5_d2 = nowDataPage + 2;

            }
            else if (nowDataPage <= 2) //特殊情况1，页首，从头赋值，多余的按钮会隐藏，如果页数少也在此情况
            {
                pageButtonValue1_d2 = 1;
                pageButtonValue2_d2 = 2;
                pageButtonValue3_d2 = 3;
                pageButtonValue4_d2 = 4;
                pageButtonValue5_d2 = 5;
            }
            else if (totalPage - nowDataPage <= 1 && totalPage >= 5) //特殊情况2，页尾，页数大于5页才考虑，从尾部赋值
            {
                pageButtonValue1_d2 = totalPage - 4;
                pageButtonValue2_d2 = totalPage - 3;
                pageButtonValue3_d2 = totalPage - 2;
                pageButtonValue4_d2 = totalPage - 1;
                pageButtonValue5_d2 = totalPage;
            }
        }

        /// <summary>  
        /// 设置页码按钮颜色文字
        /// </summary>  
        /// <returns></returns>
        public void reSetPage()
        {
            btnpage1_d2.BackColor = Color.White;
            btnpage2_d2.BackColor = Color.White;
            btnpage3_d2.BackColor = Color.White;
            btnpage4_d2.BackColor = Color.White;
            btnpage5_d2.BackColor = Color.White;

            if ((totalPage - nowDataPage) <= 2 || nowDataPage <= 2)//当当前页在收尾时，更具当前页值寻找对应的页数按钮赋值
            {

                if (pageButtonValue1_d2 == nowDataPage)
                    btnpage1_d2.BackColor = Color.FromArgb(0, 173, 232);
                else if (pageButtonValue2_d2 == nowDataPage)
                    btnpage2_d2.BackColor = Color.FromArgb(0, 173, 232);
                else if (pageButtonValue3_d2 == nowDataPage)
                    btnpage3_d2.BackColor = Color.FromArgb(0, 173, 232);
                else if (pageButtonValue4_d2 == nowDataPage)
                    btnpage4_d2.BackColor = Color.FromArgb(0, 173, 232);
                else if (pageButtonValue5_d2 == nowDataPage)
                    btnpage5_d2.BackColor = Color.FromArgb(0, 173, 232);

            }
            else //否则居中赋值
            {

                btnpage3_d2.BackColor = Color.FromArgb(0, 173, 232);

            }
            //改变相应的页码
            btnpage1_d2.Text = pageButtonValue1_d2.ToString();
            btnpage2_d2.Text = pageButtonValue2_d2.ToString();
            btnpage3_d2.Text = pageButtonValue3_d2.ToString();
            btnpage4_d2.Text = pageButtonValue4_d2.ToString();
            btnpage5_d2.Text = pageButtonValue5_d2.ToString();
        }

        /// <summary>  
        /// 线程方法：刷新ui
        /// </summary>  
        /// <returns></returns>
        public void UIthreadRefresh()
        {
            Console.WriteLine("-------[UI更新进度线程存活]  UIthread Alive：{1}--------");
            while (true)
            {
                for (int i = 0; i < aaaa.Count; i++)
                {
                    ThreadGetData(bbbb[i], aaaa[i]);
                }
                Thread.Sleep(200);
            }
        }

        /// <summary>  
        /// 根据id获取数据库数据更新控件
        /// </summary>  
        /// <returns></returns>
        public void ThreadGetData(DataLineA.UserControl1 ctl, string evId)
        {
            //查询数据库，赋值给进度条
            try
            {
                string serchQuery = "id=" + evId;
                Maticsoft.BLL.evidence evidence1 = new Maticsoft.BLL.evidence();
                DataSet ds = evidence1.GetList(serchQuery);

                if (ds.Tables[0].Rows.Count != 0)
                {

                    if (!(ds.Tables[0].Rows[0]["finished"].ToString() == null || "".Equals(ds.Tables[0].Rows[0]["finished"].ToString()) || ds.Tables[0].Rows[0]["finished"].ToString() == "-1")) //表示已上传完
                    {
                        ctl.isFinished = true;
                        switch (ds.Tables[0].Rows[0]["finished"].ToString())
                        {
                            case ("1"):
                                ctl.extent = 20;
                                break;
                            case ("2"):
                                ctl.extent = 45;
                                break;
                            case ("3"):
                                ctl.extent = 80;
                                break;
                            case ("4"):
                                ctl.extent = 99;
                                break;
                            case ("5"):
                                ctl.extent = 100;
                                break;
                            case ("true"):
                                break;
                            default:
                                ctl.extent = 0;
                                break;
                        }
                        ctl.Controls["label3"].Text = ds.Tables[0].Rows[0]["evSize"].ToString() + "KB";
                        ctl.Controls["label6"].Text = ds.Tables[0].Rows[0]["uploadNum"].ToString();
                    }
                    else if (ds.Tables[0].Rows[0]["finished"].ToString() == "-1")//上传失败
                    {
                        ctl.isFinished = false;
                        //ctl.isPause = true;
                        //ctl.isFailed = true;
                        ctl.extent = (int)(ProgressCache.getProgress(Convert.ToInt32(evId)));
                        if (evId == cur_upload_eviId.ToString())
                        {
                            ((PictureBox)ctl.Controls["pictureBox1"]).Image = Properties.Resources.pause;
                        }
                        else
                        {
                            ((PictureBox)ctl.Controls["pictureBox1"]).Image = Properties.Resources.start;
                        }
                    }
                    else //未上传完
                    {
                        ctl.isFinished = false;
                        ctl.isFailed = false;
                        if (this.uploadMessage.Text != null && this.progressLabel.Text != "")
                        {
                            //      Console.WriteLine(this.progressLabel.Text);
                            //string text = this.progressLabel.Text.Split('%')[0];
                            //ctl.extent = Convert.ToInt32(text);
                            ctl.extent = (int)(ProgressCache.getProgress(Convert.ToInt32(evId)));
                        }
                        else
                        {
                            ctl.extent = 0;
                        }
                        if (evId == cur_upload_eviId.ToString())
                        {
                            ((PictureBox)ctl.Controls["pictureBox1"]).Image = Properties.Resources.pause;
                        }
                        else
                        {
                            ((PictureBox)ctl.Controls["pictureBox1"]).Image = Properties.Resources.start;
                        }
                    }
                }
                else
                {
                    ctl.init();
                }
                ctl.resetProcessBar();
            }

            catch (System.ArgumentNullException e)
            {
                Console.WriteLine("________________Exception_third___________________");
            }



        }

        #region http请求调用接口
        /// <summary>  
        /// 调用接口开始解析
        /// </summary>  
        /// <returns></returns>     
        public static void startAnalysis(string linuxPath,string eviId)
        {
            //MessageBox.Show("调用接口");

            string query = "http://172.16.103.252:8080/myweb/sendRequest?linuxPath=" + linuxPath + "&eviId=" + eviId;
            HttpWebResponse response = CreateGetHttpResponse1(query);
            //获取流  
            Stream streamResponse = response.GetResponseStream();
            //使用UTF8解码  
            StreamReader streanReader = new StreamReader(streamResponse, Encoding.UTF8);
           // string retString = streanReader.ReadToEnd();

            //Console.WriteLine(retString);

            /*
            if (Convert.ToInt32(retString) == 0)
            {
                Console.WriteLine("colonyAnalysis-------------------  数据未解析!!!!!!!!!");
            }
            else
            {
                Console.WriteLine("colonyAnalysis-------------------  数据解析成功!!!!!!!!!");
            }*/
        }

        /// <summary>  
        ///  创建GET的HTTP请求
        /// </summary>  
        /// <returns></returns>    
        public static HttpWebResponse CreateGetHttpResponse1(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "*/*";
            //      request.Connection = "Keep-Alive";   
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1;SV1)";
            request.ContentType = "text/html;chartset=UTF-8";
            //         request.Timeout = 2000;
            request.Method = "GET";
            return (HttpWebResponse)request.GetResponse();
        }
        #endregion

        /// <summary>  
        ///  暂停按钮委托方法
        /// </summary>  
        /// <returns></returns>
        public void pauseUpload(object sender, EventArgs e) 
        {
            Maticsoft.BLL.evidence evidence_update = new Maticsoft.BLL.evidence();
            DataSet ds = evidence_update.GetList("1=1 ORDER BY id desc LIMIT 1");
            int eviId = Convert.ToInt32((sender as Control).Parent.Controls["label_id"].Text);
            string path = (sender as Control).Parent.Controls["label_localpath"].Text;
            if (eviId == cur_upload_eviId) cur_upload_eviId = -1;
            else cur_upload_eviId = eviId;
            if (uploadThread == null || uploadThread.ThreadState == ThreadState.Stopped || uploadThread.ThreadState == ThreadState.Aborted)
            {
                 uploadThread = new Thread(() =>
                {
                    Ftp.Uploader.uploadPath(path);
                });
                uploadThread.Name = "uploadThread";
                uploadThread.Start();
            }
            else if (uploadThread.ThreadState == ThreadState.Suspended)
            {
                uploadThread.Resume();
            }
            else if (uploadThread.ThreadState == ThreadState.Running)
            {
                uploadThread.Suspend();
            }
        }

        #region 10个控件对应的删除按钮委托方法（说明：因事件委托无法传多余的参数并且无法动态获取每个控件的id，故采用笨办法写10个重复的方法分别给10个控件使用）
        /// <summary>  
        ///  删除按钮委托方法
        /// </summary>  
        /// <returns></returns>
        public void deleteUpload(object sender, EventArgs e) 
        {
            //中断上传线程
            if (uploadThread != null)
            {
                uploadThread.Suspend();

                //uploadThread.Abort();
            }

            //查询路径和id
            string filePath = "";
            int id = DataList[0].id;
            foreach (FilesUploadBean filesBean in filesBeans) 
            {
                if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString())) 
                {
                    filePath = filesBean.evPathBean;
                    id = filesBean.id;
                    break;
                }
            }

            //删除已上传的文件
            if (filePath != "") 
            {
                Ftp.Uploader.deletePath(filePath);
                
                //初始化上传页面
                filesBeans[index].setInit(false);
                Ftp.Uploader.reset();
                initUploadHandlePage();
            }
                

            //删除数据库中的数据
            if (id != null) 
            {
                Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                evidence.Delete(id);
                //MessageBox.Show("fuck"+id);
            }           

            //重新加载页面
            searchDataList("");
        }

        public void deleteUpload1(object sender, EventArgs e)
        {
            string filePath = "";
            int id = DataList[1].id;

            if ("-1".Equals(DataList[1].finished))
            {

                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }
            else 
            {
                //中断上传线程
                if (uploadThread != null)
                {
                    uploadThread.Suspend();
                    //uploadThread.Abort();
                }

                //查询路径和id
                foreach (FilesUploadBean filesBean in filesBeans)
                {
                    if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString()))
                    {
                        filePath = filesBean.evPathBean;
                        id = filesBean.id;
                        break;
                    }
                }

                //删除已上传的文件
                if (filePath != "")
                {
                    Ftp.Uploader.deletePath(filePath);
                    //MessageBox.Show("fuck" + id + " " + filePath);

                    //初始化上传页面
                    filesBeans[index].setInit(false);
                    Ftp.Uploader.reset();
                    initUploadHandlePage();
                }


                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }

            //重新加载页面
            searchDataList("");
        }

        public void deleteUpload2(object sender, EventArgs e)
        {
            string filePath = "";
            int id = DataList[2].id;

            if ("-1".Equals(DataList[2].finished))
            {

                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }
            else
            {
                //中断上传线程
                if (uploadThread != null)
                {
                    uploadThread.Suspend();
                    //uploadThread.Abort();
                }

                //查询路径和id
                foreach (FilesUploadBean filesBean in filesBeans)
                {
                    if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString()))
                    {
                        filePath = filesBean.evPathBean;
                        id = filesBean.id;
                        break;
                    }
                }

                //删除已上传的文件
                if (filePath != "")
                {
                    Ftp.Uploader.deletePath(filePath);
                    //MessageBox.Show("fuck" + id + " " + filePath);

                    //初始化上传页面
                    filesBeans[index].setInit(false);
                    Ftp.Uploader.reset();
                    initUploadHandlePage();
                }


                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }

            //重新加载页面
            searchDataList("");
        }

        public void deleteUpload3(object sender, EventArgs e)
        {
            string filePath = "";
            int id = DataList[3].id;

            if ("-1".Equals(DataList[3].finished))
            {

                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }
            else
            {
                //中断上传线程
                if (uploadThread != null)
                {
                    uploadThread.Suspend();
                    //uploadThread.Abort();
                }

                //查询路径和id
                foreach (FilesUploadBean filesBean in filesBeans)
                {
                    if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString()))
                    {
                        filePath = filesBean.evPathBean;
                        id = filesBean.id;
                        break;
                    }
                }

                //删除已上传的文件
                if (filePath != "")
                {
                    Ftp.Uploader.deletePath(filePath);
                    //MessageBox.Show("fuck" + id + " " + filePath);

                    //初始化上传页面
                    filesBeans[index].setInit(false);
                    Ftp.Uploader.reset();
                    initUploadHandlePage();
                }


                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }

            //重新加载页面
            searchDataList("");
        }

        public void deleteUpload4(object sender, EventArgs e)
        {
            string filePath = "";
            int id = DataList[4].id;

            if ("-1".Equals(DataList[4].finished))
            {

                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }
            else
            {
                //中断上传线程
                if (uploadThread != null)
                {
                    uploadThread.Suspend();
                    //uploadThread.Abort();
                }

                //查询路径和id
                foreach (FilesUploadBean filesBean in filesBeans)
                {
                    if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString()))
                    {
                        filePath = filesBean.evPathBean;
                        id = filesBean.id;
                        break;
                    }
                }

                //删除已上传的文件
                if (filePath != "")
                {
                    Ftp.Uploader.deletePath(filePath);
                    //MessageBox.Show("fuck" + id + " " + filePath);

                    //初始化上传页面
                    filesBeans[index].setInit(false);
                    Ftp.Uploader.reset();
                    initUploadHandlePage();
                }


                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }

            //重新加载页面
            searchDataList("");
        }

        public void deleteUpload5(object sender, EventArgs e)
        {
            string filePath = "";
            int id = DataList[5].id;

            if ("-1".Equals(DataList[5].finished))
            {

                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }
            else
            {
                //中断上传线程
                if (uploadThread != null)
                {
                    uploadThread.Suspend();
                    //uploadThread.Abort();
                }

                //查询路径和id
                foreach (FilesUploadBean filesBean in filesBeans)
                {
                    if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString()))
                    {
                        filePath = filesBean.evPathBean;
                        id = filesBean.id;
                        break;
                    }
                }

                //删除已上传的文件
                if (filePath != "")
                {
                    Ftp.Uploader.deletePath(filePath);
                    //MessageBox.Show("fuck" + id + " " + filePath);

                    //初始化上传页面
                    filesBeans[index].setInit(false);
                    Ftp.Uploader.reset();
                    initUploadHandlePage();
                }


                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }

            //重新加载页面
            searchDataList("");
        }

        public void deleteUpload6(object sender, EventArgs e)
        {
            string filePath = "";
            int id = DataList[6].id;

            if ("-1".Equals(DataList[6].finished))
            {

                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }
            else
            {
                //中断上传线程
                if (uploadThread != null)
                {
                    uploadThread.Suspend();
                    //uploadThread.Abort();
                }

                //查询路径和id
                foreach (FilesUploadBean filesBean in filesBeans)
                {
                    if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString()))
                    {
                        filePath = filesBean.evPathBean;
                        id = filesBean.id;
                        break;
                    }
                }

                //删除已上传的文件
                if (filePath != "")
                {
                    Ftp.Uploader.deletePath(filePath);
                    //MessageBox.Show("fuck" + id + " " + filePath);

                    //初始化上传页面
                    filesBeans[index].setInit(false);
                    Ftp.Uploader.reset();
                    initUploadHandlePage();
                }


                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }

            //重新加载页面
            searchDataList("");
        }

        public void deleteUpload7(object sender, EventArgs e)
        {
            string filePath = "";
            int id = DataList[7].id;

            if ("-1".Equals(DataList[7].finished))
            {

                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }
            else
            {
                //中断上传线程
                if (uploadThread != null)
                {
                    uploadThread.Suspend();
                    //uploadThread.Abort();
                }

                //查询路径和id
                foreach (FilesUploadBean filesBean in filesBeans)
                {
                    if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString()))
                    {
                        filePath = filesBean.evPathBean;
                        id = filesBean.id;
                        break;
                    }
                }

                //删除已上传的文件
                if (filePath != "")
                {
                    Ftp.Uploader.deletePath(filePath);
                    //MessageBox.Show("fuck" + id + " " + filePath);

                    //初始化上传页面
                    filesBeans[index].setInit(false);
                    Ftp.Uploader.reset();
                    initUploadHandlePage();
                }


                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }

            //重新加载页面
            searchDataList("");
        }

        public void deleteUpload8(object sender, EventArgs e)
        {
            string filePath = "";
            int id = DataList[8].id;

            if ("-1".Equals(DataList[8].finished))
            {

                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }
            else
            {
                //中断上传线程
                if (uploadThread != null)
                {
                    uploadThread.Suspend();
                    //uploadThread.Abort();
                }

                //查询路径和id
                foreach (FilesUploadBean filesBean in filesBeans)
                {
                    if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString()))
                    {
                        filePath = filesBean.evPathBean;
                        id = filesBean.id;
                        break;
                    }
                }

                //删除已上传的文件
                if (filePath != "")
                {
                    Ftp.Uploader.deletePath(filePath);
                    //MessageBox.Show("fuck" + id + " " + filePath);

                    //初始化上传页面
                    filesBeans[index].setInit(false);
                    Ftp.Uploader.reset();
                    initUploadHandlePage();
                }


                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }

            //重新加载页面
            searchDataList("");
        }

        public void deleteUpload9(object sender, EventArgs e)
        {
            string filePath = "";
            int id = DataList[9].id;

            if ("-1".Equals(DataList[9].finished))
            {

                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }
            else
            {
                //中断上传线程
                if (uploadThread != null)
                {
                    uploadThread.Suspend();
                    //uploadThread.Abort();
                }

                //查询路径和id
                foreach (FilesUploadBean filesBean in filesBeans)
                {
                    if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString()))
                    {
                        filePath = filesBean.evPathBean;
                        id = filesBean.id;
                        break;
                    }
                }

                //删除已上传的文件
                if (filePath != "")
                {
                    Ftp.Uploader.deletePath(filePath);
                    //MessageBox.Show("fuck" + id + " " + filePath);

                    //初始化上传页面
                    filesBeans[index].setInit(false);
                    Ftp.Uploader.reset();
                    initUploadHandlePage();
                }


                //删除数据库中的数据
                if (id != null)
                {
                    Maticsoft.BLL.evidence evidence = new Maticsoft.BLL.evidence();
                    evidence.Delete(id);
                    //MessageBox.Show("fuck"+id);
                }
            }

            //重新加载页面
            searchDataList("");
        }
        #endregion

        /// <summary>  
        ///  更新数据库finished字段
        /// </summary>  
        /// <returns></returns>
        public void uploadDatabaseFinished()
        {
            string serchQuery = "finished = '' or finished IS NULL";
            Maticsoft.BLL.evidence evidence_old = new Maticsoft.BLL.evidence();
            DataSet ds = evidence_old.GetList(serchQuery);

            Maticsoft.Model.evidence evidence_new = new Maticsoft.Model.evidence();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) 
            {
                evidence_new.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                evidence_new.evName = ds.Tables[0].Rows[i]["evName"].ToString();
                evidence_new.caseID = Convert.ToInt32(ds.Tables[0].Rows[i]["caseID"].ToString());
                evidence_new.evType = Convert.ToInt32(ds.Tables[0].Rows[i]["evType"].ToString());
                evidence_new.comment = ds.Tables[0].Rows[i]["comment"].ToString();
                evidence_new.evAdmin = ds.Tables[0].Rows[i]["evAdmin"].ToString();
                evidence_new.dataTypes = Convert.ToInt32(ds.Tables[0].Rows[i]["dataTypes"].ToString());
                evidence_new.finished = "-1";
                evidence_new.localPath = ds.Tables[0].Rows[0]["localPath"].ToString();
                evidence_old.Update(evidence_new);
            }
            
        }
    }
}
