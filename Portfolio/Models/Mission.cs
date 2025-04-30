using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Mission
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime LaunchDate { get; set; }

        public Person Owner { get; set; }
    }
}
