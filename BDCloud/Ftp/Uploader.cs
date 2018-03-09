using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.ComponentModel;
using Maticsoft.BLL;
using Maticsoft.Model;
using Maticsoft.DBUtility;
using User;
using System.Configuration;
using System.Data;

namespace BDCloud.Ftp
{
    public static class Uploader
    {
        #region 连接属性
        /// <summary>  
        /// Ftp服务器ip  
        /// </summary>  
        /// 
        public static string FtpServerIP = ConfigurationSettings.AppSettings["FtpServerIP"];    
        /// <summary>  
        /// Ftp 指定用户名  
        /// </summary>  
        public static string FtpUserID = ConfigurationSettings.AppSettings["FtpUserID"];
        /// <summary>  
        /// Ftp 指定用户密码  
        /// </summary>  
        public static string FtpPassword = ConfigurationSettings.AppSettings["FtpPassword"];
        /// <summary>  
        /// Ftp 指定存储路径  
        /// </summary>
        public static string FtpPath = ConfigurationSettings.AppSettings["FtpPath"];

        ///经过Hash后的 Ftp 指定存储路径,在
        /// </summary>
        public static string FtpPathHash = "";

        #endregion

        #region action属性
	    private static DateTime prevTime;
        private static long prevUploadedDirSize;
        private static long curUploadedDirSize;
        private static long dirSize;
        private static int fileNum;
	    #endregion

        private static Form1 form = Form1.ActiveForm as Form1;

        static Uploader()
        {
            prevTime = DateTime.Now;
            prevUploadedDirSize = 0;
            curUploadedDirSize = 0;
            dirSize = 0;
            fileNum = 0;
        }

        public static void reset()
        {
            prevTime = DateTime.Now;
            prevUploadedDirSize = 0;
            curUploadedDirSize = 0;
            dirSize = 0;
            fileNum = 0;
        }

        /// <summary>
        /// 验证证书是否有效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
         
        public static FtpWebRequest getFtpWebRequest(Uri uri, string method)
        {
            FtpWebRequest request;
            // 根据uri创建FtpWebRequest对象   
            request = FtpWebRequest.Create(uri) as FtpWebRequest;
            // ftp用户名和密码   
            request.Credentials = new NetworkCredential(FtpUserID, FtpPassword);
            // 默认为true，连接不会被关闭   
            // 在一个命令之后被执行   
            request.KeepAlive = false;
            // 指定执行什么命令--文件继续上传   
            request.Method = method;
            // 指定数据传输类型   
            request.UseBinary = true;
            //如果不写会报出421错误（服务不可用）
            request.EnableSsl = true;      
            request.Timeout = -1;
            request.ReadWriteTimeout = -1;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

            return request;
        }

        /// <summary>  
        /// 获取已上传文件大小  
        /// </summary>  
        /// <param name="localFileFullPathName">本地文件全路径</param>  
        /// <returns></returns>  
        public static long getFileSize(FileInfo fileInfo)
        {
            string uri = HashUtils.getFileUri(fileInfo.FullName);
            Console.WriteLine("-------------文件在服务器的地址  uri:  " + uri); 
            // 根据uri创建FtpWebRequest对象   
            var request = getFtpWebRequest(new Uri(uri), WebRequestMethods.Ftp.GetFileSize);
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                long filesize = response.ContentLength;
                return filesize;
            }
            catch
            {
                return 0;
            }
        }

        public static void setRemoteDir(string pathName)
        {
            string uri = HashUtils.getDirUri(pathName);
            var request = getFtpWebRequest(new Uri(uri), WebRequestMethods.Ftp.MakeDirectory);
            try
            {
                var response = request.GetResponse() as FtpWebResponse;
                return;
            }
            catch
            {
                return;
            }
        }

