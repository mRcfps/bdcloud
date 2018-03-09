using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// user:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class user
	{
		public user()
		{}
		#region Model
		private int _id;
		private string _username;
		private string _password;
		private string _createdtime;
		private string _lastlogintime;
		private string _privilege;
		private string _remark;
		private string _sn;
		private string _section;
		private string _policeno;
		private string _cardno;
		private string _partment;
		private string _telephone;
		private string _phone;
		private string _email;
		private string _userrole;
		private string _allparent;
        private byte[] _header;
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
		public string username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string createdTime
		{
			set{ _createdtime=value;}
			get{return _createdtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lastLoginTime
		{
			set{ _lastlogintime=value;}
			get{return _lastlogintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string privilege
		{
			set{ _privilege=value;}
			get{return _privilege;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sn
		{
			set{ _sn=value;}
			get{return _sn;}
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
		public string policeNO
		{
			set{ _policeno=value;}
			get{return _policeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cardNO
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string partment
		{
			set{ _partment=value;}
			get{return _partment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string telephone
		{
			set{ _telephone=value;}
			get{return _telephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userrole
		{
			set{ _userrole=value;}
			get{return _userrole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string allParent
		{
			set{ _allparent=value;}
			get{return _allparent;}
		}
		/// <summary>
		/// 
		/// </summary>
        public byte[] header
		{
			set{ _header=value;}
			get{return _header;}
		}
		#endregion Model

	}
}

