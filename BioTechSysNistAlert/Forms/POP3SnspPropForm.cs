using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BioTechSysNistAlert.Properties;

namespace BioTechSysNistAlert.Forms
{
    public partial class POP3SnspPropForm : Form
    {
        public POP3SnspPropForm()
        {
            InitializeComponent();
        }

        private void SnspPropForm_Load(object sender, EventArgs e)
        {
            Settings settings = Settings.Default;
            snsp_userName.Text = settings.snsp_userName;
            snsp_Password.Text  = settings.snsp_password;
            snsp_popMail.Text  = settings.snsp_popMail;
            snsp_popPort.Text = settings.snsp_Pop3Port;

        }

        private void OK_Click(object sender, EventArgs e)
        {
            Settings settings = Settings.Default;
            settings.snsp_userName = snsp_userName.Text;
            settings.snsp_password = snsp_Password.Text;
            settings.snsp_popMail =  snsp_popMail.Text ;
            settings.snsp_Pop3Port = snsp_popPort.Text;
            settings.Save();

        }
    }
}
