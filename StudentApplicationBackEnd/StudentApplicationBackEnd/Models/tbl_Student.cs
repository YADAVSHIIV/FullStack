using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplicationBackEnd.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Class { get; set; }

    }
    public class tbl_Student : Student
    {
        public int ID { get; set; }
       
       public virtual ICollection<tbl_Mark> tbl_Marks { get; set; }
    }
}
