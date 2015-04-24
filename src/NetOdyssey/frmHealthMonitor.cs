using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetOdyssey
{
    public partial class frmHealthMonitor : Form
    {
        
        public frmHealthMonitor()
        {
            InitializeComponent();
        }        

        

        private void frmHealthMonitor_Shown(object sender, EventArgs e)
        {
            bgwHealthReport.RunWorkerAsync();
        }        

        private void bgwHealthReport_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwHealthReport.ReportProgress(0);
            while (!bgwHealthReport.CancellationPending)
            {
                bgwHealthReport.ReportProgress(0);
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void bgwHealthReport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }
    }
}
