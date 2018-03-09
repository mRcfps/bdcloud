using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDCloud
{
    public class CaseClue
    {
        private String clueNumber;//线索编号
        private String clueName;//线索名
        private String type;//线索类型
        private String department;//所属部门
        private String personCharge;//负责人
        public void setClueNumber(String clueNumber)
        {
            this.clueNumber = clueNumber;
        }
        public String getClueNumber()
        {
            return this.clueNumber;
        }
        public void getClueName(String clueName)
        {
            this.clueName = clueName;
        }
        public String getClueName()
        {
            return this.clueName;
        }
        public void setType(String type)
        {
            this.type = type;
        }
        public String getType()
        {
            return this.type;
        }
        public void setDdepartment(String department)
        {
            this.department = department;
        }
        public String getDepartment()
        {
            return this.department;
        }
        public void setPersonCharge(String personCharge)
        {
            this.personCharge = personCharge;
        }
        public String getPersonCharge()
        {
            return this.personCharge;
        }
    }
}
