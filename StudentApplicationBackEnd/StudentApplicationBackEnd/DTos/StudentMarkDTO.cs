using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplicationBackEnd.DTos
{
    public class StudentMarkDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Class { get; set; }

        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int StudentMark { get; set; }
    }
}
