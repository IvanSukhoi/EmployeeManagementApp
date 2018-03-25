using System.Collections.Generic;

namespace EmployeeManagement.WebUI.Models
{
    public class EmployeeListModel
    {
        public IEnumerable<EmployeeModel> Employees { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory{ get; set; }
    }
}