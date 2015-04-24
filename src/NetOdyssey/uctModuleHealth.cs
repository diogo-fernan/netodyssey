using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetOdyssey
{
    public partial class uctModuleHealth : UserControl
    {
        NetOdysseyHealthReporter.IHealthReporter _healthReporter;

        public uctModuleHealth(string inModuleName, NetOdysseyHealthReporter.IHealthReporter inHealthReporter, bool inModuleAllowCancel)
        {
            InitializeComponent();
            _healthReporter = inHealthReporter;
            prpModuleName = inModuleName;
            prpModuleAllowCancel = inModuleAllowCancel;
        }

        public string prpModuleName {
            get { return grpModuleName.Text; }
            set { grpModuleName.Text = value; }
        }

        public bool prpModuleAllowCancel {
            get { return pcbKillModule.Visible; }
            set { pcbKillModule.Visible = value; }
        }

        public string prpLblTasks {
            set { lblTasks.Text = value; }
        }

        public void reportHealth() {
            prpLblTasks = _healthReporter.HealthReport();
        }
    }
}
