using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDCloud.clue
{
    public class ClueListInfo
    {
        private string addTime;//导入日期
        private string evAdmin;//操作人
        private string evSize;//文件大小
        private string evType;//文件类型
        private string evName;//数据名称
        private string uploadNum;//上传数
        private string status;//上传状态

        public void setAddTime(string addTime)
        {
            this.addTime = addTime;
        }
        public string getAddTime() 
        {
            return this.addTime;
        }

        public void setEvAdmin(string evAdmin)
        {
            this.evAdmin = evAdmin;
        }
        public string getEvAdmin() 
        {
            return this.evAdmin;
        }

        public void setEvSize(string evSize)
        {
            this.evSize = evSize;
        }
        public string getEvSize()
        {
            return this.evSize;
        }

        public void setEvType(string evType)
        {
            this.evType = evType;
        }
        public string getEvType()
        {
            return this.evType;
        }

        public void setEvName(string evName)
        {
            this.evName = evName;
        }
        public string getEvName()
        {
            return this.evName;
        }

        public void setUploadNumin(string uploadNum)
        {
            this.uploadNum = uploadNum;
        }
        public string getUploadNum()
        {
            return this.uploadNum;
        }

        public void setStatus(string status)
        {
            this.status = status;
        }
        public string getStatus()
        {
            return this.status;
        }
    }
}
