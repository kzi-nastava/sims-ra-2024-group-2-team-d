using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class GiftCard : ISerializable
    {
        public int Id { get; set; }
        public DateOnly ReceiveDate { get; set; }
        public DateOnly ExpireDate { get; set; }

        public GiftCard() {}
        
        public GiftCard(DateOnly receiveDate)
        {
            ReceiveDate = receiveDate;
            ExpireDate = ReceiveDate.AddYears(1);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReceiveDate.ToString("yyyy-MM-dd HH:mm"),
                ExpireDate.ToString("yyyy-MM-dd HH:mm"),

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReceiveDate = DateOnly.Parse(values[1]);
            ExpireDate = DateOnly.Parse(values[2]);
        }


    }
}
