using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClashFreeTimetable
{
    class DBHandler
    {
        String file = "ClashFreeDB";
        String connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated security=SSPI;database=master";

        String studentTable = "student";
        String courseTable = "course";
        String studentTakeCourseTable = "studentTakeCourses";



/*
public DBHandler ()
public bool createDBFile()                                  //  creates database file dynamically to store tables
public bool createStudentTable()
public bool createCouorseTable()
public bool createStudentTakeCouorseTable()
public List<StudentTakeCourse> downloadStudentFromFirebase()
public List<StudentTakeCourse> downloadCoursesFromFirebase()
public List<StudentTakeCourse> downloadStudentTakeCoursesFromFirebase()
public int getCount(String name)                            //  returns total record count of a specific table
public void truncate(String name)                           //  truncates a specific table
public void delete(String name)                             //  deletes a specific table from DB
public bool addStudentsIntoDB(List<Student> allStudents)
public bool addCoursesIntoDB(List<Course> allCourses)
public bool addStudentTakeCoursesIntoDB(List<StudentTakeCourse> allStudentTakeCourses)
public List<Student> getAllStudents()
public List<Course> getAllCourses()
public List<StudentTakeCourse> getAllStudentTakeCourses()
public List<Student> getCourseWiseStudents(String code)     //  returns Students taking a specific course
public Student getStudent(String roll)                      //  returns a specific student
public Course getCourse(String code)                        //  returns a specific course




*/










        public DBHandler()
        {
            createDBFile();
            createCourseTable();
            createStudentTable();
            createStudentTakeCouorseTable();
        }


        public bool createDBFile(String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;

            String CreateDatabase = "CREATE DATABASE " + file + " " +
                             "ON PRIMARY " +
                             "(NAME = '" + file + "', " +
                             "FILENAME = '" + Directory.GetCurrentDirectory() + "\\" + file + ".mdf', " +
                             "SIZE = 6MB, MAXSIZE = 4GB, FILEGROWTH = 10%) " +
                             "LOG ON " +
                             "(NAME = '" + file + "_LOG" + "', " +
                             "FILENAME = '" + Directory.GetCurrentDirectory() + "\\" + file + ".ldf', " +
                             "SIZE = 1MB, MAXSIZE = 200MB, FILEGROWTH = 10%)" +
                             "";

            SqlCommand command = null;

            try
            {
                con = new SqlConnection(connectionString);
                command = new SqlCommand(CreateDatabase, con);

                con.Open();
                command.ExecuteNonQuery();
            //    Console.WriteLine("Database is created successfully.");
                return true;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1801)
                {
                    Console.WriteLine("Database already created.");
                    return true;
                }
                Console.WriteLine("Database\n\n");
              //  Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                con.Close();
            }
        }


        public bool createStudentTable(String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                String sql = "CREATE TABLE " + studentTable +
                                   "(rollNo CHAR(50) CONSTRAINT PKeyrollNo PRIMARY KEY," +
                                   "pass CHAR(50), sec CHAR(10), name CHAR(50)," +
                                   " cnic CHAR(15), address CHAR(200), cellNo CHAR(20))";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("Student Created1");
                return true;
            }
            catch (SqlException e)
            {
                con.Close();
                if (e.Number == 2714)
                {
              //      Console.WriteLine(e.ToString());
                    Console.WriteLine("Student Created2");
                    return true;
                }
                Console.WriteLine("\nError 1: Table " + studentTable + " not created.\n\n");
            //    Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool createCourseTable(String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                String sql = "CREATE TABLE " + courseTable +
                                   "(courseCode CHAR(50) CONSTRAINT PKeycourseCode PRIMARY KEY," +
                                   "courseName CHAR(100), timeTableName CHAR(100), section CHAR(10)," +
                                   " regStudents INTEGER, cHours INTEGER, teacher CHAR(100)" +
                                   ", sem INTEGER, pre CHAR(50))";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("Course Created1");
                return true;
            }
            catch (SqlException e)
            {
                con.Close();
                if (e.Number == 2714)
                {
            //        Console.WriteLine(e.ToString());
                    Console.WriteLine("Course Created2");
                    return true;
                }
                Console.WriteLine("\nError 1: Table " + courseTable + " not created.\n\n");
            //    Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool createStudentTakeCouorseTable(String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                String sql = "CREATE TABLE " + studentTakeCourseTable +
                                   "(id INTEGER CONSTRAINT PKeyid PRIMARY KEY," +
                                   "rollNo CHAR(50), courseCode CHAR(20))";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("studentTakeCourseTable Created1");
                return true;
            }
            catch (SqlException e)
            {
                con.Close();
                if (e.Number == 2714)
                {
                //    Console.WriteLine(e.ToString() + );
                    Console.WriteLine("studentTakeCourseTable Created2");
                    return true;
                }
                Console.WriteLine("\nError 1: Table " + studentTakeCourseTable + " not created.\n\n");
             //   Console.WriteLine(e.ToString());
                return false;
            }
        }


        public List<Student> downloadStudentsFromFirebase()
        {
            List<Student> allStudents = new List<Student>();
            using (StreamReader read = new StreamReader("students.txt"))        //  reading no. of customers
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    String[] arr = line.Split(',');
                    Student s1 = new Student();
                    s1.rollNo = arr[1];
                    s1.password = arr[2];
                    s1.parentSection = arr[3];
                    s1.name = arr[4];
                    s1.CNIC = arr[5];
                    s1.address = arr[6];
                    s1.cellNo = arr[7];
                    allStudents.Add(s1);
                }
            }

            /*           for (int a=0; a<allStudents.Count; a++)
                       {
                           Console.WriteLine(a+1+",  "+allStudents[a].rollNo+",  " + allStudents[a].password + ",  " + allStudents[a].parentSection + ",  " + allStudents[a].name + ",  " + allStudents[a].CNIC + ",  " + allStudents[a].address + ",  " + allStudents[a].cellNo);
                       }
           */
            return allStudents;
        }



        public List<Course> downloadCoursesFromFirebase()
        {
            List<Course> allCourses = new List<Course>();
            using (StreamReader read = new StreamReader("offerd18.txt"))        //  reading no. of customers
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    String[] arr = line.Split(',');
                    Course s1 = new Course();
                    s1.courseName = arr[2];
                    s1.creditHours = Convert.ToInt32(arr[3]);
                    s1.teacher = arr[4];

                    String up = arr[2].ToUpper();
                    if (up.Contains("ENGLISH LANGUAGE LAB") || up.Contains("DIGITAL LOGIC DESIGN - LAB"))
                    {
                        s1.section += arr[5][arr[5].Length - 2];
                        s1.section += arr[5][arr[5].Length - 1];
                    }
                    else
                    {
                        s1.section += arr[5][arr[5].Length - 1];
                    }


                    s1.semester = 0;
                    if (arr[5].Length >= 4)
                    {

                        String dumm = null;
                        dumm += arr[5][3];
                        s1.semester = Convert.ToInt32(dumm);
                        if (arr[5].Length == 5 && (arr[5][4] == 'S' || arr[5][4] == 'Y'))   //  master's course
                        {
                            dumm = null;
                            dumm += arr[5][3];
                            s1.semester = Convert.ToInt32(dumm) + 8;
                        }
                    }

                    s1.courseCode = arr[1]+s1.section;
                    s1.regStudents = Convert.ToInt32(arr[6]);
                    s1.timeTableName = arr[7];
                    s1.pre = arr[8];
                    allCourses.Add(s1);
                }
            }

         /*   for (int a = 0; a < allCourses.Count; a++)
            {
                Console.WriteLine(a + 1 + ",  " + allCourses[a].courseCode + ",  " + allCourses[a].courseName + ",  " + allCourses[a].creditHours + ",  " + allCourses[a].section + ",  " + allCourses[a].semester + ",  " + allCourses[a].regStudents + ",  " + allCourses[a].timeTableName + ",  " + allCourses[a].pre);
            }
       */     return allCourses;
        }
        
        public List<StudentTakeCourse> downloadStudentTakeCoursesFromFirebase()
        {

            List<StudentTakeCourse> allStudentTakeCourses = new List<StudentTakeCourse>();
            using (StreamReader read = new StreamReader("student_take_courses_total_reduced.txt"))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    String[] arr = line.Split(',');
                    StudentTakeCourse s1 = new StudentTakeCourse();
                    s1.idSTC = Convert.ToInt32(arr[0]);
                    s1.rollNoSTC = arr[1];
                    s1.courseCodeSTC = arr[2];
                    allStudentTakeCourses.Add(s1);
                }
            }

       /*     for (int a=0; a<allStudentTakeCourses.Count; a++)
            {
                Console.WriteLine(allStudentTakeCourses[a].idSTC+",  "+allStudentTakeCourses[a].rollNoSTC+",  " + allStudentTakeCourses[a].courseCodeSTC);
            }
     */      
            return allStudentTakeCourses;
        }

        public int getCount(String name, String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            SqlCommand cmd= null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("SELECT COUNT(*) FROM " + name, con);
                cmd.Connection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Connection.Close();
                Console.WriteLine("Count " + name+" = "+count);

                return count;
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Conting error in " + name);
                return -1;
            }
        }
        public bool truncate(String name, String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("TRUNCATE TABLE " + name, con);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                Console.WriteLine("Table " + name + " truncated successfully.\n");
                return true;
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Error in truncating " + name + " table.\n\n");
                //   Console.WriteLine(e.ToString());
                return false;
            }
        }
        public bool delete(String name, String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            SqlCommand command = null;
            try
            {
                con = new SqlConnection(connectionString);
                command = new SqlCommand("DROP TABLE " + name, con);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
                Console.WriteLine("Table "+name+" deleted successfully.\n");
                return true;
            }
            catch (Exception e)
            {
                command.Connection.Close();
                Console.WriteLine("Error in deleting " + name + " table.\n\n");
                //   Console.WriteLine(e.ToString());
                return false;
            }

        }

        

        public bool addStudentsIntoDB (List<Student> allStudents, String dummyString = "null")
        {
            truncate(studentTable);

            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();

                for (int a = 0; a < allStudents.Count; a++)
                {
                    // Adding records the table
                    String sql = "INSERT INTO " + studentTable + "(rollNo, pass, sec, name, cnic, address, cellNo) " +
                    "VALUES ('" +allStudents[a].rollNo +"', '"+ allStudents[a].password + "', '" + allStudents[a].parentSection +
                    "', '" + allStudents[a].name + "', '" + allStudents[a].CNIC + "', '" + allStudents[a].address +
                    "', '" + allStudents[a].cellNo +"') ";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.ExecuteNonQuery();

             //       Console.WriteLine(allStudents[a].rollNo+" Added");

                }

                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
                Console.WriteLine("\n\t\tInsertion error Student table.\n");
             //   Console.WriteLine(e.ToString());
                return false;
            }

            return true;
        }

        public bool addCoursesIntoDB(List<Course> allCourses, String dummyString = "null")
        {
            truncate(courseTable);

            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();

                for (int a = 0; a < allCourses.Count; a++)
                {
                    // Adding records the table
                    String sql = "INSERT INTO " + courseTable + "(courseCode, courseName, timeTableName, section, regStudents, cHours, teacher, sem, pre) " +
                    "VALUES ('" + allCourses[a].courseCode + "', '" + allCourses[a].courseName + "', '" + allCourses[a].timeTableName +
                    "', '" + allCourses[a].section + "', " + allCourses[a].regStudents + ", " + allCourses[a].creditHours +
                    ", '" + allCourses[a].teacher + "', " + allCourses[a].semester + ", '" + allCourses[a].pre + "') ";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.ExecuteNonQuery();

             //       Console.WriteLine(allCourses[a].courseCode + " Added");

                }

                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
                Console.WriteLine("\n\t\tInsertion error.\n" + e.ToString());
                //   Console.WriteLine(e.ToString());
                return false;
            }

            return true;
        }

        public bool addStudentTakeCoursesIntoDB(List<StudentTakeCourse> allStudentTakeCourses, String dummyString = "null")
        {
            truncate(studentTakeCourseTable);

            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                
                for (int a = 0; a < allStudentTakeCourses.Count; a++)
                {
                    // Adding records the table
                    String sql = "INSERT INTO " + studentTakeCourseTable + "(id, rollNo, courseCode) " +
                    "VALUES (" + allStudentTakeCourses[a].idSTC + ", '" + allStudentTakeCourses[a].rollNoSTC +
                    "', '" + allStudentTakeCourses[a].courseCodeSTC + "') ";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.ExecuteNonQuery();

                 //   Console.WriteLine(allStudentTakeCourses[a].idSTC + " Added");

                }

                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
                Console.WriteLine("\n\t\tInsertion error.\n" + e.ToString());
                //   Console.WriteLine(e.ToString());
                return false;
            }

            return true;
        }


        public List<Student> getAllStudents (String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            List<Student> allStudents = new List<Student>();
            SqlCommand cmd=null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("SELECT * FROM " + studentTable, con);
                cmd.Connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Student s1 = new Student();
                    s1.rollNo = (String)sdr["rollNo"];
                    s1.rollNo = s1.rollNo.Replace(" ", String.Empty);

                    s1.password = (String)sdr["pass"];
                    s1.password = s1.password.Replace(" ", String.Empty);

                    s1.parentSection = (String)sdr["sec"];
                    s1.parentSection = s1.parentSection.Replace(" ", String.Empty);

                    s1.name = (String)sdr["name"];
                    s1.name = s1.name.Replace(" ", String.Empty);

                    s1.CNIC = (String)sdr["cnic"];
                    s1.CNIC = s1.CNIC.Replace(" ", String.Empty);

                    s1.address = (String)sdr["address"];
                    s1.address = s1.address.Replace(" ", String.Empty);

                    s1.cellNo = (String)sdr["cellNo"];
                    s1.cellNo = s1.cellNo.Replace(" ", String.Empty);

                    //    Console.WriteLine(s1.rollNo+", "+ s1.password + ", " + s1.parentSection + ", " + s1.name + ", " + s1.CNIC + ", " + s1.address + ", " + s1.cellNo + ", ");

                    allStudents.Add(s1);
                }
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Getting all students error");
                Console.WriteLine(e.ToString());
                return null;
            }
            Console.WriteLine("students returned");
            return allStudents;
        }



        public List<Course> getAllCourses(String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            List<Course> allCourses = new List<Course>();
            SqlCommand cmd=null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("SELECT * FROM " + courseTable, con);
                cmd.Connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Course s1 = new Course();
                    s1.courseCode = (String)sdr["courseCode"];
                    s1.courseCode = s1.courseCode.Replace(" ", String.Empty);

                    s1.courseName = (String)sdr["courseName"];
                    s1.courseName = s1.courseName.Replace(" ", String.Empty);

                    s1.timeTableName = (String)sdr["timeTableName"];
                    s1.timeTableName = s1.timeTableName.Replace(" ", String.Empty);

                    s1.section = (String)sdr["section"];
                    s1.section = s1.section.Replace(" ", String.Empty);

                    s1.regStudents = (int)sdr["regStudents"];
                    s1.creditHours = (int)sdr["cHours"];

                    s1.teacher = (String)sdr["teacher"];
                    s1.teacher = s1.teacher.Replace(" ", String.Empty);

                    s1.semester = (int)sdr["sem"];

                    s1.pre = (String)sdr["pre"];
                    s1.pre = s1.pre.Replace(" ", String.Empty);
                    //      Console.WriteLine(s1.courseCode+", "+ s1.courseName + ", " + s1.timeTableName + ", " + s1.section + ", " + s1.regStudents + ", " + s1.creditHours + ", " + s1.teacher + ", " + s1.section + ", " + s1.semester);

                    allCourses.Add(s1);
                }
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Getting all courses error");
                Console.WriteLine(e.ToString());
                return null;
            }
            Console.WriteLine("Courses returned");
            return allCourses;
        }


        public List<StudentTakeCourse> getAllStudentTakeCourses(String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            List<StudentTakeCourse> allStudentTakeCourses = new List<StudentTakeCourse>();
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("SELECT * FROM " + studentTakeCourseTable, con);
                cmd.Connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    StudentTakeCourse s1 = new StudentTakeCourse();
                    s1.idSTC = (int)sdr["id"];
                    s1.courseCodeSTC = (String)sdr["courseCode"];
                    s1.rollNoSTC = (String)sdr["rollNo"];
               //     Console.WriteLine(s1.idSTC+", "+ s1.courseCodeSTC + ", " + s1.rollNoSTC);

                    allStudentTakeCourses.Add(s1);
                }
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Getting all studentTakeCourses error");
             //   Console.WriteLine(e.ToString());
                return null;
            }
        //    Console.WriteLine("StudentTakeCourses returned");
            return allStudentTakeCourses;
        }

        public List<Student> getCourseWiseStudents (String code, String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            List<Student> allStudents = new List<Student>();
            String sql = "select student.* from student inner join studentTakeCourses on student.rollNo = studentTakeCourses.rollNo where studentTakeCourses.courseCode in (select courseCode from studentTakeCourses where courseCode = '"+code+"' )";
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand(sql, con);
                cmd.Connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Student s1 = new Student();
                    s1.rollNo = (String)sdr["rollNo"];
                    s1.rollNo = s1.rollNo.Replace(" ", String.Empty);

                    s1.password = (String)sdr["pass"];
                    s1.password = s1.password.Replace(" ", String.Empty);

                    s1.parentSection = (String)sdr["sec"];
                    s1.parentSection = s1.parentSection.Replace(" ", String.Empty);

                    s1.name = (String)sdr["name"];
                    s1.name = s1.name.Replace(" ", String.Empty);

                    s1.CNIC = (String)sdr["cnic"];
                    s1.CNIC = s1.CNIC.Replace(" ", String.Empty);

                    s1.address = (String)sdr["address"];
                    s1.address = s1.address.Replace(" ", String.Empty);

                    s1.cellNo = (String)sdr["cellNo"];
                    s1.cellNo = s1.cellNo.Replace(" ", String.Empty);


                    Console.WriteLine(s1.rollNo+", "+ s1.password + ", " + s1.parentSection + ", " + s1.name + ", " + s1.CNIC + ", " + s1.address + ", " + s1.cellNo + ", ");

                    allStudents.Add(s1);
                }
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Getting all courseWise students error");
                Console.WriteLine(e.ToString());
                return null;
            }
            Console.WriteLine("course wise students returned");
            return allStudents;
        }

        public Student getStudent(String roll, String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            SqlCommand cmd = null;
            Student s1 = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("SELECT * FROM " + studentTable + " where rollNo = '" + roll + "'", con);
                cmd.Connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    s1 = new Student();
                    s1.rollNo = (String)sdr["rollNo"];
                    s1.rollNo = s1.rollNo.Replace(" ", String.Empty);

                    s1.password = (String)sdr["pass"];
                    s1.password = s1.password.Replace(" ", String.Empty);

                    s1.parentSection = (String)sdr["sec"];
                    s1.parentSection = s1.parentSection.Replace(" ", String.Empty);

                    s1.name = (String)sdr["name"];
                    s1.name = s1.name.Replace(" ", String.Empty);

                    s1.CNIC = (String)sdr["cnic"];
                    s1.CNIC = s1.CNIC.Replace(" ", String.Empty);

                    s1.address = (String)sdr["address"];
                    s1.address = s1.address.Replace(" ", String.Empty);

                    s1.cellNo = (String)sdr["cellNo"];
                    s1.cellNo = s1.cellNo.Replace(" ", String.Empty);

                    //         Console.WriteLine(s1.rollNo+", "+ s1.password + ", " + s1.parentSection + ", " + s1.name + ", " + s1.CNIC + ", " + s1.address + ", " + s1.cellNo + ", ");
                }
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Getting a students error");
                Console.WriteLine(e.ToString());
                return null;
            }
     //       Console.WriteLine("student returned");
            return s1;
        }

        public bool printStudent (Student s1)
        {
            Console.WriteLine(s1.rollNo + ", " + s1.password + ", " + s1.parentSection + ", " + s1.name + ", " + s1.CNIC + ", " + s1.address + ", " + s1.cellNo + ", ");
            return true;
        }

        public Course getCourse(String code, String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            SqlCommand cmd=null;
            Course s1 = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("SELECT * FROM " + courseTable + " where courseCode = '" + code + "'", con);
                cmd.Connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    s1 = new Course();
                    s1.courseCode = (String)sdr["courseCode"];
                    s1.courseCode = s1.courseCode.Replace(" ", String.Empty);

                    s1.courseName = (String)sdr["courseName"];
                    s1.courseName = s1.courseName.Replace(" ", String.Empty);

                    s1.timeTableName = (String)sdr["timeTableName"];
                    s1.timeTableName = s1.timeTableName.Replace(" ", String.Empty);

                    s1.section = (String)sdr["section"];
                    s1.section = s1.section.Replace(" ", String.Empty);

                    s1.regStudents = (int)sdr["regStudents"];
                    s1.creditHours = (int)sdr["cHours"];

                    s1.teacher = (String)sdr["teacher"];
                    s1.teacher = s1.teacher.Replace(" ", String.Empty);

                    s1.semester = (int)sdr["sem"];

                    s1.pre = (String)sdr["pre"];
                    s1.pre = s1.pre.Replace(" ", String.Empty);
                    //       Console.WriteLine(s1.courseCode+", "+ s1.courseName + ", " + s1.timeTableName + ", " + s1.section + ", " + s1.regStudents + ", " + s1.creditHours + ", " + s1.teacher + ", " + s1.semester + ", " + s1.pre);
                }
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Getting a course error");
                Console.WriteLine(e.ToString());
                return null;
            }
     //       Console.WriteLine("Cours returned");
            return s1;
        }

        
        public List<Course> getSemWiseCourses(int a, int b=-1, String dummyString = "null")      //  excluding labs
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            List<Course> allCourses = new List<Course>();
            String sql;
            if (b==-1)
            {
                sql = "SELECT * FROM " + courseTable + " where sem=" + a + " and cHours>=" + 3 + "";
            }
            else
            {
                sql = "SELECT * FROM " + courseTable + " where (sem=" + a + " or sem=" + b + ") and cHours>=" + 3 + "";
            }

            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand(sql, con);
                cmd.Connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

       //         Console.WriteLine("\n\n\t\t\tCourses of Semster "+a+" and "+b+".\n");
                while (sdr.Read())
                {
                    Course s1 = new Course();
                    s1.courseCode = (String)sdr["courseCode"];
                    s1.courseCode = s1.courseCode.Replace(" ", String.Empty);

                    s1.courseName = (String)sdr["courseName"];
                    s1.courseName = s1.courseName.Replace(" ", String.Empty);

                    s1.timeTableName = (String)sdr["timeTableName"];
                    s1.timeTableName = s1.timeTableName.Replace(" ", String.Empty);

                    s1.section = (String)sdr["section"];
                    s1.section = s1.section.Replace(" ", String.Empty);

                    s1.regStudents = (int)sdr["regStudents"];
                    s1.creditHours = (int)sdr["cHours"];

                    s1.teacher = (String)sdr["teacher"];
                    s1.teacher = s1.teacher.Replace(" ", String.Empty);

                    s1.semester = (int)sdr["sem"];

                    s1.pre = (String)sdr["pre"];
                    s1.pre = s1.pre.Replace(" ", String.Empty);
                    //     Console.WriteLine(s1.courseCode+", "+ s1.courseName + ", " + s1.timeTableName + ", " + s1.section + ", " + s1.regStudents + ", " + s1.creditHours + ", " + s1.teacher + ", " + s1.section + ", " + s1.semester);

                    allCourses.Add(s1);
                }
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Getting all courses error");
                Console.WriteLine(e.ToString());
                return null;
            }
      //      Console.WriteLine("Courses returned");
            return allCourses;
        }


        public List<DetailedCourse> getAllCoursesWithoutLabs(String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            List<DetailedCourse> allCourses = new List<DetailedCourse>();
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("SELECT * FROM " + courseTable + " where cHours>=3", con);
                cmd.Connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Course s1 = new Course();
                    s1.courseCode = (String)sdr["courseCode"];
                    s1.courseCode = s1.courseCode.Replace(" ", String.Empty);

                    s1.courseName = (String)sdr["courseName"];
                    s1.courseName = s1.courseName.Replace(" ", String.Empty);

                    s1.timeTableName = (String)sdr["timeTableName"];
                    s1.timeTableName = s1.timeTableName.Replace(" ", String.Empty);

                    s1.section = (String)sdr["section"];
                    s1.section = s1.section.Replace(" ", String.Empty);

                    s1.regStudents = (int)sdr["regStudents"];
                    s1.creditHours = (int)sdr["cHours"];

                    s1.teacher = (String)sdr["teacher"];
                    s1.teacher = s1.teacher.Replace(" ", String.Empty);

                    s1.semester = (int)sdr["sem"];

                    s1.pre = (String)sdr["pre"];
                    s1.pre = s1.pre.Replace(" ", String.Empty);

                    //         Console.WriteLine(s1.courseCode+", "+ s1.courseName + ", " + s1.timeTableName + ", " + s1.section + ", " + s1.regStudents + ", " + s1.creditHours + ", " + s1.teacher + ", " + s1.section + ", " + s1.semester);
               //     Console.Write(s1.courseCode+",  ");

                    DetailedCourse temp = new DetailedCourse();
                    temp.course = s1;
                    temp.remainingC_Hours = s1.creditHours;

                    allCourses.Add(temp);
                }
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Getting all courses error");
                Console.WriteLine(e.ToString());
                return null;
            }
       //     Console.WriteLine("Courses returned");
            return allCourses;
        }

        
        public List<Course> getAllCoursesOfATeacher(String teach, String dummyString = "null")
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            List<Course> allCourses = new List<Course>();
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("SELECT * FROM " + courseTable + " where cHours>=3 and teacher='" + teach + "'", con);
                cmd.Connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

         //       Console.Write(teach+"  =  ");
                while (sdr.Read())
                {
                    Course s1 = new Course();
                    s1.courseCode = (String)sdr["courseCode"];
                    s1.courseCode = s1.courseCode.Replace(" ", String.Empty);

                    s1.courseName = (String)sdr["courseName"];
                    s1.courseName = s1.courseName.Replace(" ", String.Empty);

                    s1.timeTableName = (String)sdr["timeTableName"];
                    s1.timeTableName = s1.timeTableName.Replace(" ", String.Empty);

                    s1.section = (String)sdr["section"];
                    s1.section = s1.section.Replace(" ", String.Empty);

                    s1.regStudents = (int)sdr["regStudents"];
                    s1.creditHours = (int)sdr["cHours"];

                    s1.teacher = (String)sdr["teacher"];
                    s1.teacher = s1.teacher.Replace(" ", String.Empty);

                    s1.semester = (int)sdr["sem"];

                    s1.pre = (String)sdr["pre"];
                    s1.pre = s1.pre.Replace(" ", String.Empty);

                    //       Console.Write("   ---->"+s1.courseName);

                    allCourses.Add(s1);
                }
            //    Console.Write("\n\n");
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Getting all courses error");
                Console.WriteLine(e.ToString());
                return null;
            }
            //     Console.WriteLine("Courses returned");
            return allCourses;
        }


        public List<Student> getStudentsClash(String code, String ptr, String dummyString = "null")  //  two course codes
        {
            if (dummyString != "null")
            {
                connectionString += dummyString;
            }

            SqlConnection con = null;
            List<Student> allStudents = new List<Student>();
            String sql = " select * from student where rollNo in (select D.rollNo From studentTakeCourses as D INNER JOIN (select rollNo from studentTakeCourses where courseCode = '" + code + "') as X ON D.rollNo = X.rollNo WHERE D.courseCode = '" + ptr+"')";

            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand(sql, con);
                cmd.Connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Student s1 = new Student();
                    s1.rollNo = (String)sdr["rollNo"];
                    s1.rollNo = s1.rollNo.Replace(" ", String.Empty);

                    s1.password = (String)sdr["pass"];
                    s1.password = s1.password.Replace(" ", String.Empty);

                    s1.parentSection = (String)sdr["sec"];
                    s1.parentSection = s1.parentSection.Replace(" ", String.Empty);

                    s1.name = (String)sdr["name"];
                    s1.name = s1.name.Replace(" ", String.Empty);

                    s1.CNIC = (String)sdr["cnic"];
                    s1.CNIC = s1.CNIC.Replace(" ", String.Empty);

                    s1.address = (String)sdr["address"];
                    s1.address = s1.address.Replace(" ", String.Empty);

                    s1.cellNo = (String)sdr["cellNo"];
                    s1.cellNo = s1.cellNo.Replace(" ", String.Empty);

    //                Console.WriteLine(s1.rollNo+", "+ s1.password + ", " + s1.parentSection + ", " + s1.name + ", " + s1.CNIC + ", " + s1.address + ", " + s1.cellNo + ", ");

                    allStudents.Add(s1);
                }
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                Console.WriteLine("Getting all courseWise students error");
                Console.WriteLine(e.ToString());
                return null;
            }
       //     Console.WriteLine("Clash students returned");
            return allStudents;
        }


    }
}







