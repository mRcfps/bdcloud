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
    public static class PathUtils
    {
        // 获取路径下所有文件的文件名(包括自己)
        public static ArrayList getFileNames(string pathName)
        {
            var fileNames = new ArrayList();
            if (File.Exists(pathName))
            {
                fileNames.Add(pathName);
            }
            else if (Directory.Exists(pathName))
            {
                //BFS
                var Q_dir = new Queue();
                Q_dir.Enqueue(new DirectoryInfo(pathName));
                while (Q_dir.Count > 0)
                {
                    var curDir = Q_dir.Dequeue() as DirectoryInfo;
                    foreach (var subDir in curDir.GetDirectories())
                        Q_dir.Enqueue(subDir);
                    foreach (var subFile in curDir.GetFiles())
                        fileNames.Add(subFile);
                }
            }
            return fileNames;
        }

        // 获取路径下所有文件的文件对象(包括自己)
        public static ArrayList getFileInfos(string pathName)
        {
            var fileInfos = new ArrayList();
            if (File.Exists(pathName))
            {
                fileInfos.Add(new FileInfo(pathName));
            }
            else if (Directory.Exists(pathName))
            {
                //BFS
                var Q_dir = new Queue();
                Q_dir.Enqueue(new DirectoryInfo(pathName));
                while (Q_dir.Count > 0)
                {
                    var curDir = Q_dir.Dequeue() as DirectoryInfo;
                    foreach (var subDir in curDir.GetDirectories())
                        Q_dir.Enqueue(subDir);
                    foreach (var subFile in curDir.GetFiles())
                        fileInfos.Add(subFile);
                }
            }
            return fileInfos;
        }

        //累计文件长度
        public static long getTotalFileSize(ArrayList fileInfos)
        {
            long size = 0;
            foreach (var fileInfo in fileInfos)
            {
                size += (fileInfo as FileInfo).Length;
            }
            return size;
        }
    }
}
