using System.ComponentModel;

namespace DeskBand
{
    public class ValueChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_revSpeed;
        private string m_sendSpeed;

        private string m_CpuInfo;

        private string m_RamInfo;

        public string CpuInfo
        {
            get { return m_CpuInfo; }
            set
            {
                m_CpuInfo = value;
                OnPropertyChanged("CpuInfo");
            }
        }

        public string RamInfo
        {
            get { return m_RamInfo; }
            set
            {
                m_RamInfo = value;
                OnPropertyChanged("RamInfo");
            }
        }

        public string RevSpeed
        {
            get { return m_revSpeed; }
            set
            {
                m_revSpeed = value;
                OnPropertyChanged("RevSpeed");
            }
        }

        public string SendSpeed
        {
            get { return m_sendSpeed; }
            set
            {
                m_sendSpeed = value;
                OnPropertyChanged("SendSpeed");
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}