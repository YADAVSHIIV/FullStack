using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplicationBackEnd.Models
{
    public class Subject
    { 
        public string Name { get; set; }  
    }

    public class tbl_Subject : Subject
    {
        public int ID { get; set; }

        public virtual ICollection<tbl_Mark> tbl_Marks { get; set; }
    }
}
