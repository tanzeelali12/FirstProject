using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClashFreeTimetable
{
    [TestFixture]
    class MyTestCase1
    {


        //      DBHandler Class TestCases

        [TestCase]
        public void Test1DBCreation()
        {
            DBHandler db = new DBHandler();
            Assert.IsTrue(db.createDBFile());
            Assert.IsFalse(db.createDBFile("garbage"));
        }

        [TestCase]
        public void Test2createStudentTable()
        {
            DBHandler db = new DBHandler();
            Assert.IsTrue(db.createDBFile());
            Assert.IsTrue(db.createStudentTable());
            Assert.IsTrue(db.delete("student"));
            Assert.IsTrue(db.createStudentTable());
            Assert.IsTrue(db.createStudentTable());
            Assert.IsFalse(db.createStudentTable("garbage"));


        }

        [TestCase]
        public void Test3createCourseTable()
        {
            DBHandler db = new DBHandler();
            Assert.IsTrue(db.createDBFile());
            Assert.IsTrue(db.createCourseTable());
            Assert.IsTrue(db.delete("course"));
            Assert.IsTrue(db.createCourseTable());
            Assert.IsTrue(db.createCourseTable());
            Assert.IsFalse(db.createCourseTable("garbage"));
        }

        [TestCase]
        public void Test4createStudentTakeCouorseTable()
        {
            DBHandler db = new DBHandler();
            Assert.IsTrue(db.createDBFile());
            Assert.IsTrue(db.createStudentTakeCouorseTable());
            Assert.IsTrue(db.delete("studentTakeCourses"));
            Assert.IsTrue(db.createStudentTakeCouorseTable());
            Assert.IsTrue(db.createStudentTakeCouorseTable());
            Assert.IsFalse(db.createStudentTakeCouorseTable("garbage"));
        }

        [TestCase]
        public void Test5downloadStudentsFromFirebase()
        {
            DBHandler db = new DBHandler();

            List<Student> all = db.downloadStudentsFromFirebase();
            for (int a = 0; a < all.Count; a++)
            {
                Assert.NotNull(all[a]);

            }
            Assert.NotNull(all);
        }

        [TestCase]
        public void Test6downloadCoursesFromFirebase()
        {
            DBHandler db = new DBHandler();

            List<Course> all = db.downloadCoursesFromFirebase();
            for (int a = 0; a < all.Count; a++)
            {
                Assert.NotNull(all[a]);

            }
            Assert.NotNull(all);
        }


        [TestCase]
        public void Test7downloadStudentTakeCoursesFromFirebase()
        {
            DBHandler db = new DBHandler();

            List<StudentTakeCourse> all = db.downloadStudentTakeCoursesFromFirebase();
            for (int a = 0; a < all.Count; a++)
            {
                Assert.NotNull(all[a]);

            }
            Assert.NotNull(all);
        }

        [TestCase]
        public void Test8getCount()
        {
            DBHandler db = new DBHandler();

            Assert.GreaterOrEqual(db.getCount("student"), 0);
            Assert.GreaterOrEqual(db.getCount("course"), 0);
            Assert.GreaterOrEqual(db.getCount("studentTakeCourses"), 0);

            Assert.Negative(db.getCount("student", "garbage"));
            Assert.Negative(db.getCount("course", "garbage"));
            Assert.Negative(db.getCount("studentTakeCourses", "garbage"));


        }

        [TestCase]
        public void Test9truncate()
        {
            DBHandler db = new DBHandler();

            Assert.True(db.truncate("student"));
            Assert.True(db.truncate("course"));
            Assert.True(db.truncate("studentTakeCourses"));

            Assert.False(db.truncate("student", "garbage"));
            Assert.False(db.truncate("course", "garbage"));
            Assert.False(db.truncate("studentTakeCourses", "garbage"));

            db.addStudentsIntoDB(db.downloadStudentsFromFirebase());
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());
            db.addStudentTakeCoursesIntoDB(db.downloadStudentTakeCoursesFromFirebase());
        }

        [TestCase]
        public void Test10delete()
        {
            DBHandler db = new DBHandler();

            Assert.True(db.delete("student"));
            Assert.True(db.delete("course"));
            Assert.True(db.delete("studentTakeCourses"));

            Assert.False(db.delete("student", "garbage"));
            Assert.False(db.delete("course", "garbage"));
            Assert.False(db.delete("studentTakeCourses", "garbage"));


            db.addStudentsIntoDB(db.downloadStudentsFromFirebase());
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());
            db.addStudentTakeCoursesIntoDB(db.downloadStudentTakeCoursesFromFirebase());
        }

        [TestCase]
        public void Test11addStudentsIntoDB()
        {
            DBHandler db = new DBHandler();

            Assert.True(db.addStudentsIntoDB(db.downloadStudentsFromFirebase()));
            Assert.False(db.addStudentsIntoDB(null, "garbage"));
        }

        [TestCase]
        public void Test12addCoursesIntoDB()
        {
            DBHandler db = new DBHandler();

            Assert.True(db.addCoursesIntoDB(db.downloadCoursesFromFirebase()));
            Assert.False(db.addCoursesIntoDB(null, "garbage"));
        }

        [TestCase]
        public void Test13addStudentTakeCoursesIntoDB()
        {
            DBHandler db = new DBHandler();

            Assert.True(db.addStudentTakeCoursesIntoDB(db.downloadStudentTakeCoursesFromFirebase()));
            Assert.False(db.addStudentTakeCoursesIntoDB(null, "garbage"));
        }


        [TestCase]
        public void Test14getAllStudents()
        {
            DBHandler db = new DBHandler();
            db.addStudentsIntoDB(db.downloadStudentsFromFirebase());

            List<Student> all = db.getAllStudents();
            for (int a = 0; a < all.Count; a++)
            {
                Assert.NotNull(all[a]);

            }
            Assert.NotNull(all);
            Assert.Null(db.getAllStudents("garbage"));
        }

        [TestCase]
        public void Test15getAllCourses()
        {
            DBHandler db = new DBHandler();
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());

            List<Course> all = db.getAllCourses();
            for (int a = 0; a < all.Count; a++)
            {
                Assert.NotNull(all[a]);

            }
            Assert.NotNull(all);
            Assert.Null(db.getAllCourses("garbage"));
        }

        [TestCase]
        public void Test16getAllStudentTakeCourses()
        {
            DBHandler db = new DBHandler();
            db.addStudentTakeCoursesIntoDB(db.downloadStudentTakeCoursesFromFirebase());

            Assert.IsTrue(db.createDBFile());
            List<StudentTakeCourse> all = db.getAllStudentTakeCourses();
            for (int a = 0; a < all.Count; a++)
            {
                Assert.NotNull(all[a]);

            }
            Assert.NotNull(all);
            Assert.Null(db.getAllStudentTakeCourses("garbage"));
        }


        [TestCase]
        public void Test17getCourseWiseStudents()
        {
            DBHandler db = new DBHandler();
            db.addStudentsIntoDB(db.downloadStudentsFromFirebase());
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());
            db.addStudentTakeCoursesIntoDB(db.downloadStudentTakeCoursesFromFirebase());

            List<Student> all = db.getCourseWiseStudents("CS103A");
            for (int a = 0; a < all.Count; a++)
            {
                Assert.NotNull(all[a]);

            }
            Assert.NotNull(all);
            Assert.Null(db.getCourseWiseStudents("CS103A", "garbage"));
        }

        [TestCase]
        public void Test18getStudent()
        {
            DBHandler db = new DBHandler();
            db.addStudentsIntoDB(db.downloadStudentsFromFirebase());

            Student all = db.getStudent("i140023");
            Assert.NotNull(all);
            Assert.True(db.printStudent(all));
            Assert.Null(db.getStudent("i140023", "garbage"));
        }

        [TestCase]
        public void Test19getCourse()
        {
            DBHandler db = new DBHandler();
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());

            Course all = db.getCourse("CS103A");
            Assert.NotNull(all);
            Assert.Null(db.getCourse("CS103A", "garbage"));
        }



        [TestCase]
        public void Test20getSemWiseCourses()
        {
            DBHandler db = new DBHandler();
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());

            Assert.NotNull(db.getSemWiseCourses(1));
            Assert.NotNull(db.getSemWiseCourses(1, 2));
            Assert.Null(db.getSemWiseCourses(1, 2, "garbage"));
        }

        [TestCase]
        public void Test21getAllCoursesWithoutLabs()
        {
            DBHandler db = new DBHandler();
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());

            Assert.NotNull(db.getAllCoursesWithoutLabs());
            Assert.Null(db.getAllCoursesWithoutLabs("garbage"));
        }

        [TestCase]
        public void Test22getAllCoursesOfATeacher()
        {
            DBHandler db = new DBHandler();
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());

            Assert.NotNull(db.getAllCoursesOfATeacher("Saba Rasheed Malik"));
            Assert.Null(db.getAllCoursesOfATeacher("Saba Rasheed Malik", "garbage"));
        }


        [TestCase]
        public void Test23getStudentsClash()
        {
            DBHandler db = new DBHandler();
            db.addStudentsIntoDB(db.downloadStudentsFromFirebase());
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());
            db.addStudentTakeCoursesIntoDB(db.downloadStudentTakeCoursesFromFirebase());

            List<Student> all = db.getStudentsClash("EE182A", "CS103A");

            Assert.NotNull(all);
            Assert.Null(db.getStudentsClash("A", "CS103A", "garbage"));
        }


        // Simple Classes Test Cases

        [TestCase]
        public void Test24Course()
        {
            Course c1 = new Course();
            c1.courseCode = c1.courseCode;
            c1.courseName = c1.courseName;
            c1.timeTableName = c1.timeTableName;
            c1.section = c1.section;
            c1.regStudents = c1.regStudents;
            c1.creditHours = c1.creditHours;
            c1.teacher = c1.teacher;
            c1.semester = c1.semester;
            c1.pre = c1.pre;
        }

        [TestCase]
        public void Test25Student()
        {
            Student c1 = new Student();
            c1.name = c1.name;
            c1.parentSection = c1.parentSection;
            c1.rollNo = c1.rollNo;
            c1.password = c1.password;
            c1.cellNo = c1.cellNo;
            c1.address = c1.address;
            c1.CNIC = c1.CNIC;
        }

        [TestCase]
        public void Test26StudentTakeCourse()
        {
            StudentTakeCourse c1 = new StudentTakeCourse();
            c1.courseCodeSTC = c1.courseCodeSTC;
            c1.idSTC = c1.idSTC;
            c1.rollNoSTC = c1.rollNoSTC;
        }

        [TestCase]
        public void Test27DetailedCourse()
        {
            DetailedCourse c1 = new DetailedCourse();
            c1.course = new Course();
            Course c2 = c1.course;
            c1.monday = c1.monday;
            c1.thursday = c1.thursday;
            c1.tuesday = c1.tuesday;
            c1.wednesday = c1.wednesday;
            c1.friday = c1.friday;
            c1.remainingC_Hours = c1.remainingC_Hours;
        }

        [TestCase]
        public void Test28CellCourse()
        {
            CellCourse c1 = new CellCourse();
            c1.ID = c1.ID;
            c1.code = c1.code;
            c1.originalName = c1.originalName;
            c1.name = c1.name;
            c1.c_hour = c1.c_hour;
            c1.section = c1.section;
            c1.venue = c1.venue;
            c1.timeFrom = c1.timeFrom;
            c1.timeTo = c1.timeTo;
            c1.semester = c1.semester;
            c1.day = c1.day;
        }


        //      Excel Reader Class Test Cases
        [TestCase]
        public void Test29ExcelReader()
        {
            ExcelReader er = new ExcelReader();
            Assert.True (er.start("WEEK17.xlsx", ""));
            Assert.True(er.differenceIs1("12:00-01:00"));
            Assert.True(er.differenceIs1("01:00-02:00"));
            Assert.False(er.differenceIs1("00:00-05:00"));
            Assert.NotNull(er.downloadFirebaseCourses());
            Assert.NotNull(er.loadCourses());
            Assert.True(er.isTime("12:40-01:30"));
            Assert.False(er.isTime("3"));
            Assert.True(er.timeInCell("12:40-01:30"));
            Assert.False(er.timeInCell("1240-01:30"));
            Assert.NotNull(er.clearStrings("hello ho"));
            Assert.NotNull(er.addZeros("3:40-04:50"));

            Assert.Negative(er.isACourse("xzcvsgfg"));
            Assert.Positive(er.isACourse("L.A"));

            Assert.AreEqual(er.isVenueInCell("ROOMs"), 1);
            Assert.AreEqual(er.isVenueInCell("aR#"), 2);
            Assert.AreEqual(er.isVenueInCell("CLD"), 0);

            Assert.NotNull(er.removeAMsPMs("12:00 AM"));

        }


        //       Test Cases
        [TestCase]
        public void Test30()
        {
            DBHandler db = new DBHandler();
            db.addStudentsIntoDB(db.downloadStudentsFromFirebase());
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());
            db.addStudentTakeCoursesIntoDB(db.downloadStudentTakeCoursesFromFirebase());

            Form1 frm = new Form1();
            Login log = new Login();
            ClashMessage cm = new ClashMessage();
            Generate gen = new Generate();
            //  Assert.True(er.start("WEEK17.xlsx", ""));
        }

        [TestCase]
        public void Test31()
        {
            Generate gen = new Generate();
            Assert.True (gen.doInitialWork());
            Assert.True(gen.checkInitialValues());

            ClashMessage cm = new ClashMessage();
        }

        [TestCase]
        public void Test32BackEndClashFree()
        {
            
            DBHandler db = new DBHandler();
            db.addStudentsIntoDB(db.downloadStudentsFromFirebase());
            db.addCoursesIntoDB(db.downloadCoursesFromFirebase());
            db.addStudentTakeCoursesIntoDB(db.downloadStudentTakeCoursesFromFirebase());
            Generate gen = new Generate();


            //  Assert.True(er.start("WEEK17.xlsx", ""));
        }
    }
}
