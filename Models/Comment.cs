using System;
using System.Collections.Generic;

namespace Hospitals.Models
{
    public class Comment
    {
        public int CommentID {get; set;}
        public string UserName {get; set;}

        public DateTime Date {get; set;}
        public string LinkToText {get; set;}

        public string OwnerId {get; set;}

        public int ReviewID {get; set;}

        public Review Review {get; set;}
    }
}