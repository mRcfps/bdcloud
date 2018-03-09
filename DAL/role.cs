using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:role
	/// </summary>
	public partial class role
	{
		public role()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("id", "role"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from role");
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
		public bool Add(Maticsoft.Model.role model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into role(");
			strSql.Append("roleName,roleDescribe,organization,dataScope,roleMenu,addMan,addTime,department,section,allParent)");
			strSql.Append(" values (");
			strSql.Append("@roleName,@roleDescribe,@organization,@dataScope,@roleMenu,@addMan,@addTime,@department,@section,@allParent)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@roleName", MySqlDbType.VarChar,255),
					new MySqlParameter("@roleDescribe", MySqlDbType.VarChar,255),
					new MySqlParameter("@organization", MySqlDbType.VarChar,255),
					new MySqlParameter("@dataScope", MySqlDbType.VarChar,255),
					new MySqlParameter("@roleMenu", MySqlDbType.VarChar,255),
					new MySqlParameter("@addMan", MySqlDbType.VarChar,255),
					new MySqlParameter("@addTime", MySqlDbType.VarChar,255),
					new MySqlParameter("@department", MySqlDbType.VarChar,255),
					new MySqlParameter("@section", MySqlDbType.VarChar,255),
					new MySqlParameter("@allParent", MySqlDbType.VarChar,255)};
			parameters[0].Value = model.roleName;
			parameters[1].Value = model.roleDescribe;
			parameters[2].Value = model.organization;
			parameters[3].Value = model.dataScope;
			parameters[4].Value = model.roleMenu;
			parameters[5].Value = model.addMan;
			parameters[6].Value = model.addTime;
			parameters[7].Value = model.department;
			parameters[8].Value = model.section;
			parameters[9].Value = model.allParent;

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
		public bool Update(Maticsoft.Model.role model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update role set ");
			strSql.Append("roleName=@roleName,");
			strSql.Append("roleDescribe=@roleDescribe,");
			strSql.Append("organization=@organization,");
			strSql.Append("dataScope=@dataScope,");
			strSql.Append("roleMenu=@roleMenu,");
			strSql.Append("addMan=@addMan,");
			strSql.Append("addTime=@addTime,");
			strSql.Append("department=@department,");
			strSql.Append("section=@section,");
			strSql.Append("allParent=@allParent");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@roleName", MySqlDbType.VarChar,255),
					new MySqlParameter("@roleDescribe", MySqlDbType.VarChar,255),
					new MySqlParameter("@organization", MySqlDbType.VarChar,255),
					new MySqlParameter("@dataScope", MySqlDbType.VarChar,255),
					new MySqlParameter("@roleMenu", MySqlDbType.VarChar,255),
					new MySqlParameter("@addMan", MySqlDbType.VarChar,255),
					new MySqlParameter("@addTime", MySqlDbType.VarChar,255),
					new MySqlParameter("@department", MySqlDbType.VarChar,255),
					new MySqlParameter("@section", MySqlDbType.VarChar,255),
					new MySqlParameter("@allParent", MySqlDbType.VarChar,255),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
			parameters[0].Value = model.roleName;
			parameters[1].Value = model.roleDescribe;
			parameters[2].Value = model.organization;
			parameters[3].Value = model.dataScope;
			parameters[4].Value = model.roleMenu;
			parameters[5].Value = model.addMan;
			parameters[6].Value = model.addTime;
			parameters[7].Value = model.department;
			parameters[8].Value = model.section;
			parameters[9].Value = model.allParent;
			parameters[10].Value = model.id;

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
			strSql.Append("delete from role ");
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
			strSql.Append("delete from role ");
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
		public Maticsoft.Model.role GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,roleName,roleDescribe,organization,dataScope,roleMenu,addMan,addTime,department,section,allParent from role ");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			Maticsoft.Model.role model=new Maticsoft.Model.role();
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
		public Maticsoft.Model.role DataRowToModel(DataRow row)
		{
			Maticsoft.Model.role model=new Maticsoft.Model.role();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["roleName"]!=null)
				{
					model.roleName=row["roleName"].ToString();
				}
				if(row["roleDescribe"]!=null)
				{
					model.roleDescribe=row["roleDescribe"].ToString();
				}
				if(row["organization"]!=null)
				{
					model.organization=row["organization"].ToString();
				}
				if(row["dataScope"]!=null)
				{
					model.dataScope=row["dataScope"].ToString();
				}
				if(row["roleMenu"]!=null)
				{
					model.roleMenu=row["roleMenu"].ToString();
				}
				if(row["addMan"]!=null)
				{
					model.addMan=row["addMan"].ToString();
				}
				if(row["addTime"]!=null)
				{
					model.addTime=row["addTime"].ToString();
				}
				if(row["department"]!=null)
				{
					model.department=row["department"].ToString();
				}
				if(row["section"]!=null)
				{
					model.section=row["section"].ToString();
				}
				if(row["allParent"]!=null)
				{
					model.allParent=row["allParent"].ToString();
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
			strSql.Append("select id,roleName,roleDescribe,organization,dataScope,roleMenu,addMan,addTime,department,section,allParent ");
			strSql.Append(" FROM role ");
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
			strSql.Append("select count(1) FROM role ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			strSql.Append(")AS Row, T.*  from role T ");
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
			parameters[0].Value = "role";
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

