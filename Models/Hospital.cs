using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hospitals.Models
{
    public class Hospital
    {
        public int HospitalID {get;set;}

        [Required(ErrorMessage = "Please insert hospital name")]
        [Display(Name = "Facility Name")]
        public string Name {get; set;}
        [Required(ErrorMessage = "Please insert hospital address")]
        public string Address {get; set;}
        public string State {get; set;}

        [Required(ErrorMessage = "Please insert zip code")]
        public int Zip {get; set;}
        public double RatingTotal {get; set;}
        public double Rating {get; set;}
        public int ReviewsCount {get; set;}

        public List<Review> Reviews {get;set;}

        public string OwnerId {get; set;}


    }


}