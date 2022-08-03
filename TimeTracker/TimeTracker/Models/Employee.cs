using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker
{
    public class Employee
    {
        public int Id { get; set; }
        public  int EmployeeNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime EntryDate { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
    }

}
