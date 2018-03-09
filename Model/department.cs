using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// department:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class department
	{
		public department()
		{}
		#region Model
		private int _id;
		private string _departmentnum;
		private string _departmentname;
		private string _remarks;
		private string _foundtime;
		private string _found;
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
		public string departmentNum
		{
			set{ _departmentnum=value;}
			get{return _departmentnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string departmentName
		{
			set{ _departmentname=value;}
			get{return _departmentname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string foundtime
		{
			set{ _foundtime=value;}
			get{return _foundtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string found
		{
			set{ _found=value;}
			get{return _found;}
		}
		#endregion Model

	}
}

