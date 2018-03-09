using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Security;
using Maticsoft.BLL;
using Maticsoft.Model;
using Maticsoft.DBUtility;
using BDCloud.common;
using System.Net;
using System.IO;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using User;
using System.Reflection;

namespace BDCloud
{
    public partial class Form1 : Form
    {
        //  设置插入数据库参数
        static string evName_d3 = "";
        static string caseId_d3 = "";
        static string evTpye_origin_d3 = "";
        static string comment_d3 = "";
        static string evAdmin_d3 = "";
        static string dataTypes_orgin_d3 = "";
        static string evType_d3 = "";
        static int dataType_d3 = -1;
        static List<FilesUploadBean> filesBeans = new List<FilesUploadBean>();
        static int index = 0;
        static Thread UIUploadButton;
        public void initUploadHandlePage()
        {
            index++;
            // 开启线程判断 上传按钮颜色(判定条件：同步解析文件数)
             UIUploadButton = new Thread(new ThreadStart(changeBgColorUploadButton));
             UIUploadButton.Start();
              index = 0;
            foreach(FilesUploadBean filesBean in filesBeans){
                index++;
                if (filesBean.getInit() && filesBean.getCaseId().Equals(ClueInfo.caseId.ToString()))
              {
                if ("".Equals(filesBean.getEvTpyeBean()))
                {
                    evTypes_d3.TextChanged -= new EventHandler(evTypes_d3_TextChanged);
                    evTypes_d3.Text = "电子邮件";
                    evTypes_d3.TextChanged += new EventHandler(evTypes_d3_TextChanged);
                }
                else
                {
                    evTypes_d3.TextChanged -= new EventHandler(evTypes_d3_TextChanged);
                    evTypes_d3.Text = filesBean.getEvTpyeBean();
                    evTypes_d3.TextChanged += new EventHandler(evTypes_d3_TextChanged);
                }

                FilePath.TextChanged -= new EventHandler(FilePath_TextChanged);
                FilePath.Text = filesBean.getEvPathBean();
                FilePath.TextChanged += new EventHandler(FilePath_TextChanged);

                txtEvName.TextChanged -= new EventHandler(txtEvName_TextChanged);
                txtEvName.Text = filesBean.getEvName();
                txtEvName.TextChanged += new EventHandler(txtEvName_TextChanged);
   
                txtComment.TextChanged -= new EventHandler(txtComment_TextChanged);
                txtComment.Text = filesBean.getEvComment();
                txtComment.TextChanged += new EventHandler(txtComment_TextChanged);
                if ("".Equals(filesBean.getEvDataType()))
                {
                    dataTypes_d3.TextChanged -= new EventHandler(dataTypes_d3_TextChanged);
                    dataTypes_d3.Text = "";
                    dataTypes_d3.TextChanged += new EventHandler(dataTypes_d3_TextChanged);      
                }
                else
                {
                    dataTypes_d3.TextChanged -= new EventHandler(dataTypes_d3_TextChanged);
                    dataTypes_d3.Text = filesBean.getEvDataType();
                    dataTypes_d3.TextChanged += new EventHandler(dataTypes_d3_TextChanged);
          
                }
                index--;
                break;
              }     
            }
                   if(index>=filesBeans.Count())
            {
                    FilesUploadBean filesBean = new FilesUploadBean();  //如不存在 已有filesBean,新建filesBean并插入List
                    filesBean.setInit(true);                       
                    filesBean.setCaseId(ClueInfo.caseId.ToString());
                    filesBeans.Add(filesBean);
                    evTpye_origin_d3 = "电子邮件";
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
   
                    dataTypes_orgin_d3 = "1";
                    dataTypes_d3.TextChanged -= new EventHandler(dataTypes_d3_TextChanged);
                    dataTypes_d3.Text = "移动设备";
                    dataTypes_d3.TextChanged += new EventHandler(dataTypes_d3_TextChanged);
                    progressLabel.Text = 0 + "%";
                    uploadProgressBar.Value = 0;           
            }
        }

