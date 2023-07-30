using Microsoft.AspNetCore.Mvc;
using HR_Management.Models;
using HR_Management.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Controllers
{
    public class EmployeesController: Controller
    {
        private readonly MVCEmployeesDbContext db;

        public EmployeesController(MVCEmployeesDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> Index()
        {
            var employees = await db.Employees.ToListAsync();
            return View(employees); 
        }
        [HttpGet]
        [Route("[controller]/create")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Route("[controller]/create")]
        
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                LastName = addEmployeeRequest.LastName,
                Email = addEmployeeRequest.Email,
                PersonalAddress = addEmployeeRequest.PersonalAddress,
                Phone = addEmployeeRequest.Phone,
                LastRevision = addEmployeeRequest.LastRevision,
                Role = addEmployeeRequest.Role,
                Salary = addEmployeeRequest.Salary,
                History = addEmployeeRequest.LastRevision.ToString("dd-MM-yyyy") + "," + addEmployeeRequest.Salary 
            };
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[controller]/View/{id}")]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await db.Employees.FirstOrDefaultAsync(x => (x.Id == id));
            
            if(employee is null)
                return View("Index");
            
            var viewModel = new UpdateEmployeeViewModel()
            {
                Id = Guid.NewGuid(),
                Name = employee.Name,
                LastName = employee.LastName,
                Email = employee.Email,
                PersonalAddress = employee.PersonalAddress,
                Phone = employee.Phone,
                LastRevision = employee.LastRevision,
                Role = employee.Role,
                Salary = employee.Salary
            };
            return await Task.Run(() => View("View",viewModel));
        }
        [HttpPost]
        [Route("[controller]/View/{id}")]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await db.Employees.FindAsync(model.Id);
            if(employee is null)
            {
                //
                return RedirectToAction("Index");
            }
            employee.Name = model.Name;
            employee.LastName = model.LastName;
            employee.Email = model.Email;
            employee.PersonalAddress = model.PersonalAddress;
            employee.Phone = model.Phone;
            employee.LastRevision = model.LastRevision;
            employee.Role = model.Role;
            if(employee.Salary != model.Salary)
            {
                employee.Salary = model.Salary;
                employee.History += ","+DateTime.Now.ToString("dd-MM-yyyy")+","+model.Salary;
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("[controller]/View")]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await db.Employees.FindAsync(model.Id);
            if(employee is null)
                return RedirectToAction("Index");
            db.Remove(employee);
            await db.SaveChangesAsync();     
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("[controller]/History")]
        public async Task<IActionResult> ShowHistory(UpdateEmployeeViewModel model)
        {

            var employee = await db.Employees.FindAsync(model.Id);
            if(employee is null)
                return View("Index");
            List<(float,string)> values = getHistory(employee.History); 
            return await Task.Run(() => View("ShowHistory",values));
        }

        [HttpGet]
        [Route("[controller]/{id}/Revision")]
        public async Task<IActionResult> DoRevision(Guid id)
        {
            var employee = await db.Employees.FirstOrDefaultAsync(x => (x.Id == id));
            if(employee is null)
                return View("Index");
            
            var lastRevision = employee.LastRevision;
            int monthsGap = DateTime.Now.Subtract(lastRevision).Days/30;
            float currentSalary = employee.Salary;
            if(monthsGap > 2)
            {
            //this means that we should rise his salary.
                float rise = (employee.Role == "Manager")? 1.12f :
                             (employee.Role == "Specialist")? 1.08f :
                             (employee.Role == "Worker")? 1.05f : 0;
                for (int i = 0; i < monthsGap/3; i++)
                {
                    currentSalary *= rise;
                    employee.History += ","+DateTime.Now.ToString("dd-MM-yyyy")+","+currentSalary;
                }
            }
            employee.LastRevision = DateTime.Now;
            employee.Salary = currentSalary;
            await db.SaveChangesAsync();
            return await Task.Run(() => RedirectToAction("Index"));
        }

        private List<(float, string)> getHistory(string rawHistory)
        {

            List<(float,string)> result = new List<(float, string)>();
            string[] records = rawHistory.Split(',');
            for (int i = 0; i < records.Length; i+=2)
            {
                result.Add((float.Parse(records[i+1]),records[i]));
            }
            return result;
        }
    } 
}