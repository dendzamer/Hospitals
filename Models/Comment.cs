using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hospitals.Models
{
    public class Comment
    {
        public int CommentID {get; set;}
        public string UserName {get; set;}

        public DateTime Date {get; set;}

        [MaxLength(500)]
        public string CommentText {get; set;}

        public string OwnerId {get; set;}

        public int ReviewID {get; set;}

        public Review Review {get; set;}
    }
}