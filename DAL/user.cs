using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:user
	/// </summary>
	public partial class user
	{
		public user()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("id", "user"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from user");
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
		public bool Add(Maticsoft.Model.user model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into user(");
			strSql.Append("username,password,createdTime,lastLoginTime,privilege,remark,sn,section,policeNO,cardNO,partment,telephone,phone,email,userrole,allParent,header)");
			strSql.Append(" values (");
			strSql.Append("@username,@password,@createdTime,@lastLoginTime,@privilege,@remark,@sn,@section,@policeNO,@cardNO,@partment,@telephone,@phone,@email,@userrole,@allParent,@header)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@username", MySqlDbType.VarChar,100),
					new MySqlParameter("@password", MySqlDbType.VarChar,100),
					new MySqlParameter("@createdTime", MySqlDbType.VarChar,100),
					new MySqlParameter("@lastLoginTime", MySqlDbType.VarChar,100),
					new MySqlParameter("@privilege", MySqlDbType.VarChar,100),
					new MySqlParameter("@remark", MySqlDbType.VarChar,500),
					new MySqlParameter("@sn", MySqlDbType.VarChar,100),
					new MySqlParameter("@section", MySqlDbType.VarChar,100),
					new MySqlParameter("@policeNO", MySqlDbType.VarChar,150),
					new MySqlParameter("@cardNO", MySqlDbType.VarChar,100),
					new MySqlParameter("@partment", MySqlDbType.VarChar,200),
					new MySqlParameter("@telephone", MySqlDbType.VarChar,50),
					new MySqlParameter("@phone", MySqlDbType.VarChar,50),
					new MySqlParameter("@email", MySqlDbType.VarChar,150),
					new MySqlParameter("@userrole", MySqlDbType.VarChar,111),
					new MySqlParameter("@allParent", MySqlDbType.VarChar,255),
					new MySqlParameter("@header", MySqlDbType.LongBlob)};
			parameters[0].Value = model.username;
			parameters[1].Value = model.password;
			parameters[2].Value = model.createdTime;
			parameters[3].Value = model.lastLoginTime;
			parameters[4].Value = model.privilege;
			parameters[5].Value = model.remark;
			parameters[6].Value = model.sn;
			parameters[7].Value = model.section;
			parameters[8].Value = model.policeNO;
			parameters[9].Value = model.cardNO;
			parameters[10].Value = model.partment;
			parameters[11].Value = model.telephone;
			parameters[12].Value = model.phone;
			parameters[13].Value = model.email;
			parameters[14].Value = model.userrole;
			parameters[15].Value = model.allParent;
			parameters[16].Value = model.header;

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
		public bool Update(Maticsoft.Model.user model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update user set ");
			strSql.Append("username=@username,");
			strSql.Append("password=@password,");
			strSql.Append("createdTime=@createdTime,");
			strSql.Append("lastLoginTime=@lastLoginTime,");
			strSql.Append("privilege=@privilege,");
			strSql.Append("remark=@remark,");
			strSql.Append("sn=@sn,");
			strSql.Append("section=@section,");
			strSql.Append("policeNO=@policeNO,");
			strSql.Append("cardNO=@cardNO,");
			strSql.Append("partment=@partment,");
			strSql.Append("telephone=@telephone,");
			strSql.Append("phone=@phone,");
			strSql.Append("email=@email,");
			strSql.Append("userrole=@userrole,");
			strSql.Append("allParent=@allParent,");
			strSql.Append("header=@header");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@username", MySqlDbType.VarChar,100),
					new MySqlParameter("@password", MySqlDbType.VarChar,100),
					new MySqlParameter("@createdTime", MySqlDbType.VarChar,100),
					new MySqlParameter("@lastLoginTime", MySqlDbType.VarChar,100),
					new MySqlParameter("@privilege", MySqlDbType.VarChar,100),
					new MySqlParameter("@remark", MySqlDbType.VarChar,500),
					new MySqlParameter("@sn", MySqlDbType.VarChar,100),
					new MySqlParameter("@section", MySqlDbType.VarChar,100),
					new MySqlParameter("@policeNO", MySqlDbType.VarChar,150),
					new MySqlParameter("@cardNO", MySqlDbType.VarChar,100),
					new MySqlParameter("@partment", MySqlDbType.VarChar,200),
					new MySqlParameter("@telephone", MySqlDbType.VarChar,50),
					new MySqlParameter("@phone", MySqlDbType.VarChar,50),
					new MySqlParameter("@email", MySqlDbType.VarChar,150),
					new MySqlParameter("@userrole", MySqlDbType.VarChar,111),
					new MySqlParameter("@allParent", MySqlDbType.VarChar,255),
					new MySqlParameter("@header", MySqlDbType.LongBlob),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
			parameters[0].Value = model.username;
			parameters[1].Value = model.password;
			parameters[2].Value = model.createdTime;
			parameters[3].Value = model.lastLoginTime;
			parameters[4].Value = model.privilege;
			parameters[5].Value = model.remark;
			parameters[6].Value = model.sn;
			parameters[7].Value = model.section;
			parameters[8].Value = model.policeNO;
			parameters[9].Value = model.cardNO;
			parameters[10].Value = model.partment;
			parameters[11].Value = model.telephone;
			parameters[12].Value = model.phone;
			parameters[13].Value = model.email;
			parameters[14].Value = model.userrole;
			parameters[15].Value = model.allParent;
			parameters[16].Value = model.header;
			parameters[17].Value = model.id;

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
			strSql.Append("delete from user ");
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
			strSql.Append("delete from user ");
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
		public Maticsoft.Model.user GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,username,password,createdTime,lastLoginTime,privilege,remark,sn,section,policeNO,cardNO,partment,telephone,phone,email,userrole,allParent,header from user ");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			Maticsoft.Model.user model=new Maticsoft.Model.user();
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
		public Maticsoft.Model.user DataRowToModel(DataRow row)
		{
			Maticsoft.Model.user model=new Maticsoft.Model.user();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["username"]!=null)
				{
					model.username=row["username"].ToString();
				}
				if(row["password"]!=null)
				{
					model.password=row["password"].ToString();
				}
				if(row["createdTime"]!=null)
				{
					model.createdTime=row["createdTime"].ToString();
				}
				if(row["lastLoginTime"]!=null)
				{
					model.lastLoginTime=row["lastLoginTime"].ToString();
				}
				if(row["privilege"]!=null)
				{
					model.privilege=row["privilege"].ToString();
				}
				if(row["remark"]!=null)
				{
					model.remark=row["remark"].ToString();
				}
				if(row["sn"]!=null)
				{
					model.sn=row["sn"].ToString();
				}
				if(row["section"]!=null)
				{
					model.section=row["section"].ToString();
				}
				if(row["policeNO"]!=null)
				{
					model.policeNO=row["policeNO"].ToString();
				}
				if(row["cardNO"]!=null)
				{
					model.cardNO=row["cardNO"].ToString();
				}
				if(row["partment"]!=null)
				{
					model.partment=row["partment"].ToString();
				}
				if(row["telephone"]!=null)
				{
					model.telephone=row["telephone"].ToString();
				}
				if(row["phone"]!=null)
				{
					model.phone=row["phone"].ToString();
				}
				if(row["email"]!=null)
				{
					model.email=row["email"].ToString();
				}
				if(row["userrole"]!=null)
				{
					model.userrole=row["userrole"].ToString();
				}
				if(row["allParent"]!=null)
				{
					model.allParent=row["allParent"].ToString();
				}
                if (row["header"] != null)
                {
                    model.header = row["header"] as byte[];
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
			strSql.Append("select id,username,password,createdTime,lastLoginTime,privilege,remark,sn,section,policeNO,cardNO,partment,telephone,phone,email,userrole,allParent,header ");
			strSql.Append(" FROM user ");
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
			strSql.Append("select count(1) FROM user ");
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
			strSql.Append(")AS Row, T.*  from user T ");
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
			parameters[0].Value = "user";
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

