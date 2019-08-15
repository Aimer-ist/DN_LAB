using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DN_lab.Models {

    public class DBconnST {

        private readonly string ConnStr = @"Data Source=PC-PEI\SQLEXPRESS;Initial Catalog=bn_lab;Integrated Security=True";



        // 取得學生資料=============================================
        public List<Student> GetStudents() {

            List<Student> students = new List<Student>();

            // 先訂出來的'SQL指令'(連線資料庫字串，搜尋全部資料表內容)暫且還未執行。開啟連線
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM students");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            // 是執行先前訂出來的'SQL指令'再去讀取執行的結果(全部的資料表內容)
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows) {
                while (reader.Read()) {
                    Student student = new Student {
                        Student_Id = reader.GetInt32(reader.GetOrdinal("student_id")),
                        St_Name = reader.GetString(reader.GetOrdinal("st_name")),
                        St_Paper = reader.GetString(reader.GetOrdinal("st_paper")),
                        St_Year = reader.GetInt32(reader.GetOrdinal("st_year")),
                        St_Type = reader.GetString(reader.GetOrdinal("st_type")),
                        St_School = reader.GetString(reader.GetOrdinal("st_school")),
                    };
                    students.Add(student);
                }
            }
            else {
                Console.WriteLine("資料庫內容是空的");
            }

            sqlConnection.Close();

            
            return students;
        }


        // 新增學生資料=============================================
        public void NewStudent(Student student) {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand
                (@"INSERT INTO students (st_name, st_year, st_paper, st_type, st_school) VALUES (@st_name, @st_year, @st_paper, @st_type, @st_school)");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@st_name", student.St_Name));
            sqlCommand.Parameters.Add(new SqlParameter("@st_year", student.St_Year));
            sqlCommand.Parameters.Add(new SqlParameter("@st_paper", student.St_Paper));
            sqlCommand.Parameters.Add(new SqlParameter("@st_type", student.St_Type));
            sqlCommand.Parameters.Add(new SqlParameter("@st_school", student.St_School));

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        // 取得學生編號(id)=============================================
        // 利用現有的id資訊去資料庫找符合id的資料
        public Student GetStudentById(int id) {
            Student student = new Student();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM students WHERE student_id = @id");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows) {
                while (reader.Read()) {
                    student = new Student {
                        Student_Id = reader.GetInt32(reader.GetOrdinal("student_id")),
                        St_Name = reader.GetString(reader.GetOrdinal("st_name")),
                        St_Paper = reader.GetString(reader.GetOrdinal("st_paper")),
                        St_Year = reader.GetInt32(reader.GetOrdinal("st_year")),
                        St_Type = reader.GetString(reader.GetOrdinal("st_type")),
                        St_School = reader.GetString(reader.GetOrdinal("st_school")),
                    };
                }
            }
            else {
                student.St_Name = "未找到該筆資料"; //借放
            }
            sqlConnection.Close();
            return student;
        }

        // 更新資訊到資料庫=============================================
        public void UpdateStudent(Student student) {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand
                ("UPDATE students SET st_name = @st_name, st_year = @st_year, st_paper = @st_paper, st_type = @st_type, st_school = @st_school WHERE student_id = @student_id");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@student_id", student.Student_Id));
            sqlCommand.Parameters.Add(new SqlParameter("@st_name", student.St_Name));
            sqlCommand.Parameters.Add(new SqlParameter("@st_year", student.St_Year));
            sqlCommand.Parameters.Add(new SqlParameter("@st_paper", student.St_Paper));
            sqlCommand.Parameters.Add(new SqlParameter("@st_type", student.St_Type));
            sqlCommand.Parameters.Add(new SqlParameter("@st_school", student.St_School));
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        // 刪除學生的資料=============================================
        public void DeleteStudentById(int id) {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM students WHERE student_id = @id");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

        }

    }
}