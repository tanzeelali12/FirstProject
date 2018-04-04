using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClashFreeTimetable
{
    public partial class ClashMessage : Form
    {
        public ClashMessage()
        {
            InitializeComponent();
            label2.Text = BackEndClashFree.passMessage;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Visible = false;

        }

        private void ClashMessage_Load(object sender, EventArgs e)
        {

        }
    }
}
