using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BDCloud
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            LoginForm loginForm=new LoginForm();
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(loginForm);
            /*Form1 form = new Form1();
            form.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(form);*/
        }
    }
}
