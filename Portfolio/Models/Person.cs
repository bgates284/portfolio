using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Portfolio.Models
{
    public class Person
    {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public int Age => DateTime.Now.Year - DateOfBirth.Year;

            //public Person(int? id, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
            //{
            //    Id = (int)id;
            //    FirstName = firstName;
            //    LastName = lastName;
            //    DateOfBirth = dateOfBirth;
            //    Email = email;
            //    PhoneNumber = phoneNumber;
            //}


            //public string GetFullName()
            //{
            //    return $"{FirstName} {LastName}";
            //}
    }
}
