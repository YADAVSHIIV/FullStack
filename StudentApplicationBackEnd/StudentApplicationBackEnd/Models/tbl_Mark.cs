using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplicationBackEnd.Models
{
    public class Mark
    {

        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int StudentMark { get; set; }

    }
    public class tbl_Mark: Mark
    {
        public int ID { get; set; }

        public virtual tbl_Student tbl_Student { get; set; }
        public virtual tbl_Subject tbl_Subject { get; set; }

    }
}
