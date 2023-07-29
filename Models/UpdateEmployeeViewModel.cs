namespace HR_Management.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string PersonalAddress {get; set;}
        public string Phone {get; set;}
        public DateTime LastRevision{get; set;}
        public string Role {get; set;}
        public float Salary{get; set;}
    } 
}