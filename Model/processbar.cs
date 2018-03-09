using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// processbar:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class processbar
	{
		public processbar()
		{}
		#region Model
		private int? _evid;
		private string _processstatus;
		/// <summary>
		/// 
		/// </summary>
		public int? evId
		{
			set{ _evid=value;}
			get{return _evid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string processStatus
		{
			set{ _processstatus=value;}
			get{return _processstatus;}
		}
		#endregion Model

	}
}

