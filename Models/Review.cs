using System;
using System.Collections.Generic;

namespace Hospitals.Models
{
    public class Review
    {
        public int ReviewID {get; set;}
        public string UserName {get; set;}
        public string Department {get; set;}

        public double Salary {get; set;}

        public DateTime Date {get; set;}

        public string LinkToText {get; set;}

        public string OwnerId {get; set;}

        public List<Comment> Comments {get; set;}

        public int HospitalID {get; set;}
        public Hospital Hospital {get; set;}
    }
}