        static long uploadedFileSize = 0;
        /// <summary>  
        /// 上传文件到FTP服务器(断点续传)  
        /// </summary>  
        /// <param name="localFullPath">本地文件对象</param>  
        /// <param name="remoteDirPath">远程文件夹路径</param>  
        /// <param name="fileProgressAction">报告进度的处理(第一个参数：总大小，第二个参数：当前进度)</param>  
        /// <returns></returns>         
        public static void uploadFile(FileInfo fileInfo)
        {
            // 比较已上传大小
            long fileSize = (long)fileInfo.Length;
            uploadedFileSize = getFileSize(fileInfo);

            // 如果ftp服务器上文件大小已超过本地文件大小，返回false
            if (uploadedFileSize > fileSize)
            {
                return;
            }
            
            curUploadedDirSize += uploadedFileSize;
            form.UpdateProgress((double)curUploadedDirSize / (double)dirSize);

            // 文件已上传完毕
            if (uploadedFileSize == fileSize) return;

            // 请求设置
            string uri = HashUtils.getFileUri(fileInfo.FullName);
            var request = getFtpWebRequest(new Uri(uri), WebRequestMethods.Ftp.AppendFile);

            // 上传文件时通知服务器文件的大小   
            request.ContentLength = fileInfo.Length;

            // 缓存和流设置
            int bufferSize = 1048576; // 缓冲大小设置为2kb   
            byte[] buffer = new byte[bufferSize];

            using (FileStream fileStream = fileInfo.OpenRead())
            {
                fileStream.Seek(uploadedFileSize, 0);
                using (Stream uploadStream = request.GetRequestStream())
                {
                    while (true)
                    {
                        int blockSize = fileStream.Read(buffer, 0, bufferSize);
                        if (blockSize == 0) break;
                        
                        // 把内容从file stream 写入 upload stream   
                        uploadStream.Write(buffer, 0, blockSize);
                        uploadedFileSize += blockSize;
                        curUploadedDirSize += blockSize;
                        // 当时间间隔>1000ms,更新进度

                        DateTime curTime = DateTime.Now;                        
                        TimeSpan timespan = curTime.Subtract(prevTime);
                        if (timespan.TotalMilliseconds > 1000)
                        {
                            prevTime = curTime;
                            //更新进度    
                            form.UpdateProgress((double)uploadedFileSize / (double)fileSize);
                            form.UpdateSpeed((double)(curUploadedDirSize-prevUploadedDirSize) / (double)(timespan.TotalMilliseconds) * 1000.0);
                            var speed=(double)(curUploadedDirSize-prevUploadedDirSize) / (double)(timespan.TotalMilliseconds) * 1000.0;
                            var seconds = (dirSize - curUploadedDirSize) / speed;
                            DateTime remainTime = Convert.ToDateTime("1900-01-01 00:00:00");
                            remainTime = remainTime.AddSeconds(seconds);
                            form.UpdateTime(remainTime);
                            form.UpdateProgress((double)curUploadedDirSize/(double)dirSize);
                            prevUploadedDirSize = curUploadedDirSize;
                        }
                    }
                }
            }
        }

        static string totalPathName = "";
        /// <summary>  
        /// tabUpload页面上传接口 
        /// </summary>  
        /// <param name="pathName">本地文件或文件夹全路径名称</param>  
        /// <returns></returns>    
        public static void uploadPath(string pathName)
        {
            totalPathName = pathName;

            //遍历文件夹获取所有文件的信息
            var fileInfos = PathUtils.getFileInfos(pathName);
            //totalNum = 总文件个数
            fileNum = fileInfos.Count;
            //totalSize = 总文件大小
            dirSize = PathUtils.getTotalFileSize(fileInfos);
            //uploadedNum = 已上传文件个数
            int uploadedFileNum = 0;

            if (File.Exists(pathName))
            {
                pathName = Path.GetDirectoryName(pathName);
            }

            FtpPathHash = "/opt/ftp/cxy/" + HashUtils.getHashName(pathName);    //解析参数

            HashUtils.setLocalParentPath(pathName);  //设置服务器根目录的地址

            setRemoteDir(pathName);
            Console.Out.WriteLine("create Path name:",pathName);

            form.UpdateNum(uploadedFileNum, fileNum);
            form.UpdateLog("------ 上传文件流程开始 ------");
            form.UpdateLog("***********         ***********");

            // 上传前先检查文件夹是否存在
            foreach (FileInfo fileInfo in fileInfos)
            {
                form.UpdateLog("文件 " + fileInfo.Name + " 开始上传 ");
                uploadFile(fileInfo);    
                form.UpdateLog("文件 " + fileInfo.Name + " 上传完成 ");
                // 已上传文件数+1
                form.UpdateNum(++uploadedFileNum, fileNum);
            }
            form.UpdateLog("***********         ***********");
            form.UpdateLog("------ 上传文件流程完成 ------");
            form.UpdateProgress(1.0);
            form.UpdateTime(Convert.ToDateTime("1900-01-01 00:00:00"));
            form.CompleteUpload();

            reset();
        }

