using System;
using System.Collections.Generic;
using System.Text;

namespace AutomationTest.Models
{
    public class PagingInformation
    {
        public int RecordsPerPage { set; get; }
        public int TotalRecords { set; get; }
        public int TotalPages { set; get; }
    }
}
