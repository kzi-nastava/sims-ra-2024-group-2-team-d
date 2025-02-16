﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class GiftCard : ISerializable
    {
        public int Id { get; set; }
        public DateOnly ReceiveDate { get; set; }
        public DateOnly ExpirationDate { get; set; }

        public int UserId { get; set; }

        public bool IsValid { get; set; }


        public GiftCard() { }

        public GiftCard(DateOnly receiveDate)
        {
            ReceiveDate = receiveDate;
            ExpirationDate = ReceiveDate.AddYears(1);
            IsValid = true;
        }

        public GiftCard(int id)
        {
            ReceiveDate = DateOnly.FromDateTime(DateTime.Now);
            ExpirationDate = ReceiveDate.AddYears(1);
            UserId = id;
            IsValid = true;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReceiveDate.ToString("yyyy-MM-dd"),
                ExpirationDate.ToString("yyyy-MM-dd"),
                UserId.ToString(),
                IsValid.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReceiveDate = DateOnly.Parse(values[1]);
            ExpirationDate = DateOnly.Parse(values[2]);
            UserId = int.Parse(values[3]);
            IsValid = bool.Parse(values[4]);
        }
    }
}
