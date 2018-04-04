using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClashFreeTimetable
{
    class BackEndClashFree
    {
        DBHandler db = new DBHandler();
        int timeHours, rooms;
        String[,] monday;                     //  2d array for backend traversal
        String[,] tuesday;                     //  2d array for backend traversal
        String[,] wednesday;                     //  2d array for backend traversal
        String[,] thursday;                     //  2d array for backend traversal
        String[,] friday;                     //  2d array for backend traversal


        int row = 0;
        int col = 0;

        public static string passMessage = "";



        List<Course> sem_1and2_Courses = new List<Course>();    //  prefer on monday
        List<Course> sem_3and4_Courses = new List<Course>();    //  prefer on tuesday
        List<Course> sem_5and6_Courses = new List<Course>();    //  prefer on monday
        List<Course> sem_7and8_Courses = new List<Course>();    //  prefer on tuesday
        List<Course> sem_9and10_Courses = new List<Course>();    //  prefer on tuesday
        List<Course> sem_11and12_Courses = new List<Course>();    //  prefer on tuesday


        public List<DetailedCourse> allDetailedCourses = new List<DetailedCourse>();


        FlowLayoutPanel FLP1, FLP2, FLP3, FLP4, FLP5;
        Control[] ctrls;




        /*
        public BackEndClashFree (int rm, int th, FlowLayoutPanel p1, FlowLayoutPanel p2, FlowLayoutPanel p3, FlowLayoutPanel p4, FlowLayoutPanel p5)
        public void startOriginalWork ()        //  5 day traversal
        public void proceedFurthure (int num, FlowLayoutPanel flowLayoutPanel, String [,] dayArray)



        public void refresh ()                                      //  to refresh the flow layout panel
        public void print(int num)                                  //  num shows number of day
        */
        public BackEndClashFree (int rm, int th, FlowLayoutPanel p1, FlowLayoutPanel p2, FlowLayoutPanel p3, FlowLayoutPanel p4, FlowLayoutPanel p5, String [,,] globalArray)
        {
            rooms = rm;
            timeHours = th;
            FLP1 = p1;
            FLP2 = p2;
            FLP3 = p3;
            FLP4 = p4;
            FLP5 = p5;

            monday = new String[rooms + 1, timeHours + 1];
            tuesday = new String[rooms + 1, timeHours + 1];
            wednesday = new String[rooms + 1, timeHours + 1];
            thursday = new String[rooms + 1, timeHours + 1];
            friday = new String[rooms+1, timeHours+1];
            for (int a=1; a<rooms+1; a++)
            {
                for (int b=1; b<timeHours+1; b++)
                {
                    monday[a, b] = "null";
                    tuesday[a, b] = "null";
                    wednesday[a, b] = "null";
                    thursday[a, b] = "null";
                    friday[a, b] = "null";
                }
            }

            sem_1and2_Courses = db.getSemWiseCourses(1, 2);
            sem_3and4_Courses = db.getSemWiseCourses(3, 4);
            sem_5and6_Courses = db.getSemWiseCourses(5, 6);
            sem_7and8_Courses = db.getSemWiseCourses(7, 8);
            sem_9and10_Courses = db.getSemWiseCourses(9, 10);
            sem_11and12_Courses = db.getSemWiseCourses(11, 12);

            allDetailedCourses = db.getAllCoursesWithoutLabs();
            //     allDetailedCourses = allDetailedCourses.OrderBy(o => o.course.semester).ToList();


            change();


            startOriginalWork(globalArray, true);
            startOriginalWork(globalArray, false);
            //    startOriginalWork(globalArray, false);


            for (int zx=1; zx<6; zx++)
            {
                print(zx);
            }
            printArray(monday);

            printRemainingCourses();
        }

        public bool change ()
        {

   //         Console.WriteLine(",\n ");
            for (int a=0; a<allDetailedCourses.Count; a++)
            {
      //          Console.Write(allDetailedCourses[a].course.courseCode + ", ");
            }

  //          Console.WriteLine(",\n ");

            Random rnd = new Random();
            for (int a = 0; a < allDetailedCourses.Count; a++)       //  randomly generate array of ncoins
            {
                int num1 = rnd.Next(0, allDetailedCourses.Count);
                int num2 = rnd.Next(0, allDetailedCourses.Count);

                DetailedCourse ptr = allDetailedCourses[num1];
                allDetailedCourses[num1] = allDetailedCourses[num2];
                allDetailedCourses[num2] = ptr;
      //          Console.WriteLine (rnd.Next(0, dummt.Count)+", ");
            }


            for (int a = 0; a < allDetailedCourses.Count; a++)
            {
         //       Console.Write(allDetailedCourses[a].course.courseCode + ", ");
            }
            return true;

        }



        public void startOriginalWork (String [,,] globalArray, bool semesterCheck)
        {
            for (int dayNum=1; dayNum <= 5; dayNum++)       //  traversal of 5 days
            {
               
                if (dayNum==1)
                {
                    mondaySetClasses(dayNum, FLP1, monday, semesterCheck, globalArray);
                }
                else if (dayNum==2)
                {
                    mondaySetClasses(dayNum, FLP2, tuesday, semesterCheck, globalArray);
                }
                else if (dayNum == 3)
                {
                    mondaySetClasses(dayNum, FLP3, wednesday, semesterCheck, globalArray);
                }
                else if (dayNum == 4)
                {
                    mondaySetClasses(dayNum, FLP4, thursday, semesterCheck, globalArray);
                }
                else
                {
                    mondaySetClasses(dayNum, FLP5, friday, semesterCheck, globalArray);
                }
            }
        }

        public void mondaySetClasses (int num, FlowLayoutPanel flowLayoutPanel, String[,] dayArray, bool semesterCheck, String [,,] globalArray)
        {
            for (int x = 0; x < allDetailedCourses.Count; x++)         //  all course list traversal
            {
                bool checkDay1 = getDayBool(num, allDetailedCourses[x]);          //  check that class has set on this day or not

                if (allDetailedCourses[x].remainingC_Hours != 0 && checkDay1 == false && semesterCheck)
                {
                    if (num == 1 || num==3)
                    {
                            if (allDetailedCourses[x].course.semester == 1 || allDetailedCourses[x].course.semester == 2 || allDetailedCourses[x].course.semester == 5 || allDetailedCourses[x].course.semester == 6 || allDetailedCourses[x].course.semester == 9 || allDetailedCourses[x].course.semester == 10 || allDetailedCourses[x].course.semester == 11 || allDetailedCourses[x].course.semester == 12)
                            {
                                alot(num, flowLayoutPanel, dayArray, x, globalArray);
                            }
                        
                    }

                    else if (num == 2 || num==4)
                    {
                            if (allDetailedCourses[x].course.semester == 3 || allDetailedCourses[x].course.semester == 4 || allDetailedCourses[x].course.semester == 7 || allDetailedCourses[x].course.semester == 8 || allDetailedCourses[x].course.semester == 9 || allDetailedCourses[x].course.semester == 10 || allDetailedCourses[x].course.semester == 11 || allDetailedCourses[x].course.semester == 12)
                            {
                                alot(num, flowLayoutPanel, dayArray, x, globalArray);
                            }
                        
                    }
                    else if (num == 5)
                    {
                        if (allDetailedCourses[x].course.semester <= 12 && allDetailedCourses[x].course.semester >= 1)
                            {
                                alot(num, flowLayoutPanel, dayArray, x, globalArray);
                            }
                        
                    }

                }
                if (semesterCheck==false && allDetailedCourses[x].remainingC_Hours != 0)
                {
                    alot(num, flowLayoutPanel, dayArray, x, globalArray);
                }






                //    Console.Write("\n");
            }
        }

        public void alot (int num, FlowLayoutPanel flowLayoutPanel, String[,] dayArray, int x, String [,,] globalArray)
        {
            //    Console.Write("\n"+x+") "+allDetailedCourses[x].course.courseName);
            if (traverseSlots(allDetailedCourses[x], dayArray))
            {
                dayArray[row, col] = allDetailedCourses[x].course.courseCode;
                globalArray [num-1, row, col]= allDetailedCourses[x].course.courseCode;
                //   allDetailedCourses[x].monday = true;
                setDayBool(num, x, true);
                allDetailedCourses[x].remainingC_Hours--;

                updateButton(flowLayoutPanel, row, col, allDetailedCourses[x].course.courseName + " (" + allDetailedCourses[x].course.section + ")", allDetailedCourses[x].course.semester);
                flowLayoutPanel.Refresh();
                //    Console.Write("\t\t(" + row + ", " + col + ")\n");

                int rowCheck = 1;   //  to check, that we have more row (rooms) in current time slot
                int saveIndex = x;
                while (true)
                {
                    if ((row + rowCheck) < (rooms + 1))  //  if next room available in same col
                    {
                   //     Console.Write("yes   ");
                        int indexList = findAnyPreOrPostReqCourseIndex(num, allDetailedCourses[saveIndex].course);

                        if (indexList != -1 && isTeacherClash(col, allDetailedCourses[indexList], dayArray) == false && isStudentClash(col, allDetailedCourses[indexList], dayArray) == false)              //  b=col to check clashes
                        {
                            saveIndex = indexList;
                            dayArray[row + rowCheck, col] = allDetailedCourses[indexList].course.courseCode;
                            globalArray[num - 1, row + rowCheck, col] = allDetailedCourses[indexList].course.courseCode;
                            //         allDetailedCourses[indexList].monday = true;
                            setDayBool(num, indexList, true);
                            allDetailedCourses[indexList].remainingC_Hours--;
                        //    Console.Write("\tSet");

                            updateButton(flowLayoutPanel, row + rowCheck, col, allDetailedCourses[indexList].course.courseName + " (" + allDetailedCourses[indexList].course.section + ")", allDetailedCourses[indexList].course.semester);
                            flowLayoutPanel.Refresh();
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                  //      Console.WriteLine("\tNo more rooms");
                        break;
                    }
                    rowCheck++;
             //       Console.Write("\n");
                }
          //      Console.Write("\n");
            }
        }














        //  return that course's index, which is pre or post-req, and is not assigned on this day yet
        public int findAnyPreOrPostReqCourseIndex(int num, Course cour)
        {
            for (int a = 0; a < allDetailedCourses.Count; a++)
            {
                String detailpre = allDetailedCourses[a].course.courseCode;
                detailpre = detailpre.Substring(0, detailpre.Length - 1);

                String detailpost = cour.courseCode;
                detailpost = detailpost.Substring(0, detailpost.Length - 1);


                if (allDetailedCourses[a].course.pre == detailpost)   //  post req found
                {
               //     Console.WriteLine(cour.courseCode + " (post = " + detailpost + "), ");
                    bool checkDay = getDayBool(num, allDetailedCourses[a]);          //  check that class has set on this day or not
                    if (checkDay == false && allDetailedCourses[a].remainingC_Hours != 0)//  class not already on this day && classes are yet remaining
                    {
                //        Console.WriteLine("****" + cour.courseCode + "--->" + cour.pre);
                        return a;
                    }
                }

                if (cour.pre == detailpre)   //  pre req found
                {
             //       Console.WriteLine(cour.courseCode+" (pre = "+ detailpre + "), ");
                    bool checkDay = getDayBool(num, allDetailedCourses[a]);          //  check that class has set on this day or not
                    if (checkDay==false && allDetailedCourses[a].remainingC_Hours!=0)//  class not already on this day && classes are yet remaining
                    {
             //           Console.WriteLine("****"+cour.courseCode+"--->"+cour.pre);
                        return a;
                    }                 
                }
            }
            return -1;
        }

        public bool traverseSlots (DetailedCourse temp, String[,] dayArray)
        {
            for (int a = 1; a < rooms + 1; a++)                 //  traversing rooms
            {
                for (int b = 1; b < timeHours + 1; b++)         //  traversing timeslots
                {
                    //  pick empty rooms and fits a clash free class
                    if (dayArray[a, b] == "null")               //  if empty (no class set on this slot)
                    {
                        if (isTeacherClash(b,temp, dayArray)==false && isStudentClash (b,temp, dayArray)==false) //  b=col to check clashes
                        {
                            row = a;
                            col = b;
                            return true;
                        }

                    }


                }
            }
            return false;
        }


        public bool isStudentClash(int position, DetailedCourse temp, String[,] dayArray)
        {
            for (int c = 1; c < rooms + 1; c++)
            {
                if (dayArray[c, position] != "null")
                {
                    List<Student> c1 = db.getStudentsClash (temp.course.courseCode, dayArray[c, position]);
                    if (c1.Count>0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public bool isTeacherClash(int position, DetailedCourse temp, String[,] dayArray)
        {
            for (int c=1; c<rooms+1; c++)
            {
                if (dayArray[c,position]!="null")
                {
                    Course c1 = db.getCourse(dayArray[c, position]);
                    if (temp.course.teacher==c1.teacher)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        


        public bool getDayBool (int num, DetailedCourse detailedCourse )
        {
            if (num==1)
            {
                return detailedCourse.monday;
            }
            else if (num == 2)
            {
                return detailedCourse.tuesday;
            }
            else if (num == 3)
            {
                return detailedCourse.wednesday;
            }
            else if (num == 4)
            {
                return detailedCourse.thursday;
            }
            else
            {
                return detailedCourse.friday;
            }
        }

        public bool setDayBool(int num, int index, bool val)
        {
            if (num == 1)
            {
                allDetailedCourses[index].monday = val;
            }
            else if (num == 2)
            {
                allDetailedCourses[index].tuesday = val;
            }
            else if (num == 3)
            {
                allDetailedCourses[index].wednesday = val;
            }
            else if (num == 4)
            {
                allDetailedCourses[index].thursday = val;
            }
            else
            {
                allDetailedCourses[index].friday = val;
            }
            return true;
        }


        public void updateButton (FlowLayoutPanel flowLayoutPanel, int a, int b, String name, int sem)
        {
            String takeOut = "button,"+a+","+b;
            ctrls = flowLayoutPanel.Controls.Find(takeOut, false);  //  get required button by his (unique) name
            Button tempbtn = ctrls[0] as Button;                    //  convert to button
            Array.Clear(ctrls, 0, ctrls.Length);                    //  to clear whole array (Caution)
            tempbtn.Text = name;
            if (sem==1 || sem==2)
            {
                tempbtn.BackColor = System.Drawing.Color.Pink;    //  update button color
            }
            else if (sem==3 || sem==4)
            {
                tempbtn.BackColor = System.Drawing.Color.SandyBrown;    //  update button color
            }
            else if (sem == 5 || sem == 6)
            {
                tempbtn.BackColor = System.Drawing.Color.LightGreen;    //  update button color
            }
            else if (sem == 7 || sem == 8)
            {
                tempbtn.BackColor = System.Drawing.Color.LightBlue;    //  update button color
            }
            else if (sem == 9 || sem == 10 || sem == 11 || sem == 12)
            {
                tempbtn.BackColor = System.Drawing.Color.Aqua;    //  update button color
            }
        }





        public void refresh ()                                      //  to refresh the flow layout panel
        {
     /*       FLP1.Refresh();
            FLP2.Refresh();
            FLP3.Refresh();
            FLP4.Refresh();
            FLP5.Refresh();
   */     }

        public void print(int num)                                  //  num shows number of day
        {
            String[,] arr;
            String name = null;
            if (num == 1)
            {
                arr = monday;
                name = "Monday";
            }
            else if (num==2)
            {
                arr = tuesday;
                name = "Tuesday";
            }
            else if (num == 3)
            {
                arr = wednesday;
                name = "Wednesday";
            }
            else if (num == 4)
            {
                arr = thursday;
                name = "Thursday";
            }
            else
            {
                arr = friday;
                name = "Friday";
            }

            Console.Write("\n\t\t\t\t\t"+name+"\n");
            for (int a = 1; a < rooms + 1; a++)
            {
                for (int b = 1; b < timeHours + 1; b++)
                {
                    Console.Write(arr[a,b]+"  ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");

        }





        public void printArray(String[,] dayArray)
        {
            for (int a = 1; a < rooms + 1; a++)
            {
                for (int b = 1; b < timeHours + 1; b++)
                {
                    Console.Write(dayArray[a, b] + "\t\t");
                }
                Console.Write("\n\n");
            }

        }

        public void printRemainingCourses ()
        {
            for (int a=0; a<allDetailedCourses.Count; a++)
            {
                if (allDetailedCourses[a].remainingC_Hours!=0)
                {
                    DetailedCourse c1 = allDetailedCourses[a];
                    Console.WriteLine(c1.course.courseName+" ("+c1.course.section+") = {"+c1.monday+", " + c1.tuesday + ", " + c1.wednesday + ", " + c1.thursday + ", " + c1.friday + "} ");
                }
            }


        }

        public void printGlobalArray (String [,,] globalArray)
        {
            Console.Write("\n\n\t\t\t\tGlobal Array\n\n");

            for (int a = 0; a < 5; a++)
            {
                Console.Write("\n\t\t\t"+a+1+"\n");
                for (int b = 1; b < rooms + 1; b++)
                {
                    for (int c = 1; c < timeHours + 1; c++)
                    {
                        Console.Write(globalArray[a, b, c]+",   ");
                    }
                    Console.Write("\n");
                }
            }
        }

        public void export1(String[,,] globalArray)
        {

        }

  /*      public void getClashStudents (String dayz, String [,,] globalArrayz, String codez, int rowz, int colz )
        {
            int num = 0;
            if (dayz== "Monday")
            {
                num = 0;
            }
            else if (dayz=="Tuesday")
            {
                num = 1;
            }
            else if (dayz == "Wednesday")
            {
                num = 2;
            }
            else if (dayz == "Thursday")
            {
                num = 3;
            }
            else
            {
                num = 4;
            }

            passMessage = null;

            for (int a = 1; a < rooms + 1; a++)
            {
                String hg = globalArrayz[num, a, colz];

                Course c1 = db.getCourse(hg);
                if (c1 != null)
                {
                    passMessage += "\n          " + c1.courseName + "(" + c1.section + ")\n";
                    List<Student> allClashStudents = db.getStudentsClash(codez, hg);
                    for (int b = 0; b < allClashStudents.Count; b++)
                    {
                        passMessage += (b + 1) + ")  " + allClashStudents[b].rollNo + "   " + allClashStudents[b].name + "\n";
                    }
                }
            }
            ClashMessage frm1 = new ClashMessage();
            frm1.ShowDialog();

        }
*/
    }
}
