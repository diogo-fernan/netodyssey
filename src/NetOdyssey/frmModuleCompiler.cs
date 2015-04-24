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
    public partial class frmModuleCompiler : Form
    {
        public frmModuleCompiler()
        {
            InitializeComponent();
        }

        private void trvCompileResults_AfterSelect(object sender, TreeViewEventArgs e)
        {
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }
    }
}
