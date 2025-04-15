using Interview1.Task.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Interview1.Task.DAL
{
    public class StudentDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public List<Student> GetAllStudents()
        {
            List<Student> studentList = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT st.student_Name,st.student_Gender,sm.Mark1,sm.Mark2 FROM tbl_Student st " +
                    "JOIN tbl_Student_Marks sm on st.Id = sm.student_Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Student student = new Student
                    {
                        Name = sdr["student_Name"].ToString(),
                        Gender = sdr["student_Gender"].ToString(),
                        Mark1 = Convert.ToInt32(sdr["Mark1"]),
                        Mark2 = Convert.ToInt32(sdr["Mark2"])

                    };
                    studentList.Add(student);
                }
                conn.Close();
            }
            return studentList;
        }

        public void AddStudent(Student student)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insertStudentquery = "INSERT INTO tbl_Student(student_Name,student_Gender) OUTPUT INSERTED.Id VALUES (@Name,@Gender)";
                SqlCommand cmdStudents = new SqlCommand(insertStudentquery, conn);
                cmdStudents.Parameters.AddWithValue("@Name", student.Name);
                cmdStudents.Parameters.AddWithValue("@Gender", student.Gender);

                object result = cmdStudents.ExecuteScalar();
                if (result != null)
                {
                    int studentId = Convert.ToInt32(result);
                    string insertMarkQuery = "INSERT INTO tbl_Student_Marks(student_Id,Mark1,Mark2) VALUES(@StudentId,@Mark1,@Mark2)";
                    SqlCommand cmdMarks = new SqlCommand(insertMarkQuery, conn);
                    cmdMarks.Parameters.AddWithValue("@StudentId", studentId);
                    cmdMarks.Parameters.AddWithValue("@Mark1", student.Mark1);
                    cmdMarks.Parameters.AddWithValue("@Mark2", student.Mark2);

                    cmdMarks.ExecuteNonQuery();
                }
            }
        }
        // --Table Creation query
        //create table tbl_Student(
        //Id int identity(1,1) primary key,
        //student_Name varchar(50),
        //student_Gender varchar(50)
        //)

        //create table tbl_Student_Marks(
        //Id int identity(1,1) primary key,
        //student_Id int Foreign Key References tbl_Student(Id),
        //Mark1 int,
        //Mark2 int
        //)
    }
}