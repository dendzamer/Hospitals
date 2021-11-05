using System;
using System.Collections.Generic;

namespace Hospitals.Models
{
    public class Hospital
    {
        public int HospitalID {get;set;}
        public string Name {get; set;}
        public string Address {get; set;}
        public string State {get; set;}
        public int Zip {get; set;}
        public double RatingTotal {get; set;}
        public double Rating {get; set;}
        public int ReviewsCount {get; set;}

        public List<Review> Reviews {get;set;}

        public string OwnerId {get; set;}


    }


}