﻿using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// onlinenumber:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class onlinenumber
	{
		public onlinenumber()
		{}
		#region Model
		private int _id;
		private int? _evid;
		private string _username;
		private string _onlinestatus;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? evid
		{
			set{ _evid=value;}
			get{return _evid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string onlinestatus
		{
			set{ _onlinestatus=value;}
			get{return _onlinestatus;}
		}
		#endregion Model

	}
}

