using BookingApp.Domain.Model;
using System;

namespace BookingApp.Dto
{
    public class ForumOverviewDTO
    {
        public int ForumId { get; set; }
        public string Topic { get; set; }
        public Location Location { get; set; }
        public string DateCreated { get; set; }
        public int CommentsNumber { get; set; }
        public ForumStatus Status { get; set; }
        public string VeryUseful { get; set; }
        public ForumOverviewDTO(int forumId, string topic, Location location, DateTime dateCreated, int commentsNumber, ForumStatus status, string veryUseful)
        {
            ForumId = forumId;
            Topic = topic;
            Location = location;
            DateCreated = string.Format("{0:dd.MM.yyyy.}", dateCreated);
            CommentsNumber = commentsNumber;
            Status = status;
            VeryUseful = veryUseful;
        }
    }
}
