using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DN_lab.Models {
    public class Navdata {
        public String Name { get; set; } //"研究生列表"
        public string Link { get; set; } //Index
        public string Vari { get; set; } //"M"

        // 不需要Vari變數的場合
        public Navdata(string _Name, string _Link) {
            this.Name = _Name;
            this.Link = _Link;
            this.Vari = string.Empty;
        }

        // 需要Vari變數的場合
        public Navdata(string _Name, string _Link,string _Vari) {
            this.Name = _Name;
            this.Link = _Link;
            this.Vari = _Vari;
        }
    }
}