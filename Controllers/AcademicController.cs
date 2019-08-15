using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DN_lab.Models;

namespace DN_lab.Controllers
{
    public class AcademicController : Controller
    {
        // GET: Academic
        public ActionResult Index()
        {
            return View();
        }

        // 顯示「研究計畫」 ==================================
        public ActionResult Project() {
            DBconnProject dBconnProject = new DBconnProject();
            List<Project> projects = dBconnProject.GetProjects();
            ViewBag.projects = projects;
            return View();
        }


        // 編輯「研究計畫」 ==================================
        public ActionResult EditProject(int id) {

            DBconnProject dBconnProject = new DBconnProject();
            Project project = dBconnProject.GetProjectById(id);
            return View(project);
        }
        [HttpPost]
        public ActionResult EditProject(Project project) {
            DBconnProject dBconnProject = new DBconnProject();
            dBconnProject.UpdateProject(project);
            return RedirectToAction("Project");
        }
        
        // 刪除「研究計畫」 ===========================================
        public ActionResult DeleteProject(int id) {
            DBconnProject dBconnProject = new DBconnProject();
            dBconnProject.DeleteProjectById(id);
            return RedirectToAction("Project");
        }
    }
}