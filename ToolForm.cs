using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeskBand
{
    public partial class ToolForm : Form
    {
        public ToolForm()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            //Console.WriteLine(m.Msg);
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                //捕捉关闭窗体消息 http://www.cnblogs.com/sosoft/
                //用户点击关闭窗体控制按钮   注释为最小化窗体
                //this.WindowState = FormWindowState.Minimized;

                //窗体隐藏
                this.Hide();
                return;
            }
            base.WndProc(ref m);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            this.Location = MousePosition;
            this.Location = new Point(MousePosition.X - Width, MousePosition.Y - Height);
        }
    }
}