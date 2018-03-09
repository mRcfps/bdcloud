using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace FTP
{
    public static class DataSrcTypeUtils
    {
        private static string[] suffixes ={
        ".eml",".tar", ".tar.gz",".zip" };
        private static string getPattern(string suffix)
        {
            return @"\b\S+" + suffix + "\b";
        }
        private static bool isMatch_file(string fileName)
        {
            foreach (string suffix in suffixes)
            {
                if (!Regex.IsMatch(fileName, getPattern(suffix)))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool isMatch(string pathName)
        {
            var fileNames = PathUtils.getFileNames(pathName);
            foreach (string fileName in fileNames)
            {
                if (!isMatch_file(fileName)) return false;
            }
            return true;
        }
    }
}
