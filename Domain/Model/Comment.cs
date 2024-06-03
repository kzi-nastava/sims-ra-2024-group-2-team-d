using BookingApp.Serializer;
using System;

namespace BookingApp.Domain.Model
{
    public class Comment : ISerializable
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public int NumberOfReports { get; set; }

        public Comment() {
        }

        public Comment(DateTime creationTime, string text, User user, int numberOfReports)
        {
            CreationTime = creationTime;
            Text = text;
            User = user;
            NumberOfReports = numberOfReports;
        }

        public Comment(DateTime creationTime, string text, int userId, int numberOfReports)
        {
            CreationTime = creationTime;
            Text = text;
            User = new User() { Id=userId };
            NumberOfReports = numberOfReports;

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), CreationTime.ToString(), Text, User.Id.ToString(), NumberOfReports.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            CreationTime = Convert.ToDateTime(values[1]);
            Text = values[2];
            User = new User() { Id = Convert.ToInt32(values[3]) };
            NumberOfReports = Convert.ToInt32(values[4]);
        }
    }
}
