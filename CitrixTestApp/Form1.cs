using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CitrixTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyName = @"Software\Microsoft\Windows\CurrentVersion\WinTrust\Trust Providers\Software Publishing";
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(keyName, true);
            rk.SetValue("State", 146944);
            rk.Close();
        }
    }
}
