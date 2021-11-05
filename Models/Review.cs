using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hospitals.Models
{
    public class Review
    {
        public int ReviewID {get; set;}
        public string UserName {get; set;}

       [Required(ErrorMessage = "Please choose a department")]
        public string Department {get; set;}


        [Display(Name = "Hourly rate")]
        public string Salary {get; set;}

        public DateTime Date {get; set;}

        public int Rating {get; set;}

        public string Speciality {get; set;}

        public string Agency {get; set;}


        public string EmploymentType {get; set;}

        [Required(ErrorMessage = "Please enter your review text")]
        [Display(Name = "Review Text")]
        [MaxLength(500)]
        public string ReviewText {get; set;}

        public string OwnerId {get; set;}

        public List<Comment> Comments {get; set;}

        public int HospitalID {get; set;}
        public Hospital Hospital {get; set;}
    
    }
}