using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Reflection;

namespace ClashFreeTimetable
{
    class ExcelReader
    {
        List<CellCourse> dailyClasses = new List<CellCourse>();
        List<Course> allCourses = new List<Course>();
        List<String> courseNames = new List<string>();

        String[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };


        private int venueCol;
        private int timeRow;
        private String date;

        //Create COM Objects. Create a COM object for everything that is referenced
        Excel.Application xlApp = new Excel.Application();
        Excel.Workbook xlWorkbook;
        int numSheets;
        Excel._Worksheet xlWorksheet;
        Excel.Range xlRange;
        int rowCount;
        int colCount;



        public bool start (String fileName, String path0)
        {

            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(directory, fileName);
            //Create COM Objects. Create a COM object for everything that is referenced
            xlWorkbook = xlApp.Workbooks.Open(path);
            numSheets = xlWorkbook.Sheets.Count;
            xlWorksheet = xlWorkbook.Sheets[1];
            xlRange = xlWorksheet.UsedRange;
            rowCount = xlRange.Rows.Count;
            colCount = xlRange.Columns.Count;




            

            //   Console.WriteLine("row = "+name0);
                  downloadFirebaseCourses();
                  loadCourses();
                  return generate(fileName, path0);
              
        }






        public List<Course> downloadFirebaseCourses()
        {
            using (StreamReader read = new StreamReader("offerd18.txt"))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    Course temp = new Course();
                    temp.section = null;
                    String[] ptr = line.Split(',');
                    temp.courseCode = ptr[1];
                    temp.courseName = ptr[2];
                    temp.creditHours = Convert.ToInt32(ptr[3]);
                    temp.teacher = ptr[4];

                    String arr = ptr[2].ToUpper();

                    if (arr.ToUpper().Contains("ENGLISH LANGUAGE LAB") || arr.ToUpper().Contains("DIGITAL LOGIC DESIGN - LAB"))
                    {
                        temp.section += ptr[5][ptr[5].Length - 2];
                        temp.section += ptr[5][ptr[5].Length - 1];
                    }
                    else
                    {
                        temp.section += ptr[5][ptr[5].Length - 1];
                    }


                    temp.semester = 0;
                    if (ptr[5].Length>=4)
                    {
                        
                        String dumm = null;
                        dumm += ptr[5][3];
                        temp.semester = Convert.ToInt32(dumm);
                        if (ptr[5].Length == 5 && (ptr[5][4] == 'S' || ptr[5][4] == 'Y'))   //  master's course
                        {
                            dumm = null;
                            dumm += ptr[5][3];
                            temp.semester = Convert.ToInt32(dumm) + 8;
                        }
                    }



                    temp.regStudents = Convert.ToInt32(ptr[6]);
                    temp.timeTableName = ptr[7];
                    temp.pre = ptr[8];

                   
               //     Console.WriteLine(temp.courseName + "\t\t\t" + temp.semester);
                    
                    allCourses.Add(temp);
                }

                return allCourses;

            }
        }
        public List<String> loadCourses()
        {
            for (int a = 0; a < allCourses.Count; a++)
            {
                courseNames.Add(allCourses[a].timeTableName);
            }
            return courseNames;
        }


        public static String FromExcelSerialDate(int SerialDate)
        {
            if (SerialDate > 59) SerialDate -= 1; //Excel/Lotus 2/29/1900 bug   
            return (new DateTime(1899, 12, 31).AddDays(SerialDate)).ToString();
        }

