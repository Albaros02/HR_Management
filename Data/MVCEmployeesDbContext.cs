namespace HR_Management.Models.Domain;
using Microsoft.EntityFrameworkCore;
public class MVCEmployeesDbContext : DbContext
{
    public MVCEmployeesDbContext(DbContextOptions options): base(options)
    {

    }
    public DbSet<Employee> Employees {get; set;}
}