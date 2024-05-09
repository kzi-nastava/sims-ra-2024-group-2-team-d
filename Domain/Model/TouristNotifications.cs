using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using BookingApp.Serializer;
using InitialProject.CustomClasses;

namespace BookingApp.Domain.Model
{
    public enum NotificationType { TourCreation, AddedToLiveTour, TourRequestAcceptance }
    public class TouristNotifications : ISerializable
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
        public int NotificationId { get; set; }

        public int UserId { get; set; }


        public TouristNotifications(int notificationId, string message, NotificationType type, int userId)
        {
            IsRead = false;
            Type = type;
            Message = message;
            NotificationId = notificationId;
            UserId = userId;

        }

        public TouristNotifications()
        {
            IsRead = false;

        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Message,
                IsRead.ToString(),
                Type.ToString(),
                NotificationId.ToString(),
                UserId.ToString()
            };
            return csvValues;

        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Message = values[1];
            IsRead = bool.Parse(values[2]);
            Type = (NotificationType)Enum.Parse(typeof(NotificationType), values[3]);
            NotificationId = int.Parse(values[4]);
            UserId = int.Parse(values[5]);

        }
    }
}
