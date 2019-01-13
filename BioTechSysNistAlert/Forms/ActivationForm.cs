using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BioTechSysNistAlert.Forms
{
    public partial class ActivationForm : Form
    {
        public ActivationForm( string key )
        {
            InitializeComponent();
            txtSerial.Text = key;
        }
    }
}
