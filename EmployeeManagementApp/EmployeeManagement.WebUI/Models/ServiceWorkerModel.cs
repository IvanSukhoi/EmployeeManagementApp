using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.WebUI.Models
{
    public class ServiceWorkerModel : EmployeeModel
    {
        public Profession TypeOfWorker { get; set; }
    }
}