        //上传成功后更新evidence 中的字段finished='true'
        public void updateFilesSuccStatus()
        {
            Maticsoft.BLL.evidence evidence_update = new Maticsoft.BLL.evidence();
            Maticsoft.Model.evidence evidence_model = new Maticsoft.Model.evidence();
            DataSet ds=evidence_update.GetList("1=1 ORDER BY id desc LIMIT 1");
            evidence_model.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            evidence_model.evName=ds.Tables[0].Rows[0]["evName"].ToString();
            evidence_model.caseID=Convert.ToInt32(ds.Tables[0].Rows[0]["caseID"].ToString());
            evidence_model.evType =  Convert.ToInt32(ds.Tables[0].Rows[0]["evType"].ToString());
            evidence_model.comment = ds.Tables[0].Rows[0]["comment"].ToString();
            evidence_model.evAdmin = ds.Tables[0].Rows[0]["evAdmin"].ToString();
            evidence_model.dataTypes = Convert.ToInt32(ds.Tables[0].Rows[0]["dataTypes"].ToString());
            evidence_model.finished = "true";
            evidence_model.localPath = ds.Tables[0].Rows[0]["localPath"].ToString();
            evidence_update.Update(evidence_model);
            Console.WriteLine("---------------更新数据库信息：" + evidence_model.id + " " + evidence_model.evName + "   " + evidence_model.caseID);
            filesBeans[index].setId(evidence_model.id);
            filesBeans[index].setHasBean(Convert.ToBoolean(evidence_model.finished));
        }

        /// <summary>  
        ///   数据源类型为evType
        /// </summary>  
        /// <param name="evTpyes">数据源类型的中文内容</param>
        /// <returns>数数据源类型对应的代号</returns>
        public static string getEvTpye(string evTpyes)
        {
            if (evTpyes == null || "".Equals(evTpyes))
            {
                Console.WriteLine("请选择上传数据类型");
                return null;
            }
            if ("电子邮件".Equals(evTpyes))
            {
                evType_d3 = "1";
            }
            else if ("综合文档".Equals(evTpyes))
            {
                evType_d3 = "2";

            }
            else if ("电子话单".Equals(evTpyes))
            {
                evType_d3 = "3";
            }
            else if ("图片资料".Equals(evTpyes))
            {
                evType_d3 = "6";

            }
            else
            {
                evType_d3 = "-1";
            }
            return evType_d3;
        }

        /// <summary>  
        ///  数据来源为dataType
        /// </summary>  
        /// <param name="dataTypes">数据来源类型的中文内容</param>
        /// <returns> 数据来源对应的代号</returns>
        public static int getDataType(string dataTypes)
        {
            if ("移动设备".Equals(dataTypes))
            {
                dataType_d3 = 1;
            }
            else if ("通信运营".Equals(dataTypes))
            {
                dataType_d3 = 2;
            }
            else if ("社交网站".Equals(dataTypes))
            {
                dataType_d3 = 3;
            }
            else if ("手音频视频".Equals(dataTypes))
            {
                dataType_d3 = 4;
            }
            else if ("采集数据".Equals(dataTypes))
            {
                dataType_d3 = 5;
            }
            else if ("口供资料".Equals(dataTypes))
            {
                dataType_d3 = 6;
            }
            else if ("其他来源".Equals(dataTypes))
            {
                dataType_d3 = 7;
            }
            else
            {
                dataType_d3 = -1;
            }
            return dataType_d3;
        }

