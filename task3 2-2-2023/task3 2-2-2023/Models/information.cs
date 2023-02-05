//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace task3_2_2_2023.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class information
    {
        public int Id { get; set; }
        [Required]
        [StringLength(12)]

        [Display(Name = "First Name")]
        public string First_Name { get; set; }
        [StringLength(12)]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }
        [EmailAddress]
        public string EMail { get; set; }
        //[RegularExpression(@"^(?[0]{1}?[7]{1}?([7-9]{1}))?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]

        [Required]
        [RegularExpression("((079)|(078)|(077)){1}[0-9]{7}", ErrorMessage = "Enter A Valid Jordanian Phone Number")]


        public string Phone { get; set; }
        [Range(18, 50)]


        public Nullable<int> Age { get; set; }
        [MaxLength(10)]
        [Display(Name = "Job title")]
        public string Job_title { get; set; }
        [Required]
        public Nullable<bool> Gender { get; set; }

        public string GenderDisplay
        {
            get
            {
                if (Gender == null)
                {
                    return "Not specified";
                }
                else
                {
                    return Gender == true ? "Male" : "Female";
                }
            }
        }

        public string Cv { get; set; }

        public string img { get; set; }


    }
}