        /// <summary>  
        ///  将 已上传文件大小、文件本地地址 参数写入到数据库中
        /// </summary>  
        /// <returns></returns>
        public static void updateFilesSize()
        {
            Maticsoft.BLL.evidence evidence_update = new Maticsoft.BLL.evidence();
            Maticsoft.Model.evidence evidence_model = new Maticsoft.Model.evidence();
            DataSet ds = evidence_update.GetList("1=1 ORDER BY id desc LIMIT 1");
            evidence_model.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            evidence_model.evName = ds.Tables[0].Rows[0]["evName"].ToString();
            evidence_model.caseID = Convert.ToInt32(ds.Tables[0].Rows[0]["caseID"].ToString());
            evidence_model.evType = Convert.ToInt32(ds.Tables[0].Rows[0]["evType"].ToString());
            evidence_model.comment = ds.Tables[0].Rows[0]["comment"].ToString();
            evidence_model.evAdmin = ds.Tables[0].Rows[0]["evAdmin"].ToString();
            evidence_model.dataTypes = Convert.ToInt32(ds.Tables[0].Rows[0]["dataTypes"].ToString());
            evidence_model.finished = "-1";
          
            evidence_model.localPath = totalPathName;   //  文件本地上传根目录
            evidence_update.Update(evidence_model);
            Console.WriteLine("---------------更新数据库信息：" + evidence_model.id + "   " + evidence_model.dirPath);
        }

        public static string listDir(string dirName)
        {
            try
            {
                string uri = HashUtils.getDirUri(dirName);
                var request = getFtpWebRequest(new Uri(uri), WebRequestMethods.Ftp.ListDirectory);
                var response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string dirs = reader.ReadToEnd();
                Console.Out.WriteLine(dirs);

                reader.Close();
                response.Close();
                return dirs;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return "";
        }

        public static void deleteFile(string fileName)
        {
            try
            {

                string uri = HashUtils.getUri(fileName);
                Console.Out.WriteLine(fileName + " 正在删除 " + uri);
                var request = getFtpWebRequest(new Uri(uri), WebRequestMethods.Ftp.DeleteFile);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                Console.Out.WriteLine(fileName + " 已删除");
            }
            catch
            {
                Console.Out.WriteLine(fileName + " 还未上传");
            }
        }

        public static void deleteDir(string dirName)
        {
            try
            {
                string uri = HashUtils.getDirUri(dirName);
                var request = getFtpWebRequest(new Uri(uri), WebRequestMethods.Ftp.RemoveDirectory);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                Console.Out.WriteLine(dirName + " 已删除");
            }
            catch
            {
                Console.Out.WriteLine(dirName + " 删除失败");
            }
        }

        public static void deletePath(string pathName)
        {
            char[] chars = { '\r', '\n' };
            char[] slash = { '/' };
            Console.Out.WriteLine("pathName: " + HashUtils.getDirUri(pathName));
            var fileNames = listDir(pathName).Split(chars, StringSplitOptions.RemoveEmptyEntries);
            foreach (var fileName in fileNames)
            {
                Console.Out.WriteLine("FileName: " + fileName);
                deleteFile(fileName);
            }
            deleteDir(pathName);
        }
    }
}