        // 开启线程判断 上传按钮颜色(判定条件：同步解析文件数)
        public void changeBgColorUploadButton()
        {
            while(true){
            int count_d3 = judgeAnalysisFiles();
            if (count_d3 < 3 && count_d3 >= 0)
            {
                uploadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(171)))), ((int)(((byte)(229)))));
                uploadButton.Text = "上传";
            }
            Thread.Sleep(1000);   
             }
        }
        //将上传内容进行存入到bean中，再次进入项目后进行刷新
        public void saveLogBean()
        {
            filesBeans[index].setCaseId(ClueInfo.caseId.ToString());
            filesBeans[index].setEvName(evName_d3);
            filesBeans[index].setEvPathBean(FilePath.Text);
            filesBeans[index].setEvTpyeBean(evTpye_origin_d3);
            filesBeans[index].setEvDataType(dataTypes_orgin_d3);
            filesBeans[index].setEvComment(comment_d3);
            filesBeans[index].setHasBean(true);
            filesBeans[index].setInit(true);
        }

        //从控件获取参数进行赋值
        public bool getParamsToSql()
        {
            evName_d3 = txtEvName.Text;
            caseId_d3 = ClueInfo.caseId.ToString();
            if (!evTypes_d3.Text.Equals("电子邮件"))
            {
                evTpye_origin_d3 = evTypes_d3.Text;
            }
            else
            {
                evTypes_d3.TextChanged -= evTypes_d3_TextChanged;
                evTypes_d3.Text = "电子邮件";
                evTypes_d3.TextChanged += evTypes_d3_TextChanged;
            }
            evType_d3 = getEvTpye(evTpye_origin_d3);
            comment_d3 = txtComment.Text;
            evAdmin_d3 = User.UserInfo.username.ToString();
            if (!dataTypes_d3.Text.Equals(""))
            {
                dataTypes_orgin_d3 = dataTypes_d3.Text;
            }
            dataType_d3 = getDataType(dataTypes_orgin_d3);
         
            saveLogBean(); 
            return true;
        }


        /// <summary>  
        ///  判断当前用户解析批次数
        /// </summary>  
        /// <returns>当前用户解析的批次数</returns>
        public int judgeAnalysisFiles(){
            Maticsoft.BLL.onlinenumber onlinenumber_judge = new Maticsoft.BLL.onlinenumber();
            DataSet ds = onlinenumber_judge.GetList("username='"+User.UserInfo.username.ToString()+"'");
            int count = 0;
            if (ds.Tables[0].Rows.Count == 0)
            {
                return count;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["onlinestatus"].Equals("1"))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>  
        ///  将参数写入到数据库onlinenumber表中  :  1:解析中  ；0:解析完成  当前用户同时解析最多三个批次（即三个1）。
        /// </summary>  
        /// <returns></returns>
        public void insertOnLineNumberDataToDB()
        {
            Maticsoft.BLL.onlinenumber onlinenumber_add = new Maticsoft.BLL.onlinenumber();
            Maticsoft.Model.onlinenumber onlinenumber_model = new Maticsoft.Model.onlinenumber();

            Maticsoft.BLL.evidence evidence_update = new Maticsoft.BLL.evidence();
            Maticsoft.Model.evidence evidence_model = new Maticsoft.Model.evidence();
            DataSet ds = evidence_update.GetList("1=1 ORDER BY id desc LIMIT 1");
            evidence_model.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());

            onlinenumber_model.evid = evidence_model.id;
            onlinenumber_model.username = evAdmin_d3;
            onlinenumber_model.onlinestatus = "1";
            onlinenumber_add.Add(onlinenumber_model);
        }

        /// <summary>  
        ///  将参数写入到数据库中
        /// </summary>  
        /// <returns></returns>
        public bool insertDataToDB()
        {
            if (txtEvName.Text == null || "".Equals(txtEvName.Text.Trim()))
            {
                MessageBox.Show("数据名称不能为空");
                return false;
            }
            if (FilePath.Text == null || "".Equals(FilePath.Text.Trim()))
            {
                MessageBox.Show("上传文件不能为空");
                return false;
            }
            Maticsoft.BLL.evidence evidence_add = new Maticsoft.BLL.evidence();
            Maticsoft.Model.evidence evidence_model = new Maticsoft.Model.evidence();

            //构造假的param
            /*           evName = "邮件test";
                       caseId = "160";
                       evTpye_origin = "综合文档";
                       comment = "lv is handsome";
                       evAdmin = "王利明";
                       dataTypes_orgin = "综合文档";  */

            if(!getParamsToSql()){
                return  false;
            }
            
            evidence_model.evName = evName_d3;
            evidence_model.caseID = Convert.ToInt32(caseId_d3);
            evidence_model.evType = Convert.ToInt32(getEvTpye(evTpye_origin_d3));
            evidence_model.comment = comment_d3;
            evidence_model.evAdmin = evAdmin_d3;
            evidence_model.dataTypes = Convert.ToInt32(getDataType(dataTypes_orgin_d3));
            evidence_model.localPath = FilePath.Text;
            Console.WriteLine("---------------插入数据库信息1111111：" + evName_d3 + " " + caseId_d3 + " " + evType_d3 + " " + comment_d3 + " " + evAdmin_d3 + " " + dataType_d3+"     address:::: " + FilePath.Text);
            Console.WriteLine("---------------插入数据库信息22222222222：" + evidence_model.localPath);
            evidence_add.Add(evidence_model);
            Maticsoft.BLL.evidence evidence_update = new Maticsoft.BLL.evidence();
          
            DataSet ds = evidence_update.GetList("1=1 ORDER BY id desc LIMIT 1");
            filesBeans[index].id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            return true;
            //  调用接口：
            //  test_interface:http://172.16.103.252:8080/myweb/test
            //  submit2server:http://172.16.103.252:8080/myweb/decomTar?linuxPath=&eviId=
            //  submit2colony:http://172.16.103.252:8080/myweb/executeSparkShell?eviId=

            //dataTransimit();
            //colonyAnalysis();

        }

        /// <summary>  
        ///  发送存储地址到服务器，服务器转存到集群
        /// </summary>  
        /// <returns></returns>
        public void dataTransimit()
        {
            HttpWebResponse response = CreateGetHttpResponse("http://172.16.103.252:8080/myweb/decomTar?linuxPath=/opt/ftp/cxy/&eviId=532");
            //获取流  
            Stream streamResponse = response.GetResponseStream();
            //使用UTF8解码  
            StreamReader streanReader = new StreamReader(streamResponse, Encoding.UTF8);
            string retString = streanReader.ReadToEnd();
            /*
              返回参数如果为0 ，说明linuxPath下 没有文件；  返回参数为1 ，数据转存成功，集群地址写入Mysql中 table evidence中的dirPath中
            */
            if (Convert.ToInt32(retString) == 0)
            {
                Console.WriteLine("dataTransimit-------------------  服务器地址下没有文件!!!!!!!!!");
            }
            else
            {
                Console.WriteLine("dataTransimit-------------------  数据转存成功!!!!!!!!!");
            }
        }

        /// <summary>  
        ///  集群对数据进行解析
        /// </summary>  
        /// <returns></returns>     
        public void colonyAnalysis()
        {
            HttpWebResponse response = CreateGetHttpResponse("http://172.16.103.252:8080/myweb/executeSparkShell?eviId=532");
            //获取流  
            Stream streamResponse = response.GetResponseStream();
            //使用UTF8解码  
            StreamReader streanReader = new StreamReader(streamResponse, Encoding.UTF8);
            string retString = streanReader.ReadToEnd();
            /*
              返回参数如果为0 ，解析失败（没有文件）；  返回参数为1 ，数据解析成功
            */
            if (Convert.ToInt32(retString) == 0)
            {
                Console.WriteLine("colonyAnalysis-------------------  数据未解析!!!!!!!!!");
            }
            else
            {
                Console.WriteLine("colonyAnalysis-------------------  数据解析成功!!!!!!!!!");
            }
        }

        /// <summary>  
        ///  创建GET的HTTP请求
        /// </summary>  
        /// <returns></returns>    
        public HttpWebResponse CreateGetHttpResponse(string url)
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
        private void evTypes_d3_TextChanged(object sender, EventArgs e)
        {
            getParamsToSql();
        }

        private void FilePath_TextChanged(object sender, EventArgs e)
        {
            getParamsToSql();
        }

        private void txtEvName_TextChanged(object sender, EventArgs e)
        {            
            getParamsToSql();
        }

        private void txtComment_TextChanged(object sender, EventArgs e)
        {           
            getParamsToSql();
        }

       private void dataTypes_d3_TextChanged(object sender, EventArgs e)
        {
           Console.WriteLine("aaaaaaaaaaaaaaaaaaaa    ");
            getParamsToSql();
        }
    }
}
