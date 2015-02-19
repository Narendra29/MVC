using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration;

namespace Practice.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }




    [Table("Employement")]
    public class Employement
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Emp_Name { get; set; }

        [StringLength(255)]
        public string Emp_address { get; set; }

        [StringLength(255)]
        public string Designation { get; set; }

        public string UAN_ID { get; set; }

        public string[] UAN_ID1 { get; set; }

        public decimal salary { get; set; }

        public int Country_ID { get; set; } 

        public int State_Id { get; set; }

        public bool Active { get; set; } 

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        [NotMapped]         
        public SelectList CountryList { get; set; }     
       

    } 

    [Table("Country")]
    public class Country
    {
        [Key]
        public int Country_ID { get; set; }
        public string Country_Name { get; set; }
    }

    [Table("State")]
    public class State
    {
        [Key]
        public int State_Id { get; set; }
        public int Country_ID { get; set; }
        public string State_Name { get; set; }
    }


    [Table("UAN")]
    public class UAN
    {
        [Key]
        public int UAN_ID { get; set; }

        [StringLength(50)]
        public string UAN_Number { get; set; }
    }

    public class UsersDbContext :DbContext
    {
        public DbSet<Employement> Employeelist { get; set; }
        public DbSet<UAN> UanList { get; set; }
        public DbSet<Country> countrylist { get; set; }
        public DbSet<State> statelist { get; set; } 
      
        
    }

}
