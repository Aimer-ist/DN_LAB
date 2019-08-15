using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DN_lab.Models {
    public class Project {
        public int Project_Id { get; set; }
        public string Pro_Name { get; set; }
        public string Pro_Status { get; set; }
        public string Pro_Source { get; set; }
        public string Pro_Number { get; set; }
        public DateTime Pro_Start { get; set; }
        public DateTime Pro_Finish { get; set; }
    }
}