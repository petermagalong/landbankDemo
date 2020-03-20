using System;
namespace landbankDemo.Tables
{
    public class TimeUserTable
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Checklog { get; set; }
        public DateTime Datelog { get; set; }
        public string Location { get; set; }
        public string ScanNumber { get; set; }
    }
}
