using System;

namespace BookingApp.Dto
{
    public class CommentDTO
    {
        public string Username { get; set; }
        public string Comment { get; set; }
        public string SPostedDate { get; set; }
        public DateTime PostedDate { get; set; }
        public bool IsHighlighted { get; set; }
        public bool IsntHighlighted { get; set; }
        public CommentDTO(string userName, string comment, DateTime postedDate, bool highlighted)
        {
            Username = userName;
            Comment = comment;
            PostedDate = postedDate;
            SPostedDate = string.Format("{0:dd.MM.yyyy.}", postedDate);
            IsHighlighted = highlighted;
            IsntHighlighted = !highlighted;
        }
    }
}
