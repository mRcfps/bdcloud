using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;


namespace DataLineA
{
    public partial class UserControl1 : UserControl
    {
        public int extent = 0; //上传、解析的程度
        public bool isFinished = false;//是否上传完
        public bool isPause = true;//是否暂停
        public bool isFailed = false;

        //暂停按钮事件委托
        public delegate void ClikcPicBox(object sender, EventArgs e);
        public event ClikcPicBox pause;

        //删除按钮事件委托
        public event ClikcPicBox delete;
        
        public UserControl1()
        {
            InitializeComponent();

            //this.timer1.Interval = 60;//时间间隔
            //this.timer1.Enabled = true;//开启计时器
            init();
            //pictureBox2.Visible = false;
        }

        public void init() 
        {
            extent = 0;
            isFinished = false;
            isPause = true;

            resetProcessBar();
        }

        //根据查询到的进度重置进度条
        public void resetProcessBar() 
        {
            myProcessBar1.Value = extent;

            if (isFinished) //如果上传完
            {
                //暂停、删除按钮隐藏
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;

                myProcessBar1.ForegroundColor = Color.FromArgb(120, 208, 59);
                if (myProcessBar1.Value ==0)
                {
                    label7.Text = "解析失败";
                    label7.ForeColor = Color.FromArgb(153, 153, 153);
                }
                else if (myProcessBar1.Value < 100)
                {
                    label7.Text = "正在解析";
                    label7.ForeColor = Color.FromArgb(120, 208, 59);                   
                }
                else
                {
                    label7.Text = "解析完成";
                    label7.ForeColor = Color.FromArgb(153, 153, 153);
                }
            }
            else //如果没上传完
            {
                //暂停、删除按钮出现
                
                pictureBox1.Visible = true;
                //if (isPause) pictureBox1.Image = Properties.Resources.start;
                //else pictureBox1.Image = Properties.Resources.pause;

                pictureBox2.Visible = true;
                
                myProcessBar1.ForegroundColor = Color.FromArgb(4, 171, 229);
                if (myProcessBar1.Value == 0 && isFailed)
                {
                    pictureBox1.Visible = false;
                    label7.Text = "上传失败";
                    label7.ForeColor = Color.FromArgb(153, 153, 153);
                }
                else if (myProcessBar1.Value < 100)
                {
                    label7.Text = "上传中";
                    label7.ForeColor = Color.FromArgb(4, 171, 229);                    
                }
                else 
                {
                    label7.Text = "上传完成";
                    label7.ForeColor = Color.FromArgb(153, 153, 153);
                    //isFinished = true;
                }

            }
        }

        //测试用\使用计时器更新进度条
        private void timer1_Tick(object sender, EventArgs e)//计时器
        {

            if (myProcessBar1.Value < 100)
            {
                if (isFinished)
                {
                    label7.Text = "正在解析";
                    label7.ForeColor = Color.FromArgb(120, 208, 59);
                    myProcessBar1.ForegroundColor = Color.FromArgb(120, 208, 59);
                }
                else
                {
                    label7.Text = "上传中";
                    label7.ForeColor = Color.FromArgb(4, 171, 229);
                    myProcessBar1.ForegroundColor = Color.FromArgb(4, 171, 229);
                }
                myProcessBar1.Value = extent;
                extent++;
            }

            if (myProcessBar1.Value >= 100)
            {
                if (isFinished)
                {
                    timer1.Enabled = false;
                    label7.Text = "解析完成";
                    label7.ForeColor = Color.FromArgb(153, 153, 153);
                }
                else
                {
                    myProcessBar1.Value = 0;
                    extent = 0;
                    label7.Text = "上传完成";
                    label7.ForeColor = Color.FromArgb(153, 153, 153);
                    isFinished = true;
                }
            }

        }

        //暂停点击事件
        private void pictureBox1_Click(object sender, EventArgs e)//暂停\开始按钮
        {
            if (extent < 100 && isFinished == false)
            {
                if (isPause)
                {
                    //pictureBox1.BackgroundImage = null;
                    pictureBox1.Image = Properties.Resources.pause;
                    isPause = false;

                    //timer1.Enabled = false;
                }
                else
                {
                    //pictureBox1.BackgroundImage = null;
                    pictureBox1.Image = Properties.Resources.start;
                    isPause = true;

                    //timer1.Enabled = true;
                }
            }

            if (pause != null) 
            {
                pause(sender,e);
            }
        }

        //重置暂停图标
        public void resetPauseButton() 
        {
            if (isPause)
            {
                //pictureBox1.BackgroundImage = null;
                pictureBox1.Image = Properties.Resources.pause;
                isPause = false;

                //timer1.Enabled = false;
            }
            else
            {
                //pictureBox1.BackgroundImage = null;
                pictureBox1.Image = Properties.Resources.start;
                isPause = true;

                //timer1.Enabled = true;
            }
        }

        //删除按钮点击事件
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            System.Reflection.Assembly thisExe;

            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            /*
            Type type = MethodBase.GetCurrentMethod().DeclaringType;

            string _namespace = type.Namespace + "";*/
            System.IO.Stream file = thisExe.GetManifestResourceStream("DataLineA.Resources.loading.gif");

            this.pictureBox2.Image = Image.FromStream(file); 
            if (delete != null)
            {
                delete(sender, e);
            }
        }

        public void resetDeleteButton() 
        {
            this.pictureBox2.Image = Properties.Resources.close;
        }

    }

}
