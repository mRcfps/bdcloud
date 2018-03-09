using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MyProcessBar
{
    public class MyProcessBar : Control
    {
        public MyProcessBar()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        //定义变量
        Rectangle foreRect;

        Rectangle backRect;
        //Rectangle setRect;

        Color backgroundColor = Color.FromArgb(238, 239, 240);
        Color foregroundColor = Color.Gray;
        //Color setRectColor = Color.FromArgb(238, 239, 240);
        Color fontColor = Color.Black;

        int maximum = 100;
        int minimum = 0;
        double myValue = 0;

        bool showPercent;
        float fontSize = 6;
        FontFamily myFontFamily = new FontFamily("微软雅黑");

        //设置属性值
        //[Category("General"), Description("Show Percent Tag"), Browsable(true)]
        public bool ShowPercentTag
        {
            get { return showPercent; }
            set
            {
                showPercent = value;
                Invalidate();
            }
        }
        //[Category("General"), Description("Control's Maximum"), Browsable(true)]
        public int Maximum
        {
            get { return maximum; }
            set
            {
                maximum = value;
                Invalidate();
            }
        }
        //[Category("General"), Description("Control's Minimum"), Browsable(true)]
        public int Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                Invalidate();
            }
        }

        //[Category("General"), Description("Control's FontSize"), Browsable(true)]
        public float FontSize
        {
            get { return fontSize; }
            set
            {
                this.fontSize = value;
                Invalidate();
            }
        }
        //[Category("General"), Description("Control's FontFamily"), Browsable(true)]
        public FontFamily MyFontFamily
        {
            get { return myFontFamily; }
            set
            {
                this.myFontFamily = value;
                Invalidate();
            }
        }

        //[Category("Color"), Browsable(true)]
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                this.backgroundColor = value;
                Invalidate();
            }
        }
        //[Category("Color"), Browsable(true)]
        public Color ForegroundColor
        {
            get { return foregroundColor; }
            set
            {
                this.foregroundColor = value;
                Invalidate();
            }
        }
        //[Category("Color"), Browsable(true)]
        /*
        public Color SetRectColor
        {
            get { return setRectColor; }
            set
            {
                this.setRectColor = value;
                Invalidate();
            }
        }*/
        //[Category("Color"), Browsable(true)]
        public Color FontColor
        {
            get { return fontColor; }
            set
            {
                this.fontColor = value;
                Invalidate();
            }
        }

        //确定位置
        //[Category("General"), Description("Control's Width"), Browsable(true)]
        public new int Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                foreRect.X = backRect.X = base.Width / 20;
                backRect.Width = base.Width * 9 / 10;
                foreRect.Width = (int)(myValue / maximum * backRect.Width);
                //setRect.X = (int)(myValue / maximum * (backRect.Width - backRect.Height) + foreRect.X);

                Invalidate();
            }
        }
        //[Category("General"), Description("Control's Height"), Browsable(true)]
        public new int Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                foreRect.Height = backRect.Height = base.Height / 3;
                foreRect.Y = backRect.Y = base.Height / 3;
                Invalidate();
            }
        }

        //进度条value事件
        protected EventHandler OnValueChanged;
        public event EventHandler ValueChanged
        {
            add
            {
                if (OnValueChanged != null)
                {
                    foreach (Delegate d in OnValueChanged.GetInvocationList())
                    {
                        if (object.ReferenceEquals(d, value)) { return; }
                    }
                }
                OnValueChanged = (EventHandler)Delegate.Combine(OnValueChanged, value);
            }
            remove
            {
                OnValueChanged = (EventHandler)Delegate.Remove(OnValueChanged, value);
            }
        }

        //value属性
        //[Category("General"), Description("Control's Value"), Browsable(true)]
        public double Value
        {
            get { return myValue; }
            set
            {
                if (myValue < Minimum)
                    throw new ArgumentException("小于最小值");
                if (myValue > Maximum)
                    throw new ArgumentException("超过最大值");

                myValue = value;
                foreRect.Width = (int)(myValue / maximum * backRect.Width);
                //setRect.X = (int)(myValue / maximum * (backRect.Width - backRect.Height) + backRect.X);

                if ((myValue - maximum) > 0)
                {
                    foreRect.Width = backRect.Width;
                    //setRect.X = backRect.Width - backRect.Height + backRect.X;
                }

                //如果添加了响应函数,则执行该函数
                if (OnValueChanged != null)
                {
                    OnValueChanged(this, EventArgs.Empty);
                }

                Invalidate();
            }
        }

        //绘制控件
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawRect(e.Graphics);
            DrawText(e.Graphics);
        }
        private void DrawRect(Graphics e)
        {
            Pen pen = new Pen(this.foregroundColor);

            e.FillRectangle(new SolidBrush(this.backgroundColor), backRect);
            e.DrawRectangle(new Pen(Color.Black), backRect);

            e.FillRectangle(new SolidBrush(this.foregroundColor), foreRect);
            e.DrawRectangle(new Pen(Color.Black), foreRect);

            //e.FillRectangle(new SolidBrush(this.setRectColor), setRect);
            //e.DrawRectangle(new Pen(Color.Black), setRect);
        }
        private void DrawText(Graphics e)
        {
            Point point = new Point();
            point.X = this.backRect.X + this.backRect.Width * 3 / 7;
            point.Y = this.backRect.Y + this.backRect.Height / 3;

            SolidBrush brush = new SolidBrush(fontColor);
            Font font = new Font(myFontFamily, this.fontSize);
            string percent = ((int)this.myValue).ToString() + "%";

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            e.DrawString(percent, font, brush, backRect, format);
        }

        //改变大小事件
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = Width;
            this.Height = Height;
            Invalidate();
        }
    }
}