/*
        public String dateIncrementer(string date)
        {
            String[] arr = date.Split('-');
            date = (Convert.ToInt32(arr[0]) + 1).ToString();
            date += "-";
            date += arr[1];
            date += "-";
            date += arr[2];
            return date;
        }
*/
        public String dateChanger(string date)
        {
            String[] ptr = date.Split(' ');
            String[] arr = ptr[0].Split('-');
            date = arr[0];
            date += "-";
            date += arr[1];
            date += "-";
            date += arr[2];
            return date;
        }

        public bool isTime(string time)
        {
            String[] arr = time.Split(':');
            if (arr.Length == 3)
            {
                int num = 34;
                //  a : b-c :d
                //  12:30-01:30  (checks if a & d are numbers, then it is time)
                if (int.TryParse(arr[0], out num) && int.TryParse(arr[2], out num))
                {
                    return true;
                }
            }
            return false;
        }

        public bool timeInCell(String cellData)
        {
            int timeCount = 0;
            //  checks that there are 2 ":" in cell text or not, if yes time is embedded in the cell,
            for (int a = 0; a < cellData.Length; a++)
            {
                if (cellData[a] == ':')
                {
                    timeCount++;
                }
            }
            if (timeCount >= 2)   //  time embedded in cell
            {
                return true;
            }

            return false;
        }

        public String clearStrings(String temp)
        {
            return temp.Replace(".", String.Empty).Replace(")", String.Empty).Replace("(", String.Empty).Replace(" ", String.Empty);
        }

        public bool differenceIs1(String temp)
        {
            temp = clearStrings(temp);
            String[] first = temp.Split('-');
            String[] second = first[0].Split(':');
            String[] third = first[1].Split(':');

            if (Convert.ToInt32(third[0]) == 1 && Convert.ToInt32(second[0]) == 12) //  if 12:**-01:** (differnce is 1 hour)
            {
                return true;
            }

            if ((Convert.ToInt32(third[0]) - Convert.ToInt32(second[0])) == 1)
            {
                return true;
            }
            return false;
        }
        
        public String extractTime(String cellData)
        {
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            String time = null;
            bool started = true;        //  only 1 time to go in 'if'.
            int count = 0;

            int length = 0;
            for (int a = 0; a < cellData.Length; a++)
            {
                length++;
                //  check if char is a digit or not
                if (started && digits.Contains(cellData[a]) && (cellData[a + 1] == ':' || cellData[a + 2] == ':'))
                {
                    time += cellData[a];
                    started = false;
                }

                else if (!started)
                {
                    time += cellData[a];

                    if (cellData[a] == ':')
                    {
                        count++;
                    }
                    if (count == 2)
                    {
                        time += cellData[a + 1];
                        length++;
                        if ((length + 1) <= cellData.Length && digits.Contains(cellData[a + 2]))
                        {
                            time += cellData[a + 2];
                        }
                        break;
                    }
                }
            }
            return time;
        }
        public string addZeros(String time)
        {
            String[] ptr = time.Split(':');
            if (ptr[0].Length == 1)
            {
                ptr[0] = "0" + ptr[0];
            }
            if (ptr[1].Length == 1)
            {
                ptr[1] = "0" + ptr[1];
            }
            return (ptr[0] + ":" + ptr[1]);
        }

        public String getName(int a)
        {
            return allCourses[a].timeTableName;
        }

        public String getSection(int a)
        {
            return allCourses[a].section;
        }

        public String getoriginalName(int a)
        {
            return allCourses[a].courseName;
        }
        public String getCode(int a)
        {
            return allCourses[a].courseCode;
        }

        public int getC_Hour(int a)
        {
            return allCourses[a].creditHours;
        }

        public int getSemester (int a)
        {
            return allCourses[a].semester;
        }
        public int isACourse(String cellData)
        {
            String formattedName = clearStrings(cellData);
            for (int a = 0; a < allCourses.Count; a++)
            {
                String storedName = clearStrings(allCourses[a].timeTableName);
                if (storedName.ToUpper() == formattedName.ToUpper())
                {
                    return a;
                }
            }

            int n = isACourseContains(cellData);

            return n;
        }

        public int isACourseContains(String cellData)
        {
            String formattedName = clearStrings(cellData);
            for (int a = 0; a < allCourses.Count; a++)
            {
                String storedName = clearStrings(allCourses[a].timeTableName);
                String match = cellData.ToUpper();
                if (!match.Contains("LAB") && formattedName.ToUpper().Contains(storedName.ToUpper()))
                {
                    return a;
                }
            }
            return -1;
        }

 /*       public int isACourseMixed(String cellData)
        {
            String formattedName = clearStrings(cellData);
            for (int a = 0; a < allCourses.Count; a++)
            {
                String storedName = clearStrings(allCourses[a].timeTableName);
                if (storedName.ToUpper().Contains(formattedName.ToUpper()) || formattedName.ToUpper().Contains(storedName.ToUpper()))
                {
                    return a;
                }
                String match = cellData.ToUpper();
                if (!match.Contains("LAB") && formattedName.ToUpper().Contains(storedName.ToUpper()))
                {
                    return a;
                }
            }
            return -1;
        }
*/
        public int isVenueInCell(String cellData)
        {
            cellData = clearStrings(cellData).ToUpper();
            if (cellData.Contains("ROOM"))
            {
                return 1;
            }
            if (cellData.Contains("R#"))
            {
                return 2;
            }

            return 0;
        }

        public String getVenue1(String cellData)
        {
            String ven = null;
            bool check = false;
            cellData = clearStrings(cellData).ToUpper();
            for (int a = 0; a < cellData.Length; a++)
            {
                if (check == false && ((cellData[a] == 'R' && cellData[a + 1] == 'O' && cellData[a + 2] == 'O' && cellData[a + 3] == 'M')))
                {
                    check = true;
                }
                if (check)
                {
                    ven += cellData[a];
                }
            }
            return ven;
        }

        public String getVenue2(String cellData)
        {
            String ven = null;
            bool check = false;
            cellData = clearStrings(cellData).ToUpper();
            for (int a = 0; a < cellData.Length; a++)
            {
                if (check == false && (cellData[a] == 'R' && cellData[a + 1] == '#'))
                {
                    check = true;
                }
                if (check)
                {
                    ven += cellData[a];
                }
            }
            return ven;
        }
        public String removeAMsPMs(String time)
        {
            time = time.Replace(" ", String.Empty); //  remove whitespaces
            time = time.Replace("AM", String.Empty); //  remove whitespaces
            time = time.Replace("PM", String.Empty); //  remove whitespaces
            time = time.Replace("am", String.Empty); //  remove whitespaces
            time = time.Replace("pm", String.Empty); //  remove whitespaces
            time = time.Replace("Pm", String.Empty); //  remove whitespaces
            time = time.Replace("Am", String.Empty); //  remove whitespaces
            time = time.Replace("aM", String.Empty); //  remove whitespaces
            time = time.Replace("pM", String.Empty); //  remove whitespaces
            return time;
        }
        public bool generate(String pathPlusName, String path0)
        {
            bool labTime = false;                       // if 1 hour course lies under lab tab, then find correct time
            bool dateFound = false;

            Form1 frm = new Form1();

            int num = 78;
            String day = "";      //  to store course's day
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    try
                    {
                        //  if this (x,y) cell is present && has some value inside it
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        {
                            String cellData = xlWorksheet.Cells[i, j].Value2.ToString();
                            //    Console.WriteLine("(" + i + "," + j + ")" + cellData + "\t");

                            //      if data is a number (date converted to a serial no. ) && (serial no length must be 5 to be date)
                            if (dateFound == false && int.TryParse(cellData, out num) && (cellData).Length == 5)
                            {
                                //    Console.WriteLine("(" + i + "," + j + ")" + cellData + "\t");

                                cellData = FromExcelSerialDate(Convert.ToInt32(cellData));
                                //  Console.WriteLine("(" + i + "," + j + ")" + cellData + "\t");
                                cellData = dateChanger(cellData);
                                //Console.WriteLine("(" + i + "," + j + ")" + cellData + "\t");
                                date = cellData;
                                dateFound = true;
                                continue;
                            }
                            else
                            {
                                //  Console.WriteLine("(" + i + "," + j + ")" + cellData + "\t");


                                if (clearStrings(cellData).ToUpper() == "Venue".ToUpper())       //  stores the location of Venue, for furthure loop work
                                {
                                    venueCol = j;
                                    continue;
                                }

                                if (j == venueCol)        //  if venue column
                                {
                                    continue;
                                }


                                //  Store the day
                                if (cellData == days[0] || cellData == days[1] || cellData == days[2] || cellData == days[3] || cellData == days[4] || cellData == days[5])
                                {
                                    day = cellData;
                                    frm.label3.Text = "Finding classes on " + day;
                                    
                                    labTime = false;    //  sets it to false at every new day start
                                    continue;
                                }

                                if (clearStrings(cellData).ToUpper() == "Labs".ToUpper())
                                {
                                    labTime = true;
                                    continue;
                                }


                                //<-----------------------------------------FINDING NAME-----------------------------------------------> 
                                //      Console.WriteLine("1");

                                CellCourse temp = new CellCourse();

                                int returnedNo = isACourse(cellData);
                                if (returnedNo == -1)    //  not a course (w.r.t our D.B)
                                {
                                    continue;
                                }
                                else
                                {
                                    temp.code = getCode(returnedNo);
                                    temp.originalName = getoriginalName(returnedNo);
                                    temp.name = getName(returnedNo);
                                    temp.section = getSection(returnedNo);
                                    temp.c_hour = getC_Hour(returnedNo);
                                    temp.semester = getSemester(returnedNo);

                                    //      Console.WriteLine("(" + i + "," + j + ")" + cellData + "\t\t" + temp.name+"("+temp.section+")");
                                }


                                //     Console.WriteLine("2");

                                //<-----------------------------------------SETTING DAY------------------------------------------------> 

                                temp.day = day;
                                //     Console.WriteLine("3");
                                //<-----------------------------------------FINDING VENUE----------------------------------------------> 
                                if (isVenueInCell(cellData) == 1)
                                {
                                    temp.venue = getVenue1(cellData);   //  ROOM
                                }
                                else if (isVenueInCell(cellData) == 2)
                                {
                                    temp.venue = getVenue2(cellData);   //  R#
                                }
                                else
                                {
                                    //        Console.WriteLine(xlWorksheet.Cells[i, 2].Value2.ToString());

                                    temp.venue = xlWorksheet.Cells[i, venueCol].Value2.ToString();
                                    //         Console.WriteLine("3b");

                                }


                                //     Console.WriteLine("4");

                                //<-----------------------------------------FINDING TIME-----------------------------------------------> 

                                if (timeInCell(cellData))
                                {
                                    String time = extractTime(cellData);
                                    time = removeAMsPMs(time);

                                    if (time.Contains("-"))
                                    {
                                        String[] arrTime = time.Split('-');

                                        arrTime[0] = addZeros(arrTime[0]);  //  add missing zeros
                                        arrTime[1] = addZeros(arrTime[1]);

                                        temp.timeFrom = arrTime[0];
                                        temp.timeTo = arrTime[1];
                                    }
                                    else if (time.ToLower().Contains("to"))
                                    {
                                        String[] arrTime = time.ToLower().Split('t');

                                        arrTime[1] = arrTime[1].Substring(1, arrTime[1].Length - 1);
                                        arrTime[0] = addZeros(arrTime[0]);
                                        arrTime[1] = addZeros(arrTime[1]);

                                        temp.timeFrom = arrTime[0];
                                        temp.timeTo = arrTime[1];
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nTime Parsing Error in cell = " + cellData);

                                    }
                                    //       Console.WriteLine(temp.timeFrom+"-"+temp.timeTo+"--->"+cellData);                    
                                }


                                else    //  time is in proper place (in top time row)
                                {
                                    for (int c = i; c > 0; c--)
                                    {
                                    //  finds the time and day
                                    missElse36:
                                        if (xlRange.Cells[c, j] != null && xlRange.Cells[c, j].Value2 != null && isTime(xlWorksheet.Cells[c, j].Value2.ToString()))
                                        {
                                            //  if under lab tab, find suitable time
                                            String gj = cellData.ToUpper();
                                            if (labTime && !gj.Contains("LAB"))
                                            {//     difference should be 1 hour, as its a class, not a lab

                                                /*       if (differenceIs1(xlWorksheet.Cells[c, j].Value2.ToString()))
                                                       {

                                                           String gh = xlRange.Cells[c, j].Value2.ToString().Replace(" ", String.Empty);
                                                           String[] tu = gh.Split('-');
                                                           temp.timeFrom = tu[0];
                                                           temp.timeTo = tu[1];

                                                           break;
                                                       }
                                                 */
                                                c--;
                                                goto missElse36;
                                            }
                                            //                              display found time                                  displays venue
                                            else
                                            {
                                                String gh = xlRange.Cells[c, j].Value2.ToString().Replace(" ", String.Empty);
                                                String[] tu = gh.Split('-');
                                                temp.timeFrom = tu[0];
                                                temp.timeTo = tu[1];

                                                break;
                                            }
                                        }
                                    }

                                }
                                //          Console.WriteLine("5");

                                dailyClasses.Add(temp);
                                Console.Write("(" + i + "," + j + ")" + cellData + "\t\t" + temp.name + "(" + temp.section + ")");
                                Console.WriteLine("\t\t" + temp.originalName + "\t\t" + temp.venue + "\t\t" + temp.timeFrom + "-" + temp.timeTo + ", " + temp.day + ",  " + temp.c_hour);
                            }

                        }   //  if
                    }
                    catch (Exception er)
                    {
                        Console.WriteLine(er);
                    }





                }   //  for
            }   //  for

            try
            {
                //  generate filenames to write data and errors
                String errorFile = path0;
                String writeFile = path0;
                for (int a = 0; a < pathPlusName.Length; a++)
                {
                    if (pathPlusName[a] == '.')
                    {
                        break;
                    }
                    if (a >= path0.Length)
                    {
                        errorFile += pathPlusName[a];
                        writeFile += pathPlusName[a];
                    }
                }
                errorFile += "_error_log.txt";
                writeFile += ".txt";
         /*       using (StreamWriter write1 = new StreamWriter(errorFile))
                { }

                frm.label3.Text = "Done Finding";

                using (StreamWriter write = new StreamWriter(writeFile))
                {
                    for (int a = 0; a < dailyClasses.Count; a++)
                    {
                        String dumm = dailyClasses[a].timeFrom + "-" + dailyClasses[a].timeTo;
                        if (dumm.Length != 11)
                        {
                            using (StreamWriter write1 = new StreamWriter(errorFile, true))
                            {
                                write1.Write((a + 1) + "," + dailyClasses[a].code + "," + dailyClasses[a].originalName + "," + dailyClasses[a].name + "," + dailyClasses[a].section + "," + dailyClasses[a].c_hour + ",");
                                write1.Write(dailyClasses[a].venue + "," + dailyClasses[a].timeFrom + "-" + dailyClasses[a].timeTo + "," + dailyClasses[a].day + "," + dailyClasses[a].semester + "\n");
                            }

                        }
                        write.Write((a + 1) + "," + dailyClasses[a].code + "," + dailyClasses[a].originalName + "," + dailyClasses[a].name + "," + dailyClasses[a].section + "," + dailyClasses[a].c_hour + ",");
                        write.Write(dailyClasses[a].venue + "," + dailyClasses[a].timeFrom + "-" + dailyClasses[a].timeTo + "," + dailyClasses[a].day + "," + dailyClasses[a].semester + "\n");
                    }
                }

                */
                close();
                return true;
            }
            catch (Exception er)
            {
                Console.WriteLine(er);
                close();
                return true;
            }
        }

        private void close ()
        {
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }


    }
}
