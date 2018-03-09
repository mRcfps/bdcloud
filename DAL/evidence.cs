using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
    /// <summary>
    /// 数据访问类:evidence
    /// </summary>
    public partial class evidence
    {
        public evidence()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("id", "evidence");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from evidence");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.evidence model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into evidence(");
            strSql.Append("caseID,spersonID,sunitID,evName,comment,evAdmin,dirPath,evSize,emlCount,docCount,pdfCount,zipCount,rarCount,percent,finished,status,mailBox,dealinfo,addTime,currFlag,finishTime,uptype,isdel,startSolrTime,evType,indexFlag,UUID,dataTypes,uploadNum,successNum,errorNum,localPath)");
            strSql.Append(" values (");
            strSql.Append("@caseID,@spersonID,@sunitID,@evName,@comment,@evAdmin,@dirPath,@evSize,@emlCount,@docCount,@pdfCount,@zipCount,@rarCount,@percent,@finished,@status,@mailBox,@dealinfo,@addTime,@currFlag,@finishTime,@uptype,@isdel,@startSolrTime,@evType,@indexFlag,@UUID,@dataTypes,@uploadNum,@successNum,@errorNum,@localPath)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@caseID", MySqlDbType.Int32,11),
					new MySqlParameter("@spersonID", MySqlDbType.Int32,11),
					new MySqlParameter("@sunitID", MySqlDbType.Int32,11),
					new MySqlParameter("@evName", MySqlDbType.VarChar,100),
					new MySqlParameter("@comment", MySqlDbType.VarChar,500),
					new MySqlParameter("@evAdmin", MySqlDbType.VarChar,100),
					new MySqlParameter("@dirPath", MySqlDbType.VarChar,500),
					new MySqlParameter("@evSize", MySqlDbType.VarChar,100),
					new MySqlParameter("@emlCount", MySqlDbType.Int32,11),
					new MySqlParameter("@docCount", MySqlDbType.Int32,11),
					new MySqlParameter("@pdfCount", MySqlDbType.Int32,11),
					new MySqlParameter("@zipCount", MySqlDbType.Int32,11),
					new MySqlParameter("@rarCount", MySqlDbType.Int32,11),
					new MySqlParameter("@percent", MySqlDbType.VarChar,100),
					new MySqlParameter("@finished", MySqlDbType.VarChar,100),
					new MySqlParameter("@status", MySqlDbType.VarChar,100),
					new MySqlParameter("@mailBox", MySqlDbType.VarChar,1000),
					new MySqlParameter("@dealinfo", MySqlDbType.VarChar,255),
					new MySqlParameter("@addTime", MySqlDbType.VarChar,255),
					new MySqlParameter("@currFlag", MySqlDbType.VarChar,5),
					new MySqlParameter("@finishTime", MySqlDbType.VarChar,255),
					new MySqlParameter("@uptype", MySqlDbType.Int32,1),
					new MySqlParameter("@isdel", MySqlDbType.Int32,1),
					new MySqlParameter("@startSolrTime", MySqlDbType.VarChar,255),
					new MySqlParameter("@evType", MySqlDbType.Int32,11),
					new MySqlParameter("@indexFlag", MySqlDbType.Int32,11),
					new MySqlParameter("@UUID", MySqlDbType.VarChar,255),
					new MySqlParameter("@dataTypes", MySqlDbType.Int32,11),
					new MySqlParameter("@uploadNum", MySqlDbType.VarChar,255),
					new MySqlParameter("@successNum", MySqlDbType.VarChar,255),
					new MySqlParameter("@errorNum", MySqlDbType.VarChar,255),
					new MySqlParameter("@localPath", MySqlDbType.VarChar,500)};
            parameters[0].Value = model.caseID;
            parameters[1].Value = model.spersonID;
            parameters[2].Value = model.sunitID;
            parameters[3].Value = model.evName;
            parameters[4].Value = model.comment;
            parameters[5].Value = model.evAdmin;
            parameters[6].Value = model.dirPath;
            parameters[7].Value = model.evSize;
            parameters[8].Value = model.emlCount;
            parameters[9].Value = model.docCount;
            parameters[10].Value = model.pdfCount;
            parameters[11].Value = model.zipCount;
            parameters[12].Value = model.rarCount;
            parameters[13].Value = model.percent;
            parameters[14].Value = model.finished;
            parameters[15].Value = model.status;
            parameters[16].Value = model.mailBox;
            parameters[17].Value = model.dealinfo;
            parameters[18].Value = model.addTime;
            parameters[19].Value = model.currFlag;
            parameters[20].Value = model.finishTime;
            parameters[21].Value = model.uptype;
            parameters[22].Value = model.isdel;
            parameters[23].Value = model.startSolrTime;
            parameters[24].Value = model.evType;
            parameters[25].Value = model.indexFlag;
            parameters[26].Value = model.UUID;
            parameters[27].Value = model.dataTypes;
            parameters[28].Value = model.uploadNum;
            parameters[29].Value = model.successNum;
            parameters[30].Value = model.errorNum;
            parameters[31].Value = model.localPath;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.evidence model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update evidence set ");
            strSql.Append("caseID=@caseID,");
            strSql.Append("spersonID=@spersonID,");
            strSql.Append("sunitID=@sunitID,");
            strSql.Append("evName=@evName,");
            strSql.Append("comment=@comment,");
            strSql.Append("evAdmin=@evAdmin,");
            strSql.Append("dirPath=@dirPath,");
            strSql.Append("evSize=@evSize,");
            strSql.Append("emlCount=@emlCount,");
            strSql.Append("docCount=@docCount,");
            strSql.Append("pdfCount=@pdfCount,");
            strSql.Append("zipCount=@zipCount,");
            strSql.Append("rarCount=@rarCount,");
            strSql.Append("percent=@percent,");
            strSql.Append("finished=@finished,");
            strSql.Append("status=@status,");
            strSql.Append("mailBox=@mailBox,");
            strSql.Append("dealinfo=@dealinfo,");
            strSql.Append("addTime=@addTime,");
            strSql.Append("currFlag=@currFlag,");
            strSql.Append("finishTime=@finishTime,");
            strSql.Append("uptype=@uptype,");
            strSql.Append("isdel=@isdel,");
            strSql.Append("startSolrTime=@startSolrTime,");
            strSql.Append("evType=@evType,");
            strSql.Append("indexFlag=@indexFlag,");
            strSql.Append("UUID=@UUID,");
            strSql.Append("dataTypes=@dataTypes,");
            strSql.Append("uploadNum=@uploadNum,");
            strSql.Append("successNum=@successNum,");
            strSql.Append("errorNum=@errorNum,");
            strSql.Append("localPath=@localPath");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@caseID", MySqlDbType.Int32,11),
					new MySqlParameter("@spersonID", MySqlDbType.Int32,11),
					new MySqlParameter("@sunitID", MySqlDbType.Int32,11),
					new MySqlParameter("@evName", MySqlDbType.VarChar,100),
					new MySqlParameter("@comment", MySqlDbType.VarChar,500),
					new MySqlParameter("@evAdmin", MySqlDbType.VarChar,100),
					new MySqlParameter("@dirPath", MySqlDbType.VarChar,500),
					new MySqlParameter("@evSize", MySqlDbType.VarChar,100),
					new MySqlParameter("@emlCount", MySqlDbType.Int32,11),
					new MySqlParameter("@docCount", MySqlDbType.Int32,11),
					new MySqlParameter("@pdfCount", MySqlDbType.Int32,11),
					new MySqlParameter("@zipCount", MySqlDbType.Int32,11),
					new MySqlParameter("@rarCount", MySqlDbType.Int32,11),
					new MySqlParameter("@percent", MySqlDbType.VarChar,100),
					new MySqlParameter("@finished", MySqlDbType.VarChar,100),
					new MySqlParameter("@status", MySqlDbType.VarChar,100),
					new MySqlParameter("@mailBox", MySqlDbType.VarChar,1000),
					new MySqlParameter("@dealinfo", MySqlDbType.VarChar,255),
					new MySqlParameter("@addTime", MySqlDbType.VarChar,255),
					new MySqlParameter("@currFlag", MySqlDbType.VarChar,5),
					new MySqlParameter("@finishTime", MySqlDbType.VarChar,255),
					new MySqlParameter("@uptype", MySqlDbType.Int32,1),
					new MySqlParameter("@isdel", MySqlDbType.Int32,1),
					new MySqlParameter("@startSolrTime", MySqlDbType.VarChar,255),
					new MySqlParameter("@evType", MySqlDbType.Int32,11),
					new MySqlParameter("@indexFlag", MySqlDbType.Int32,11),
					new MySqlParameter("@UUID", MySqlDbType.VarChar,255),
					new MySqlParameter("@dataTypes", MySqlDbType.Int32,11),
					new MySqlParameter("@uploadNum", MySqlDbType.VarChar,255),
					new MySqlParameter("@successNum", MySqlDbType.VarChar,255),
					new MySqlParameter("@errorNum", MySqlDbType.VarChar,255),
					new MySqlParameter("@localPath", MySqlDbType.VarChar,500),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
            parameters[0].Value = model.caseID;
            parameters[1].Value = model.spersonID;
            parameters[2].Value = model.sunitID;
            parameters[3].Value = model.evName;
            parameters[4].Value = model.comment;
            parameters[5].Value = model.evAdmin;
            parameters[6].Value = model.dirPath;
            parameters[7].Value = model.evSize;
            parameters[8].Value = model.emlCount;
            parameters[9].Value = model.docCount;
            parameters[10].Value = model.pdfCount;
            parameters[11].Value = model.zipCount;
            parameters[12].Value = model.rarCount;
            parameters[13].Value = model.percent;
            parameters[14].Value = model.finished;
            parameters[15].Value = model.status;
            parameters[16].Value = model.mailBox;
            parameters[17].Value = model.dealinfo;
            parameters[18].Value = model.addTime;
            parameters[19].Value = model.currFlag;
            parameters[20].Value = model.finishTime;
            parameters[21].Value = model.uptype;
            parameters[22].Value = model.isdel;
            parameters[23].Value = model.startSolrTime;
            parameters[24].Value = model.evType;
            parameters[25].Value = model.indexFlag;
            parameters[26].Value = model.UUID;
            parameters[27].Value = model.dataTypes;
            parameters[28].Value = model.uploadNum;
            parameters[29].Value = model.successNum;
            parameters[30].Value = model.errorNum;
            parameters[31].Value = model.localPath;
            parameters[32].Value = model.id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from evidence ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
            parameters[0].Value = id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from evidence ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.evidence GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,caseID,spersonID,sunitID,evName,comment,evAdmin,dirPath,evSize,emlCount,docCount,pdfCount,zipCount,rarCount,percent,finished,status,mailBox,dealinfo,addTime,currFlag,finishTime,uptype,isdel,startSolrTime,evType,indexFlag,UUID,dataTypes,uploadNum,successNum,errorNum,localPath from evidence ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
            parameters[0].Value = id;

            Maticsoft.Model.evidence model = new Maticsoft.Model.evidence();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.evidence DataRowToModel(DataRow row)
        {
            Maticsoft.Model.evidence model = new Maticsoft.Model.evidence();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["caseID"] != null && row["caseID"].ToString() != "")
                {
                    model.caseID = int.Parse(row["caseID"].ToString());
                }
                if (row["spersonID"] != null && row["spersonID"].ToString() != "")
                {
                    model.spersonID = int.Parse(row["spersonID"].ToString());
                }
                if (row["sunitID"] != null && row["sunitID"].ToString() != "")
                {
                    model.sunitID = int.Parse(row["sunitID"].ToString());
                }
                if (row["evName"] != null)
                {
                    model.evName = row["evName"].ToString();
                }
                if (row["comment"] != null)
                {
                    model.comment = row["comment"].ToString();
                }
                if (row["evAdmin"] != null)
                {
                    model.evAdmin = row["evAdmin"].ToString();
                }
                if (row["dirPath"] != null)
                {
                    model.dirPath = row["dirPath"].ToString();
                }
                if (row["evSize"] != null)
                {
                    model.evSize = row["evSize"].ToString();
                }
                if (row["emlCount"] != null && row["emlCount"].ToString() != "")
                {
                    model.emlCount = int.Parse(row["emlCount"].ToString());
                }
                if (row["docCount"] != null && row["docCount"].ToString() != "")
                {
                    model.docCount = int.Parse(row["docCount"].ToString());
                }
                if (row["pdfCount"] != null && row["pdfCount"].ToString() != "")
                {
                    model.pdfCount = int.Parse(row["pdfCount"].ToString());
                }
                if (row["zipCount"] != null && row["zipCount"].ToString() != "")
                {
                    model.zipCount = int.Parse(row["zipCount"].ToString());
                }
                if (row["rarCount"] != null && row["rarCount"].ToString() != "")
                {
                    model.rarCount = int.Parse(row["rarCount"].ToString());
                }
                if (row["percent"] != null)
                {
                    model.percent = row["percent"].ToString();
                }
                if (row["finished"] != null)
                {
                    model.finished = row["finished"].ToString();
                }
                if (row["status"] != null)
                {
                    model.status = row["status"].ToString();
                }
                if (row["mailBox"] != null)
                {
                    model.mailBox = row["mailBox"].ToString();
                }
                if (row["dealinfo"] != null)
                {
                    model.dealinfo = row["dealinfo"].ToString();
                }
                if (row["addTime"] != null)
                {
                    model.addTime = row["addTime"].ToString();
                }
                if (row["currFlag"] != null)
                {
                    model.currFlag = row["currFlag"].ToString();
                }
                if (row["finishTime"] != null)
                {
                    model.finishTime = row["finishTime"].ToString();
                }
                if (row["uptype"] != null && row["uptype"].ToString() != "")
                {
                    model.uptype = int.Parse(row["uptype"].ToString());
                }
                if (row["isdel"] != null && row["isdel"].ToString() != "")
                {
                    model.isdel = int.Parse(row["isdel"].ToString());
                }
                if (row["startSolrTime"] != null)
                {
                    model.startSolrTime = row["startSolrTime"].ToString();
                }
                if (row["evType"] != null && row["evType"].ToString() != "")
                {
                    model.evType = int.Parse(row["evType"].ToString());
                }
                if (row["indexFlag"] != null && row["indexFlag"].ToString() != "")
                {
                    model.indexFlag = int.Parse(row["indexFlag"].ToString());
                }
                if (row["UUID"] != null)
                {
                    model.UUID = row["UUID"].ToString();
                }
                if (row["dataTypes"] != null && row["dataTypes"].ToString() != "")
                {
                    model.dataTypes = int.Parse(row["dataTypes"].ToString());
                }
                if (row["uploadNum"] != null)
                {
                    model.uploadNum = row["uploadNum"].ToString();
                }
                if (row["successNum"] != null)
                {
                    model.successNum = row["successNum"].ToString();
                }
                if (row["errorNum"] != null)
                {
                    model.errorNum = row["errorNum"].ToString();
                }
                if (row["localPath"] != null)
                {
                    model.localPath = row["localPath"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,caseID,spersonID,sunitID,evName,comment,evAdmin,dirPath,evSize,emlCount,docCount,pdfCount,zipCount,rarCount,percent,finished,status,mailBox,dealinfo,addTime,currFlag,finishTime,uptype,isdel,startSolrTime,evType,indexFlag,UUID,dataTypes,uploadNum,successNum,errorNum,localPath ");
            strSql.Append(" FROM evidence ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM evidence ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from evidence T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            MySqlParameter[] parameters = {
                    new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@PageSize", MySqlDbType.Int32),
                    new MySqlParameter("@PageIndex", MySqlDbType.Int32),
                    new MySqlParameter("@IsReCount", MySqlDbType.Bit),
                    new MySqlParameter("@OrderType", MySqlDbType.Bit),
                    new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "evidence";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

