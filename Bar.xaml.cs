using CSDeskBand;
using System.Runtime.InteropServices;
using System.Threading;
using System.Management;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows;
using System.Linq;

namespace DeskBand
{
    [ComVisible(true)]
    [Guid("E546998B-BFCE-E8B0-626E-A96E27BAE646")]
    [CSDeskBandRegistration(Name = "任务栏助手")]
    public partial class NetSpeed
    {
        private int m_width = 90;
        private int m_height = 20;

        private ValueChanged m_ValueChanged = new ValueChanged();
        private NetSpeedCalc m_netSpeedCalc = new NetSpeedCalc();

        private Thread m_thread;
        private int m_interval = 1000;
        private static PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        private ToolForm m_toolForm = new ToolForm();

        public NetSpeed()
        {
            InitializeComponent();
            var t = GetScale();
            float xScale = (float)t.Width / 96;
            float yScale = (float)t.Height / 96;
            Options.MinHorizontalSize = new System.Windows.Size((int)(this.m_width * xScale), (int)(this.m_height * yScale));
            Options.MinVerticalSize = new System.Windows.Size((int)(this.m_width * xScale), (int)(this.m_height * yScale));

            this.Width = (int)(this.m_width * xScale);
            this.Height = (int)(this.m_height * yScale);
            this.FontSize = (int)(tb_revSpeed.FontSize * xScale);

            this.tb_revSpeed.DataContext = this.m_ValueChanged;
            this.tb_sendSpeed.DataContext = this.m_ValueChanged;
            this.tb_cpuinfo.DataContext = this.m_ValueChanged;
            this.tb_raminfo.DataContext = this.m_ValueChanged;

            this.m_thread = new Thread(new ThreadStart(async () =>
            {
                while (true)
                {
                    this.SetSpeed();
                    //   Thread.Sleep(this.m_interval);
                    await Task.Delay(this.m_interval);
                }
            }));
            this.m_thread.Start();
        }

        private System.Windows.Size GetScale()
        {
            System.Windows.Size t = new System.Windows.Size();
            using (ManagementClass mc = new ManagementClass("Win32_DesktopMonitor"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {
                    foreach (ManagementObject each in moc.Cast<ManagementObject>())
                    {
                        t.Width = int.Parse((each.Properties["PixelsPerXLogicalInch"].Value.ToString()));
                        t.Height = int.Parse((each.Properties["PixelsPerYLogicalInch"].Value.ToString()));
                        //        t = new System.Windows.Size(x, y);
                    }
                }
            }
            return t;
        }

        private void SetSpeed()
        {
            this.m_netSpeedCalc.CalcSpeed();
            this.m_ValueChanged.SendSpeed = this.SpeedFormat(this.m_netSpeedCalc.SendSpeed);
            this.m_ValueChanged.RevSpeed = this.SpeedFormat(this.m_netSpeedCalc.RevSpeed);
            this.m_ValueChanged.CpuInfo = $"CPU:{cpuCounter.NextValue(),3:0}%";
            this.m_ValueChanged.RamInfo = $"RAM:{SysInfo.MemPersent,3:0}%";
        }

        private string SpeedFormat(long speed)
        {
            string formatSpeed;

            if (speed < 1024)
            {
                formatSpeed = speed.ToString() + "B/s";
            }
            else if (speed < 1024 * 1024)
            {
                formatSpeed = string.Format("{0:N1}KB/s", (double)speed / 1024);
            }
            else
            {
                formatSpeed = string.Format("{0:N2}MB/s", ((double)speed / 1024) / 1024);
            }

            return formatSpeed;
        }

        private void ShowToolWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            m_toolForm.Show();
            m_toolForm.Activate();
            //    var pos = Mouse.GetPosition(e.Source as FrameworkElement);
            //   m_toolForm.Location = new System.Drawing.Point((int)pos.X, (int)pos.Y);
        }
    }
}