using System;

namespace BookingApp.Dto
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public string SPostedDate { get; set; }
        public DateTime PostedDate { get; set; }
        public bool IsHighlighted { get; set; }
        public bool IsntHighlighted { get; set; }
        public int NumberOfReports { get; set; }
        public CommentDTO(string userName, string comment, DateTime postedDate, bool highlighted)
        {
            Username = userName;
            Comment = comment;
            PostedDate = postedDate;
            SPostedDate = string.Format("{0:dd.MM.yyyy.}", postedDate);
            IsHighlighted = highlighted;
            IsntHighlighted = !highlighted;
            NumberOfReports = 0;
        }
    }
}
