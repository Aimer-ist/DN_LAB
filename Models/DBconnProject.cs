using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace DN_lab.Models {
    public class DBconnProject {

        private readonly string ConnStr = @"Data Source=PC-PEI\SQLEXPRESS;Initial Catalog=bn_lab;Integrated Security=True";

        // 取得計畫資料=============================================
        public List<Project> GetProjects() {
            List<Project> projects = new List<Project>();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM projects");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            // 是執行先前訂出來的'SQL指令'再去讀取執行的結果(全部的資料表內容)
            // 前面尋找到資料後要對資料做「擷取」
            SqlDataReader reader = sqlCommand.ExecuteReader();

            // 如果讀取的到「一行」就將資料加入至這回圈內的project
            if (reader.HasRows) {
                // reader.Read()一次只讀一行
                while (reader.Read()) {
                    Project project = new Project() {

                        // 藉由資料庫的「欄位」、「型態」取得資料庫內容，並放入至Models的Project類別內
                        // GetOrdinal是指將欄位名稱轉換成資料庫的「第幾個欄位」
                        Project_Id = reader.GetInt32(reader.GetOrdinal("project_id")),
                        Pro_Name = reader.GetString(reader.GetOrdinal("pro_name")),
                        Pro_Status = reader.GetString(reader.GetOrdinal("pro_status")),
                        Pro_Source = reader.GetString(reader.GetOrdinal("pro_source")),
                        Pro_Number = SafeGetString(reader, reader.GetOrdinal("pro_number")),
                        Pro_Start = SafeGetDateTime(reader,reader.GetOrdinal("pro_start")),
                        Pro_Finish = SafeGetDateTime(reader,reader.GetOrdinal("pro_finish"))
                    };
                    projects.Add(project);
                }
            }
            else {
                Console.WriteLine("資料庫內容是空的");
            }
            sqlConnection.Close();
            // 將取得到的資料最後放入 List 內
            return projects;
        }

        


        // 取得計畫(id)=============================================
        // 利用現有的id資訊去資料庫找符合id的資料
        public Project GetProjectById(int id) {

            Project project = new Project();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM projects WHERE project_id = @id");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows) {
                while (reader.Read()) {
                    project = new Project {
                        Project_Id = reader.GetInt32(reader.GetOrdinal("project_id")),
                        Pro_Name = reader.GetString(reader.GetOrdinal("pro_name")),
                        Pro_Status = reader.GetString(reader.GetOrdinal("pro_status")),
                        Pro_Source = reader.GetString(reader.GetOrdinal("pro_source")),
                        Pro_Number = SafeGetString(reader, reader.GetOrdinal("pro_number")),
                        Pro_Start = SafeGetDateTime(reader, reader.GetOrdinal("pro_start")),
                        Pro_Finish = SafeGetDateTime(reader, reader.GetOrdinal("pro_finish"))
                    };
                }
            }
            else {
                project.Pro_Name = "未找到該筆資料"; //借放
            }
            sqlConnection.Close();
            return project;
        }

        // 更新資訊到資料庫 =============================================
        public void UpdateProject(Project project) {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand
                ("UPDATE projects SET pro_name = @pro_name, pro_status = @pro_status, pro_source = @pro_source , pro_number = @pro_number , pro_start = @pro_start , pro_finish = @pro_finish WHERE project_id = @project_id");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@project_id", project.Project_Id));
            sqlCommand.Parameters.Add(new SqlParameter("@pro_name", project.Pro_Name));
            sqlCommand.Parameters.Add(new SqlParameter("@pro_status", project.Pro_Status));
            sqlCommand.Parameters.Add(new SqlParameter("@pro_source", project.Pro_Source));
            sqlCommand.Parameters.Add(new SqlParameter("@pro_number", project.Pro_Number));
            sqlCommand.Parameters.Add(new SqlParameter("@pro_start", project.Pro_Start));
            sqlCommand.Parameters.Add(new SqlParameter("@pro_finish", project.Pro_Finish));
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        // 刪除資料=============================================
        public void DeleteProjectById(int id) {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(@"DELETE FROM projects WHERE project_id = @id");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        // 資料有null時使用 =============================================
        // static表示靜態不可再做更動
        public static string SafeGetString(SqlDataReader reader, int colIndex) {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
        public static DateTime SafeGetDateTime(SqlDataReader reader, int colIndex) {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            DateTime dateNull = new DateTime();
            return dateNull;
        }
    }
}