using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// evidence:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class evidence
    {
        public evidence()
        { }
        #region Model
        private int _id;
        private int? _caseid;
        private int? _spersonid;
        private int? _sunitid;
        private string _evname;
        private string _comment;
        private string _evadmin;
        private string _dirpath;
        private string _evsize = "0";
        private int? _emlcount;
        private int? _doccount;
        private int? _pdfcount;
        private int? _zipcount;
        private int? _rarcount;
        private string _percent;
        private string _finished;
        private string _status;
        private string _mailbox;
        private string _dealinfo;
        private string _addtime;
        private string _currflag;
        private string _finishtime;
        private int _uptype = 0;
        private int _isdel = 0;
        private string _startsolrtime;
        private int? _evtype;
        private int? _indexflag;
        private string _uuid;
        private int? _datatypes;
        private string _uploadnum;
        private string _successnum;
        private string _errornum;
        private string _localpath;
        /// <summary>
        /// auto_increment
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? caseID
        {
            set { _caseid = value; }
            get { return _caseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? spersonID
        {
            set { _spersonid = value; }
            get { return _spersonid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sunitID
        {
            set { _sunitid = value; }
            get { return _sunitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string evName
        {
            set { _evname = value; }
            get { return _evname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string evAdmin
        {
            set { _evadmin = value; }
            get { return _evadmin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dirPath
        {
            set { _dirpath = value; }
            get { return _dirpath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string evSize
        {
            set { _evsize = value; }
            get { return _evsize; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? emlCount
        {
            set { _emlcount = value; }
            get { return _emlcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? docCount
        {
            set { _doccount = value; }
            get { return _doccount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? pdfCount
        {
            set { _pdfcount = value; }
            get { return _pdfcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? zipCount
        {
            set { _zipcount = value; }
            get { return _zipcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? rarCount
        {
            set { _rarcount = value; }
            get { return _rarcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string percent
        {
            set { _percent = value; }
            get { return _percent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string finished
        {
            set { _finished = value; }
            get { return _finished; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mailBox
        {
            set { _mailbox = value; }
            get { return _mailbox; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dealinfo
        {
            set { _dealinfo = value; }
            get { return _dealinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string addTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string currFlag
        {
            set { _currflag = value; }
            get { return _currflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string finishTime
        {
            set { _finishtime = value; }
            get { return _finishtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int uptype
        {
            set { _uptype = value; }
            get { return _uptype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int isdel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string startSolrTime
        {
            set { _startsolrtime = value; }
            get { return _startsolrtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? evType
        {
            set { _evtype = value; }
            get { return _evtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? indexFlag
        {
            set { _indexflag = value; }
            get { return _indexflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UUID
        {
            set { _uuid = value; }
            get { return _uuid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? dataTypes
        {
            set { _datatypes = value; }
            get { return _datatypes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string uploadNum
        {
            set { _uploadnum = value; }
            get { return _uploadnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string successNum
        {
            set { _successnum = value; }
            get { return _successnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string errorNum
        {
            set { _errornum = value; }
            get { return _errornum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string localPath
        {
            set { _localpath = value; }
            get { return _localpath; }
        }
        #endregion Model

    }
}

