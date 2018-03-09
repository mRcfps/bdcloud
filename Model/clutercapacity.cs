using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// clutercapacity:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class clutercapacity
	{
		public clutercapacity()
		{}
		#region Model
		private int _id;
		private string _totalcapacity;
		private string _usedcapacity;
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
		public string totalCapacity
		{
			set{ _totalcapacity=value;}
			get{return _totalcapacity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string usedCapacity
		{
			set{ _usedcapacity=value;}
			get{return _usedcapacity;}
		}
		#endregion Model

	}
}

