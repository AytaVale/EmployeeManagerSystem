using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker
{
    public class WorkTime
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeNumber { get; set; }
        public int Day { get; set; }
        public int EntryHours { get; set; }
        public int EntryMinute { get; set; }
        public int ExitHours { get; set; }
        public int ExitMinute { get; set; }
    }
}
