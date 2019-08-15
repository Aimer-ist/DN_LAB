using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DN_lab.Models;

namespace DN_lab.Controllers {

    // 連線到View的Student頁面 =============================
    public class StudentController : Controller {

        // 首頁(顯示全部) ==================================
        public ActionResult Index(String type="M") {
            DBconnST dBconnST = new DBconnST();
            List<Student> students = dBconnST.GetStudents();
            
            //                   .排序方法( 一筆資料的代稱 => 要排序的欄位 ).轉換成List();
            students = students.Where ( student => student.St_Type == type).ToList();
            ViewBag.students = students;
            return View();
        }

        // 新增 ===========================================
        public ActionResult CreateStudent() {

            // 沒有用DBconnST dBconnST = new DBconnST();
            // 是因為不用在該頁面連線置資料庫讀取資料
            return View();
        }
        [HttpPost]
        public ActionResult CreateStudent(Student student) {
            DBconnST dBconnST = new DBconnST();
            try {
                dBconnST.NewStudent(student);
            }
            catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }

        // 編輯 ===========================================
        public ActionResult EditStudent(int id) {
            DBconnST dBconnST = new DBconnST();
            Student student = dBconnST.GetStudentById(id);
            return View(student);
        }
        [HttpPost]
        public ActionResult EditStudent(Student student) {
            DBconnST dBconnST = new DBconnST();
            dBconnST.UpdateStudent(student);
            return RedirectToAction("Index");
        }
        
        // 刪除 ===========================================
        public ActionResult DeleteStudent(int id) {
            DBconnST dBconnST = new DBconnST();
            dBconnST.DeleteStudentById(id);
            return RedirectToAction("Index");
        }
    }
}