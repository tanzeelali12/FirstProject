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
    public partial class Generate : Form
    {

        BackEndClashFree bECF;
        DBHandler db = new DBHandler();
        int noOfRooms;
        int noOfTimeSlots;

        String[,,] dayArray;

        public Generate()
        {
            InitializeComponent();
            doInitialWork();

            object ob = new object();
            EventArgs ea = new EventArgs();
            open_Click(ob, ea);

        }

        public bool doInitialWork ()
        {
            this.AutoScroll = true;
            return true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public bool checkInitialValues ()
        {
            //      filled by ourselves for testing etc.
            startHour.Value = 8;
            startMin.Value = 30;
            endHour.Value = 4;
            endMin.Value = 30;
            rooms.Value = 14;

            return true;
        }
        
        public int calculateTimeHours()     //calculate number of classes per day, w.r.t entered values
        {
                 int b = 0;
                 for (int a=Convert.ToInt32(startHour.Value); a< Convert.ToInt32(endHour.Value)+12; a++)
                 {
                     b++;
                 }

                 Console.WriteLine("Day's Total Hours = "+b);
                 return b;
            
        }

        private void toolTip(object sender, EventArgs e)
        {
   /*         var btn = sender as Button;
            ToolTip t1 = new ToolTip();
            t1.Show(btn.Text, (Control)sender);
   */     }

        private void clickMonday(object sender, EventArgs e)
        {
   /*         if (code.Text == null || db.getCourse(code.Text)==null)
            {
                MessageBox.Show("Enter a Valid Course Code");
                return;
            }
            var btn = sender as Button;

            String[] ptr = btn.Name.Split(',');
            Console.WriteLine(ptr[1]+"Mon " + ptr[2]);

            bECF.getClashStudents("Monday", dayArray,code.Text, Convert.ToInt32(ptr[1]), Convert.ToInt32(ptr[2]));
   */     }

        private void clickTuesday(object sender, EventArgs e)
        {
   /*         if (code.Text == null || db.getCourse(code.Text) == null)
            {
                MessageBox.Show("Enter a Valid Course Code");
                return;
            }
            var btn = sender as Button;

            String[] ptr = btn.Name.Split(',');
            Console.WriteLine(ptr[1] + "Tues " + ptr[2]);

            bECF.getClashStudents("Tuesday", dayArray, code.Text, Convert.ToInt32(ptr[1]), Convert.ToInt32(ptr[2]));
   */     }

        private void clickWednesday(object sender, EventArgs e)
        {
    /*        if (code.Text == null || db.getCourse(code.Text) == null)
            {
                MessageBox.Show("Enter a Valid Course Code");
                return;
            }
            var btn = sender as Button;

            String[] ptr = btn.Name.Split(',');
            Console.WriteLine(ptr[1] + "Wed " + ptr[2]);

            bECF.getClashStudents("Wednesday", dayArray, code.Text, Convert.ToInt32(ptr[1]), Convert.ToInt32(ptr[2]));
   */     }

        private void clickThursday(object sender, EventArgs e)
        {
   /*         if (code.Text == null || db.getCourse(code.Text) == null)
            {
                MessageBox.Show("Enter a Valid Course Code");
                return;
            }
            var btn = sender as Button;

            String[] ptr = btn.Name.Split(',');
            Console.WriteLine(ptr[1] + "Thurs " + ptr[2]);

            bECF.getClashStudents("Thursday", dayArray, code.Text, Convert.ToInt32(ptr[1]), Convert.ToInt32(ptr[2]));
 */       }

        private void clickFriday(object sender, EventArgs e)
        {
/*            if (code.Text == null || db.getCourse(code.Text) == null)
            {
                MessageBox.Show("Enter a Valid Course Code");
                return;
            }
            var btn = sender as Button;

            String[] ptr = btn.Name.Split(',');
            Console.WriteLine(ptr[1] + "Fri " + ptr[2]);

            bECF.getClashStudents("Friday", dayArray, code.Text, Convert.ToInt32(ptr[1]), Convert.ToInt32(ptr[2]));
 */       }
        public bool setButtonAttributes (String day, Button button)          //  to reduce lines from generate_Click function
        {
            button.Width = 140;
            button.Height = 20;

            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));      //  text font size
            button.BackColor = System.Drawing.Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            button.FlatAppearance.BorderSize = 1;
            button.Margin = new Padding(0, 0, 0, 0);
            button.MouseHover += new System.EventHandler(toolTip);
            if (day == "Monday")
            {
                button.Click += new System.EventHandler(clickMonday);
            }
            else if (day == "Tuesday")
            {
                button.Click += new System.EventHandler(clickTuesday);
            }
            else if (day == "Wednesday")
            {
                button.Click += new System.EventHandler(clickWednesday);
            }
            else if (day == "Thursday")
            {
                button.Click += new System.EventHandler(clickThursday);
            }
            else if (day == "Friday")
            {
                button.Click += new System.EventHandler(clickFriday);
            }


            return true;
        }

        public bool buildInitialGUI (String day, int timehours)
        {

            for (int a = 0; a < rooms.Value + 1; a++)
            {
                for (int b = 0; b < timehours + 1; b++)
                {
                    Button button = new Button();
                    button.Name = "button," + a+"," + b;
                    button.Text = "button," + a+"," + b;

                    setButtonAttributes(day, button);

                    if (b == timehours)   //  if last button, next row
                    {
                        if (day=="Monday")
                        {
                            flowLayoutPanel1.SetFlowBreak(button, true);
                        }
                        else if (day=="Tuesday")
                        {
                            flowLayoutPanel2.SetFlowBreak(button, true);
                        }
                        else if (day == "Wednesday")
                        {
                            flowLayoutPanel3.SetFlowBreak(button, true);
                        }
                        else if (day == "Thursday")
                        {
                            flowLayoutPanel4.SetFlowBreak(button, true);
                        }
                        else if (day == "Friday")
                        {
                            flowLayoutPanel5.SetFlowBreak(button, true);
                        }
                    }

                    if (a == 0 && b == 0)           //  set day
                    {
                        button.Text = day;
                        button.Font = new Font(button.Font.Name, button.Font.Size, FontStyle.Bold);
                        button.BackColor = System.Drawing.Color.Gray;

                        // button.Attributes.Add("style", "font-size:20px; color:bisque; font-weight:bold;");
                    }
                    else if (a == 0)              //  set time slots
                    {
                        int dumm1 = (Convert.ToInt32(startHour.Value) + b - 1) % 12;      //dont show in (13:00, so make it 1:00) (basically after 12:00 pm time)
                        int dumm2 = (Convert.ToInt32(startHour.Value) + b) % 12;
                        if (dumm1 == 0)           //   0 = 12, because of modulus
                        {
                            dumm1 = 12;
                        }
                        if (dumm2 == 0)
                        {
                            dumm2 = 12;
                        }
                        button.Text = (dumm1) + ":" + (startMin.Value) + "-" + (dumm2) + ":" + (startMin.Value);
                        button.Font = new Font(button.Font.Name, button.Font.Size, FontStyle.Bold);
                        button.BackColor = System.Drawing.Color.Gray;
                    }
                    else if (b == 0)           //  set room nos
                    {
                        button.Text = "Room " + a;
                        button.Font = new Font(button.Font.Name, button.Font.Size, FontStyle.Bold);
                    }

                    if (day == "Monday")
                    {
                        flowLayoutPanel1.Controls.Add(button);
                    }
                    else if (day == "Tuesday")
                    {
                        flowLayoutPanel2.Controls.Add(button);
                    }
                    else if (day == "Wednesday")
                    {
                        flowLayoutPanel3.Controls.Add(button);
                    }
                    else if (day == "Thursday")
                    {
                        flowLayoutPanel4.Controls.Add(button);
                    }
                    else if (day == "Friday")
                    {
                        flowLayoutPanel5.Controls.Add(button);
                    }
                }

            }
            return true;


        }
        private void generate_Click(object sender, EventArgs e)
        {
            
        }
        

        private void Generate_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void open_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear(); //to remove all controls (for reset button)
            flowLayoutPanel2.Controls.Clear(); //to remove all controls (for reset button)
            flowLayoutPanel3.Controls.Clear(); //to remove all controls (for reset button)
            flowLayoutPanel4.Controls.Clear(); //to remove all controls (for reset button)
            flowLayoutPanel5.Controls.Clear(); //to remove all controls (for reset button)




            if (!checkInitialValues())             //  checks all values filled correctly by the user
            {
                return;
            }

            int timehours = calculateTimeHours();       //  no. of classes (hours)

            label4.Left = ((Convert.ToInt32(timehours)+1) * 160 );
            label7.Top = (Convert.ToInt32(rooms.Value) * 24 * 7);

            flowLayoutPanel1.Top = (Convert.ToInt32(rooms.Value) * 23);
            flowLayoutPanel2.Top = (Convert.ToInt32(rooms.Value) * 23*2);
            flowLayoutPanel3.Top = (Convert.ToInt32(rooms.Value) * 23*3);
            flowLayoutPanel4.Top = (Convert.ToInt32(rooms.Value) * 23*4);
            flowLayoutPanel5.Top = (Convert.ToInt32(rooms.Value) * 23*5);
            buildInitialGUI("Monday", timehours);
            buildInitialGUI("Tuesday", timehours);
            buildInitialGUI("Wednesday", timehours);
            buildInitialGUI("Thursday", timehours);
            buildInitialGUI("Friday", timehours);


            dayArray = new String[5, Convert.ToInt32(rooms.Value) + 1, timehours + 1];
            for (int a=0; a<5; a++)
            {
                for (int b = 1; b < Convert.ToInt32(rooms.Value) + 1; b++)
                {
                    for (int c = 1; c < timehours+1; c++)
                    {
                        dayArray[a, b, c] = "null";
                    }
                }
            }


            bECF = new BackEndClashFree(Convert.ToInt32(rooms.Value), timehours,flowLayoutPanel1, flowLayoutPanel2, flowLayoutPanel3, flowLayoutPanel4, flowLayoutPanel5, dayArray);

            bECF.printGlobalArray(dayArray);
        }

        private void button2_Click(object sender, EventArgs e)
        {
   /*         this.Visible = false;
            Form1 frm1 = new Form1();
            frm1.ShowDialog();
     */   }

        private void update_Click(object sender, EventArgs e)
        {
       /*     db.addStudentsIntoDB(db.downloadStudentsFromFirebase());
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());
            db.addStudentTakeCoursesIntoDB(db.downloadStudentTakeCoursesFromFirebase());
            */

        }

        private void Close_Click(object sender, EventArgs e)
        {
    //        Application.Exit();
        }

        private void export_Click(object sender, EventArgs e)
        {
    //        bECF.export1(dayArray);            
        }

        private void code_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
