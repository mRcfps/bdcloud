using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// caseinfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class caseinfo
	{
		public caseinfo()
		{}
		#region Model
		private int _id;
		private string _casename;
		private string _casenum;
		private string _username;
		private string _mainparty;
		private string _createdtime;
		private string _relatedcompany;
		private string _relatedobject;
		private string _trustee;
		private string _inquiretime;
		private string _status;
		private string _remark;
		private string _label;
		private string _section= "1";
		private string _labels;
		private string _department= "1";
		private string _sheng;
		private string _shi;
		private string _qu;
		private string _casetype;
		private string _accepttime;
		private string _allarea;
		private string _allparent;
		private string _wherefrom;
		private string _threadtype;
		private string _suspectaddressid;
		private string _threadsource;
		private string _suspectpersonid;
		private string _suspectunitid;
		private string _susitem;
		private string _commitcrimedate;
		private string _disposal;
		private string _crossnum;
		private string _userrole;
		private string _userid;
		private string _userpartment;
		private string _usersection;
		private string _agreestatus;
		private string _substation;
		private string _other;
		private string _othertext;
		private string _opinioninfo;
		private string _fileurl;
		private string _filename;
		private string _isthread;
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
		public string caseName
		{
			set{ _casename=value;}
			get{return _casename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string caseNum
		{
			set{ _casenum=value;}
			get{return _casenum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mainParty
		{
			set{ _mainparty=value;}
			get{return _mainparty;}
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
		public string relatedCompany
		{
			set{ _relatedcompany=value;}
			get{return _relatedcompany;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string relatedObject
		{
			set{ _relatedobject=value;}
			get{return _relatedobject;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string trustee
		{
			set{ _trustee=value;}
			get{return _trustee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string inquireTime
		{
			set{ _inquiretime=value;}
			get{return _inquiretime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string status
		{
			set{ _status=value;}
			get{return _status;}
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
		public string label
		{
			set{ _label=value;}
			get{return _label;}
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
		public string labels
		{
			set{ _labels=value;}
			get{return _labels;}
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
		public string sheng
		{
			set{ _sheng=value;}
			get{return _sheng;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string shi
		{
			set{ _shi=value;}
			get{return _shi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string qu
		{
			set{ _qu=value;}
			get{return _qu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string caseType
		{
			set{ _casetype=value;}
			get{return _casetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string acceptTime
		{
			set{ _accepttime=value;}
			get{return _accepttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string allArea
		{
			set{ _allarea=value;}
			get{return _allarea;}
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
		public string whereFrom
		{
			set{ _wherefrom=value;}
			get{return _wherefrom;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string threadType
		{
			set{ _threadtype=value;}
			get{return _threadtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string suspectAddressID
		{
			set{ _suspectaddressid=value;}
			get{return _suspectaddressid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string threadSource
		{
			set{ _threadsource=value;}
			get{return _threadsource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string suspectPersonID
		{
			set{ _suspectpersonid=value;}
			get{return _suspectpersonid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string suspectUnitID
		{
			set{ _suspectunitid=value;}
			get{return _suspectunitid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string susItem
		{
			set{ _susitem=value;}
			get{return _susitem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string commitCrimeDate
		{
			set{ _commitcrimedate=value;}
			get{return _commitcrimedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string disposal
		{
			set{ _disposal=value;}
			get{return _disposal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string crossNum
		{
			set{ _crossnum=value;}
			get{return _crossnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userRole
		{
			set{ _userrole=value;}
			get{return _userrole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userPartment
		{
			set{ _userpartment=value;}
			get{return _userpartment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userSection
		{
			set{ _usersection=value;}
			get{return _usersection;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string agreeStatus
		{
			set{ _agreestatus=value;}
			get{return _agreestatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string substation
		{
			set{ _substation=value;}
			get{return _substation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string other
		{
			set{ _other=value;}
			get{return _other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string otherText
		{
			set{ _othertext=value;}
			get{return _othertext;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string opinionInfo
		{
			set{ _opinioninfo=value;}
			get{return _opinioninfo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fileUrl
		{
			set{ _fileurl=value;}
			get{return _fileurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fileName
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string isThread
		{
			set{ _isthread=value;}
			get{return _isthread;}
		}
		#endregion Model

	}
}

