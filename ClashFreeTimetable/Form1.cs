using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClashFreeTimetable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
  
        }
      
/*        private void generate_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Generate frm1 = new Generate();
            frm1.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Generate frm1 = new Generate();
            frm1.ShowDialog();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
       //     ToolTip t1 = new ToolTip();
       //     t1.Show("Click to generate clashfree timetable", (Control)sender);

            Cursor.Current = Cursors.Hand;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Width = 200;
            pictureBox1.Height = 140;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Width = 187;
            pictureBox1.Height = 124;
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            openFileDialog1.ShowDialog();
            String fileName = openFileDialog1.FileName;

            string directoryPath = Path.GetDirectoryName(openFileDialog1.FileName);

       //     Console.WriteLine(fileName);
       //     Console.WriteLine(directoryPath);


            label3.Text = "Finding Classes...";
            ExcelReader er = new ExcelReader();
            bool result = er.start(fileName, directoryPath);
            label3.Text = "Done Finding";
            if (result)
            {
                MessageBox.Show("Data extracted Successfully from Excel File");
            //    Application.Exit();
            }
            

        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Width = 200;
            pictureBox2.Height = 140;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Width = 181;
            pictureBox2.Height = 125;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
     //       ToolTip t1 = new ToolTip();
      //      t1.Show("Click to extract info from excel file", (Control)sender);
            Cursor.Current = Cursors.Hand;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        */


    }
}
