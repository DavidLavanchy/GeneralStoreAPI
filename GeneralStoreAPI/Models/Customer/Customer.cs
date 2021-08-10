using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models.Customer
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                string FullName = $"{FirstName} {LastName}";
                return FullName;
            }
        } 

    }
}