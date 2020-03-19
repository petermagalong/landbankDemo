using System;
namespace landbankDemo.Tables
{
    public class RegUserTable
    {
        public string ScanNumber { get; set; }
        public string EmployeeNumber { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
    }
}
