using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CluePanel
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        public delegate void ClikcButton(object sender, EventArgs e);
        public delegate void ClikcPicBox(object sender, EventArgs e);
        public delegate void ClikcPanel(object sender, EventArgs e);
        public delegate void MouseEnterPanel(object sender, EventArgs e);
        public delegate void MouseLeavePanel(object sender, EventArgs e);
        public delegate void MouseHoverButton(object sender, EventArgs e);
        public event ClikcButton ButtonClick;
        public event ClikcPicBox picClick;
        public event ClikcPanel panelClick;
        public event MouseEnterPanel mouseEnterPanel;
        public event MouseLeavePanel mouseLeavePanel;
        public event MouseHoverButton mouseHoverButton;
        /*添加点击上传按钮的点击事件*/
        public void getButtonClick()
        {
            button1.Click += new EventHandler(button1_Click); //绑定委托事件
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
                ButtonClick(sender, e);
        }
        /*添加线索内容的点击事件*/
        public void getPicClick()
        {
            pictureBox1.Click += new EventHandler(pictureBox1_Click); //绑定委托事件
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (picClick != null)
                picClick(sender, e);
        }
        /*添加自定义控件跳转的点击事件*/
        public void getPanelClick()
        {
            panel1.Click += new EventHandler(pictureBox1_Click); //绑定委托事件
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            if (panelClick != null)
                panelClick(sender, e);
        }
        /*添加鼠标进入控件显示上传按钮事件*/
        public void getMouseEnterPanel()
        {
            panel1.MouseEnter += new EventHandler(panel1_MouseEnter); //绑定委托事件
        }
        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            if (mouseEnterPanel != null)
                mouseEnterPanel(sender, e);
        }
        /*添加鼠标离开控件隐藏上传按钮事件*/
        public void getMouseleavePanel()
        {
            panel1.MouseLeave += new EventHandler(panel1_MouseLeave); //绑定委托事件
        }
        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            if (mouseLeavePanel != null)
                mouseLeavePanel(sender, e);
        }

        /*添加鼠标移动到线索详情按钮上的事件*/
        public void getMouseHoverButton()
        {
            pictureBox1.MouseHover += new EventHandler(pictureBox1_MouseHover);
        }
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (mouseHoverButton != null)
                mouseHoverButton(sender, e);
        }
        
    }
}
