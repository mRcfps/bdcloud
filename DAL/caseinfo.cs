using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:caseinfo
	/// </summary>
	public partial class caseinfo
	{
		public caseinfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("id", "caseinfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from caseinfo");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.caseinfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into caseinfo(");
			strSql.Append("caseName,caseNum,userName,mainParty,createdTime,relatedCompany,relatedObject,trustee,inquireTime,status,remark,label,section,labels,department,sheng,shi,qu,caseType,acceptTime,allArea,allParent,whereFrom,threadType,suspectAddressID,threadSource,suspectPersonID,suspectUnitID,susItem,commitCrimeDate,disposal,crossNum,userRole,userID,userPartment,userSection,agreeStatus,substation,other,otherText,opinionInfo,fileUrl,fileName,isThread)");
			strSql.Append(" values (");
			strSql.Append("@caseName,@caseNum,@userName,@mainParty,@createdTime,@relatedCompany,@relatedObject,@trustee,@inquireTime,@status,@remark,@label,@section,@labels,@department,@sheng,@shi,@qu,@caseType,@acceptTime,@allArea,@allParent,@whereFrom,@threadType,@suspectAddressID,@threadSource,@suspectPersonID,@suspectUnitID,@susItem,@commitCrimeDate,@disposal,@crossNum,@userRole,@userID,@userPartment,@userSection,@agreeStatus,@substation,@other,@otherText,@opinionInfo,@fileUrl,@fileName,@isThread)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@caseName", MySqlDbType.VarChar,100),
					new MySqlParameter("@caseNum", MySqlDbType.VarChar,100),
					new MySqlParameter("@userName", MySqlDbType.VarChar,100),
					new MySqlParameter("@mainParty", MySqlDbType.VarChar,100),
					new MySqlParameter("@createdTime", MySqlDbType.VarChar,100),
					new MySqlParameter("@relatedCompany", MySqlDbType.VarChar,500),
					new MySqlParameter("@relatedObject", MySqlDbType.VarChar,500),
					new MySqlParameter("@trustee", MySqlDbType.VarChar,500),
					new MySqlParameter("@inquireTime", MySqlDbType.VarChar,100),
					new MySqlParameter("@status", MySqlDbType.VarChar,100),
					new MySqlParameter("@remark", MySqlDbType.VarChar,500),
					new MySqlParameter("@label", MySqlDbType.VarChar,500),
					new MySqlParameter("@section", MySqlDbType.VarChar,200),
					new MySqlParameter("@labels", MySqlDbType.VarChar,255),
					new MySqlParameter("@department", MySqlDbType.VarChar,200),
					new MySqlParameter("@sheng", MySqlDbType.VarChar,255),
					new MySqlParameter("@shi", MySqlDbType.VarChar,255),
					new MySqlParameter("@qu", MySqlDbType.VarChar,255),
					new MySqlParameter("@caseType", MySqlDbType.VarChar,255),
					new MySqlParameter("@acceptTime", MySqlDbType.VarChar,100),
					new MySqlParameter("@allArea", MySqlDbType.VarChar,255),
					new MySqlParameter("@allParent", MySqlDbType.VarChar,255),
					new MySqlParameter("@whereFrom", MySqlDbType.VarChar,255),
					new MySqlParameter("@threadType", MySqlDbType.VarChar,255),
					new MySqlParameter("@suspectAddressID", MySqlDbType.VarChar,255),
					new MySqlParameter("@threadSource", MySqlDbType.VarChar,255),
					new MySqlParameter("@suspectPersonID", MySqlDbType.VarChar,255),
					new MySqlParameter("@suspectUnitID", MySqlDbType.VarChar,255),
					new MySqlParameter("@susItem", MySqlDbType.VarChar,2000),
					new MySqlParameter("@commitCrimeDate", MySqlDbType.VarChar,255),
					new MySqlParameter("@disposal", MySqlDbType.VarChar,255),
					new MySqlParameter("@crossNum", MySqlDbType.VarChar,255),
					new MySqlParameter("@userRole", MySqlDbType.VarChar,255),
					new MySqlParameter("@userID", MySqlDbType.VarChar,255),
					new MySqlParameter("@userPartment", MySqlDbType.VarChar,255),
					new MySqlParameter("@userSection", MySqlDbType.VarChar,255),
					new MySqlParameter("@agreeStatus", MySqlDbType.VarChar,255),
					new MySqlParameter("@substation", MySqlDbType.VarChar,255),
					new MySqlParameter("@other", MySqlDbType.VarChar,255),
					new MySqlParameter("@otherText", MySqlDbType.VarChar,255),
					new MySqlParameter("@opinionInfo", MySqlDbType.VarChar,255),
					new MySqlParameter("@fileUrl", MySqlDbType.VarChar,255),
					new MySqlParameter("@fileName", MySqlDbType.VarChar,255),
					new MySqlParameter("@isThread", MySqlDbType.VarChar,20)};
			parameters[0].Value = model.caseName;
			parameters[1].Value = model.caseNum;
			parameters[2].Value = model.userName;
			parameters[3].Value = model.mainParty;
			parameters[4].Value = model.createdTime;
			parameters[5].Value = model.relatedCompany;
			parameters[6].Value = model.relatedObject;
			parameters[7].Value = model.trustee;
			parameters[8].Value = model.inquireTime;
			parameters[9].Value = model.status;
			parameters[10].Value = model.remark;
			parameters[11].Value = model.label;
			parameters[12].Value = model.section;
			parameters[13].Value = model.labels;
			parameters[14].Value = model.department;
			parameters[15].Value = model.sheng;
			parameters[16].Value = model.shi;
			parameters[17].Value = model.qu;
			parameters[18].Value = model.caseType;
			parameters[19].Value = model.acceptTime;
			parameters[20].Value = model.allArea;
			parameters[21].Value = model.allParent;
			parameters[22].Value = model.whereFrom;
			parameters[23].Value = model.threadType;
			parameters[24].Value = model.suspectAddressID;
			parameters[25].Value = model.threadSource;
			parameters[26].Value = model.suspectPersonID;
			parameters[27].Value = model.suspectUnitID;
			parameters[28].Value = model.susItem;
			parameters[29].Value = model.commitCrimeDate;
			parameters[30].Value = model.disposal;
			parameters[31].Value = model.crossNum;
			parameters[32].Value = model.userRole;
			parameters[33].Value = model.userID;
			parameters[34].Value = model.userPartment;
			parameters[35].Value = model.userSection;
			parameters[36].Value = model.agreeStatus;
			parameters[37].Value = model.substation;
			parameters[38].Value = model.other;
			parameters[39].Value = model.otherText;
			parameters[40].Value = model.opinionInfo;
			parameters[41].Value = model.fileUrl;
			parameters[42].Value = model.fileName;
			parameters[43].Value = model.isThread;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(Maticsoft.Model.caseinfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update caseinfo set ");
			strSql.Append("caseName=@caseName,");
			strSql.Append("caseNum=@caseNum,");
			strSql.Append("userName=@userName,");
			strSql.Append("mainParty=@mainParty,");
			strSql.Append("createdTime=@createdTime,");
			strSql.Append("relatedCompany=@relatedCompany,");
			strSql.Append("relatedObject=@relatedObject,");
			strSql.Append("trustee=@trustee,");
			strSql.Append("inquireTime=@inquireTime,");
			strSql.Append("status=@status,");
			strSql.Append("remark=@remark,");
			strSql.Append("label=@label,");
			strSql.Append("section=@section,");
			strSql.Append("labels=@labels,");
			strSql.Append("department=@department,");
			strSql.Append("sheng=@sheng,");
			strSql.Append("shi=@shi,");
			strSql.Append("qu=@qu,");
			strSql.Append("caseType=@caseType,");
			strSql.Append("acceptTime=@acceptTime,");
			strSql.Append("allArea=@allArea,");
			strSql.Append("allParent=@allParent,");
			strSql.Append("whereFrom=@whereFrom,");
			strSql.Append("threadType=@threadType,");
			strSql.Append("suspectAddressID=@suspectAddressID,");
			strSql.Append("threadSource=@threadSource,");
			strSql.Append("suspectPersonID=@suspectPersonID,");
			strSql.Append("suspectUnitID=@suspectUnitID,");
			strSql.Append("susItem=@susItem,");
			strSql.Append("commitCrimeDate=@commitCrimeDate,");
			strSql.Append("disposal=@disposal,");
			strSql.Append("crossNum=@crossNum,");
			strSql.Append("userRole=@userRole,");
			strSql.Append("userID=@userID,");
			strSql.Append("userPartment=@userPartment,");
			strSql.Append("userSection=@userSection,");
			strSql.Append("agreeStatus=@agreeStatus,");
			strSql.Append("substation=@substation,");
			strSql.Append("other=@other,");
			strSql.Append("otherText=@otherText,");
			strSql.Append("opinionInfo=@opinionInfo,");
			strSql.Append("fileUrl=@fileUrl,");
			strSql.Append("fileName=@fileName,");
			strSql.Append("isThread=@isThread");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@caseName", MySqlDbType.VarChar,100),
					new MySqlParameter("@caseNum", MySqlDbType.VarChar,100),
					new MySqlParameter("@userName", MySqlDbType.VarChar,100),
					new MySqlParameter("@mainParty", MySqlDbType.VarChar,100),
					new MySqlParameter("@createdTime", MySqlDbType.VarChar,100),
					new MySqlParameter("@relatedCompany", MySqlDbType.VarChar,500),
					new MySqlParameter("@relatedObject", MySqlDbType.VarChar,500),
					new MySqlParameter("@trustee", MySqlDbType.VarChar,500),
					new MySqlParameter("@inquireTime", MySqlDbType.VarChar,100),
					new MySqlParameter("@status", MySqlDbType.VarChar,100),
					new MySqlParameter("@remark", MySqlDbType.VarChar,500),
					new MySqlParameter("@label", MySqlDbType.VarChar,500),
					new MySqlParameter("@section", MySqlDbType.VarChar,200),
					new MySqlParameter("@labels", MySqlDbType.VarChar,255),
					new MySqlParameter("@department", MySqlDbType.VarChar,200),
					new MySqlParameter("@sheng", MySqlDbType.VarChar,255),
					new MySqlParameter("@shi", MySqlDbType.VarChar,255),
					new MySqlParameter("@qu", MySqlDbType.VarChar,255),
					new MySqlParameter("@caseType", MySqlDbType.VarChar,255),
					new MySqlParameter("@acceptTime", MySqlDbType.VarChar,100),
					new MySqlParameter("@allArea", MySqlDbType.VarChar,255),
					new MySqlParameter("@allParent", MySqlDbType.VarChar,255),
					new MySqlParameter("@whereFrom", MySqlDbType.VarChar,255),
					new MySqlParameter("@threadType", MySqlDbType.VarChar,255),
					new MySqlParameter("@suspectAddressID", MySqlDbType.VarChar,255),
					new MySqlParameter("@threadSource", MySqlDbType.VarChar,255),
					new MySqlParameter("@suspectPersonID", MySqlDbType.VarChar,255),
					new MySqlParameter("@suspectUnitID", MySqlDbType.VarChar,255),
					new MySqlParameter("@susItem", MySqlDbType.VarChar,2000),
					new MySqlParameter("@commitCrimeDate", MySqlDbType.VarChar,255),
					new MySqlParameter("@disposal", MySqlDbType.VarChar,255),
					new MySqlParameter("@crossNum", MySqlDbType.VarChar,255),
					new MySqlParameter("@userRole", MySqlDbType.VarChar,255),
					new MySqlParameter("@userID", MySqlDbType.VarChar,255),
					new MySqlParameter("@userPartment", MySqlDbType.VarChar,255),
					new MySqlParameter("@userSection", MySqlDbType.VarChar,255),
					new MySqlParameter("@agreeStatus", MySqlDbType.VarChar,255),
					new MySqlParameter("@substation", MySqlDbType.VarChar,255),
					new MySqlParameter("@other", MySqlDbType.VarChar,255),
					new MySqlParameter("@otherText", MySqlDbType.VarChar,255),
					new MySqlParameter("@opinionInfo", MySqlDbType.VarChar,255),
					new MySqlParameter("@fileUrl", MySqlDbType.VarChar,255),
					new MySqlParameter("@fileName", MySqlDbType.VarChar,255),
					new MySqlParameter("@isThread", MySqlDbType.VarChar,20),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
			parameters[0].Value = model.caseName;
			parameters[1].Value = model.caseNum;
			parameters[2].Value = model.userName;
			parameters[3].Value = model.mainParty;
			parameters[4].Value = model.createdTime;
			parameters[5].Value = model.relatedCompany;
			parameters[6].Value = model.relatedObject;
			parameters[7].Value = model.trustee;
			parameters[8].Value = model.inquireTime;
			parameters[9].Value = model.status;
			parameters[10].Value = model.remark;
			parameters[11].Value = model.label;
			parameters[12].Value = model.section;
			parameters[13].Value = model.labels;
			parameters[14].Value = model.department;
			parameters[15].Value = model.sheng;
			parameters[16].Value = model.shi;
			parameters[17].Value = model.qu;
			parameters[18].Value = model.caseType;
			parameters[19].Value = model.acceptTime;
			parameters[20].Value = model.allArea;
			parameters[21].Value = model.allParent;
			parameters[22].Value = model.whereFrom;
			parameters[23].Value = model.threadType;
			parameters[24].Value = model.suspectAddressID;
			parameters[25].Value = model.threadSource;
			parameters[26].Value = model.suspectPersonID;
			parameters[27].Value = model.suspectUnitID;
			parameters[28].Value = model.susItem;
			parameters[29].Value = model.commitCrimeDate;
			parameters[30].Value = model.disposal;
			parameters[31].Value = model.crossNum;
			parameters[32].Value = model.userRole;
			parameters[33].Value = model.userID;
			parameters[34].Value = model.userPartment;
			parameters[35].Value = model.userSection;
			parameters[36].Value = model.agreeStatus;
			parameters[37].Value = model.substation;
			parameters[38].Value = model.other;
			parameters[39].Value = model.otherText;
			parameters[40].Value = model.opinionInfo;
			parameters[41].Value = model.fileUrl;
			parameters[42].Value = model.fileName;
			parameters[43].Value = model.isThread;
			parameters[44].Value = model.id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from caseinfo ");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from caseinfo ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.caseinfo GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,caseName,caseNum,userName,mainParty,createdTime,relatedCompany,relatedObject,trustee,inquireTime,status,remark,label,section,labels,department,sheng,shi,qu,caseType,acceptTime,allArea,allParent,whereFrom,threadType,suspectAddressID,threadSource,suspectPersonID,suspectUnitID,susItem,commitCrimeDate,disposal,crossNum,userRole,userID,userPartment,userSection,agreeStatus,substation,other,otherText,opinionInfo,fileUrl,fileName,isThread from caseinfo ");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			Maticsoft.Model.caseinfo model=new Maticsoft.Model.caseinfo();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public Maticsoft.Model.caseinfo DataRowToModel(DataRow row)
		{
			Maticsoft.Model.caseinfo model=new Maticsoft.Model.caseinfo();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["caseName"]!=null)
				{
					model.caseName=row["caseName"].ToString();
				}
				if(row["caseNum"]!=null)
				{
					model.caseNum=row["caseNum"].ToString();
				}
				if(row["userName"]!=null)
				{
					model.userName=row["userName"].ToString();
				}
				if(row["mainParty"]!=null)
				{
					model.mainParty=row["mainParty"].ToString();
				}
				if(row["createdTime"]!=null)
				{
					model.createdTime=row["createdTime"].ToString();
				}
				if(row["relatedCompany"]!=null)
				{
					model.relatedCompany=row["relatedCompany"].ToString();
				}
				if(row["relatedObject"]!=null)
				{
					model.relatedObject=row["relatedObject"].ToString();
				}
				if(row["trustee"]!=null)
				{
					model.trustee=row["trustee"].ToString();
				}
				if(row["inquireTime"]!=null)
				{
					model.inquireTime=row["inquireTime"].ToString();
				}
				if(row["status"]!=null)
				{
					model.status=row["status"].ToString();
				}
				if(row["remark"]!=null)
				{
					model.remark=row["remark"].ToString();
				}
				if(row["label"]!=null)
				{
					model.label=row["label"].ToString();
				}
				if(row["section"]!=null)
				{
					model.section=row["section"].ToString();
				}
				if(row["labels"]!=null)
				{
					model.labels=row["labels"].ToString();
				}
				if(row["department"]!=null)
				{
					model.department=row["department"].ToString();
				}
				if(row["sheng"]!=null)
				{
					model.sheng=row["sheng"].ToString();
				}
				if(row["shi"]!=null)
				{
					model.shi=row["shi"].ToString();
				}
				if(row["qu"]!=null)
				{
					model.qu=row["qu"].ToString();
				}
				if(row["caseType"]!=null)
				{
					model.caseType=row["caseType"].ToString();
				}
				if(row["acceptTime"]!=null)
				{
					model.acceptTime=row["acceptTime"].ToString();
				}
				if(row["allArea"]!=null)
				{
					model.allArea=row["allArea"].ToString();
				}
				if(row["allParent"]!=null)
				{
					model.allParent=row["allParent"].ToString();
				}
				if(row["whereFrom"]!=null)
				{
					model.whereFrom=row["whereFrom"].ToString();
				}
				if(row["threadType"]!=null)
				{
					model.threadType=row["threadType"].ToString();
				}
				if(row["suspectAddressID"]!=null)
				{
					model.suspectAddressID=row["suspectAddressID"].ToString();
				}
				if(row["threadSource"]!=null)
				{
					model.threadSource=row["threadSource"].ToString();
				}
				if(row["suspectPersonID"]!=null)
				{
					model.suspectPersonID=row["suspectPersonID"].ToString();
				}
				if(row["suspectUnitID"]!=null)
				{
					model.suspectUnitID=row["suspectUnitID"].ToString();
				}
				if(row["susItem"]!=null)
				{
					model.susItem=row["susItem"].ToString();
				}
				if(row["commitCrimeDate"]!=null)
				{
					model.commitCrimeDate=row["commitCrimeDate"].ToString();
				}
				if(row["disposal"]!=null)
				{
					model.disposal=row["disposal"].ToString();
				}
				if(row["crossNum"]!=null)
				{
					model.crossNum=row["crossNum"].ToString();
				}
				if(row["userRole"]!=null)
				{
					model.userRole=row["userRole"].ToString();
				}
				if(row["userID"]!=null)
				{
					model.userID=row["userID"].ToString();
				}
				if(row["userPartment"]!=null)
				{
					model.userPartment=row["userPartment"].ToString();
				}
				if(row["userSection"]!=null)
				{
					model.userSection=row["userSection"].ToString();
				}
				if(row["agreeStatus"]!=null)
				{
					model.agreeStatus=row["agreeStatus"].ToString();
				}
				if(row["substation"]!=null)
				{
					model.substation=row["substation"].ToString();
				}
				if(row["other"]!=null)
				{
					model.other=row["other"].ToString();
				}
				if(row["otherText"]!=null)
				{
					model.otherText=row["otherText"].ToString();
				}
				if(row["opinionInfo"]!=null)
				{
					model.opinionInfo=row["opinionInfo"].ToString();
				}
				if(row["fileUrl"]!=null)
				{
					model.fileUrl=row["fileUrl"].ToString();
				}
				if(row["fileName"]!=null)
				{
					model.fileName=row["fileName"].ToString();
				}
				if(row["isThread"]!=null)
				{
					model.isThread=row["isThread"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,caseName,caseNum,userName,mainParty,createdTime,relatedCompany,relatedObject,trustee,inquireTime,status,remark,label,section,labels,department,sheng,shi,qu,caseType,acceptTime,allArea,allParent,whereFrom,threadType,suspectAddressID,threadSource,suspectPersonID,suspectUnitID,susItem,commitCrimeDate,disposal,crossNum,userRole,userID,userPartment,userSection,agreeStatus,substation,other,otherText,opinionInfo,fileUrl,fileName,isThread ");
			strSql.Append(" FROM caseinfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM caseinfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperMySQL.GetSingle(strSql.ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from caseinfo T ");
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
			parameters[0].Value = "caseinfo";
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

