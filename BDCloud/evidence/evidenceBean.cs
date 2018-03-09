using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDCloud
{
    class FilesUploadBean
    {
        public bool init = false;
        public int id = 0;
        public string evTpyeBean = "";
        public string evPathBean = "";
        public string evName = "";
        public string evComment = "";
        public string evDataType = "";
        public bool hasBean = false;
        public string caseId = "";
        public string getCaseId()
        {
            return caseId;
        }
        public void setCaseId(string caseId)
        {
            this.caseId = caseId;
        }
        public int getId()
        {
            return id;
        }
        public void setId(int id)
        {
            this.id = id;
        }

        public string getEvTpyeBean()
        {
            return evTpyeBean;
        }
        public void setEvTpyeBean(string evTpyeBean)
        {
            this.evTpyeBean = evTpyeBean;
        }

        public string getEvPathBean()
        {
            return evPathBean;
        }
        public void setEvPathBean(string evPathBean)
        {
            this.evPathBean = evPathBean;
        }

        public string getEvName()
        {
            return evName;
        }
        public void setEvName(string evName)
        {
            this.evName = evName;
        }

        public string getEvComment()
        {
            return evComment;
        }
        public void setEvComment(string evComment)
        {
            this.evComment = evComment;
        }

        public string getEvDataType()
        {
            return evDataType;
        }
        public void setEvDataType(string evDataType)
        {
            this.evDataType = evDataType;
        }

        public bool getHasBean()
        {
            return hasBean;
        }
        public void setHasBean(bool hasBean)
        {
            this.hasBean = hasBean;
        }
        public bool getInit()
        {
            return init;
        }
        public void setInit(bool init)
        {
            this.init = init;
        }

    }
}
