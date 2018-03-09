using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace BDCloud
{
    //  分页需初始化 Pagination 和  其对应的分页方法：分页方法的参数默认为翻到的页数

    delegate void changePageMethod(int page);

    class Pagination
    {
        public static changePageMethod method = null;
        //eg.  method = new changePageMethod(Form1.form.showPages_d1);
        public  int CurrentPage = 1;
        private int startPage = 1;
        private int endPage =1;
        private int range = 5;
        public int threshold = 3;
        public int sumNum = 0;
        private Label lbPageInformation=new Label();
        private Button btnPage1 = new Button();
        private Button btnPage2 = new Button();
        private Button btnPage3 = new Button();
        private Button btnPage4 = new Button();
        private Button btnPage5 = new Button();
        public TextBox tbPageJump = new TextBox();
        public Panel panel_page_jump = new Panel();
        private Label lbPageJump = new Label();
        private Button btnLastPage = new Button();
        private Button btnNextPage = new Button();
        public Pagination(Label lbPageInformation, Button btnPage1, Button btnPage2, Button btnPage3, Button btnPage4, Button btnPage5, TextBox tbPageJump,Panel panel_page_jump,
            Label lbPageJump, Button btnLastPage, Button btnNextPage, int endPage,int sumNum)
        {           
            this.endPage = endPage;
            this.lbPageInformation = lbPageInformation;
            this.btnPage1 = btnPage1;
            this.btnPage2 = btnPage2;
            this.btnPage3 = btnPage3;
            this.btnPage4 = btnPage4;
            this.btnPage5 = btnPage5;
            this.tbPageJump = tbPageJump;
            this.lbPageJump = lbPageJump;
            this.btnLastPage = btnLastPage;
            this.btnNextPage = btnNextPage;
            this.panel_page_jump = panel_page_jump;
            this.sumNum = sumNum;
            this.initPager();
            this.initComponent();
       }
        public Pagination()
        {
        }
        //根据不同的endPage去初始化 button
        public void initPager()
        {
            this.btnPage1.Visible = true;
            this.btnPage2.Visible = true;
            this.btnPage3.Visible = true;
            this.btnPage4.Visible = true;
            this.btnPage5.Visible = true;
            this.tbPageJump.Text = "";
            if (endPage == 0)     //对搜索结果为零条进行临界判断，同endPage=1
            {
                endPage = 1;
            }
            this.lbPageInformation.Text = "总共" + sumNum + "条，当前" + CurrentPage + "/" + endPage + "页";
            this.btnNextPage.Location = new Point(894, 473);
            this.panel_page_jump.Location = new Point(929, 473);
            this.lbPageJump.Location = new Point(976, 476);
            if (this.endPage > this.range)
            {            
                this.btnPage1.Text = "1";
                this.btnPage2.Text = "2";
                this.btnPage3.Text = "3";
                this.btnPage4.Text = "4";
                this.btnPage5.Text = "5";
            }
            else
            {
                for (int i = 0; i < this.endPage; i++)
                {
                    if (i == 0)
                    {
                        this.btnPage1.Text = "1";
                    }
                    if (i == 1)
                    {
                        this.btnPage2.Text = "2";
                    }
                    if (i == 2)
                    {
                        this.btnPage3.Text = "3";
                    }
                    if (i == 3)
                    {
                        this.btnPage4.Text = "4";
                    }
                    if (i == 4)
                    {
                        this.btnPage5.Text = "5";
                    }
                }
                switch (this.endPage)
                {       
                    case 1:
                        this.btnPage2.Visible = false;
                        this.btnPage3.Visible = false;
                        this.btnPage4.Visible = false;
                        this.btnPage5.Visible = false;

                        this.btnNextPage.Left = this.btnNextPage.Left - 4 * 25;
                        this.panel_page_jump.Left = this.panel_page_jump.Left - 4 * 25;
                        this.lbPageJump.Left = this.lbPageJump.Left - 4 * 25;

                        break;
                    case 2:
                        this.btnPage3.Visible = false;
                        this.btnPage4.Visible = false;
                        this.btnPage5.Visible = false;

                        this.btnNextPage.Left = this.btnNextPage.Left - 3 * 25;
                        this.panel_page_jump.Left = this.panel_page_jump.Left - 3 * 25;
                        this.lbPageJump.Left = this.lbPageJump.Left - 3 * 25;

                        break;
                    case 3:
                        this.btnPage4.Visible = false;
                        this.btnPage5.Visible = false;

                        this.btnNextPage.Left = this.btnNextPage.Left - 2 * 25;
                        this.panel_page_jump.Left = this.panel_page_jump.Left - 2 * 25;
                        this.lbPageJump.Left = this.lbPageJump.Left - 2 * 25;
                        break;
                    case 4:
                        this.btnPage5.Visible = false;

                        this.btnNextPage.Left = this.btnNextPage.Left - 25;
                        this.panel_page_jump.Left = this.panel_page_jump.Left - 25;
                        this.lbPageJump.Left = this.lbPageJump.Left - 25;
                        break;
                }
            }
            btnStyleChange(this.btnPage1);
        }

        //利用反射机制清除控件的响应事件。
       public void ClearEvent(Control control, string eventname)
        {
            if (control == null) return;
            if (string.IsNullOrEmpty(eventname)) return;
            BindingFlags mPropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;
            BindingFlags mFieldFlags = BindingFlags.Static | BindingFlags.NonPublic;
            Type controlType = typeof(System.Windows.Forms.Control);
            PropertyInfo propertyInfo = controlType.GetProperty("Events", mPropertyFlags);
            EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(control, null);
            FieldInfo fieldInfo = (typeof(Control)).GetField("Event" + eventname, mFieldFlags);
            Delegate d = eventHandlerList[fieldInfo.GetValue(control)];
            if (d == null) return;
            EventInfo eventInfo = controlType.GetEvent(eventname);
            foreach (Delegate dx in d.GetInvocationList())
                eventInfo.RemoveEventHandler(control, dx);
        }

        //初始化分页相关控件的响应事件
        public void initComponent()
        {
            //清除上一次初始化时的点击事件。
            ClearEvent(tbPageJump, "KeyPress");
            ClearEvent(btnPage1, "Click");
            ClearEvent(btnPage2, "Click");
            ClearEvent(btnPage3, "Click");
            ClearEvent(btnPage4, "Click");
            ClearEvent(btnPage5, "Click");
            ClearEvent(btnLastPage, "Click");
            ClearEvent(btnNextPage, "Click");
            ClearEvent(lbPageJump, "Click");

            this.tbPageJump.KeyPress += (sender, e) =>
            {
                if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
  
            this.btnPage1.Click += (sender, e) =>
            {
                this.changeStatus(btnPage1);
                method(CurrentPage);
            };
            btnPage2.Click += (sender, e) =>
            {
                this.changeStatus(btnPage2);
                 method(CurrentPage);
            };
            btnPage3.Click += (sender, e) =>
            {
                this.changeStatus(btnPage3);
                method(CurrentPage);
            };
            btnPage4.Click += (sender, e) =>
            {
                this.changeStatus(btnPage4);
                method(CurrentPage);
            };
            btnPage5.Click += (sender, e) =>
            {
                this.changeStatus(btnPage5);
                method(CurrentPage);
            };
            btnLastPage.Click += (sender, e) =>
            {
                if (CurrentPage > startPage)
                {
                    this.getButtonStatusByColor(btnLastPage);
                }
                method(CurrentPage);
            };

            this.btnNextPage.Click += (sender, e) =>
            {
                if(this.CurrentPage<this.endPage){
                    this.getButtonStatusByColor(this.btnNextPage);
                }                
                method(CurrentPage);
            };

            // 跳转事件响应
            lbPageJump.Click += (sender, e) =>
            {
                int param = 0;      //针对跳转中如果总页数小于五页做逻辑判断、
                int.TryParse(tbPageJump.Text, out param);
                if (param == 0)
                {
                    return;
                }
                int sum = Convert.ToInt32(tbPageJump.Text);             
                if (sum < 1 || sum > endPage)
                {
                    MessageBox.Show("跳转参数有误，请重新输入");
                    tbPageJump.Text = "";
                    return;
                }                
                CurrentPage = Convert.ToInt32(tbPageJump.Text);
                this.jumpToSpecPage(CurrentPage);
            };
        }

        //跳转功能实现
        public void jumpToSpecPage(int pageIndex)
        {
            lbPageInformation.Text = "总共" + sumNum + "条，当前" + pageIndex + "/" + endPage + "页";
            if (endPage > 5)
            {
                if (pageIndex <= threshold)
                {
                    this.getCurrentButton(pageIndex);
                }
                else
                {
                    Button temp = new Button();
                    temp.Text = Convert.ToString(pageIndex);
                    this.changeStatus(temp);
                }
            }
            else
            {
                btnPage1.BackColor = System.Drawing.Color.White;
                btnPage2.BackColor = System.Drawing.Color.White;
                btnPage3.BackColor = System.Drawing.Color.White;
                btnPage4.BackColor = System.Drawing.Color.White;
                btnPage5.BackColor = System.Drawing.Color.White;
                if (pageIndex == 1)
                {
                    btnPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232)))));
                }
                if (pageIndex == 2)
                {
                    btnPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232)))));
                }
                if (pageIndex == 3)
                {
                    btnPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232)))));
                }
                if (pageIndex == 4)
                {
                    btnPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232)))));
                }
                if (pageIndex == 5)
                {
                    btnPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232)))));
                }
            }
            method(pageIndex);
        }


        public void getButtonStatusByColor(Button button )
        {    
            //首先得到颜色显示在哪个button中，即事件焦点显示
            if (btnPage1.BackColor == System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232))))))
            {
                if (button.Name == btnLastPage.Name)
                {
                    //不做处理
                }else if (button.Name == btnNextPage.Name)
                {
                    this.changeStatus(btnPage2);
                }
            }else
                if (btnPage2.BackColor == System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232))))))
            {
                if (button.Name == btnLastPage.Name)
                {
                    this.changeStatus(btnPage1);
                }
                else if (button.Name == btnNextPage.Name)
                {
                    this.changeStatus(btnPage3);
                }
            }else
                    if (btnPage3.BackColor == System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232))))))
            {
                if (button.Name == btnLastPage.Name)
                {
                    this.changeStatus(btnPage2);
                }
                else if (button.Name == btnNextPage.Name)
                {
                    this.changeStatus(btnPage4);
                }
            }else
                  if (btnPage4.BackColor == System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232))))))
            {
                if (button.Name == btnLastPage.Name)
                {
                    this.changeStatus(btnPage3);
                }
                else if (button.Name == btnNextPage.Name)
                {
                    this.changeStatus(btnPage5);
                }
            }else
                      if (btnPage5.BackColor == System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232))))))
            {
                if (button.Name == btnLastPage.Name)
                {
                    this.changeStatus(btnPage4);
                }
                else if (button.Name == btnNextPage.Name)
                {
                    //不做处理
                }
            }

        }
       
        //改变被点击按钮的颜色
        public void changeStatus(Button button)
        {
            //判断分页方法
            if (method == null)
            {
                MessageBox.Show("分页方法未初始化，请初始化！");
                return;
            }
            int param = 0;      //针对跳转中如果总页数小于五页做逻辑判断、
            int.TryParse(button.Text, out param);
            if (param == 0)
            {
                return;
            }
            btnPage1.BackColor = System.Drawing.Color.White;
            btnPage2.BackColor = System.Drawing.Color.White;
            btnPage3.BackColor = System.Drawing.Color.White;
            btnPage4.BackColor = System.Drawing.Color.White;
            btnPage5.BackColor = System.Drawing.Color.White;
            CurrentPage=Convert.ToInt32(button.Text);       //获取当前页；被点击后页
            lbPageInformation.Text = "总共" + sumNum + "条，当前" + CurrentPage + "/" + endPage + "页";       
            if (endPage > 5)
            {
                //1-10
                if (System.Math.Abs(CurrentPage - startPage) >= threshold && System.Math.Abs(CurrentPage - endPage) >= threshold)
                {
                    btnPage1.Text = Convert.ToString(CurrentPage - 2);
                    btnPage2.Text = Convert.ToString(CurrentPage - 1);
                    btnPage3.Text = Convert.ToString(CurrentPage);
                    btnPage4.Text = Convert.ToString(CurrentPage + 1);
                    btnPage5.Text = Convert.ToString(CurrentPage + 2);
                    btnStyleChange(btnPage3);
                }
                else if (System.Math.Abs(CurrentPage - startPage) >= threshold && System.Math.Abs(CurrentPage - endPage) < threshold)
                {
                    int currentButton = range - System.Math.Abs(CurrentPage - endPage);
                    this.getCurrentButton(currentButton);
                }
                else
                {
                    this.getCurrentButton(CurrentPage);
                }
            }
            else   // 如果总页数小于五，就把对应的点击按钮变色
            {
                btnStyleChange(button);               
            }
        }

        //如果button 需居中，刷新5个button的状态
        public Button getCurrentButton(int num)
        {
            btnPage1.BackColor = System.Drawing.Color.White;
            btnPage2.BackColor = System.Drawing.Color.White;
            btnPage3.BackColor = System.Drawing.Color.White;
            btnPage4.BackColor = System.Drawing.Color.White;
            btnPage5.BackColor = System.Drawing.Color.White;
            switch (num){
            case 1:
                    btnPage1.Text = Convert.ToString(CurrentPage);
                    btnPage2.Text = Convert.ToString(CurrentPage + 1);
                    btnPage3.Text = Convert.ToString(CurrentPage+2);
                    btnPage4.Text = Convert.ToString(CurrentPage + 3);
                    btnPage5.Text = Convert.ToString(CurrentPage + 4);                   
                    btnStyleChange(btnPage1); 
                    return btnPage1;                   
            case 2:
                   btnPage1.Text = Convert.ToString(CurrentPage-1);
                    btnPage2.Text = Convert.ToString(CurrentPage );
                    btnPage3.Text = Convert.ToString(CurrentPage+1);
                    btnPage4.Text = Convert.ToString(CurrentPage + 2);
                    btnPage5.Text = Convert.ToString(CurrentPage + 3);                   
                           btnStyleChange(btnPage2);
                    return btnPage2;                   
            case 3:
                    btnPage1.Text = Convert.ToString(CurrentPage-2);
                    btnPage2.Text = Convert.ToString(CurrentPage - 1);
                    btnPage3.Text = Convert.ToString(CurrentPage);
                    btnPage4.Text = Convert.ToString(CurrentPage +1);
                    btnPage5.Text = Convert.ToString(CurrentPage +2);                  
                           btnStyleChange(btnPage3);
                    return btnPage3;                   
            case 4:
                    btnPage1.Text = Convert.ToString(CurrentPage - 3);
                    btnPage2.Text = Convert.ToString(CurrentPage - 2);
                    btnPage3.Text = Convert.ToString(CurrentPage-1);
                    btnPage4.Text = Convert.ToString(CurrentPage );
                    btnPage5.Text = Convert.ToString(CurrentPage + 1);                   
                    btnStyleChange(btnPage4);
                    return btnPage4;                   
            case 5:
                    btnPage1.Text = Convert.ToString(CurrentPage -4);
                    btnPage2.Text = Convert.ToString(CurrentPage -3);
                    btnPage3.Text = Convert.ToString(CurrentPage -2);
                    btnPage4.Text = Convert.ToString(CurrentPage-1);
                    btnPage5.Text = Convert.ToString(CurrentPage);
                    btnStyleChange(btnPage5);
                    return btnPage5;                    
            }
            return null;
        }

        // 按钮选中样式修改
        public void btnStyleChange(System.Windows.Forms.Button btn)
        {
            btnPage1.BackColor = System.Drawing.Color.White;
            btnPage2.BackColor = System.Drawing.Color.White;
            btnPage3.BackColor = System.Drawing.Color.White;
            btnPage4.BackColor = System.Drawing.Color.White;
            btnPage5.BackColor = System.Drawing.Color.White;
            btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(232)))));
        }
     
    }
}
