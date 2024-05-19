using Spark.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Models
{
    public class CommentsModel
    {
        public int CommentId { get; set; }

        [Required]
        public string Comment { get; set; }

        public string CreatedById { get; set; }

        public string CreadtedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ProjectId { get; set; }

        public int Rating { get; set; }


        public string CreadtedAtToString()
        {
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = now - this.CreatedAt;
            if (timeSpan < TimeSpan.Zero)
            {
                return "In the future";
            }

            double days = timeSpan.TotalDays;
            double hours = timeSpan.TotalHours;
            double minutes = timeSpan.TotalMinutes;
            double seconds = timeSpan.TotalSeconds;

            if (days > 1)
            {
                return $"{Math.Floor(days)} days ago";
            }
            else if (hours > 1)
            {
                return $"{Math.Floor(hours)} hours ago";
            }
            else if (minutes > 1)
            {
                return $"{Math.Floor(minutes)} minutes ago";
            }
            else
            {
                return $"{Math.Floor(seconds)} seconds ago";
            }
        }

    }
}
