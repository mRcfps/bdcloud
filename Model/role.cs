using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// role:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class role
	{
		public role()
		{}
		#region Model
		private int _id;
		private string _rolename;
		private string _roledescribe;
		private string _organization;
		private string _datascope;
		private string _rolemenu;
		private string _addman;
		private string _addtime;
		private string _department;
		private string _section;
		private string _allparent;
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
		public string roleName
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string roleDescribe
		{
			set{ _roledescribe=value;}
			get{return _roledescribe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string organization
		{
			set{ _organization=value;}
			get{return _organization;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dataScope
		{
			set{ _datascope=value;}
			get{return _datascope;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string roleMenu
		{
			set{ _rolemenu=value;}
			get{return _rolemenu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string addMan
		{
			set{ _addman=value;}
			get{return _addman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string addTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string department
		{
			set{ _department=value;}
			get{return _department;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string section
		{
			set{ _section=value;}
			get{return _section;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string allParent
		{
			set{ _allparent=value;}
			get{return _allparent;}
		}
		#endregion Model

	}
}

