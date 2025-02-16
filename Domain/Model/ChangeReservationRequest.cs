﻿using BookingApp.Serializer;
using System;

namespace BookingApp.Domain.Model
{
    public enum StatusType { Pending, Canceled, Approved }
    public class ChangeReservationRequest : ISerializable
    {
        private int _requestId;
        private int _reservationId;
        private int _accommodationId;
        private string _accommodationName;
        private DateTime _newStartDate;
        private DateTime _newEndDate;
        private StatusType _requestStatus;
        private int _userId;
        private int _ownerId;
        private string _ownerComment;

        public ChangeReservationRequest(int reservationId, int accommmodationId, string accommodationName, DateTime newStartDate, DateTime newEndDate, StatusType requestStatus, int userId, int ownerId)
        {
            ReservationId = reservationId;
            AccommodationName = accommodationName;
            AccommodationId = accommmodationId;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
            RequestStatus = requestStatus;
            UserId = userId;
            OwnerId = ownerId;
            OwnerComment = "-";
        }
        public ChangeReservationRequest()
        {
        }
        public int ReservationId { get => _reservationId; set => _reservationId = value; }
        public DateTime NewStartDate { get => _newStartDate; set => _newStartDate = value; }
        public DateTime NewEndDate { get => _newEndDate; set => _newEndDate = value; }
        public StatusType RequestStatus { get => _requestStatus; set => _requestStatus = value; }
        public int RequestId { get => _requestId; set => _requestId = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public int OwnerId { get => _ownerId; set => _ownerId = value; }
        public int AccommodationId { get => _accommodationId; set => _accommodationId = value; }
        public string OwnerComment { get => _ownerComment; set => _ownerComment = value; }
        public string AccommodationName { get => _accommodationName; set => _accommodationName = value; }
        public string IsDateAvailable { get; set; } = "Da";

        public string[] ToCSV()
        {
            string[] csvValues = {
                _requestId.ToString(),
                _reservationId.ToString(),
                _accommodationId.ToString(),
                _accommodationName.ToString(),
                _newStartDate.ToString(),
                _newEndDate.ToString(),
                _requestStatus.ToString(),
                _userId.ToString(),
                _ownerId.ToString(),
                _ownerComment
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            _requestId = Convert.ToInt32(values[0]);
            _reservationId = Convert.ToInt32(values[1]);
            _accommodationId = Convert.ToInt32(values[2]);
            _accommodationName = Convert.ToString(values[3]);
            _newStartDate = Convert.ToDateTime(values[4]);
            _newEndDate = Convert.ToDateTime(values[5]);
            _requestStatus = (StatusType)Enum.Parse(typeof(StatusType), values[6]);
            _userId = Convert.ToInt32(values[7]);
            _ownerId = Convert.ToInt32(values[8]);
            _ownerComment = Convert.ToString(values[9]);
        }
    }
}
