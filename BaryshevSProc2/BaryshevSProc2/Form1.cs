using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;


namespace BaryshevSProc2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SystemEvents.DisplaySettingsChanged += new EventHandler(DisplaySettingsChanged);
            SystemEvents.TimeChanged += new EventHandler(SystemEvents_TimeChanged);
        }
        void DisplaySettingsChanged(object obj, EventArgs ea)
        {
            MessageBox.Show("Вы изменили разрешение экрана");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(SystemInformation.UserName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(string fname, string arg)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fname;
            proc.StartInfo.Arguments = arg;
            proc.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Извлекаем строку из ресурса String1
           // MessageBox.Show(WindowsApplication1.Properties.Resources.String1);
            // Извлекаем значок из ресурса Icon1
            // и устанавливаем его в качестве значка формы
           // this.Icon = WindowsApplication1.Properties.Resources.Icon1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BackgroundColor = Color.Red;
            Properties.Settings.Default.Save();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RegistryKey newIETitle = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main", true);
            newIETitle.SetValue("Window Title", "C#. Народные советы");
            newIETitle.Close();
            MessageBox.Show("Закройте IE и запустите его снова");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            myKey.SetValue("MyProgram", Application.ExecutablePath);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            myKey.DeleteValue("MyProgram");
        }
        private string GetProcessorArchitecture()
        {
            RegistryKey environmentKey = Registry.LocalMachine; // раздел HKLM
            environmentKey = environmentKey.OpenSubKey(
            @"System\CurrentControlSet\Control\Session Manager\Environment",
            false);
            string strEnvironment =
            environmentKey.GetValue("PROCESSOR_ARCHITECTURE").ToString();
            return strEnvironment;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetProcessorArchitecture());
        }
        void DisplaySettingsChanging(object obj, EventArgs ea)
        {
            MessageBox.Show("Разрешение экрана изменилось");
        }
        void SystemEvents_TimeChanged(object sender, EventArgs e)
        {
            this.Text = "Вы зачем поменяли системное время?";
        }
    }
}
