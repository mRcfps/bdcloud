using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;
using System.IO;
using User;

namespace BDCloud.Ftp
{
    public static class HashUtils
    {
        public static string localParentPath = "";

        private static string getLocalIPV4()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }

        private static string getLocalFullPathName(string localShortPathName)
        {
            if (File.Exists(localShortPathName))
            {
                var fileInfo = new FileInfo(localShortPathName);
                return fileInfo.FullName;
            }
            else if (Directory.Exists(localShortPathName))
            {
                var dirInfo = new DirectoryInfo(localShortPathName);
                return dirInfo.FullName;
            }
            return null;
        }

        //为防止文件名重复，本地文件上传到服务器上时用哈希重命名
        public static string getHashName(string localShortPathName)
        {
            if (Directory.Exists(localShortPathName))
            {
                return UserInfo.id.GetHashCode().ToString()
                    + getLocalIPV4().GetHashCode().ToString()
                    + getLocalFullPathName(localShortPathName).GetHashCode().ToString();
            }
            else if (File.Exists(localShortPathName))       //针对文件的 Hash  加上后缀,方便服务器解析
            {
                string uri = UserInfo.id.GetHashCode().ToString()
                    + getLocalIPV4().GetHashCode().ToString()
                    + getLocalFullPathName(localShortPathName).GetHashCode().ToString();
                string extension = "";
                if (localShortPathName.Contains(".tar"))
                {
                    string[] extensions = new string[100];

                    extensions = localShortPathName.Split('.');
                    extension = "." + extensions[1] + "." + extensions[2];
                }
                else
                {
                    extension = System.IO.Path.GetExtension(localShortPathName);
                }

                uri = uri + extension;
                return uri;
            }
            return null;
        }

        public static void setLocalParentPath(string path)        //不返回
        {
            localParentPath = path;
        }

        public static string getLocalParentPath()        //不返回
        {
            return localParentPath;
        }

        /// <summary>  
        /// 拼接uri  
        /// </summary>  
        /// <param name="remoteFolderPath">远程文件夹名</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string getFileUri(string fileName = null)
        {
            string uri = String.Empty;
            uri = "ftp://" + Uploader.FtpServerIP
                + "/" + Uploader.FtpPath
                 + "/" + getHashName(getLocalParentPath())
                 + "/" + getHashName(fileName);
            return uri;
        }

        //传入本地上传文件夹绝对路径，获得服务器上存储的文件夹名
        public static string getDirUri(string pathName = null)
        {
            string uri = String.Empty;
            uri = "ftp://" + Uploader.FtpServerIP
            + "/" + Uploader.FtpPath
            + "/" + getHashName(pathName);
            return uri;
        }

        public static string getUri(string uri)
        {
            uri = "ftp://" + Uploader.FtpServerIP
            + "/" + Uploader.FtpPath
            + "/" + uri;
            return uri;
        }
    }
}
